using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Classification;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Text.Tagging;
using Microsoft.VisualStudio.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CSharp = Microsoft.CodeAnalysis.CSharp;
using VB = Microsoft.CodeAnalysis.VisualBasic;

namespace SemanticColorizer
{

    [Export(typeof(ITaggerProvider))]
    [ContentType("CSharp")]
    [ContentType("Basic")]
    [TagType(typeof(IClassificationTag))]
    internal class SemanticColorizerProvider : ITaggerProvider
    {
        [Import]
        internal IClassificationTypeRegistryService ClassificationRegistry = null; // Set via MEF

        public ITagger<T> CreateTagger<T>(ITextBuffer buffer) where T : ITag {
            return (ITagger<T>)new SemanticColorizer(buffer, ClassificationRegistry);
        }
    }

    class SemanticColorizer : ITagger<IClassificationTag>
    {
        private ITextBuffer theBuffer;
        private IClassificationType fieldType;
        private IClassificationType enumFieldType;
        private IClassificationType extensionMethodType;
        private IClassificationType staticMethodType;
        private IClassificationType normalMethodType;
        private IClassificationType constructorType;
        private IClassificationType typeParameterType;
        private IClassificationType parameterType;
        private IClassificationType namespaceType;
        private IClassificationType propertyType;
        private IClassificationType localType;
        private IClassificationType typeSpecialType;
        private IClassificationType typeNormalType;

        private IClassificationType typeClassType;
        private IClassificationType typeStructType;
        private Cache cache;
#pragma warning disable CS0067
        public event EventHandler<SnapshotSpanEventArgs> TagsChanged;
#pragma warning restore CS0067

        internal SemanticColorizer(ITextBuffer buffer, IClassificationTypeRegistryService registry) {
            theBuffer = buffer;
            fieldType = registry.GetClassificationType(Constants.FieldFormat);
            enumFieldType = registry.GetClassificationType(Constants.EnumFieldFormat);
            extensionMethodType = registry.GetClassificationType(Constants.ExtensionMethodFormat);
            staticMethodType = registry.GetClassificationType(Constants.StaticMethodFormat);
            normalMethodType = registry.GetClassificationType(Constants.NormalMethodFormat);
            constructorType = registry.GetClassificationType(Constants.ConstructorFormat);
            typeParameterType = registry.GetClassificationType(Constants.TypeParameterFormat);
            parameterType = registry.GetClassificationType(Constants.ParameterFormat);
            namespaceType = registry.GetClassificationType(Constants.NamespaceFormat);
            propertyType = registry.GetClassificationType(Constants.PropertyFormat);
            localType = registry.GetClassificationType(Constants.LocalFormat);
            typeSpecialType = registry.GetClassificationType(Constants.TypeSpecialFormat);
            typeNormalType = registry.GetClassificationType(Constants.TypeNormalFormat);
            typeClassType = registry.GetClassificationType(Constants.TypeClassFormat);
            typeStructType = registry.GetClassificationType(Constants.TypeStructFormat);
        }

        public IEnumerable<ITagSpan<IClassificationTag>> GetTags(NormalizedSnapshotSpanCollection spans) {
            if (spans.Count == 0) {
                return Enumerable.Empty<ITagSpan<IClassificationTag>>();
            }
            if (this.cache == null || this.cache.Snapshot != spans[0].Snapshot) {
                // this makes me feel dirty, but otherwise it will not
                // work reliably, as TryGetSemanticModel() often will return false
                // should make this into a completely async process somehow
                var task = Cache.Resolve(theBuffer, spans[0].Snapshot);
                try
                {
                    task.Wait();
                }
                catch (Exception)
                {
                    // TODO: report this to someone.
                    return Enumerable.Empty<ITagSpan<IClassificationTag>>();
                }
                cache = task.Result;
                if (cache == null)
                {
                    // TODO: report this to someone.
                    return Enumerable.Empty<ITagSpan<IClassificationTag>>();
                }
            }
            return GetTagsImpl(this.cache, spans);
        }

        private IEnumerable<ITagSpan<IClassificationTag>> GetTagsImpl(
              Cache doc,
              NormalizedSnapshotSpanCollection spans) {
            var snapshot = spans[0].Snapshot;
            var comparer = StringComparer.InvariantCultureIgnoreCase;

            IEnumerable<ClassifiedSpan> identifiers =
              GetIdentifiersInSpans(doc.Workspace, doc.SemanticModel, spans);

            foreach (var id in identifiers) {
                var node = GetExpression(doc.SyntaxRoot.FindNode(id.TextSpan));
                var symbol = doc.SemanticModel.GetSymbolInfo(node).Symbol;
                if (symbol == null) symbol = doc.SemanticModel.GetDeclaredSymbol(node);
                if (symbol == null) {
                    continue;
                }

                if (id.ClassificationType.Equals("identifier")) {
                    switch (symbol.Kind) {
                        case SymbolKind.Field:
                            if (symbol.ContainingType.TypeKind != TypeKind.Enum) {
                                yield return id.TextSpan.ToTagSpan(snapshot, fieldType);
                            }
                            else {
                                yield return id.TextSpan.ToTagSpan(snapshot, enumFieldType);
                            }
                            break;
                        case SymbolKind.Method:
                            var method = (IMethodSymbol)symbol;
                            if (method.IsExtensionMethod) {
                                yield return id.TextSpan.ToTagSpan(snapshot, extensionMethodType);
                            }
                            else if (symbol.IsStatic) {
                                yield return id.TextSpan.ToTagSpan(snapshot, staticMethodType);
                            }
                            else {
                                yield return id.TextSpan.ToTagSpan(snapshot, normalMethodType);
                            }
                            break;
                        case SymbolKind.TypeParameter:
                            yield return id.TextSpan.ToTagSpan(snapshot, typeParameterType);
                            break;
                        case SymbolKind.Parameter:
                            yield return id.TextSpan.ToTagSpan(snapshot, parameterType);
                            break;
                        case SymbolKind.Namespace:
                            yield return id.TextSpan.ToTagSpan(snapshot, namespaceType);
                            break;
                        case SymbolKind.Property:
                            yield return id.TextSpan.ToTagSpan(snapshot, propertyType);
                            break;
                        case SymbolKind.Local:
                            yield return id.TextSpan.ToTagSpan(snapshot, localType);
                            break;
                    }
                }
                if (id.ClassificationType.Equals("keyword")) {
                    switch (symbol.Kind) {
                        case SymbolKind.NamedType:
                            if (node is PredefinedTypeSyntax) {
                                var type = (INamedTypeSymbol)symbol;
                                if (type.Name.Equals("Void"))
                                    continue;
                                if (type.TypeKind == TypeKind.Struct) {
                                    yield return id.TextSpan.ToTagSpan(snapshot, typeStructType);
                                }
                                if (type.TypeKind == TypeKind.Class) {
                                    yield return id.TextSpan.ToTagSpan(snapshot, typeClassType);
                                }
                            }
                            break;
                    }
                }
            }
        }

        private SyntaxNode GetExpression(SyntaxNode node) {
            if (node.CSharpKind() == CSharp.SyntaxKind.Argument) {
                return ((CSharp.Syntax.ArgumentSyntax)node).Expression;
            }
            else if (node.CSharpKind() == CSharp.SyntaxKind.AttributeArgument) {
                return ((CSharp.Syntax.AttributeArgumentSyntax)node).Expression;
            }
            else if (node.VBKind() == VB.SyntaxKind.SimpleArgument) {
                return ((VB.Syntax.SimpleArgumentSyntax)node).Expression;
            }
            return node;
        }

        private IEnumerable<ClassifiedSpan> GetIdentifiersInSpans(
              Workspace workspace, SemanticModel model,
              NormalizedSnapshotSpanCollection spans) {
            var comparer = StringComparer.InvariantCultureIgnoreCase;
            var classifiedSpans =
              spans.SelectMany(span => {
                  var textSpan = TextSpan.FromBounds(span.Start, span.End);
                  return Classifier.GetClassifiedSpans(model, textSpan, workspace);
              });

            return classifiedSpans;
        }

        private class Cache
        {
            public Workspace Workspace { get; private set; }
            public Document Document { get; private set; }
            public SemanticModel SemanticModel { get; private set; }
            public SyntaxNode SyntaxRoot { get; private set; }
            public ITextSnapshot Snapshot { get; private set; }

            private Cache() {}

            public static async Task<Cache> Resolve(ITextBuffer buffer, ITextSnapshot snapshot) {
                var workspace = buffer.GetWorkspace();
                var document = snapshot.GetOpenDocumentInCurrentContextWithChanges();
                if (document == null)
                {
                    // Razor cshtml returns a null document for some reason.
                    return null;
                }

                // the ConfigureAwait() calls are important,
                // otherwise we'll deadlock VS
                var semanticModel = await document.GetSemanticModelAsync().ConfigureAwait(false);
                var syntaxRoot = await document.GetSyntaxRootAsync().ConfigureAwait(false);
                return new Cache {
                    Workspace = workspace,
                    Document = document,
                    SemanticModel = semanticModel,
                    SyntaxRoot = syntaxRoot,
                    Snapshot = snapshot
                };
            }
        }
    }
}

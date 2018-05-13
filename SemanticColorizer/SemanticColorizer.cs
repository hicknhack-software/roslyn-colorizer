using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Classification;
using Microsoft.CodeAnalysis.Text;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Text.Tagging;
using Microsoft.VisualStudio.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
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
#pragma warning disable CS0649
        [Import]
        internal IClassificationTypeRegistryService ClassificationRegistry; // Set via MEF
#pragma warning restore CS0649

        public ITagger<T> CreateTagger<T>(ITextBuffer buffer) where T : ITag
        {
            return (ITagger<T>)new SemanticColorizer(buffer, ClassificationRegistry);
        }
    }

    class SemanticColorizer : ITagger<IClassificationTag>
    {
        private static readonly HashSet<string> SupportedClassificationTypeNames;
        private readonly ITextBuffer _theBuffer;
        private readonly IClassificationType _fieldType;
        private readonly IClassificationType _enumFieldType;
        private readonly IClassificationType _extensionMethodType;
        private readonly IClassificationType _staticMethodType;
        private readonly IClassificationType _normalMethodType;
        private readonly IClassificationType _localFunctionType;
        private readonly IClassificationType _constructorType;
        private readonly IClassificationType _typeParameterType;
        private readonly IClassificationType _parameterType;
        private readonly IClassificationType _namespaceType;
        private readonly IClassificationType _propertyType;
        private readonly IClassificationType _localType;
        private readonly IClassificationType _typeSpecialType;
        private readonly IClassificationType _eventType;
        private readonly IClassificationType _classType;
        private readonly IClassificationType _structType;
        private readonly IClassificationType _delegateType;
        private readonly IClassificationType _interfaceType;
        private readonly IClassificationType _enumType;
        private Cache _cache;
#pragma warning disable CS0067
        public event EventHandler<SnapshotSpanEventArgs> TagsChanged;
#pragma warning restore CS0067

        static class NewClassificationTypeNames
        {
            public const string PropertyName = "property name";
            public const string EventName = "event name";
            public const string ExtensionMethodName = "extension method name";
            public const string MethodName = "method name";
            public const string ParameterName = "parameter name";
            public const string LocalName = "local name";
            public const string FieldName = "field name";
            public const string EnumMemberName = "enum member name";
            public const string ConstantName = "constant name";
        }
        public const MethodKind LocalMethodKind = (MethodKind)17;

        static SemanticColorizer()
        {
            SupportedClassificationTypeNames = new HashSet<string>
            {
                ClassificationTypeNames.ClassName,
                ClassificationTypeNames.StructName,
                NewClassificationTypeNames.FieldName,
                NewClassificationTypeNames.PropertyName,
                ClassificationTypeNames.InterfaceName,
                ClassificationTypeNames.DelegateName,
                ClassificationTypeNames.EnumName,
                NewClassificationTypeNames.EnumMemberName,
                ClassificationTypeNames.Keyword,
                ClassificationTypeNames.Identifier,
                NewClassificationTypeNames.EventName,
                NewClassificationTypeNames.LocalName,
                NewClassificationTypeNames.ParameterName,
                NewClassificationTypeNames.ExtensionMethodName,
                NewClassificationTypeNames.ConstantName,
                ClassificationTypeNames.TypeParameterName,
                NewClassificationTypeNames.MethodName
            };
        }

        internal SemanticColorizer(ITextBuffer buffer, IClassificationTypeRegistryService registry)
        {
            _theBuffer = buffer;
            _fieldType = registry.GetClassificationType(Constants.FieldFormat);
            _enumFieldType = registry.GetClassificationType(Constants.EnumFieldFormat);
            _extensionMethodType = registry.GetClassificationType(Constants.ExtensionMethodFormat);
            _staticMethodType = registry.GetClassificationType(Constants.StaticMethodFormat);
            _normalMethodType = registry.GetClassificationType(Constants.NormalMethodFormat);
            _localFunctionType = registry.GetClassificationType(Constants.LocalFunctionFormat);
            _constructorType = registry.GetClassificationType(Constants.ConstructorFormat);
            _typeParameterType = registry.GetClassificationType(Constants.TypeParameterFormat);
            _parameterType = registry.GetClassificationType(Constants.ParameterFormat);
            _namespaceType = registry.GetClassificationType(Constants.NamespaceFormat);
            _propertyType = registry.GetClassificationType(Constants.PropertyFormat);
            _localType = registry.GetClassificationType(Constants.LocalFormat);
            _typeSpecialType = registry.GetClassificationType(Constants.TypeSpecialFormat);
            _eventType = registry.GetClassificationType(Constants.EventFormat);
            _classType = registry.GetClassificationType(Constants.ClassFormat);
            _structType = registry.GetClassificationType(Constants.StructFormat);
            _delegateType = registry.GetClassificationType(Constants.DelegateFormat);
            _interfaceType = registry.GetClassificationType(Constants.InterfaceFormat);
            _enumType = registry.GetClassificationType(Constants.EnumFormat);
        }

        public IEnumerable<ITagSpan<IClassificationTag>> GetTags(NormalizedSnapshotSpanCollection spans)
        {
            if (spans.Count == 0)
            {
                return Enumerable.Empty<ITagSpan<IClassificationTag>>();
            }
            if (_cache == null || _cache.Snapshot != spans[0].Snapshot)
            {
                // this makes me feel dirty, but otherwise it will not
                // work reliably, as TryGetSemanticModel() often will return false
                // should make this into a completely async process somehow
                var task = Cache.Resolve(_theBuffer, spans[0].Snapshot);
                try
                {
                    task.Wait();
                }
                catch (Exception)
                {
                    // TODO: report this to someone.
                    return Enumerable.Empty<ITagSpan<IClassificationTag>>();
                }
                _cache = task.Result;
                if (_cache == null)
                {
                    // TODO: report this to someone.
                    return Enumerable.Empty<ITagSpan<IClassificationTag>>();
                }
            }
            return GetTagsImpl(_cache, spans);
        }

        private IEnumerable<ITagSpan<IClassificationTag>> GetTagsImpl(
              Cache doc,
              NormalizedSnapshotSpanCollection spans)
        {
            var snapshot = spans[0].Snapshot;

            IEnumerable<ClassifiedSpan> classifiedSpans =
              GetClassifiedSpans(doc.Workspace, doc.SemanticModel, spans);

            foreach (var span in classifiedSpans)
            {
                var node = GetExpression(doc.SyntaxRoot.FindNode(span.TextSpan));
                var symbol = doc.SemanticModel.GetSymbolInfo(node).Symbol;
                if (symbol == null) symbol = doc.SemanticModel.GetDeclaredSymbol(node);
                if (symbol == null)
                {
                    continue;
                }
                switch (symbol.Kind)
                {
                    case SymbolKind.Field:
                        switch (span.ClassificationType)
                        {
                            case NewClassificationTypeNames.ConstantName:
                            case NewClassificationTypeNames.FieldName:
                                yield return span.TextSpan.ToTagSpan(snapshot, _fieldType);
                                break;
                            case NewClassificationTypeNames.EnumMemberName:
                                yield return span.TextSpan.ToTagSpan(snapshot, _enumFieldType);
                                break;
                        }
                        break;
                    case SymbolKind.Method:
                        var methodSymbol = (IMethodSymbol)symbol;
                        switch (span.ClassificationType)
                        {
                            case ClassificationTypeNames.Identifier:
                                if (IsConstructor(methodSymbol))
                                {
                                    yield return span.TextSpan.ToTagSpan(snapshot, _constructorType);
                                }
                                //local function definition
                                else if (methodSymbol.MethodKind == LocalMethodKind)
                                {
                                    yield return span.TextSpan.ToTagSpan(snapshot, _localFunctionType);
                                }
                                else if (methodSymbol.IsExtensionMethod)
                                {
                                    yield return span.TextSpan.ToTagSpan(snapshot, _extensionMethodType);
                                }
                                break;
                            case NewClassificationTypeNames.ExtensionMethodName:
                                yield return span.TextSpan.ToTagSpan(snapshot, _extensionMethodType);
                                break;
                            case NewClassificationTypeNames.MethodName:
                                //local function call
                                if (methodSymbol.MethodKind == LocalMethodKind)
                                {
                                    yield return span.TextSpan.ToTagSpan(snapshot, _localFunctionType);
                                }
                                //static method call
                                else if (methodSymbol.MethodKind == MethodKind.Ordinary && methodSymbol.IsStatic)
                                {
                                    yield return span.TextSpan.ToTagSpan(snapshot, _staticMethodType);
                                }
                                //other method call
                                else
                                {
                                    yield return span.TextSpan.ToTagSpan(snapshot, _normalMethodType);
                                }
                                break;
                        }
                        break;
                    case SymbolKind.TypeParameter:
                        yield return span.TextSpan.ToTagSpan(snapshot, _typeParameterType);
                        break;
                    case SymbolKind.Parameter:
                        yield return span.TextSpan.ToTagSpan(snapshot, _parameterType);
                        break;
                    case SymbolKind.Namespace:
                        if (span.ClassificationType != ClassificationTypeNames.Keyword)
                        {
                            yield return span.TextSpan.ToTagSpan(snapshot, _namespaceType);
                        }
                        break;
                    case SymbolKind.Property:
                        yield return span.TextSpan.ToTagSpan(snapshot, _propertyType);
                        break;
                    case SymbolKind.Local:
                        yield return span.TextSpan.ToTagSpan(snapshot, _localType);
                        break;
                    case SymbolKind.Event:
                        yield return span.TextSpan.ToTagSpan(snapshot, _eventType);
                        break;
                    case SymbolKind.NamedType:
                        if (IsSpecialType(symbol))
                        {
                            yield return span.TextSpan.ToTagSpan(snapshot, _typeSpecialType);
                        }
                        else
                        {
                            switch (span.ClassificationType)
                            {
                                case ClassificationTypeNames.StructName:
                                    yield return span.TextSpan.ToTagSpan(snapshot, _structType);
                                    break;
                                case ClassificationTypeNames.ClassName:
                                    yield return span.TextSpan.ToTagSpan(snapshot, _classType);
                                    break;
                                case ClassificationTypeNames.InterfaceName:
                                    yield return span.TextSpan.ToTagSpan(snapshot, _interfaceType);
                                    break;
                                case ClassificationTypeNames.DelegateName:
                                    yield return span.TextSpan.ToTagSpan(snapshot, _delegateType);
                                    break;
                                case ClassificationTypeNames.EnumName:
                                    yield return span.TextSpan.ToTagSpan(snapshot, _enumType);
                                    break;
                            }
                        }
                        break;
                }
            }
        }

        private bool IsSpecialType(ISymbol symbol)
        {
            var type = (INamedTypeSymbol)symbol;
            return type.SpecialType != SpecialType.None;
        }

        private SyntaxNode GetExpression(SyntaxNode node)
        {
            if (node.CSharpKind() == CSharp.SyntaxKind.Argument)
            {
                return ((CSharp.Syntax.ArgumentSyntax)node).Expression;
            }
            else if (node.CSharpKind() == CSharp.SyntaxKind.AttributeArgument)
            {
                return ((CSharp.Syntax.AttributeArgumentSyntax)node).Expression;
            }
            else if (node.VbKind() == VB.SyntaxKind.SimpleArgument)
            {
                return ((VB.Syntax.SimpleArgumentSyntax)node).Expression;
            }
            return node;
        }

        private bool IsConstructor(IMethodSymbol methodSymbol)
        {
            return methodSymbol.MethodKind == MethodKind.Constructor ||
                   methodSymbol.MethodKind == MethodKind.StaticConstructor ||
                   methodSymbol.MethodKind == MethodKind.SharedConstructor;
        }

        private IEnumerable<ClassifiedSpan> GetClassifiedSpans(
              Workspace workspace, SemanticModel model,
              NormalizedSnapshotSpanCollection spans)
        {
            var comparer = StringComparer.InvariantCultureIgnoreCase;
            var classifiedSpans =
              spans.SelectMany(span =>
              {
                  var textSpan = TextSpan.FromBounds(span.Start, span.End);
                  return Classifier.GetClassifiedSpans(model, textSpan, workspace);
              });
            return classifiedSpans.Where(span =>
                SupportedClassificationTypeNames.Contains(span.ClassificationType, comparer));
        }

        private class Cache
        {
            public Workspace Workspace { get; private set; }
            public Document Document { get; private set; }
            public SemanticModel SemanticModel { get; private set; }
            public SyntaxNode SyntaxRoot { get; private set; }
            public ITextSnapshot Snapshot { get; private set; }

            private Cache() { }

            public static async Task<Cache> Resolve(ITextBuffer buffer, ITextSnapshot snapshot)
            {
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
                return new Cache
                {
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

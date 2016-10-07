using System.ComponentModel.Composition;
using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Utilities;

namespace SemanticColorizer
{
    internal static class ClassificationTypes
    {
        [Export(typeof(ClassificationTypeDefinition))]
        [Name(Constants.FieldFormat)]
        internal static ClassificationTypeDefinition FieldType = null;

        [Export(typeof(ClassificationTypeDefinition))]
        [Name(Constants.EnumFieldFormat)]
        internal static ClassificationTypeDefinition EnumFieldType = null;

        [Export(typeof(ClassificationTypeDefinition))]
        [Name(Constants.ExtensionMethodFormat)]
        internal static ClassificationTypeDefinition ExtensionMethodType = null;

        [Export(typeof(ClassificationTypeDefinition))]
        [Name(Constants.StaticMethodFormat)]
        internal static ClassificationTypeDefinition StaticMethodType = null;

        [Export(typeof(ClassificationTypeDefinition))]
        [Name(Constants.NormalMethodFormat)]
        internal static ClassificationTypeDefinition NormalMethodType = null;

        [Export(typeof(ClassificationTypeDefinition))]
        [Name(Constants.ConstructorFormat)]
        internal static ClassificationTypeDefinition ConstructorType = null;

        [Export(typeof(ClassificationTypeDefinition))]
        [Name(Constants.TypeParameterFormat)]
        internal static ClassificationTypeDefinition TypeParameterType = null;

        [Export(typeof(ClassificationTypeDefinition))]
        [Name(Constants.ParameterFormat)]
        internal static ClassificationTypeDefinition ParameterType = null;

        [Export(typeof(ClassificationTypeDefinition))]
        [Name(Constants.NamespaceFormat)]
        internal static ClassificationTypeDefinition NamespaceType = null;

        [Export(typeof(ClassificationTypeDefinition))]
        [Name(Constants.PropertyFormat)]
        internal static ClassificationTypeDefinition PropertyType = null;

        [Export(typeof(ClassificationTypeDefinition))]
        [Name(Constants.LocalFormat)]
        internal static ClassificationTypeDefinition LocalType = null;

        [Export(typeof(ClassificationTypeDefinition))]
        [Name(Constants.TypeSpecialFormat)]
        internal static ClassificationTypeDefinition TypeSpecialType = null;

        [Export(typeof(ClassificationTypeDefinition))]
        [Name(Constants.TypeNormalFormat)]
        internal static ClassificationTypeDefinition TypeNormalType = null;

        [Export(typeof(ClassificationTypeDefinition))]
        [Name(Constants.EventFormat)]
        internal static ClassificationTypeDefinition EventType = null;
    }
}

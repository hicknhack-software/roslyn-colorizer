using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Utilities;
using System.ComponentModel.Composition;

namespace SemanticColorizer
{
    internal static class ClassificationTypes
    {
#pragma warning disable CS0649
        [Export(typeof(ClassificationTypeDefinition))]
        [Name(Constants.FieldFormat)]
        internal static ClassificationTypeDefinition FieldType;

        [Export(typeof(ClassificationTypeDefinition))]
        [Name(Constants.EnumFieldFormat)]
        internal static ClassificationTypeDefinition EnumFieldType;

        [Export(typeof(ClassificationTypeDefinition))]
        [Name(Constants.ExtensionMethodFormat)]
        internal static ClassificationTypeDefinition ExtensionMethodType;

        [Export(typeof(ClassificationTypeDefinition))]
        [Name(Constants.StaticMethodFormat)]
        internal static ClassificationTypeDefinition StaticMethodType;

        [Export(typeof(ClassificationTypeDefinition))]
        [Name(Constants.NormalMethodFormat)]
        internal static ClassificationTypeDefinition NormalMethodType;

        [Export(typeof(ClassificationTypeDefinition))]
        [Name(Constants.LocalFunctionFormat)]
        internal static ClassificationTypeDefinition LocalFunctionType;

        [Export(typeof(ClassificationTypeDefinition))]
        [Name(Constants.ConstructorFormat)]
        internal static ClassificationTypeDefinition ConstructorType;

        [Export(typeof(ClassificationTypeDefinition))]
        [Name(Constants.TypeParameterFormat)]
        internal static ClassificationTypeDefinition TypeParameterType;

        [Export(typeof(ClassificationTypeDefinition))]
        [Name(Constants.ParameterFormat)]
        internal static ClassificationTypeDefinition ParameterType;

        [Export(typeof(ClassificationTypeDefinition))]
        [Name(Constants.NamespaceFormat)]
        internal static ClassificationTypeDefinition NamespaceType;

        [Export(typeof(ClassificationTypeDefinition))]
        [Name(Constants.PropertyFormat)]
        internal static ClassificationTypeDefinition PropertyType;

        [Export(typeof(ClassificationTypeDefinition))]
        [Name(Constants.LocalFormat)]
        internal static ClassificationTypeDefinition LocalType;

        [Export(typeof(ClassificationTypeDefinition))]
        [Name(Constants.TypeSpecialFormat)]
        internal static ClassificationTypeDefinition TypeSpecialType;

        [Export(typeof(ClassificationTypeDefinition))]
        [Name(Constants.ClassFormat)]
        internal static ClassificationTypeDefinition ClassType;

        [Export(typeof(ClassificationTypeDefinition))]
        [Name(Constants.InterfaceFormat)]
        internal static ClassificationTypeDefinition InterfaceType;

        [Export(typeof(ClassificationTypeDefinition))]
        [Name(Constants.EnumFormat)]
        internal static ClassificationTypeDefinition EnumType;

        [Export(typeof(ClassificationTypeDefinition))]
        [Name(Constants.StructFormat)]
        internal static ClassificationTypeDefinition StructType;

        [Export(typeof(ClassificationTypeDefinition))]
        [Name(Constants.EventFormat)]
        internal static ClassificationTypeDefinition EventType;

        [Export(typeof(ClassificationTypeDefinition))]
        [Name(Constants.DelegateFormat)]
        internal static ClassificationTypeDefinition DelegateType;
#pragma warning restore CS0649
    }
}

﻿using Microsoft.VisualStudio.Text.Classification;
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
        [Name(Constants.EventFormat)]
        internal static ClassificationTypeDefinition EventType;

        [Export(typeof(ClassificationTypeDefinition))]
        [Name(Constants.ControlFlowKeywordFormat)]
        internal static ClassificationTypeDefinition ControlFlowKeywordType;
#pragma warning restore CS0649
    }
}

using Microsoft.CodeAnalysis.Classification;
using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Utilities;
using System.ComponentModel.Composition;
using System.Windows.Media;

namespace SemanticColorizer
{
    [Export(typeof(EditorFormatDefinition))]
    [ClassificationType(ClassificationTypeNames = Constants.FieldFormat)]
    [Name(Constants.FieldFormat)]
    [UserVisible(true)]
    [Order(After = ClassificationTypeNames.Identifier)]
    internal sealed class SemanticFieldFormat : ClassificationFormatDefinition
    {
        public SemanticFieldFormat()
        {
            DisplayName = "Semantic Field";
            ForegroundColor = Colors.SaddleBrown;
        }
    }

    [Export(typeof(EditorFormatDefinition))]
    [ClassificationType(ClassificationTypeNames = Constants.EnumFieldFormat)]
    [Name(Constants.EnumFieldFormat)]
    [UserVisible(true)]
    [Order(After = ClassificationTypeNames.Identifier)]
    internal sealed class SemanticEnumFieldFormat : ClassificationFormatDefinition
    {
        public SemanticEnumFieldFormat()
        {
            DisplayName = "Semantic Enum Field";
        }
    }

    [Export(typeof(EditorFormatDefinition))]
    [ClassificationType(ClassificationTypeNames = Constants.ExtensionMethodFormat)]
    [Name(Constants.ExtensionMethodFormat)]
    [UserVisible(true)]
    [Order(After = ClassificationTypeNames.Identifier)]
    internal sealed class SemanticExtensionMethodFormat : ClassificationFormatDefinition
    {
        public SemanticExtensionMethodFormat()
        {
            DisplayName = "Semantic Extension Method";
            IsItalic = true;
        }
    }

    [Export(typeof(EditorFormatDefinition))]
    [ClassificationType(ClassificationTypeNames = Constants.StaticMethodFormat)]
    [Name(Constants.StaticMethodFormat)]
    [UserVisible(true)]
    [Order(After = ClassificationTypeNames.Identifier)]
    internal sealed class SemanticStaticMethodFormat : ClassificationFormatDefinition
    {
        public SemanticStaticMethodFormat()
        {
            DisplayName = "Semantic Static Method";
        }
    }

    [Export(typeof(EditorFormatDefinition))]
    [ClassificationType(ClassificationTypeNames = Constants.NormalMethodFormat)]
    [Name(Constants.NormalMethodFormat)]
    [UserVisible(true)]
    [Order(After = ClassificationTypeNames.Identifier)]
    internal sealed class SemanticNormalMethodFormat : ClassificationFormatDefinition
    {
        public SemanticNormalMethodFormat()
        {
            DisplayName = "Semantic Normal Method";
        }
    }
    [Export(typeof(EditorFormatDefinition))]
    [ClassificationType(ClassificationTypeNames = Constants.LocalFunctionFormat)]
    [Name(Constants.LocalFunctionFormat)]
    [UserVisible(true)]
    [Order(After = ClassificationTypeNames.Identifier)]
    internal sealed class SemanticLocalFunctionFormat : ClassificationFormatDefinition
    {
        public SemanticLocalFunctionFormat()
        {
            DisplayName = "Semantic Local Function";
        }
    }

    [Export(typeof(EditorFormatDefinition))]
    [ClassificationType(ClassificationTypeNames = Constants.ConstructorFormat)]
    [Name(Constants.ConstructorFormat)]
    [UserVisible(true)]
    [Order(After = ClassificationTypeNames.Identifier)]
    internal sealed class SemanticConstructorFormat : ClassificationFormatDefinition
    {
        public SemanticConstructorFormat()
        {
            DisplayName = "Semantic Constructor";
        }
    }

    [Export(typeof(EditorFormatDefinition))]
    [ClassificationType(ClassificationTypeNames = Constants.ParameterFormat)]
    [Name(Constants.ParameterFormat)]
    [UserVisible(true)]
    [Order(After = ClassificationTypeNames.Identifier)]
    internal sealed class SemanticParameterFormat : ClassificationFormatDefinition
    {
        public SemanticParameterFormat()
        {
            DisplayName = "Semantic Parameter";
            ForegroundColor = Colors.SlateGray;
        }
    }

    [Export(typeof(EditorFormatDefinition))]
    [ClassificationType(ClassificationTypeNames = Constants.NamespaceFormat)]
    [Name(Constants.NamespaceFormat)]
    [UserVisible(true)]
    [Order(After = ClassificationTypeNames.Identifier)]
    internal sealed class SemanticNamespaceFormat : ClassificationFormatDefinition
    {
        public SemanticNamespaceFormat()
        {
            DisplayName = "Semantic Namespace";
            ForegroundColor = Colors.LimeGreen;
        }
    }

    [Export(typeof(EditorFormatDefinition))]
    [ClassificationType(ClassificationTypeNames = Constants.PropertyFormat)]
    [Name(Constants.PropertyFormat)]
    [UserVisible(true)]
    [Order(After = ClassificationTypeNames.Identifier)]
    internal sealed class SemanticPropertyFormat : ClassificationFormatDefinition
    {
        public SemanticPropertyFormat()
        {
            DisplayName = "Semantic Property";
        }
    }

    [Export(typeof(EditorFormatDefinition))]
    [ClassificationType(ClassificationTypeNames = Constants.LocalFormat)]
    [Name(Constants.LocalFormat)]
    [UserVisible(true)]
    [Order(After = ClassificationTypeNames.Identifier)]
    internal sealed class SemanticLocalFormat : ClassificationFormatDefinition
    {
        public SemanticLocalFormat()
        {
            DisplayName = "Semantic Local";
        }
    }

    [Export(typeof(EditorFormatDefinition))]
    [ClassificationType(ClassificationTypeNames = Constants.TypeSpecialFormat)]
    [Name(Constants.TypeSpecialFormat)]
    [UserVisible(true)]
    [Order(After = ClassificationTypeNames.Identifier)]
    internal sealed class SemanticTypeSpecialFormat : ClassificationFormatDefinition
    {
        public SemanticTypeSpecialFormat()
        {
            DisplayName = "Semantic Special Type";
        }
    }

    [Export(typeof(EditorFormatDefinition))]
    [ClassificationType(ClassificationTypeNames = Constants.EventFormat)]
    [Name(Constants.EventFormat)]
    [UserVisible(true)]
    [Order(After = ClassificationTypeNames.Identifier)]
    internal sealed class SemanticEventFormat : ClassificationFormatDefinition
    {
        public SemanticEventFormat()
        {
            DisplayName = "Semantic Event";
        }
    }

    [Export(typeof(EditorFormatDefinition))]
    [ClassificationType(ClassificationTypeNames = Constants.ControlFlowKeywordFormat)]
    [Name(Constants.ControlFlowKeywordFormat)]
    [UserVisible(true)]
    [Order(After = ClassificationTypeNames.Keyword)]
    internal sealed class SemanticControlFlowKeywordFormat : ClassificationFormatDefinition
    {
        public SemanticControlFlowKeywordFormat()
        {
            DisplayName = "Semantic Control Flow Keyword";
        }
    }
}

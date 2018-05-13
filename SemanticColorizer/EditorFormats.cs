using System.ComponentModel.Composition;
using System.Windows.Media;
using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Utilities;

namespace SemanticColorizer
{
    [Export(typeof(EditorFormatDefinition))]
    [ClassificationType(ClassificationTypeNames = Constants.FieldFormat)]
    [Name(Constants.FieldFormat)]
    [UserVisible(true)]
    [Order(After = Priority.Default)]
    internal sealed class SemanticFieldFormat : ClassificationFormatDefinition
    {
        public SemanticFieldFormat() {
            DisplayName = "Semantic Field";
        }
    }

    [Export(typeof(EditorFormatDefinition))]
    [ClassificationType(ClassificationTypeNames = Constants.EnumFieldFormat)]
    [Name(Constants.EnumFieldFormat)]
    [UserVisible(true)]
    [Order(After = Priority.Default)]
    internal sealed class SemanticEnumFieldFormat : ClassificationFormatDefinition
    {
        public SemanticEnumFieldFormat() {
            DisplayName = "Semantic Enum Field";
        }
    }

    [Export(typeof(EditorFormatDefinition))]
    [ClassificationType(ClassificationTypeNames = Constants.ExtensionMethodFormat)]
    [Name(Constants.ExtensionMethodFormat)]
    [UserVisible(true)]
    [Order(After = Priority.Default)]
    internal sealed class SemanticExtensionMethodFormat : ClassificationFormatDefinition
    {
        public SemanticExtensionMethodFormat() {
            DisplayName = "Semantic Extension Method";
            IsItalic = true;
        }
    }

    [Export(typeof(EditorFormatDefinition))]
    [ClassificationType(ClassificationTypeNames = Constants.StaticMethodFormat)]
    [Name(Constants.StaticMethodFormat)]
    [UserVisible(true)]
    [Order(After = Priority.Default)]
    internal sealed class SemanticStaticMethodFormat : ClassificationFormatDefinition
    {
        public SemanticStaticMethodFormat() {
            DisplayName = "Semantic Static Method";
        }
    }

    [Export(typeof(EditorFormatDefinition))]
    [ClassificationType(ClassificationTypeNames = Constants.NormalMethodFormat)]
    [Name(Constants.NormalMethodFormat)]
    [UserVisible(true)]
    [Order(After = Priority.Default)]
    internal sealed class SemanticNormalMethodFormat : ClassificationFormatDefinition
    {
        public SemanticNormalMethodFormat() {
            DisplayName = "Semantic Normal Method";
        }
    }
    [Export(typeof(EditorFormatDefinition))]
    [ClassificationType(ClassificationTypeNames = Constants.LocalFunctionFormat)]
    [Name(Constants.LocalFunctionFormat)]
    [UserVisible(true)]
    [Order(After = Priority.Default)]
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
    [Order(After = Priority.Default)]
    internal sealed class SemanticConstructorFormat : ClassificationFormatDefinition
    {
        public SemanticConstructorFormat() {
            DisplayName = "Semantic Constructor";
        }
    }

    [Export(typeof(EditorFormatDefinition))]
    [ClassificationType(ClassificationTypeNames = Constants.TypeParameterFormat)]
    [Name(Constants.TypeParameterFormat)]
    [UserVisible(true)]
    [Order(After = Priority.Default)]
    internal sealed class SemanticTypeParameterFormat : ClassificationFormatDefinition
    {
        public SemanticTypeParameterFormat() {
            DisplayName = "Semantic Type Parameter";
            ForegroundColor = Colors.SlateGray;
        }
    }

    [Export(typeof(EditorFormatDefinition))]
    [ClassificationType(ClassificationTypeNames = Constants.ParameterFormat)]
    [Name(Constants.ParameterFormat)]
    [UserVisible(true)]
    [Order(After = Priority.Default)]
    internal sealed class SemanticParameterFormat : ClassificationFormatDefinition
    {
        public SemanticParameterFormat() {
            DisplayName = "Semantic Parameter";
            ForegroundColor = Colors.SlateGray;
        }
    }

    [Export(typeof(EditorFormatDefinition))]
    [ClassificationType(ClassificationTypeNames = Constants.NamespaceFormat)]
    [Name(Constants.NamespaceFormat)]
    [UserVisible(true)]
    [Order(After = Priority.Default)]
    internal sealed class SemanticNamespaceFormat : ClassificationFormatDefinition
    {
        public SemanticNamespaceFormat() {
            DisplayName = "Semantic Namespace";
            ForegroundColor = Colors.LimeGreen;
        }
    }

    [Export(typeof(EditorFormatDefinition))]
    [ClassificationType(ClassificationTypeNames = Constants.PropertyFormat)]
    [Name(Constants.PropertyFormat)]
    [UserVisible(true)]
    [Order(After = Priority.Default)]
    internal sealed class SemanticPropertyFormat : ClassificationFormatDefinition
    {
        public SemanticPropertyFormat() {
            DisplayName = "Semantic Property";
        }
    }

    [Export(typeof(EditorFormatDefinition))]
    [ClassificationType(ClassificationTypeNames = Constants.LocalFormat)]
    [Name(Constants.LocalFormat)]
    [UserVisible(true)]
    [Order(After = Priority.Default)]
    internal sealed class SemanticLocalFormat : ClassificationFormatDefinition
    {
        public SemanticLocalFormat() {
            DisplayName = "Semantic Local";
        }
    }

    [Export(typeof(EditorFormatDefinition))]
    [ClassificationType(ClassificationTypeNames = Constants.ClassFormat)]
    [Name(Constants.ClassFormat)]
    [UserVisible(true)]
    [Order(After = Priority.Default)]
    internal sealed class SemanticClassFormat : ClassificationFormatDefinition
    {
        public SemanticClassFormat() {
            DisplayName = "Semantic Class";
        }
    }

    [Export(typeof(EditorFormatDefinition))]
    [ClassificationType(ClassificationTypeNames = Constants.InterfaceFormat)]
    [Name(Constants.InterfaceFormat)]
    [UserVisible(true)]
    [Order(After = Priority.Default)]
    internal sealed class SemanticInterfaceFormat : ClassificationFormatDefinition
    {
        public SemanticInterfaceFormat() {
            DisplayName = "Semantic Interface";
        }
    }

    [Export(typeof(EditorFormatDefinition))]
    [ClassificationType(ClassificationTypeNames = Constants.EventFormat)]
    [Name(Constants.EventFormat)]
    [UserVisible(true)]
    [Order(After = Priority.Default)]
    internal sealed class SemanticEventFormat : ClassificationFormatDefinition
    {
        public SemanticEventFormat()
        {
            DisplayName = "Semantic Event";
        }
    }

    [Export(typeof(EditorFormatDefinition))]
    [ClassificationType(ClassificationTypeNames = Constants.StructFormat)]
    [Name(Constants.StructFormat)]
    [UserVisible(true)]
    [Order(After = Priority.Default)]
    internal sealed class SemanticStructFormat : ClassificationFormatDefinition
    {
        public SemanticStructFormat()
        {
            DisplayName = "Semantic Struct";
        }
    }

    [Export(typeof(EditorFormatDefinition))]
    [ClassificationType(ClassificationTypeNames = Constants.EnumFormat)]
    [Name(Constants.EnumFormat)]
    [UserVisible(true)]
    [Order(After = Priority.Default)]
    internal sealed class SemanticEnumFormat : ClassificationFormatDefinition
    {
        public SemanticEnumFormat()
        {
            DisplayName = "Semantic Enum";
        }
    }

    [Export(typeof(EditorFormatDefinition))]
    [ClassificationType(ClassificationTypeNames = Constants.DelegateFormat)]
    [Name(Constants.DelegateFormat)]
    [UserVisible(true)]
    [Order(After = Priority.Default)]
    internal sealed class SemanticDelegateFormat : ClassificationFormatDefinition
    {
        public SemanticDelegateFormat()
        {
            DisplayName = "Semantic Delegate";
        }
    }
}

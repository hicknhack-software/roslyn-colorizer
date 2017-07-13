using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis;

namespace SemanticColorizer
{
    public static class CSharpExtensions
    {
        public static SyntaxKind CSharpKind(this SyntaxNode node) {
            return node.Kind();
        }
    }
}

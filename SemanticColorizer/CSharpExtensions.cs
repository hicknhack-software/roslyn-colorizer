using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis;

namespace SemanticColorizer
{
    public static class CSharpExtensions
    {
        public static SyntaxKind CSharpKind(this SyntaxNode node)
        {
            return node.Kind();
        }
    }
}

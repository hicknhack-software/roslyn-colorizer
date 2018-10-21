using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace SemanticColorizer
{
    public static class CSharpExtensions
    {
        public static SyntaxKind CSharpKind(this SyntaxNode node)
        {
            return node.Kind();
        }

        public static int CountCSharpControlKeywords(this SyntaxNode node)
        {
            var csKind = node.CSharpKind();

            if (csKind != SyntaxKind.None) 
            {
                switch (csKind) 
                {
                    case SyntaxKind.IfStatement:
                    case SyntaxKind.SwitchStatement:
                    case SyntaxKind.CaseSwitchLabel:
                    case SyntaxKind.DefaultSwitchLabel:
                    case SyntaxKind.ForStatement:
                    case SyntaxKind.ForEachStatement:
                    case SyntaxKind.WhileStatement:
                    case SyntaxKind.DoStatement:
                    case SyntaxKind.GotoStatement:
                    case SyntaxKind.ReturnStatement:
                    case SyntaxKind.ThrowStatement:
                    case SyntaxKind.TryStatement:
                    case SyntaxKind.CatchClause:
                    case SyntaxKind.FinallyClause:
                    case SyntaxKind.ContinueStatement:
                    case SyntaxKind.BreakStatement:
                        return 1;

                    case SyntaxKind.YieldReturnStatement:
                    case SyntaxKind.YieldBreakStatement:
                        return 2;

                    case SyntaxKind.ElseClause:
                        return ((ElseClauseSyntax)node).Statement is IfStatementSyntax ? 2 : 1;
                }
            }

            return 0;
        }
    }
}

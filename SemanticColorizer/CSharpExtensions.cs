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

        public static (int count, object extraKeyword) CountCSharpControlKeywords(this SyntaxNode node)
        {
            // extraKeyword is an object instead of a 'Nullable<SyntaxToken>' as a minor optimization to avoid
            // returning a rather large struct every time this method is called. Do/while loops are pretty rare
            // so the very occasional boxing is goign to be faster

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
                    case SyntaxKind.GotoStatement:
                    case SyntaxKind.ReturnStatement:
                    case SyntaxKind.ThrowStatement:
                    case SyntaxKind.TryStatement:
                    case SyntaxKind.CatchClause:
                    case SyntaxKind.FinallyClause:
                    case SyntaxKind.ContinueStatement:
                    case SyntaxKind.BreakStatement:
                        return (1, null);

                    case SyntaxKind.YieldReturnStatement:
                    case SyntaxKind.YieldBreakStatement:
                        return (2, null);

                    case SyntaxKind.ElseClause:
                        return ((ElseClauseSyntax)node).Statement is IfStatementSyntax ? (2, (object)null) : (1, null);

                    case SyntaxKind.DoStatement:
                        var doStatement = (DoStatementSyntax)node;
                        var whileKeyword = doStatement.WhileKeyword;

                        return whileKeyword.Span.IsEmpty ? (1, (object)null) : (1, whileKeyword);
                }
            }

            return (0, null);
        }
    }
}

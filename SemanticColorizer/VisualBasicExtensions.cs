using Microsoft.CodeAnalysis.VisualBasic;
using Microsoft.CodeAnalysis;

namespace SemanticColorizer
{
    public static class VbExtensions
    {
        public static SyntaxKind VbKind(this SyntaxNode node)
        {
            return node.Kind();
        }

        public static int CountVbControlKeywords(this SyntaxNode node)
        {
            var vbKind = node.VbKind();            
            
            if (vbKind != SyntaxKind.None) 
            {
                switch (vbKind) 
                {
                    case SyntaxKind.IfStatement:
                    case SyntaxKind.ElseIfStatement:
                    case SyntaxKind.ElseStatement:
                    case SyntaxKind.CaseStatement:
                    case SyntaxKind.ForStatement:
                    case SyntaxKind.NextStatement:
                    case SyntaxKind.WhileStatement:
                    case SyntaxKind.SimpleDoStatement:
                    case SyntaxKind.SimpleLoopStatement:
                    case SyntaxKind.GoToKeyword:
                    case SyntaxKind.ReturnStatement:
                    case SyntaxKind.ThrowStatement:
                    case SyntaxKind.TryStatement:
                    case SyntaxKind.CatchStatement:
                    case SyntaxKind.FinallyStatement:
                    case SyntaxKind.YieldStatement:
                    case SyntaxKind.EndStatement:
                        return 1;

                    case SyntaxKind.SelectStatement:
                    case SyntaxKind.EndIfStatement:
                    case SyntaxKind.DoWhileStatement:
                    case SyntaxKind.DoUntilStatement:
                    case SyntaxKind.ExitWhileStatement:
                    case SyntaxKind.EndWhileStatement:
                    case SyntaxKind.ExitDoStatement:
                    case SyntaxKind.ExitSelectStatement:
                    case SyntaxKind.EndSelectStatement:
                    case SyntaxKind.ExitTryStatement:
                    case SyntaxKind.EndTryStatement:
                    case SyntaxKind.ContinueWhileStatement:
                    case SyntaxKind.ContinueDoStatement:
                    case SyntaxKind.ContinueForStatement:
                    case SyntaxKind.ForEachStatement:
                    case SyntaxKind.ExitFunctionStatement:
                    case SyntaxKind.ExitOperatorStatement:
                    case SyntaxKind.ExitPropertyStatement:
                    case SyntaxKind.ExitSubStatement:
                    case SyntaxKind.ExitForStatement:
                    case SyntaxKind.CaseElseStatement:
                        return 2;
                }
            }

            return 0;
        }
    }
}

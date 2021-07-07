using System.Collections.Generic;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace System.Linq.Async.SourceGenerator
{
    internal sealed class SyntaxReceiver : ISyntaxReceiver
    {
        public IList<MethodDeclarationSyntax> Candidates { get; } = new List<MethodDeclarationSyntax>();

        public void OnVisitSyntaxNode(SyntaxNode syntaxNode)
        {
            if (syntaxNode is MethodDeclarationSyntax { AttributeLists: { Count: >0 } } methodDeclarationSyntax)
            {
                Candidates.Add(methodDeclarationSyntax);
            }
        }
    }
}

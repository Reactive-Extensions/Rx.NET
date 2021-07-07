using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace System.Linq.Async.SourceGenerator
{
    internal sealed record AsyncMethod(IMethodSymbol Symbol, MethodDeclarationSyntax Syntax);
}

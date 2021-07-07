using System.Collections.Generic;

using Microsoft.CodeAnalysis;

namespace System.Linq.Async.SourceGenerator
{
    internal sealed record AsyncMethodGrouping(SyntaxTree SyntaxTree, IEnumerable<AsyncMethod> Methods);
}

using System.Collections.Generic;
using System.IO;
using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace System.Linq.Async.SourceGenerator
{
    [Generator]
    public sealed class AsyncOverloadsGenerator : ISourceGenerator
    {
        private const string AttributeSource =
            "using System;\n" +
            "using System.Diagnostics;\n" +
            "namespace System.Linq\n" +
            "{\n" +
            "    [AttributeUsage(AttributeTargets.Method)]\n" +
            "    [Conditional(\"COMPILE_TIME_ONLY\")]\n" +
            "    internal sealed class GenerateAsyncOverloadAttribute : Attribute { }\n" +
            "}\n";

        public void Initialize(GeneratorInitializationContext context)
        {
            context.RegisterForSyntaxNotifications(() => new SyntaxReceiver());
            context.RegisterForPostInitialization(c => c.AddSource("GenerateAsyncOverloadAttribute", AttributeSource));
        }

        public void Execute(GeneratorExecutionContext context)
        {
            if (context.SyntaxReceiver is not SyntaxReceiver syntaxReceiver) return;

            var options = GetGenerationOptions(context);
            var methodsBySyntaxTree = GetMethodsGroupedBySyntaxTree(context, syntaxReceiver);

            foreach (var grouping in methodsBySyntaxTree)
                context.AddSource(
                    $"{Path.GetFileNameWithoutExtension(grouping.SyntaxTree.FilePath)}.AsyncOverloads",
                    GenerateOverloads(grouping, options));
        }

        private static GenerationOptions GetGenerationOptions(GeneratorExecutionContext context)
            => new(SupportFlatAsyncApi: context.ParseOptions.PreprocessorSymbolNames.Contains("SUPPORT_FLAT_ASYNC_API"));

        private static IEnumerable<AsyncMethodGrouping> GetMethodsGroupedBySyntaxTree(GeneratorExecutionContext context, SyntaxReceiver syntaxReceiver)
            => GetMethodsGroupedBySyntaxTree(
                context,
                syntaxReceiver,
                GetAsyncOverloadAttributeSymbol(context));

        private static string GenerateOverloads(AsyncMethodGrouping grouping, GenerationOptions options)
        {
            var usings = grouping.SyntaxTree.GetRoot() is CompilationUnitSyntax compilationUnit
                ? compilationUnit.Usings.ToString()
                : string.Empty;

            var overloads = new StringBuilder();
            overloads.AppendLine("#nullable enable");
            overloads.AppendLine(usings);
            overloads.AppendLine("namespace System.Linq");
            overloads.AppendLine("{");
            overloads.AppendLine("    partial class AsyncEnumerable");
            overloads.AppendLine("    {");

            foreach (var method in grouping.Methods)
                overloads.AppendLine(GenerateOverload(method, options));

            overloads.AppendLine("    }");
            overloads.AppendLine("}");

            return overloads.ToString();
        }

        private static string GenerateOverload(AsyncMethod method, GenerationOptions options)
            => MethodDeclaration(method.Syntax.ReturnType, GetMethodName(method.Symbol, options))
                .WithModifiers(TokenList(Token(SyntaxKind.PublicKeyword), Token(SyntaxKind.StaticKeyword)))
                .WithTypeParameterList(method.Syntax.TypeParameterList)
                .WithParameterList(method.Syntax.ParameterList)
                .WithConstraintClauses(method.Syntax.ConstraintClauses)
                .WithExpressionBody(ArrowExpressionClause(
                    InvocationExpression(
                        IdentifierName(method.Symbol.Name),
                        ArgumentList(
                            SeparatedList(
                                method.Syntax.ParameterList.Parameters
                                    .Select(p => Argument(IdentifierName(p.Identifier))))))))
                .WithSemicolonToken(Token(SyntaxKind.SemicolonToken))
                .WithLeadingTrivia(method.Syntax.GetLeadingTrivia().Where(t => t.GetStructure() is not DirectiveTriviaSyntax))
                .NormalizeWhitespace()
                .ToFullString();

        private static INamedTypeSymbol GetAsyncOverloadAttributeSymbol(GeneratorExecutionContext context)
            => context.Compilation.GetTypeByMetadataName("System.Linq.GenerateAsyncOverloadAttribute") ?? throw new InvalidOperationException();

        private static IEnumerable<AsyncMethodGrouping> GetMethodsGroupedBySyntaxTree(GeneratorExecutionContext context, SyntaxReceiver syntaxReceiver, INamedTypeSymbol attributeSymbol)
            => from candidate in syntaxReceiver.Candidates
               group candidate by candidate.SyntaxTree into grouping
               let model = context.Compilation.GetSemanticModel(grouping.Key)
               select new AsyncMethodGrouping(
                  grouping.Key,
                  from methodSyntax in grouping
                  let methodSymbol = model.GetDeclaredSymbol(methodSyntax) ?? throw new InvalidOperationException()
                  where methodSymbol.GetAttributes().Any(a => SymbolEqualityComparer.Default.Equals(a.AttributeClass!, attributeSymbol))
                  select new AsyncMethod(methodSymbol, methodSyntax));

        private static string GetMethodName(IMethodSymbol methodSymbol, GenerationOptions options)
        {
            var methodName = methodSymbol.Name.Replace("Core", "");
            return options.SupportFlatAsyncApi
                ? methodName.Replace("Await", "").Replace("WithCancellation", "")
                : methodName;
        }
    }
}

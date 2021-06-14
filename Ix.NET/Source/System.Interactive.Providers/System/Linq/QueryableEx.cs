// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT License.
// See the LICENSE file in the project root for more information. 

using System.Collections.Generic;
using System.Globalization;
using System.Linq.Expressions;
using System.Reflection;

namespace System.Linq
{
    /// <summary>
    /// Provides a set of additional static methods that allow querying enumerable sequences.
    /// </summary>
    public static partial class QueryableEx
    {
        /// <summary>
        /// Gets the local Queryable provider.
        /// </summary>
        public static IQueryProvider Provider { get; } = new QueryProviderShim();

        private sealed class QueryProviderShim : IQueryProvider
        {
            public IQueryable<TElement> CreateQuery<TElement>(Expression expression)
            {
                var provider = new TElement[0].AsQueryable().Provider;
                var res = Redir(expression);
                return provider.CreateQuery<TElement>(res);
            }

            public IQueryable CreateQuery(Expression expression)
            {
                return CreateQuery<object>(expression);
            }

            public TResult Execute<TResult>(Expression expression)
            {
                var provider = new TResult[0].AsQueryable().Provider;
                var res = Redir(expression);
                return provider.Execute<TResult>(res);
            }

            public object Execute(Expression expression)
            {
                return Execute<object>(expression);
            }

            private static Expression Redir(Expression expression)
            {
                if (expression is MethodCallExpression mce && mce.Method.DeclaringType == typeof(QueryableEx))
                {
                    if (mce.Arguments.Count >= 1 && typeof(IQueryProvider).IsAssignableFrom(mce.Arguments[0].Type))
                    {
                        if (mce.Arguments[0] is ConstantExpression ce)
                        {
                            if (ce.Value is QueryProviderShim)
                            {
                                var targetType = typeof(QueryableEx);
                                var method = mce.Method;
                                var methods = GetMethods(targetType);
                                var arguments = mce.Arguments.Skip(1).ToList();

                                //
                                // From all the operators with the method's name, find the one that matches all arguments.
                                //
                                var typeArgs = method.IsGenericMethod ? method.GetGenericArguments() : null;
                                var targetMethod = methods[method.Name].FirstOrDefault(candidateMethod => ArgsMatch(candidateMethod, arguments, typeArgs));
                                if (targetMethod == null)
                                    throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, "There is no method '{0}' on type '{1}' that matches the specified arguments", method.Name, targetType.Name));

                                //
                                // Restore generic arguments.
                                //
                                if (typeArgs != null)
                                    targetMethod = targetMethod.MakeGenericMethod(typeArgs);

                                //
                                // Finally, we need to deal with mismatches on Expression<Func<...>> versus Func<...>.
                                //
                                var parameters = targetMethod.GetParameters();
                                for (int i = 0, n = parameters.Length; i < n; i++)
                                {
                                    arguments[i] = Unquote(arguments[i]);
                                }

                                //
                                // Emit a new call to the discovered target method.
                                //
                                return Expression.Call(null, targetMethod, arguments);
                            }
                        }
                    }
                }

                return expression;
            }

            private static ILookup<string, MethodInfo> GetMethods(Type type)
            {
                return type.GetMethods(BindingFlags.Static | BindingFlags.Public).ToLookup(m => m.Name);
            }

            private static bool ArgsMatch(MethodInfo method, IList<Expression> arguments, Type[]? typeArgs)
            {
                //
                // Number of parameters should match. Notice we've sanitized IQueryProvider "this"
                // parameters first (see Redir).
                //
                var parameters = method.GetParameters();
                if (parameters.Length != arguments.Count)
                    return false;

                //
                // Genericity should match too.
                //
                if (!method.IsGenericMethod && typeArgs != null && typeArgs.Length > 0)
                    return false;

                //
                // Reconstruct the generic method if needed.
                //
                if (method.IsGenericMethodDefinition)
                {
                    if (typeArgs == null)
                        return false;

                    if (method.GetGenericArguments().Length != typeArgs.Length)
                        return false;

                    var result = method.MakeGenericMethod(typeArgs);
                    parameters = result.GetParameters();
                }

                //
                // Check compatibility for the parameter types.
                //
                for (int i = 0, n = arguments.Count; i < n; i++)
                {
                    var parameterType = parameters[i].ParameterType;
                    var argument = arguments[i];

                    //
                    // For operators that take a function (like Where, Select), we'll be faced
                    // with a quoted argument and a discrepancy between Expression<Func<...>>
                    // and the underlying Func<...>.
                    //
                    if (!parameterType.IsAssignableFrom(argument.Type))
                    {
                        argument = Unquote(argument);
                        if (!parameterType.IsAssignableFrom(argument.Type))
                            return false;
                    }
                }

                return true;
            }

            private static Expression Unquote(Expression expression)
            {
                //
                // Get rid of all outer quotes around an expression.
                //
                while (expression.NodeType == ExpressionType.Quote)
                    expression = ((UnaryExpression)expression).Operand;

                return expression;
            }
        }

        internal static Expression GetSourceExpression<TSource>(IEnumerable<TSource> source)
        {
            if (source is IQueryable<TSource> q)
                return q.Expression;

            return Expression.Constant(source, typeof(IEnumerable<TSource>));
        }

        internal static Expression GetSourceExpression<TSource>(IEnumerable<TSource>[] sources)
        {
            return Expression.NewArrayInit(
                typeof(IEnumerable<TSource>),
                sources.Select(source => GetSourceExpression(source))
            );
        }

        internal static MethodInfo InfoOf<R>(Expression<Func<R>> f)
        {
            return ((MethodCallExpression)f.Body).Method;
        }

        private sealed class TSource { }
        private sealed class TResult { }
        private sealed class TAccumulate { }
        private sealed class TOther { }
        private sealed class TKey { }
        private sealed class TException : Exception { }
    }
}

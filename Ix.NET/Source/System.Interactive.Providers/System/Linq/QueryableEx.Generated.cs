// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT License.
// See the LICENSE file in the project root for more information. 

using System.Collections.Generic;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Reflection;

namespace System.Linq
{
    public static partial class QueryableEx
    {
        private static MethodInfo s_Buffer__TSource__2__0;
        
        private static MethodInfo Buffer__TSource__2__0(Type TSource) =>
            (s_Buffer__TSource__2__0 ??
            (s_Buffer__TSource__2__0 = new Func<IQueryable<TSource>, int, IQueryable<IList<TSource>>>(Buffer<TSource>).GetMethodInfo().GetGenericMethodDefinition())).MakeGenericMethod(TSource);

        public static IQueryable<IList<TSource>> Buffer<TSource>(this IQueryable<TSource> source, int count)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            return source.Provider.CreateQuery<IList<TSource>>(Expression.Call(Buffer__TSource__2__0(typeof(TSource)), source.Expression, Expression.Constant(count, typeof(int))));
        }

#pragma warning disable 1591
        [EditorBrowsable(EditorBrowsableState.Never)]
        public static IEnumerable<IList<TSource>> Buffer<TSource>(IEnumerable<TSource> source, int count)
        {
#if REFERENCE_ASSEMBLY
            return default;
#else
            return EnumerableEx.Buffer<TSource>(source, count);
#endif
        }
#pragma warning restore 1591

        private static MethodInfo s_Buffer__TSource__3__0;
        
        private static MethodInfo Buffer__TSource__3__0(Type TSource) =>
            (s_Buffer__TSource__3__0 ??
            (s_Buffer__TSource__3__0 = new Func<IQueryable<TSource>, int, int, IQueryable<IList<TSource>>>(Buffer<TSource>).GetMethodInfo().GetGenericMethodDefinition())).MakeGenericMethod(TSource);

        public static IQueryable<IList<TSource>> Buffer<TSource>(this IQueryable<TSource> source, int count, int skip)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            return source.Provider.CreateQuery<IList<TSource>>(Expression.Call(Buffer__TSource__3__0(typeof(TSource)), source.Expression, Expression.Constant(count, typeof(int)), Expression.Constant(skip, typeof(int))));
        }

#pragma warning disable 1591
        [EditorBrowsable(EditorBrowsableState.Never)]
        public static IEnumerable<IList<TSource>> Buffer<TSource>(IEnumerable<TSource> source, int count, int skip)
        {
#if REFERENCE_ASSEMBLY
            return default;
#else
            return EnumerableEx.Buffer<TSource>(source, count, skip);
#endif
        }
#pragma warning restore 1591

        private static MethodInfo s_Catch__TSource__1__0;
        
        private static MethodInfo Catch__TSource__1__0(Type TSource) =>
            (s_Catch__TSource__1__0 ??
            (s_Catch__TSource__1__0 = new Func<IQueryable<IEnumerable<TSource>>, IQueryable<TSource>>(Catch<TSource>).GetMethodInfo().GetGenericMethodDefinition())).MakeGenericMethod(TSource);

        public static IQueryable<TSource> Catch<TSource>(this IQueryable<IEnumerable<TSource>> sources)
        {
            if (sources == null)
                throw new ArgumentNullException(nameof(sources));

            return sources.Provider.CreateQuery<TSource>(Expression.Call(Catch__TSource__1__0(typeof(TSource)), sources.Expression));
        }

#pragma warning disable 1591
        [EditorBrowsable(EditorBrowsableState.Never)]
        public static IEnumerable<TSource> Catch<TSource>(IEnumerable<IEnumerable<TSource>> sources)
        {
#if REFERENCE_ASSEMBLY
            return default;
#else
            return EnumerableEx.Catch<TSource>(sources);
#endif
        }
#pragma warning restore 1591

        private static MethodInfo s_Catch__TSource__2__0;
        
        private static MethodInfo Catch__TSource__2__0(Type TSource) =>
            (s_Catch__TSource__2__0 ??
            (s_Catch__TSource__2__0 = new Func<IQueryable<TSource>, IEnumerable<TSource>, IQueryable<TSource>>(Catch<TSource>).GetMethodInfo().GetGenericMethodDefinition())).MakeGenericMethod(TSource);

        public static IQueryable<TSource> Catch<TSource>(this IQueryable<TSource> first, IEnumerable<TSource> second)
        {
            if (first == null)
                throw new ArgumentNullException(nameof(first));
            if (second == null)
                throw new ArgumentNullException(nameof(second));

            return first.Provider.CreateQuery<TSource>(Expression.Call(Catch__TSource__2__0(typeof(TSource)), first.Expression, GetSourceExpression(second)));
        }

#pragma warning disable 1591
        [EditorBrowsable(EditorBrowsableState.Never)]
        public static IEnumerable<TSource> Catch<TSource>(IEnumerable<TSource> first, IEnumerable<TSource> second)
        {
#if REFERENCE_ASSEMBLY
            return default;
#else
            return EnumerableEx.Catch<TSource>(first, second);
#endif
        }
#pragma warning restore 1591

        private static MethodInfo s_Catch__TSource_TException__2__0;
        
        private static MethodInfo Catch__TSource_TException__2__0(Type TSource, Type TException) =>
            (s_Catch__TSource_TException__2__0 ??
            (s_Catch__TSource_TException__2__0 = new Func<IQueryable<TSource>, Expression<Func<TException, IEnumerable<TSource>>>, IQueryable<TSource>>(Catch<TSource, TException>).GetMethodInfo().GetGenericMethodDefinition())).MakeGenericMethod(TSource, TException);

        public static IQueryable<TSource> Catch<TSource, TException>(this IQueryable<TSource> source, Expression<Func<TException, IEnumerable<TSource>>> handler) where TException : Exception
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));
            if (handler == null)
                throw new ArgumentNullException(nameof(handler));

            return source.Provider.CreateQuery<TSource>(Expression.Call(Catch__TSource_TException__2__0(typeof(TSource), typeof(TException)), source.Expression, handler));
        }

#pragma warning disable 1591
        [EditorBrowsable(EditorBrowsableState.Never)]
        public static IEnumerable<TSource> Catch<TSource, TException>(IEnumerable<TSource> source, Func<TException, IEnumerable<TSource>> handler) where TException : Exception
        {
#if REFERENCE_ASSEMBLY
            return default;
#else
            return EnumerableEx.Catch<TSource, TException>(source, handler);
#endif
        }
#pragma warning restore 1591

        private static MethodInfo s_Concat__TSource__1__0;
        
        private static MethodInfo Concat__TSource__1__0(Type TSource) =>
            (s_Concat__TSource__1__0 ??
            (s_Concat__TSource__1__0 = new Func<IQueryable<IEnumerable<TSource>>, IQueryable<TSource>>(Concat<TSource>).GetMethodInfo().GetGenericMethodDefinition())).MakeGenericMethod(TSource);

        public static IQueryable<TSource> Concat<TSource>(this IQueryable<IEnumerable<TSource>> sources)
        {
            if (sources == null)
                throw new ArgumentNullException(nameof(sources));

            return sources.Provider.CreateQuery<TSource>(Expression.Call(Concat__TSource__1__0(typeof(TSource)), sources.Expression));
        }

#pragma warning disable 1591
        [EditorBrowsable(EditorBrowsableState.Never)]
        public static IEnumerable<TSource> Concat<TSource>(IEnumerable<IEnumerable<TSource>> sources)
        {
#if REFERENCE_ASSEMBLY
            return default;
#else
            return EnumerableEx.Concat<TSource>(sources);
#endif
        }
#pragma warning restore 1591

        private static MethodInfo s_Distinct__TSource_TKey__2__0;
        
        private static MethodInfo Distinct__TSource_TKey__2__0(Type TSource, Type TKey) =>
            (s_Distinct__TSource_TKey__2__0 ??
            (s_Distinct__TSource_TKey__2__0 = new Func<IQueryable<TSource>, Expression<Func<TSource, TKey>>, IQueryable<TSource>>(Distinct<TSource, TKey>).GetMethodInfo().GetGenericMethodDefinition())).MakeGenericMethod(TSource, TKey);

        public static IQueryable<TSource> Distinct<TSource, TKey>(this IQueryable<TSource> source, Expression<Func<TSource, TKey>> keySelector)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));
            if (keySelector == null)
                throw new ArgumentNullException(nameof(keySelector));

            return source.Provider.CreateQuery<TSource>(Expression.Call(Distinct__TSource_TKey__2__0(typeof(TSource), typeof(TKey)), source.Expression, keySelector));
        }

#pragma warning disable 1591
        [EditorBrowsable(EditorBrowsableState.Never)]
        public static IEnumerable<TSource> Distinct<TSource, TKey>(IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
        {
#if REFERENCE_ASSEMBLY
            return default;
#else
            return EnumerableEx.Distinct<TSource, TKey>(source, keySelector);
#endif
        }
#pragma warning restore 1591

        private static MethodInfo s_Distinct__TSource_TKey__3__0;
        
        private static MethodInfo Distinct__TSource_TKey__3__0(Type TSource, Type TKey) =>
            (s_Distinct__TSource_TKey__3__0 ??
            (s_Distinct__TSource_TKey__3__0 = new Func<IQueryable<TSource>, Expression<Func<TSource, TKey>>, IEqualityComparer<TKey>, IQueryable<TSource>>(Distinct<TSource, TKey>).GetMethodInfo().GetGenericMethodDefinition())).MakeGenericMethod(TSource, TKey);

        public static IQueryable<TSource> Distinct<TSource, TKey>(this IQueryable<TSource> source, Expression<Func<TSource, TKey>> keySelector, IEqualityComparer<TKey> comparer)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));
            if (keySelector == null)
                throw new ArgumentNullException(nameof(keySelector));
            if (comparer == null)
                throw new ArgumentNullException(nameof(comparer));

            return source.Provider.CreateQuery<TSource>(Expression.Call(Distinct__TSource_TKey__3__0(typeof(TSource), typeof(TKey)), source.Expression, keySelector, Expression.Constant(comparer, typeof(IEqualityComparer<TKey>))));
        }

#pragma warning disable 1591
        [EditorBrowsable(EditorBrowsableState.Never)]
        public static IEnumerable<TSource> Distinct<TSource, TKey>(IEnumerable<TSource> source, Func<TSource, TKey> keySelector, IEqualityComparer<TKey> comparer)
        {
#if REFERENCE_ASSEMBLY
            return default;
#else
            return EnumerableEx.Distinct<TSource, TKey>(source, keySelector, comparer);
#endif
        }
#pragma warning restore 1591

        private static MethodInfo s_DistinctUntilChanged__TSource__1__0;
        
        private static MethodInfo DistinctUntilChanged__TSource__1__0(Type TSource) =>
            (s_DistinctUntilChanged__TSource__1__0 ??
            (s_DistinctUntilChanged__TSource__1__0 = new Func<IQueryable<TSource>, IQueryable<TSource>>(DistinctUntilChanged<TSource>).GetMethodInfo().GetGenericMethodDefinition())).MakeGenericMethod(TSource);

        public static IQueryable<TSource> DistinctUntilChanged<TSource>(this IQueryable<TSource> source)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            return source.Provider.CreateQuery<TSource>(Expression.Call(DistinctUntilChanged__TSource__1__0(typeof(TSource)), source.Expression));
        }

#pragma warning disable 1591
        [EditorBrowsable(EditorBrowsableState.Never)]
        public static IEnumerable<TSource> DistinctUntilChanged<TSource>(IEnumerable<TSource> source)
        {
#if REFERENCE_ASSEMBLY
            return default;
#else
            return EnumerableEx.DistinctUntilChanged<TSource>(source);
#endif
        }
#pragma warning restore 1591

        private static MethodInfo s_DistinctUntilChanged__TSource__2__0;
        
        private static MethodInfo DistinctUntilChanged__TSource__2__0(Type TSource) =>
            (s_DistinctUntilChanged__TSource__2__0 ??
            (s_DistinctUntilChanged__TSource__2__0 = new Func<IQueryable<TSource>, IEqualityComparer<TSource>, IQueryable<TSource>>(DistinctUntilChanged<TSource>).GetMethodInfo().GetGenericMethodDefinition())).MakeGenericMethod(TSource);

        public static IQueryable<TSource> DistinctUntilChanged<TSource>(this IQueryable<TSource> source, IEqualityComparer<TSource> comparer)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));
            if (comparer == null)
                throw new ArgumentNullException(nameof(comparer));

            return source.Provider.CreateQuery<TSource>(Expression.Call(DistinctUntilChanged__TSource__2__0(typeof(TSource)), source.Expression, Expression.Constant(comparer, typeof(IEqualityComparer<TSource>))));
        }

#pragma warning disable 1591
        [EditorBrowsable(EditorBrowsableState.Never)]
        public static IEnumerable<TSource> DistinctUntilChanged<TSource>(IEnumerable<TSource> source, IEqualityComparer<TSource> comparer)
        {
#if REFERENCE_ASSEMBLY
            return default;
#else
            return EnumerableEx.DistinctUntilChanged<TSource>(source, comparer);
#endif
        }
#pragma warning restore 1591

        private static MethodInfo s_DistinctUntilChanged__TSource_TKey__2__0;
        
        private static MethodInfo DistinctUntilChanged__TSource_TKey__2__0(Type TSource, Type TKey) =>
            (s_DistinctUntilChanged__TSource_TKey__2__0 ??
            (s_DistinctUntilChanged__TSource_TKey__2__0 = new Func<IQueryable<TSource>, Expression<Func<TSource, TKey>>, IQueryable<TSource>>(DistinctUntilChanged<TSource, TKey>).GetMethodInfo().GetGenericMethodDefinition())).MakeGenericMethod(TSource, TKey);

        public static IQueryable<TSource> DistinctUntilChanged<TSource, TKey>(this IQueryable<TSource> source, Expression<Func<TSource, TKey>> keySelector)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));
            if (keySelector == null)
                throw new ArgumentNullException(nameof(keySelector));

            return source.Provider.CreateQuery<TSource>(Expression.Call(DistinctUntilChanged__TSource_TKey__2__0(typeof(TSource), typeof(TKey)), source.Expression, keySelector));
        }

#pragma warning disable 1591
        [EditorBrowsable(EditorBrowsableState.Never)]
        public static IEnumerable<TSource> DistinctUntilChanged<TSource, TKey>(IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
        {
#if REFERENCE_ASSEMBLY
            return default;
#else
            return EnumerableEx.DistinctUntilChanged<TSource, TKey>(source, keySelector);
#endif
        }
#pragma warning restore 1591

        private static MethodInfo s_DistinctUntilChanged__TSource_TKey__3__0;
        
        private static MethodInfo DistinctUntilChanged__TSource_TKey__3__0(Type TSource, Type TKey) =>
            (s_DistinctUntilChanged__TSource_TKey__3__0 ??
            (s_DistinctUntilChanged__TSource_TKey__3__0 = new Func<IQueryable<TSource>, Expression<Func<TSource, TKey>>, IEqualityComparer<TKey>, IQueryable<TSource>>(DistinctUntilChanged<TSource, TKey>).GetMethodInfo().GetGenericMethodDefinition())).MakeGenericMethod(TSource, TKey);

        public static IQueryable<TSource> DistinctUntilChanged<TSource, TKey>(this IQueryable<TSource> source, Expression<Func<TSource, TKey>> keySelector, IEqualityComparer<TKey> comparer)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));
            if (keySelector == null)
                throw new ArgumentNullException(nameof(keySelector));
            if (comparer == null)
                throw new ArgumentNullException(nameof(comparer));

            return source.Provider.CreateQuery<TSource>(Expression.Call(DistinctUntilChanged__TSource_TKey__3__0(typeof(TSource), typeof(TKey)), source.Expression, keySelector, Expression.Constant(comparer, typeof(IEqualityComparer<TKey>))));
        }

#pragma warning disable 1591
        [EditorBrowsable(EditorBrowsableState.Never)]
        public static IEnumerable<TSource> DistinctUntilChanged<TSource, TKey>(IEnumerable<TSource> source, Func<TSource, TKey> keySelector, IEqualityComparer<TKey> comparer)
        {
#if REFERENCE_ASSEMBLY
            return default;
#else
            return EnumerableEx.DistinctUntilChanged<TSource, TKey>(source, keySelector, comparer);
#endif
        }
#pragma warning restore 1591

        private static MethodInfo s_Do__TSource__2__0;
        
        private static MethodInfo Do__TSource__2__0(Type TSource) =>
            (s_Do__TSource__2__0 ??
            (s_Do__TSource__2__0 = new Func<IQueryable<TSource>, Expression<Action<TSource>>, IQueryable<TSource>>(Do<TSource>).GetMethodInfo().GetGenericMethodDefinition())).MakeGenericMethod(TSource);

        public static IQueryable<TSource> Do<TSource>(this IQueryable<TSource> source, Expression<Action<TSource>> onNext)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));
            if (onNext == null)
                throw new ArgumentNullException(nameof(onNext));

            return source.Provider.CreateQuery<TSource>(Expression.Call(Do__TSource__2__0(typeof(TSource)), source.Expression, onNext));
        }

#pragma warning disable 1591
        [EditorBrowsable(EditorBrowsableState.Never)]
        public static IEnumerable<TSource> Do<TSource>(IEnumerable<TSource> source, Action<TSource> onNext)
        {
#if REFERENCE_ASSEMBLY
            return default;
#else
            return EnumerableEx.Do<TSource>(source, onNext);
#endif
        }
#pragma warning restore 1591

        private static MethodInfo s_Do__TSource__2__1;
        
        private static MethodInfo Do__TSource__2__1(Type TSource) =>
            (s_Do__TSource__2__1 ??
            (s_Do__TSource__2__1 = new Func<IQueryable<TSource>, IObserver<TSource>, IQueryable<TSource>>(Do<TSource>).GetMethodInfo().GetGenericMethodDefinition())).MakeGenericMethod(TSource);

        public static IQueryable<TSource> Do<TSource>(this IQueryable<TSource> source, IObserver<TSource> observer)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));
            if (observer == null)
                throw new ArgumentNullException(nameof(observer));

            return source.Provider.CreateQuery<TSource>(Expression.Call(Do__TSource__2__1(typeof(TSource)), source.Expression, Expression.Constant(observer, typeof(IObserver<TSource>))));
        }

#pragma warning disable 1591
        [EditorBrowsable(EditorBrowsableState.Never)]
        public static IEnumerable<TSource> Do<TSource>(IEnumerable<TSource> source, IObserver<TSource> observer)
        {
#if REFERENCE_ASSEMBLY
            return default;
#else
            return EnumerableEx.Do<TSource>(source, observer);
#endif
        }
#pragma warning restore 1591

        private static MethodInfo s_Do__TSource__3__0;
        
        private static MethodInfo Do__TSource__3__0(Type TSource) =>
            (s_Do__TSource__3__0 ??
            (s_Do__TSource__3__0 = new Func<IQueryable<TSource>, Expression<Action<TSource>>, Action, IQueryable<TSource>>(Do<TSource>).GetMethodInfo().GetGenericMethodDefinition())).MakeGenericMethod(TSource);

        public static IQueryable<TSource> Do<TSource>(this IQueryable<TSource> source, Expression<Action<TSource>> onNext, Action onCompleted)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));
            if (onNext == null)
                throw new ArgumentNullException(nameof(onNext));
            if (onCompleted == null)
                throw new ArgumentNullException(nameof(onCompleted));

            return source.Provider.CreateQuery<TSource>(Expression.Call(Do__TSource__3__0(typeof(TSource)), source.Expression, onNext, Expression.Constant(onCompleted, typeof(Action))));
        }

#pragma warning disable 1591
        [EditorBrowsable(EditorBrowsableState.Never)]
        public static IEnumerable<TSource> Do<TSource>(IEnumerable<TSource> source, Action<TSource> onNext, Action onCompleted)
        {
#if REFERENCE_ASSEMBLY
            return default;
#else
            return EnumerableEx.Do<TSource>(source, onNext, onCompleted);
#endif
        }
#pragma warning restore 1591

        private static MethodInfo s_Do__TSource__3__1;
        
        private static MethodInfo Do__TSource__3__1(Type TSource) =>
            (s_Do__TSource__3__1 ??
            (s_Do__TSource__3__1 = new Func<IQueryable<TSource>, Expression<Action<TSource>>, Expression<Action<Exception>>, IQueryable<TSource>>(Do<TSource>).GetMethodInfo().GetGenericMethodDefinition())).MakeGenericMethod(TSource);

        public static IQueryable<TSource> Do<TSource>(this IQueryable<TSource> source, Expression<Action<TSource>> onNext, Expression<Action<Exception>> onError)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));
            if (onNext == null)
                throw new ArgumentNullException(nameof(onNext));
            if (onError == null)
                throw new ArgumentNullException(nameof(onError));

            return source.Provider.CreateQuery<TSource>(Expression.Call(Do__TSource__3__1(typeof(TSource)), source.Expression, onNext, onError));
        }

#pragma warning disable 1591
        [EditorBrowsable(EditorBrowsableState.Never)]
        public static IEnumerable<TSource> Do<TSource>(IEnumerable<TSource> source, Action<TSource> onNext, Action<Exception> onError)
        {
#if REFERENCE_ASSEMBLY
            return default;
#else
            return EnumerableEx.Do<TSource>(source, onNext, onError);
#endif
        }
#pragma warning restore 1591

        private static MethodInfo s_Do__TSource__4__0;
        
        private static MethodInfo Do__TSource__4__0(Type TSource) =>
            (s_Do__TSource__4__0 ??
            (s_Do__TSource__4__0 = new Func<IQueryable<TSource>, Expression<Action<TSource>>, Expression<Action<Exception>>, Action, IQueryable<TSource>>(Do<TSource>).GetMethodInfo().GetGenericMethodDefinition())).MakeGenericMethod(TSource);

        public static IQueryable<TSource> Do<TSource>(this IQueryable<TSource> source, Expression<Action<TSource>> onNext, Expression<Action<Exception>> onError, Action onCompleted)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));
            if (onNext == null)
                throw new ArgumentNullException(nameof(onNext));
            if (onError == null)
                throw new ArgumentNullException(nameof(onError));
            if (onCompleted == null)
                throw new ArgumentNullException(nameof(onCompleted));

            return source.Provider.CreateQuery<TSource>(Expression.Call(Do__TSource__4__0(typeof(TSource)), source.Expression, onNext, onError, Expression.Constant(onCompleted, typeof(Action))));
        }

#pragma warning disable 1591
        [EditorBrowsable(EditorBrowsableState.Never)]
        public static IEnumerable<TSource> Do<TSource>(IEnumerable<TSource> source, Action<TSource> onNext, Action<Exception> onError, Action onCompleted)
        {
#if REFERENCE_ASSEMBLY
            return default;
#else
            return EnumerableEx.Do<TSource>(source, onNext, onError, onCompleted);
#endif
        }
#pragma warning restore 1591

        private static MethodInfo s_DoWhile__TResult__2__0;
        
        private static MethodInfo DoWhile__TResult__2__0(Type TResult) =>
            (s_DoWhile__TResult__2__0 ??
            (s_DoWhile__TResult__2__0 = new Func<IQueryable<TResult>, Expression<Func<bool>>, IQueryable<TResult>>(DoWhile<TResult>).GetMethodInfo().GetGenericMethodDefinition())).MakeGenericMethod(TResult);

        public static IQueryable<TResult> DoWhile<TResult>(this IQueryable<TResult> source, Expression<Func<bool>> condition)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));
            if (condition == null)
                throw new ArgumentNullException(nameof(condition));

            return source.Provider.CreateQuery<TResult>(Expression.Call(DoWhile__TResult__2__0(typeof(TResult)), source.Expression, condition));
        }

#pragma warning disable 1591
        [EditorBrowsable(EditorBrowsableState.Never)]
        public static IEnumerable<TResult> DoWhile<TResult>(IEnumerable<TResult> source, Func<bool> condition)
        {
#if REFERENCE_ASSEMBLY
            return default;
#else
            return EnumerableEx.DoWhile<TResult>(source, condition);
#endif
        }
#pragma warning restore 1591

        private static MethodInfo s_Expand__TSource__2__0;
        
        private static MethodInfo Expand__TSource__2__0(Type TSource) =>
            (s_Expand__TSource__2__0 ??
            (s_Expand__TSource__2__0 = new Func<IQueryable<TSource>, Expression<Func<TSource, IEnumerable<TSource>>>, IQueryable<TSource>>(Expand<TSource>).GetMethodInfo().GetGenericMethodDefinition())).MakeGenericMethod(TSource);

        public static IQueryable<TSource> Expand<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, IEnumerable<TSource>>> selector)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));
            if (selector == null)
                throw new ArgumentNullException(nameof(selector));

            return source.Provider.CreateQuery<TSource>(Expression.Call(Expand__TSource__2__0(typeof(TSource)), source.Expression, selector));
        }

#pragma warning disable 1591
        [EditorBrowsable(EditorBrowsableState.Never)]
        public static IEnumerable<TSource> Expand<TSource>(IEnumerable<TSource> source, Func<TSource, IEnumerable<TSource>> selector)
        {
#if REFERENCE_ASSEMBLY
            return default;
#else
            return EnumerableEx.Expand<TSource>(source, selector);
#endif
        }
#pragma warning restore 1591

        private static MethodInfo s_Finally__TSource__2__0;
        
        private static MethodInfo Finally__TSource__2__0(Type TSource) =>
            (s_Finally__TSource__2__0 ??
            (s_Finally__TSource__2__0 = new Func<IQueryable<TSource>, Action, IQueryable<TSource>>(Finally<TSource>).GetMethodInfo().GetGenericMethodDefinition())).MakeGenericMethod(TSource);

        public static IQueryable<TSource> Finally<TSource>(this IQueryable<TSource> source, Action finallyAction)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));
            if (finallyAction == null)
                throw new ArgumentNullException(nameof(finallyAction));

            return source.Provider.CreateQuery<TSource>(Expression.Call(Finally__TSource__2__0(typeof(TSource)), source.Expression, Expression.Constant(finallyAction, typeof(Action))));
        }

#pragma warning disable 1591
        [EditorBrowsable(EditorBrowsableState.Never)]
        public static IEnumerable<TSource> Finally<TSource>(IEnumerable<TSource> source, Action finallyAction)
        {
#if REFERENCE_ASSEMBLY
            return default;
#else
            return EnumerableEx.Finally<TSource>(source, finallyAction);
#endif
        }
#pragma warning restore 1591

        private static MethodInfo s_Hide__TSource__1__0;
        
        private static MethodInfo Hide__TSource__1__0(Type TSource) =>
            (s_Hide__TSource__1__0 ??
            (s_Hide__TSource__1__0 = new Func<IQueryable<TSource>, IQueryable<TSource>>(Hide<TSource>).GetMethodInfo().GetGenericMethodDefinition())).MakeGenericMethod(TSource);

        public static IQueryable<TSource> Hide<TSource>(this IQueryable<TSource> source)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            return source.Provider.CreateQuery<TSource>(Expression.Call(Hide__TSource__1__0(typeof(TSource)), source.Expression));
        }

#pragma warning disable 1591
        [EditorBrowsable(EditorBrowsableState.Never)]
        public static IEnumerable<TSource> Hide<TSource>(IEnumerable<TSource> source)
        {
#if REFERENCE_ASSEMBLY
            return default;
#else
            return EnumerableEx.Hide<TSource>(source);
#endif
        }
#pragma warning restore 1591

        private static MethodInfo s_IgnoreElements__TSource__1__0;
        
        private static MethodInfo IgnoreElements__TSource__1__0(Type TSource) =>
            (s_IgnoreElements__TSource__1__0 ??
            (s_IgnoreElements__TSource__1__0 = new Func<IQueryable<TSource>, IQueryable<TSource>>(IgnoreElements<TSource>).GetMethodInfo().GetGenericMethodDefinition())).MakeGenericMethod(TSource);

        public static IQueryable<TSource> IgnoreElements<TSource>(this IQueryable<TSource> source)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            return source.Provider.CreateQuery<TSource>(Expression.Call(IgnoreElements__TSource__1__0(typeof(TSource)), source.Expression));
        }

#pragma warning disable 1591
        [EditorBrowsable(EditorBrowsableState.Never)]
        public static IEnumerable<TSource> IgnoreElements<TSource>(IEnumerable<TSource> source)
        {
#if REFERENCE_ASSEMBLY
            return default;
#else
            return EnumerableEx.IgnoreElements<TSource>(source);
#endif
        }
#pragma warning restore 1591

        private static MethodInfo s_IsEmpty__TSource__1__0;
        
        private static MethodInfo IsEmpty__TSource__1__0(Type TSource) =>
            (s_IsEmpty__TSource__1__0 ??
            (s_IsEmpty__TSource__1__0 = new Func<IQueryable<TSource>, bool>(IsEmpty<TSource>).GetMethodInfo().GetGenericMethodDefinition())).MakeGenericMethod(TSource);

        public static bool IsEmpty<TSource>(this IQueryable<TSource> source)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            return source.Provider.Execute<bool>(Expression.Call(IsEmpty__TSource__1__0(typeof(TSource)), source.Expression));
        }

#pragma warning disable 1591
        [EditorBrowsable(EditorBrowsableState.Never)]
        public static bool IsEmpty<TSource>(IEnumerable<TSource> source)
        {
#if REFERENCE_ASSEMBLY
            return default;
#else
            return EnumerableEx.IsEmpty<TSource>(source);
#endif
        }
#pragma warning restore 1591

        private static MethodInfo s_Max__TSource__2__0;
        
        private static MethodInfo Max__TSource__2__0(Type TSource) =>
            (s_Max__TSource__2__0 ??
            (s_Max__TSource__2__0 = new Func<IQueryable<TSource>, IComparer<TSource>, TSource>(Max<TSource>).GetMethodInfo().GetGenericMethodDefinition())).MakeGenericMethod(TSource);

        public static TSource Max<TSource>(this IQueryable<TSource> source, IComparer<TSource> comparer)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));
            if (comparer == null)
                throw new ArgumentNullException(nameof(comparer));

            return source.Provider.Execute<TSource>(Expression.Call(Max__TSource__2__0(typeof(TSource)), source.Expression, Expression.Constant(comparer, typeof(IComparer<TSource>))));
        }

#pragma warning disable 1591
        [EditorBrowsable(EditorBrowsableState.Never)]
        public static TSource Max<TSource>(IEnumerable<TSource> source, IComparer<TSource> comparer)
        {
#if REFERENCE_ASSEMBLY
            return default;
#else
            return EnumerableEx.Max<TSource>(source, comparer);
#endif
        }
#pragma warning restore 1591

        private static MethodInfo s_MaxBy__TSource_TKey__2__0;
        
        private static MethodInfo MaxBy__TSource_TKey__2__0(Type TSource, Type TKey) =>
            (s_MaxBy__TSource_TKey__2__0 ??
            (s_MaxBy__TSource_TKey__2__0 = new Func<IQueryable<TSource>, Expression<Func<TSource, TKey>>, IList<TSource>>(MaxBy<TSource, TKey>).GetMethodInfo().GetGenericMethodDefinition())).MakeGenericMethod(TSource, TKey);

        public static IList<TSource> MaxBy<TSource, TKey>(this IQueryable<TSource> source, Expression<Func<TSource, TKey>> keySelector)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));
            if (keySelector == null)
                throw new ArgumentNullException(nameof(keySelector));

            return source.Provider.Execute<IList<TSource>>(Expression.Call(MaxBy__TSource_TKey__2__0(typeof(TSource), typeof(TKey)), source.Expression, keySelector));
        }

#pragma warning disable 1591
        [EditorBrowsable(EditorBrowsableState.Never)]
        public static IList<TSource> MaxBy<TSource, TKey>(IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
        {
#if REFERENCE_ASSEMBLY
            return default;
#else
            return EnumerableEx.MaxBy<TSource, TKey>(source, keySelector);
#endif
        }
#pragma warning restore 1591

        private static MethodInfo s_MaxBy__TSource_TKey__3__0;
        
        private static MethodInfo MaxBy__TSource_TKey__3__0(Type TSource, Type TKey) =>
            (s_MaxBy__TSource_TKey__3__0 ??
            (s_MaxBy__TSource_TKey__3__0 = new Func<IQueryable<TSource>, Expression<Func<TSource, TKey>>, IComparer<TKey>, IList<TSource>>(MaxBy<TSource, TKey>).GetMethodInfo().GetGenericMethodDefinition())).MakeGenericMethod(TSource, TKey);

        public static IList<TSource> MaxBy<TSource, TKey>(this IQueryable<TSource> source, Expression<Func<TSource, TKey>> keySelector, IComparer<TKey> comparer)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));
            if (keySelector == null)
                throw new ArgumentNullException(nameof(keySelector));
            if (comparer == null)
                throw new ArgumentNullException(nameof(comparer));

            return source.Provider.Execute<IList<TSource>>(Expression.Call(MaxBy__TSource_TKey__3__0(typeof(TSource), typeof(TKey)), source.Expression, keySelector, Expression.Constant(comparer, typeof(IComparer<TKey>))));
        }

#pragma warning disable 1591
        [EditorBrowsable(EditorBrowsableState.Never)]
        public static IList<TSource> MaxBy<TSource, TKey>(IEnumerable<TSource> source, Func<TSource, TKey> keySelector, IComparer<TKey> comparer)
        {
#if REFERENCE_ASSEMBLY
            return default;
#else
            return EnumerableEx.MaxBy<TSource, TKey>(source, keySelector, comparer);
#endif
        }
#pragma warning restore 1591

        private static MethodInfo s_Memoize__TSource_TResult__2__0;
        
        private static MethodInfo Memoize__TSource_TResult__2__0(Type TSource, Type TResult) =>
            (s_Memoize__TSource_TResult__2__0 ??
            (s_Memoize__TSource_TResult__2__0 = new Func<IQueryable<TSource>, Expression<Func<IEnumerable<TSource>, IEnumerable<TResult>>>, IQueryable<TResult>>(Memoize<TSource, TResult>).GetMethodInfo().GetGenericMethodDefinition())).MakeGenericMethod(TSource, TResult);

        public static IQueryable<TResult> Memoize<TSource, TResult>(this IQueryable<TSource> source, Expression<Func<IEnumerable<TSource>, IEnumerable<TResult>>> selector)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));
            if (selector == null)
                throw new ArgumentNullException(nameof(selector));

            return source.Provider.CreateQuery<TResult>(Expression.Call(Memoize__TSource_TResult__2__0(typeof(TSource), typeof(TResult)), source.Expression, selector));
        }

#pragma warning disable 1591
        [EditorBrowsable(EditorBrowsableState.Never)]
        public static IEnumerable<TResult> Memoize<TSource, TResult>(IEnumerable<TSource> source, Func<IEnumerable<TSource>, IEnumerable<TResult>> selector)
        {
#if REFERENCE_ASSEMBLY
            return default;
#else
            return EnumerableEx.Memoize<TSource, TResult>(source, selector);
#endif
        }
#pragma warning restore 1591

        private static MethodInfo s_Memoize__TSource_TResult__3__0;
        
        private static MethodInfo Memoize__TSource_TResult__3__0(Type TSource, Type TResult) =>
            (s_Memoize__TSource_TResult__3__0 ??
            (s_Memoize__TSource_TResult__3__0 = new Func<IQueryable<TSource>, int, Expression<Func<IEnumerable<TSource>, IEnumerable<TResult>>>, IQueryable<TResult>>(Memoize<TSource, TResult>).GetMethodInfo().GetGenericMethodDefinition())).MakeGenericMethod(TSource, TResult);

        public static IQueryable<TResult> Memoize<TSource, TResult>(this IQueryable<TSource> source, int readerCount, Expression<Func<IEnumerable<TSource>, IEnumerable<TResult>>> selector)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));
            if (selector == null)
                throw new ArgumentNullException(nameof(selector));

            return source.Provider.CreateQuery<TResult>(Expression.Call(Memoize__TSource_TResult__3__0(typeof(TSource), typeof(TResult)), source.Expression, Expression.Constant(readerCount, typeof(int)), selector));
        }

#pragma warning disable 1591
        [EditorBrowsable(EditorBrowsableState.Never)]
        public static IEnumerable<TResult> Memoize<TSource, TResult>(IEnumerable<TSource> source, int readerCount, Func<IEnumerable<TSource>, IEnumerable<TResult>> selector)
        {
#if REFERENCE_ASSEMBLY
            return default;
#else
            return EnumerableEx.Memoize<TSource, TResult>(source, readerCount, selector);
#endif
        }
#pragma warning restore 1591

        private static MethodInfo s_Min__TSource__2__0;
        
        private static MethodInfo Min__TSource__2__0(Type TSource) =>
            (s_Min__TSource__2__0 ??
            (s_Min__TSource__2__0 = new Func<IQueryable<TSource>, IComparer<TSource>, TSource>(Min<TSource>).GetMethodInfo().GetGenericMethodDefinition())).MakeGenericMethod(TSource);

        public static TSource Min<TSource>(this IQueryable<TSource> source, IComparer<TSource> comparer)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));
            if (comparer == null)
                throw new ArgumentNullException(nameof(comparer));

            return source.Provider.Execute<TSource>(Expression.Call(Min__TSource__2__0(typeof(TSource)), source.Expression, Expression.Constant(comparer, typeof(IComparer<TSource>))));
        }

#pragma warning disable 1591
        [EditorBrowsable(EditorBrowsableState.Never)]
        public static TSource Min<TSource>(IEnumerable<TSource> source, IComparer<TSource> comparer)
        {
#if REFERENCE_ASSEMBLY
            return default;
#else
            return EnumerableEx.Min<TSource>(source, comparer);
#endif
        }
#pragma warning restore 1591

        private static MethodInfo s_MinBy__TSource_TKey__2__0;
        
        private static MethodInfo MinBy__TSource_TKey__2__0(Type TSource, Type TKey) =>
            (s_MinBy__TSource_TKey__2__0 ??
            (s_MinBy__TSource_TKey__2__0 = new Func<IQueryable<TSource>, Expression<Func<TSource, TKey>>, IList<TSource>>(MinBy<TSource, TKey>).GetMethodInfo().GetGenericMethodDefinition())).MakeGenericMethod(TSource, TKey);

        public static IList<TSource> MinBy<TSource, TKey>(this IQueryable<TSource> source, Expression<Func<TSource, TKey>> keySelector)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));
            if (keySelector == null)
                throw new ArgumentNullException(nameof(keySelector));

            return source.Provider.Execute<IList<TSource>>(Expression.Call(MinBy__TSource_TKey__2__0(typeof(TSource), typeof(TKey)), source.Expression, keySelector));
        }

#pragma warning disable 1591
        [EditorBrowsable(EditorBrowsableState.Never)]
        public static IList<TSource> MinBy<TSource, TKey>(IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
        {
#if REFERENCE_ASSEMBLY
            return default;
#else
            return EnumerableEx.MinBy<TSource, TKey>(source, keySelector);
#endif
        }
#pragma warning restore 1591

        private static MethodInfo s_MinBy__TSource_TKey__3__0;
        
        private static MethodInfo MinBy__TSource_TKey__3__0(Type TSource, Type TKey) =>
            (s_MinBy__TSource_TKey__3__0 ??
            (s_MinBy__TSource_TKey__3__0 = new Func<IQueryable<TSource>, Expression<Func<TSource, TKey>>, IComparer<TKey>, IList<TSource>>(MinBy<TSource, TKey>).GetMethodInfo().GetGenericMethodDefinition())).MakeGenericMethod(TSource, TKey);

        public static IList<TSource> MinBy<TSource, TKey>(this IQueryable<TSource> source, Expression<Func<TSource, TKey>> keySelector, IComparer<TKey> comparer)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));
            if (keySelector == null)
                throw new ArgumentNullException(nameof(keySelector));
            if (comparer == null)
                throw new ArgumentNullException(nameof(comparer));

            return source.Provider.Execute<IList<TSource>>(Expression.Call(MinBy__TSource_TKey__3__0(typeof(TSource), typeof(TKey)), source.Expression, keySelector, Expression.Constant(comparer, typeof(IComparer<TKey>))));
        }

#pragma warning disable 1591
        [EditorBrowsable(EditorBrowsableState.Never)]
        public static IList<TSource> MinBy<TSource, TKey>(IEnumerable<TSource> source, Func<TSource, TKey> keySelector, IComparer<TKey> comparer)
        {
#if REFERENCE_ASSEMBLY
            return default;
#else
            return EnumerableEx.MinBy<TSource, TKey>(source, keySelector, comparer);
#endif
        }
#pragma warning restore 1591

        private static MethodInfo s_OnErrorResumeNext__TSource__1__0;
        
        private static MethodInfo OnErrorResumeNext__TSource__1__0(Type TSource) =>
            (s_OnErrorResumeNext__TSource__1__0 ??
            (s_OnErrorResumeNext__TSource__1__0 = new Func<IQueryable<IEnumerable<TSource>>, IQueryable<TSource>>(OnErrorResumeNext<TSource>).GetMethodInfo().GetGenericMethodDefinition())).MakeGenericMethod(TSource);

        public static IQueryable<TSource> OnErrorResumeNext<TSource>(this IQueryable<IEnumerable<TSource>> sources)
        {
            if (sources == null)
                throw new ArgumentNullException(nameof(sources));

            return sources.Provider.CreateQuery<TSource>(Expression.Call(OnErrorResumeNext__TSource__1__0(typeof(TSource)), sources.Expression));
        }

#pragma warning disable 1591
        [EditorBrowsable(EditorBrowsableState.Never)]
        public static IEnumerable<TSource> OnErrorResumeNext<TSource>(IEnumerable<IEnumerable<TSource>> sources)
        {
#if REFERENCE_ASSEMBLY
            return default;
#else
            return EnumerableEx.OnErrorResumeNext<TSource>(sources);
#endif
        }
#pragma warning restore 1591

        private static MethodInfo s_OnErrorResumeNext__TSource__2__0;
        
        private static MethodInfo OnErrorResumeNext__TSource__2__0(Type TSource) =>
            (s_OnErrorResumeNext__TSource__2__0 ??
            (s_OnErrorResumeNext__TSource__2__0 = new Func<IQueryable<TSource>, IEnumerable<TSource>, IQueryable<TSource>>(OnErrorResumeNext<TSource>).GetMethodInfo().GetGenericMethodDefinition())).MakeGenericMethod(TSource);

        public static IQueryable<TSource> OnErrorResumeNext<TSource>(this IQueryable<TSource> first, IEnumerable<TSource> second)
        {
            if (first == null)
                throw new ArgumentNullException(nameof(first));
            if (second == null)
                throw new ArgumentNullException(nameof(second));

            return first.Provider.CreateQuery<TSource>(Expression.Call(OnErrorResumeNext__TSource__2__0(typeof(TSource)), first.Expression, GetSourceExpression(second)));
        }

#pragma warning disable 1591
        [EditorBrowsable(EditorBrowsableState.Never)]
        public static IEnumerable<TSource> OnErrorResumeNext<TSource>(IEnumerable<TSource> first, IEnumerable<TSource> second)
        {
#if REFERENCE_ASSEMBLY
            return default;
#else
            return EnumerableEx.OnErrorResumeNext<TSource>(first, second);
#endif
        }
#pragma warning restore 1591

        private static MethodInfo s_Publish__TSource_TResult__2__0;
        
        private static MethodInfo Publish__TSource_TResult__2__0(Type TSource, Type TResult) =>
            (s_Publish__TSource_TResult__2__0 ??
            (s_Publish__TSource_TResult__2__0 = new Func<IQueryable<TSource>, Expression<Func<IEnumerable<TSource>, IEnumerable<TResult>>>, IQueryable<TResult>>(Publish<TSource, TResult>).GetMethodInfo().GetGenericMethodDefinition())).MakeGenericMethod(TSource, TResult);

        public static IQueryable<TResult> Publish<TSource, TResult>(this IQueryable<TSource> source, Expression<Func<IEnumerable<TSource>, IEnumerable<TResult>>> selector)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));
            if (selector == null)
                throw new ArgumentNullException(nameof(selector));

            return source.Provider.CreateQuery<TResult>(Expression.Call(Publish__TSource_TResult__2__0(typeof(TSource), typeof(TResult)), source.Expression, selector));
        }

#pragma warning disable 1591
        [EditorBrowsable(EditorBrowsableState.Never)]
        public static IEnumerable<TResult> Publish<TSource, TResult>(IEnumerable<TSource> source, Func<IEnumerable<TSource>, IEnumerable<TResult>> selector)
        {
#if REFERENCE_ASSEMBLY
            return default;
#else
            return EnumerableEx.Publish<TSource, TResult>(source, selector);
#endif
        }
#pragma warning restore 1591

        private static MethodInfo s_Repeat__TSource__1__0;
        
        private static MethodInfo Repeat__TSource__1__0(Type TSource) =>
            (s_Repeat__TSource__1__0 ??
            (s_Repeat__TSource__1__0 = new Func<IQueryable<TSource>, IQueryable<TSource>>(Repeat<TSource>).GetMethodInfo().GetGenericMethodDefinition())).MakeGenericMethod(TSource);

        public static IQueryable<TSource> Repeat<TSource>(this IQueryable<TSource> source)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            return source.Provider.CreateQuery<TSource>(Expression.Call(Repeat__TSource__1__0(typeof(TSource)), source.Expression));
        }

#pragma warning disable 1591
        [EditorBrowsable(EditorBrowsableState.Never)]
        public static IEnumerable<TSource> Repeat<TSource>(IEnumerable<TSource> source)
        {
#if REFERENCE_ASSEMBLY
            return default;
#else
            return EnumerableEx.Repeat<TSource>(source);
#endif
        }
#pragma warning restore 1591

        private static MethodInfo s_Repeat__TSource__2__0;
        
        private static MethodInfo Repeat__TSource__2__0(Type TSource) =>
            (s_Repeat__TSource__2__0 ??
            (s_Repeat__TSource__2__0 = new Func<IQueryable<TSource>, int, IQueryable<TSource>>(Repeat<TSource>).GetMethodInfo().GetGenericMethodDefinition())).MakeGenericMethod(TSource);

        public static IQueryable<TSource> Repeat<TSource>(this IQueryable<TSource> source, int count)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            return source.Provider.CreateQuery<TSource>(Expression.Call(Repeat__TSource__2__0(typeof(TSource)), source.Expression, Expression.Constant(count, typeof(int))));
        }

#pragma warning disable 1591
        [EditorBrowsable(EditorBrowsableState.Never)]
        public static IEnumerable<TSource> Repeat<TSource>(IEnumerable<TSource> source, int count)
        {
#if REFERENCE_ASSEMBLY
            return default;
#else
            return EnumerableEx.Repeat<TSource>(source, count);
#endif
        }
#pragma warning restore 1591

        private static MethodInfo s_Retry__TSource__1__0;
        
        private static MethodInfo Retry__TSource__1__0(Type TSource) =>
            (s_Retry__TSource__1__0 ??
            (s_Retry__TSource__1__0 = new Func<IQueryable<TSource>, IQueryable<TSource>>(Retry<TSource>).GetMethodInfo().GetGenericMethodDefinition())).MakeGenericMethod(TSource);

        public static IQueryable<TSource> Retry<TSource>(this IQueryable<TSource> source)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            return source.Provider.CreateQuery<TSource>(Expression.Call(Retry__TSource__1__0(typeof(TSource)), source.Expression));
        }

#pragma warning disable 1591
        [EditorBrowsable(EditorBrowsableState.Never)]
        public static IEnumerable<TSource> Retry<TSource>(IEnumerable<TSource> source)
        {
#if REFERENCE_ASSEMBLY
            return default;
#else
            return EnumerableEx.Retry<TSource>(source);
#endif
        }
#pragma warning restore 1591

        private static MethodInfo s_Retry__TSource__2__0;
        
        private static MethodInfo Retry__TSource__2__0(Type TSource) =>
            (s_Retry__TSource__2__0 ??
            (s_Retry__TSource__2__0 = new Func<IQueryable<TSource>, int, IQueryable<TSource>>(Retry<TSource>).GetMethodInfo().GetGenericMethodDefinition())).MakeGenericMethod(TSource);

        public static IQueryable<TSource> Retry<TSource>(this IQueryable<TSource> source, int retryCount)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            return source.Provider.CreateQuery<TSource>(Expression.Call(Retry__TSource__2__0(typeof(TSource)), source.Expression, Expression.Constant(retryCount, typeof(int))));
        }

#pragma warning disable 1591
        [EditorBrowsable(EditorBrowsableState.Never)]
        public static IEnumerable<TSource> Retry<TSource>(IEnumerable<TSource> source, int retryCount)
        {
#if REFERENCE_ASSEMBLY
            return default;
#else
            return EnumerableEx.Retry<TSource>(source, retryCount);
#endif
        }
#pragma warning restore 1591

        private static MethodInfo s_Scan__TSource__2__0;
        
        private static MethodInfo Scan__TSource__2__0(Type TSource) =>
            (s_Scan__TSource__2__0 ??
            (s_Scan__TSource__2__0 = new Func<IQueryable<TSource>, Expression<Func<TSource, TSource, TSource>>, IQueryable<TSource>>(Scan<TSource>).GetMethodInfo().GetGenericMethodDefinition())).MakeGenericMethod(TSource);

        public static IQueryable<TSource> Scan<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, TSource, TSource>> accumulator)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));
            if (accumulator == null)
                throw new ArgumentNullException(nameof(accumulator));

            return source.Provider.CreateQuery<TSource>(Expression.Call(Scan__TSource__2__0(typeof(TSource)), source.Expression, accumulator));
        }

#pragma warning disable 1591
        [EditorBrowsable(EditorBrowsableState.Never)]
        public static IEnumerable<TSource> Scan<TSource>(IEnumerable<TSource> source, Func<TSource, TSource, TSource> accumulator)
        {
#if REFERENCE_ASSEMBLY
            return default;
#else
            return EnumerableEx.Scan<TSource>(source, accumulator);
#endif
        }
#pragma warning restore 1591

        private static MethodInfo s_Scan__TSource_TAccumulate__3__0;
        
        private static MethodInfo Scan__TSource_TAccumulate__3__0(Type TSource, Type TAccumulate) =>
            (s_Scan__TSource_TAccumulate__3__0 ??
            (s_Scan__TSource_TAccumulate__3__0 = new Func<IQueryable<TSource>, TAccumulate, Expression<Func<TAccumulate, TSource, TAccumulate>>, IQueryable<TAccumulate>>(Scan<TSource, TAccumulate>).GetMethodInfo().GetGenericMethodDefinition())).MakeGenericMethod(TSource, TAccumulate);

        public static IQueryable<TAccumulate> Scan<TSource, TAccumulate>(this IQueryable<TSource> source, TAccumulate seed, Expression<Func<TAccumulate, TSource, TAccumulate>> accumulator)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));
            if (accumulator == null)
                throw new ArgumentNullException(nameof(accumulator));

            return source.Provider.CreateQuery<TAccumulate>(Expression.Call(Scan__TSource_TAccumulate__3__0(typeof(TSource), typeof(TAccumulate)), source.Expression, Expression.Constant(seed, typeof(TAccumulate)), accumulator));
        }

#pragma warning disable 1591
        [EditorBrowsable(EditorBrowsableState.Never)]
        public static IEnumerable<TAccumulate> Scan<TSource, TAccumulate>(IEnumerable<TSource> source, TAccumulate seed, Func<TAccumulate, TSource, TAccumulate> accumulator)
        {
#if REFERENCE_ASSEMBLY
            return default;
#else
            return EnumerableEx.Scan<TSource, TAccumulate>(source, seed, accumulator);
#endif
        }
#pragma warning restore 1591

        private static MethodInfo s_SelectMany__TSource_TOther__2__0;
        
        private static MethodInfo SelectMany__TSource_TOther__2__0(Type TSource, Type TOther) =>
            (s_SelectMany__TSource_TOther__2__0 ??
            (s_SelectMany__TSource_TOther__2__0 = new Func<IQueryable<TSource>, IEnumerable<TOther>, IQueryable<TOther>>(SelectMany<TSource, TOther>).GetMethodInfo().GetGenericMethodDefinition())).MakeGenericMethod(TSource, TOther);

        public static IQueryable<TOther> SelectMany<TSource, TOther>(this IQueryable<TSource> source, IEnumerable<TOther> other)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));
            if (other == null)
                throw new ArgumentNullException(nameof(other));

            return source.Provider.CreateQuery<TOther>(Expression.Call(SelectMany__TSource_TOther__2__0(typeof(TSource), typeof(TOther)), source.Expression, GetSourceExpression(other)));
        }

#pragma warning disable 1591
        [EditorBrowsable(EditorBrowsableState.Never)]
        public static IEnumerable<TOther> SelectMany<TSource, TOther>(IEnumerable<TSource> source, IEnumerable<TOther> other)
        {
#if REFERENCE_ASSEMBLY
            return default;
#else
            return EnumerableEx.SelectMany<TSource, TOther>(source, other);
#endif
        }
#pragma warning restore 1591

        private static MethodInfo s_Share__TSource_TResult__2__0;
        
        private static MethodInfo Share__TSource_TResult__2__0(Type TSource, Type TResult) =>
            (s_Share__TSource_TResult__2__0 ??
            (s_Share__TSource_TResult__2__0 = new Func<IQueryable<TSource>, Expression<Func<IEnumerable<TSource>, IEnumerable<TResult>>>, IQueryable<TResult>>(Share<TSource, TResult>).GetMethodInfo().GetGenericMethodDefinition())).MakeGenericMethod(TSource, TResult);

        public static IQueryable<TResult> Share<TSource, TResult>(this IQueryable<TSource> source, Expression<Func<IEnumerable<TSource>, IEnumerable<TResult>>> selector)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));
            if (selector == null)
                throw new ArgumentNullException(nameof(selector));

            return source.Provider.CreateQuery<TResult>(Expression.Call(Share__TSource_TResult__2__0(typeof(TSource), typeof(TResult)), source.Expression, selector));
        }

#pragma warning disable 1591
        [EditorBrowsable(EditorBrowsableState.Never)]
        public static IEnumerable<TResult> Share<TSource, TResult>(IEnumerable<TSource> source, Func<IEnumerable<TSource>, IEnumerable<TResult>> selector)
        {
#if REFERENCE_ASSEMBLY
            return default;
#else
            return EnumerableEx.Share<TSource, TResult>(source, selector);
#endif
        }
#pragma warning restore 1591

        private static MethodInfo s_SkipLast__TSource__2__0;
        
        private static MethodInfo SkipLast__TSource__2__0(Type TSource) =>
            (s_SkipLast__TSource__2__0 ??
            (s_SkipLast__TSource__2__0 = new Func<IQueryable<TSource>, int, IQueryable<TSource>>(SkipLast<TSource>).GetMethodInfo().GetGenericMethodDefinition())).MakeGenericMethod(TSource);

        public static IQueryable<TSource> SkipLast<TSource>(this IQueryable<TSource> source, int count)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            return source.Provider.CreateQuery<TSource>(Expression.Call(SkipLast__TSource__2__0(typeof(TSource)), source.Expression, Expression.Constant(count, typeof(int))));
        }

#pragma warning disable 1591
        [EditorBrowsable(EditorBrowsableState.Never)]
        public static IEnumerable<TSource> SkipLast<TSource>(IEnumerable<TSource> source, int count)
        {
#if REFERENCE_ASSEMBLY
            return default;
#else
            return EnumerableEx.SkipLast<TSource>(source, count);
#endif
        }
#pragma warning restore 1591

        private static MethodInfo s_StartWith__TSource__2__0;
        
        private static MethodInfo StartWith__TSource__2__0(Type TSource) =>
            (s_StartWith__TSource__2__0 ??
            (s_StartWith__TSource__2__0 = new Func<IQueryable<TSource>, TSource[], IQueryable<TSource>>(StartWith<TSource>).GetMethodInfo().GetGenericMethodDefinition())).MakeGenericMethod(TSource);

        public static IQueryable<TSource> StartWith<TSource>(this IQueryable<TSource> source, params TSource[] values)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));
            if (values == null)
                throw new ArgumentNullException(nameof(values));

            return source.Provider.CreateQuery<TSource>(Expression.Call(StartWith__TSource__2__0(typeof(TSource)), source.Expression, Expression.Constant(values, typeof(TSource[]))));
        }

#pragma warning disable 1591
        [EditorBrowsable(EditorBrowsableState.Never)]
        public static IEnumerable<TSource> StartWith<TSource>(IEnumerable<TSource> source, TSource[] values)
        {
#if REFERENCE_ASSEMBLY
            return default;
#else
            return EnumerableEx.StartWith<TSource>(source, values);
#endif
        }
#pragma warning restore 1591

        private static MethodInfo s_TakeLast__TSource__2__0;
        
        private static MethodInfo TakeLast__TSource__2__0(Type TSource) =>
            (s_TakeLast__TSource__2__0 ??
            (s_TakeLast__TSource__2__0 = new Func<IQueryable<TSource>, int, IQueryable<TSource>>(TakeLast<TSource>).GetMethodInfo().GetGenericMethodDefinition())).MakeGenericMethod(TSource);

        public static IQueryable<TSource> TakeLast<TSource>(this IQueryable<TSource> source, int count)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            return source.Provider.CreateQuery<TSource>(Expression.Call(TakeLast__TSource__2__0(typeof(TSource)), source.Expression, Expression.Constant(count, typeof(int))));
        }

#pragma warning disable 1591
        [EditorBrowsable(EditorBrowsableState.Never)]
        public static IEnumerable<TSource> TakeLast<TSource>(IEnumerable<TSource> source, int count)
        {
#if REFERENCE_ASSEMBLY
            return default;
#else
            return EnumerableEx.TakeLast<TSource>(source, count);
#endif
        }
#pragma warning restore 1591

    }
}

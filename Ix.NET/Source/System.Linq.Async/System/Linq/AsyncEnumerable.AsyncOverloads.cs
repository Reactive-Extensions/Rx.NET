// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT License.
// See the LICENSE file in the project root for more information. 

using System.Collections.Generic;
using System.Collections.Immutable;
using System.Threading;
using System.Threading.Tasks;

namespace System.Linq
{
    partial class AsyncEnumerable
    {
#if SUPPORT_FLAT_ASYNC_API
        public static ValueTask<TSource> AggregateAsync<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, TSource, ValueTask<TSource>> accumulator, CancellationToken cancellationToken = default) => AggregateAwaitAsyncCore<TSource>(source, accumulator, cancellationToken);
        public static ValueTask<TAccumulate> AggregateAsync<TSource, TAccumulate>(this IAsyncEnumerable<TSource> source, TAccumulate seed, Func<TAccumulate, TSource, ValueTask<TAccumulate>> accumulator, CancellationToken cancellationToken = default) => AggregateAwaitAsyncCore<TSource, TAccumulate>(source, seed, accumulator, cancellationToken);
        public static ValueTask<TResult> AggregateAsync<TSource, TAccumulate, TResult>(this IAsyncEnumerable<TSource> source, TAccumulate seed, Func<TAccumulate, TSource, ValueTask<TAccumulate>> accumulator, Func<TAccumulate, ValueTask<TResult>> resultSelector, CancellationToken cancellationToken = default) => AggregateAwaitAsyncCore<TSource, TAccumulate, TResult>(source, seed, accumulator, resultSelector, cancellationToken);
        public static ValueTask<bool> AllAsync<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, ValueTask<bool>> predicate, CancellationToken cancellationToken = default) => AllAwaitAsyncCore<TSource>(source, predicate, cancellationToken);
        public static ValueTask<bool> AnyAsync<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, ValueTask<bool>> predicate, CancellationToken cancellationToken = default) => AnyAwaitAsyncCore<TSource>(source, predicate, cancellationToken);
        public static ValueTask<double> AverageAsync<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, ValueTask<int>> selector, CancellationToken cancellationToken = default) => AverageAwaitAsyncCore<TSource>(source, selector, cancellationToken);
        public static ValueTask<double> AverageAsync<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, ValueTask<long>> selector, CancellationToken cancellationToken = default) => AverageAwaitAsyncCore<TSource>(source, selector, cancellationToken);
        public static ValueTask<float> AverageAsync<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, ValueTask<float>> selector, CancellationToken cancellationToken = default) => AverageAwaitAsyncCore<TSource>(source, selector, cancellationToken);
        public static ValueTask<double> AverageAsync<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, ValueTask<double>> selector, CancellationToken cancellationToken = default) => AverageAwaitAsyncCore<TSource>(source, selector, cancellationToken);
        public static ValueTask<decimal> AverageAsync<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, ValueTask<decimal>> selector, CancellationToken cancellationToken = default) => AverageAwaitAsyncCore<TSource>(source, selector, cancellationToken);
        public static ValueTask<double?> AverageAsync<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, ValueTask<int?>> selector, CancellationToken cancellationToken = default) => AverageAwaitAsyncCore<TSource>(source, selector, cancellationToken);
        public static ValueTask<double?> AverageAsync<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, ValueTask<long?>> selector, CancellationToken cancellationToken = default) => AverageAwaitAsyncCore<TSource>(source, selector, cancellationToken);
        public static ValueTask<float?> AverageAsync<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, ValueTask<float?>> selector, CancellationToken cancellationToken = default) => AverageAwaitAsyncCore<TSource>(source, selector, cancellationToken);
        public static ValueTask<double?> AverageAsync<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, ValueTask<double?>> selector, CancellationToken cancellationToken = default) => AverageAwaitAsyncCore<TSource>(source, selector, cancellationToken);
        public static ValueTask<decimal?> AverageAsync<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, ValueTask<decimal?>> selector, CancellationToken cancellationToken = default) => AverageAwaitAsyncCore<TSource>(source, selector, cancellationToken);
        public static ValueTask<int> CountAsync<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, ValueTask<bool>> predicate, CancellationToken cancellationToken = default) => CountAwaitAsyncCore<TSource>(source, predicate, cancellationToken);
        public static ValueTask<TSource> FirstAsync<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, ValueTask<bool>> predicate, CancellationToken cancellationToken = default) => FirstAwaitAsyncCore<TSource>(source, predicate, cancellationToken);
        public static ValueTask<TSource?> FirstOrDefaultAsync<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, ValueTask<bool>> predicate, CancellationToken cancellationToken = default) => FirstOrDefaultAwaitAsyncCore<TSource>(source, predicate, cancellationToken);
        public static Task ForEachAsync<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, Task> action, CancellationToken cancellationToken = default) => ForEachAwaitAsyncCore<TSource>(source, action, cancellationToken);
        public static Task ForEachAsync<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, int, Task> action, CancellationToken cancellationToken = default) => ForEachAwaitAsyncCore<TSource>(source, action, cancellationToken);
        public static IAsyncEnumerable<IAsyncGrouping<TKey, TSource>> GroupBy<TSource, TKey>(this IAsyncEnumerable<TSource> source, Func<TSource, ValueTask<TKey>> keySelector) => GroupByAwaitCore<TSource, TKey>(source, keySelector);
        public static IAsyncEnumerable<IAsyncGrouping<TKey, TSource>> GroupBy<TSource, TKey>(this IAsyncEnumerable<TSource> source, Func<TSource, ValueTask<TKey>> keySelector, IEqualityComparer<TKey> comparer) => GroupByAwaitCore<TSource, TKey>(source, keySelector, comparer);
        public static IAsyncEnumerable<IAsyncGrouping<TKey, TElement>> GroupBy<TSource, TKey, TElement>(this IAsyncEnumerable<TSource> source, Func<TSource, ValueTask<TKey>> keySelector, Func<TSource, ValueTask<TElement>> elementSelector) => GroupByAwaitCore<TSource, TKey, TElement>(source, keySelector, elementSelector);
        public static IAsyncEnumerable<TResult> GroupBy<TSource, TKey, TResult>(this IAsyncEnumerable<TSource> source, Func<TSource, ValueTask<TKey>> keySelector, Func<TKey, IAsyncEnumerable<TSource>, ValueTask<TResult>> resultSelector) => GroupByAwaitCore<TSource, TKey, TResult>(source, keySelector, resultSelector);
        public static IAsyncEnumerable<TResult> GroupBy<TSource, TKey, TElement, TResult>(this IAsyncEnumerable<TSource> source, Func<TSource, ValueTask<TKey>> keySelector, Func<TSource, ValueTask<TElement>> elementSelector, Func<TKey, IAsyncEnumerable<TElement>, ValueTask<TResult>> resultSelector) => GroupByAwaitCore<TSource, TKey, TElement, TResult>(source, keySelector, elementSelector, resultSelector);
        public static IAsyncEnumerable<IAsyncGrouping<TKey, TElement>> GroupBy<TSource, TKey, TElement>(this IAsyncEnumerable<TSource> source, Func<TSource, ValueTask<TKey>> keySelector, Func<TSource, ValueTask<TElement>> elementSelector, IEqualityComparer<TKey> comparer) => GroupByAwaitCore<TSource, TKey, TElement>(source, keySelector, elementSelector, comparer);
        public static IAsyncEnumerable<TResult> GroupBy<TSource, TKey, TResult>(this IAsyncEnumerable<TSource> source, Func<TSource, ValueTask<TKey>> keySelector, Func<TKey, IAsyncEnumerable<TSource>, ValueTask<TResult>> resultSelector, IEqualityComparer<TKey> comparer) => GroupByAwaitCore<TSource, TKey, TResult>(source, keySelector, resultSelector, comparer);
        public static IAsyncEnumerable<TResult> GroupBy<TSource, TKey, TElement, TResult>(this IAsyncEnumerable<TSource> source, Func<TSource, ValueTask<TKey>> keySelector, Func<TSource, ValueTask<TElement>> elementSelector, Func<TKey, IAsyncEnumerable<TElement>, ValueTask<TResult>> resultSelector, IEqualityComparer<TKey> comparer) => GroupByAwaitCore<TSource, TKey, TElement, TResult>(source, keySelector, elementSelector, resultSelector, comparer);
        public static IAsyncEnumerable<TResult> GroupJoin<TOuter, TInner, TKey, TResult>(this IAsyncEnumerable<TOuter> outer, IAsyncEnumerable<TInner> inner, Func<TOuter, ValueTask<TKey>> outerKeySelector, Func<TInner, ValueTask<TKey>> innerKeySelector, Func<TOuter, IAsyncEnumerable<TInner>, ValueTask<TResult>> resultSelector) => GroupJoinAwaitCore<TOuter, TInner, TKey, TResult>(outer, inner, outerKeySelector, innerKeySelector, resultSelector);
        public static IAsyncEnumerable<TResult> GroupJoin<TOuter, TInner, TKey, TResult>(this IAsyncEnumerable<TOuter> outer, IAsyncEnumerable<TInner> inner, Func<TOuter, ValueTask<TKey>> outerKeySelector, Func<TInner, ValueTask<TKey>> innerKeySelector, Func<TOuter, IAsyncEnumerable<TInner>, ValueTask<TResult>> resultSelector, IEqualityComparer<TKey> comparer) => GroupJoinAwaitCore<TOuter, TInner, TKey, TResult>(outer, inner, outerKeySelector, innerKeySelector, resultSelector, comparer);
        public static IAsyncEnumerable<TResult> Join<TOuter, TInner, TKey, TResult>(this IAsyncEnumerable<TOuter> outer, IAsyncEnumerable<TInner> inner, Func<TOuter, ValueTask<TKey>> outerKeySelector, Func<TInner, ValueTask<TKey>> innerKeySelector, Func<TOuter, TInner, ValueTask<TResult>> resultSelector) => JoinAwaitCore<TOuter, TInner, TKey, TResult>(outer, inner, outerKeySelector, innerKeySelector, resultSelector);
        public static IAsyncEnumerable<TResult> Join<TOuter, TInner, TKey, TResult>(this IAsyncEnumerable<TOuter> outer, IAsyncEnumerable<TInner> inner, Func<TOuter, ValueTask<TKey>> outerKeySelector, Func<TInner, ValueTask<TKey>> innerKeySelector, Func<TOuter, TInner, ValueTask<TResult>> resultSelector, IEqualityComparer<TKey> comparer) => JoinAwaitCore<TOuter, TInner, TKey, TResult>(outer, inner, outerKeySelector, innerKeySelector, resultSelector, comparer);
        public static ValueTask<TSource> LastAsync<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, ValueTask<bool>> predicate, CancellationToken cancellationToken = default) => LastAwaitAsyncCore<TSource>(source, predicate, cancellationToken);
        public static ValueTask<TSource?> LastOrDefaultAsync<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, ValueTask<bool>> predicate, CancellationToken cancellationToken = default) => LastOrDefaultAwaitAsyncCore<TSource>(source, predicate, cancellationToken);
        public static ValueTask<long> LongCountAsync<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, ValueTask<bool>> predicate, CancellationToken cancellationToken = default) => LongCountAwaitAsyncCore<TSource>(source, predicate, cancellationToken);
        public static ValueTask<long> MaxAsync<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, ValueTask<long>> selector, CancellationToken cancellationToken = default) => MaxAwaitAsyncCore<TSource>(source, selector, cancellationToken);
        public static ValueTask<long?> MaxAsync<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, ValueTask<long?>> selector, CancellationToken cancellationToken = default) => MaxAwaitAsyncCore<TSource>(source, selector, cancellationToken);
        public static ValueTask<float> MaxAsync<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, ValueTask<float>> selector, CancellationToken cancellationToken = default) => MaxAwaitAsyncCore<TSource>(source, selector, cancellationToken);
        public static ValueTask<float?> MaxAsync<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, ValueTask<float?>> selector, CancellationToken cancellationToken = default) => MaxAwaitAsyncCore<TSource>(source, selector, cancellationToken);
        public static ValueTask<double> MaxAsync<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, ValueTask<double>> selector, CancellationToken cancellationToken = default) => MaxAwaitAsyncCore<TSource>(source, selector, cancellationToken);
        public static ValueTask<double?> MaxAsync<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, ValueTask<double?>> selector, CancellationToken cancellationToken = default) => MaxAwaitAsyncCore<TSource>(source, selector, cancellationToken);
        public static ValueTask<decimal> MaxAsync<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, ValueTask<decimal>> selector, CancellationToken cancellationToken = default) => MaxAwaitAsyncCore<TSource>(source, selector, cancellationToken);
        public static ValueTask<decimal?> MaxAsync<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, ValueTask<decimal?>> selector, CancellationToken cancellationToken = default) => MaxAwaitAsyncCore<TSource>(source, selector, cancellationToken);
        public static ValueTask<TResult> MaxAsync<TSource, TResult>(this IAsyncEnumerable<TSource> source, Func<TSource, ValueTask<TResult>> selector, CancellationToken cancellationToken = default) => MaxAwaitAsyncCore<TSource, TResult>(source, selector, cancellationToken);
        public static ValueTask<int> MaxAsync<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, ValueTask<int>> selector, CancellationToken cancellationToken = default) => MaxAwaitAsyncCore<TSource>(source, selector, cancellationToken);
        public static ValueTask<int?> MaxAsync<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, ValueTask<int?>> selector, CancellationToken cancellationToken = default) => MaxAwaitAsyncCore<TSource>(source, selector, cancellationToken);
        public static ValueTask<double> MinAsync<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, ValueTask<double>> selector, CancellationToken cancellationToken = default) => MinAwaitAsyncCore<TSource>(source, selector, cancellationToken);
        public static ValueTask<double?> MinAsync<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, ValueTask<double?>> selector, CancellationToken cancellationToken = default) => MinAwaitAsyncCore<TSource>(source, selector, cancellationToken);
        public static ValueTask<decimal> MinAsync<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, ValueTask<decimal>> selector, CancellationToken cancellationToken = default) => MinAwaitAsyncCore<TSource>(source, selector, cancellationToken);
        public static ValueTask<decimal?> MinAsync<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, ValueTask<decimal?>> selector, CancellationToken cancellationToken = default) => MinAwaitAsyncCore<TSource>(source, selector, cancellationToken);
        public static ValueTask<int> MinAsync<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, ValueTask<int>> selector, CancellationToken cancellationToken = default) => MinAwaitAsyncCore<TSource>(source, selector, cancellationToken);
        public static ValueTask<int?> MinAsync<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, ValueTask<int?>> selector, CancellationToken cancellationToken = default) => MinAwaitAsyncCore<TSource>(source, selector, cancellationToken);
        public static ValueTask<long> MinAsync<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, ValueTask<long>> selector, CancellationToken cancellationToken = default) => MinAwaitAsyncCore<TSource>(source, selector, cancellationToken);
        public static ValueTask<long?> MinAsync<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, ValueTask<long?>> selector, CancellationToken cancellationToken = default) => MinAwaitAsyncCore<TSource>(source, selector, cancellationToken);
        public static ValueTask<float> MinAsync<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, ValueTask<float>> selector, CancellationToken cancellationToken = default) => MinAwaitAsyncCore<TSource>(source, selector, cancellationToken);
        public static ValueTask<float?> MinAsync<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, ValueTask<float?>> selector, CancellationToken cancellationToken = default) => MinAwaitAsyncCore<TSource>(source, selector, cancellationToken);
        public static ValueTask<TResult> MinAsync<TSource, TResult>(this IAsyncEnumerable<TSource> source, Func<TSource, ValueTask<TResult>> selector, CancellationToken cancellationToken = default) => MinAwaitAsyncCore<TSource, TResult>(source, selector, cancellationToken);
        public static IOrderedAsyncEnumerable<TSource> OrderBy<TSource, TKey>(this IAsyncEnumerable<TSource> source, Func<TSource, ValueTask<TKey>> keySelector) => OrderByAwaitCore<TSource, TKey>(source, keySelector);
        public static IOrderedAsyncEnumerable<TSource> OrderBy<TSource, TKey>(this IAsyncEnumerable<TSource> source, Func<TSource, ValueTask<TKey>> keySelector, IComparer<TKey> comparer) => OrderByAwaitCore<TSource, TKey>(source, keySelector, comparer);
        public static IOrderedAsyncEnumerable<TSource> OrderByDescending<TSource, TKey>(this IAsyncEnumerable<TSource> source, Func<TSource, ValueTask<TKey>> keySelector) => OrderByDescendingAwaitCore<TSource, TKey>(source, keySelector);
        public static IOrderedAsyncEnumerable<TSource> OrderByDescending<TSource, TKey>(this IAsyncEnumerable<TSource> source, Func<TSource, ValueTask<TKey>> keySelector, IComparer<TKey> comparer) => OrderByDescendingAwaitCore<TSource, TKey>(source, keySelector, comparer);
        public static IAsyncEnumerable<TResult> Select<TSource, TResult>(this IAsyncEnumerable<TSource> source, Func<TSource, ValueTask<TResult>> selector) => SelectAwaitCore<TSource, TResult>(source, selector);
        public static IAsyncEnumerable<TResult> Select<TSource, TResult>(this IAsyncEnumerable<TSource> source, Func<TSource, int, ValueTask<TResult>> selector) => SelectAwaitCore<TSource, TResult>(source, selector);
        public static IAsyncEnumerable<TResult> SelectMany<TSource, TResult>(this IAsyncEnumerable<TSource> source, Func<TSource, ValueTask<IAsyncEnumerable<TResult>>> selector) => SelectManyAwaitCore<TSource, TResult>(source, selector);
        public static IAsyncEnumerable<TResult> SelectMany<TSource, TResult>(this IAsyncEnumerable<TSource> source, Func<TSource, int, ValueTask<IAsyncEnumerable<TResult>>> selector) => SelectManyAwaitCore<TSource, TResult>(source, selector);
        public static IAsyncEnumerable<TResult> SelectMany<TSource, TCollection, TResult>(this IAsyncEnumerable<TSource> source, Func<TSource, ValueTask<IAsyncEnumerable<TCollection>>> collectionSelector, Func<TSource, TCollection, ValueTask<TResult>> resultSelector) => SelectManyAwaitCore<TSource, TCollection, TResult>(source, collectionSelector, resultSelector);
        public static IAsyncEnumerable<TResult> SelectMany<TSource, TCollection, TResult>(this IAsyncEnumerable<TSource> source, Func<TSource, int, ValueTask<IAsyncEnumerable<TCollection>>> collectionSelector, Func<TSource, TCollection, ValueTask<TResult>> resultSelector) => SelectManyAwaitCore<TSource, TCollection, TResult>(source, collectionSelector, resultSelector);
        public static ValueTask<TSource> SingleAsync<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, ValueTask<bool>> predicate, CancellationToken cancellationToken = default) => SingleAwaitAsyncCore<TSource>(source, predicate, cancellationToken);
        public static ValueTask<TSource?> SingleOrDefaultAsync<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, ValueTask<bool>> predicate, CancellationToken cancellationToken = default) => SingleOrDefaultAwaitAsyncCore<TSource>(source, predicate, cancellationToken);
        public static IAsyncEnumerable<TSource> SkipWhile<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, ValueTask<bool>> predicate) => SkipWhileAwaitCore<TSource>(source, predicate);
        public static IAsyncEnumerable<TSource> SkipWhile<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, int, ValueTask<bool>> predicate) => SkipWhileAwaitCore<TSource>(source, predicate);
        public static ValueTask<long?> SumAsync<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, ValueTask<long?>> selector, CancellationToken cancellationToken = default) => SumAwaitAsyncCore<TSource>(source, selector, cancellationToken);
        public static ValueTask<float?> SumAsync<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, ValueTask<float?>> selector, CancellationToken cancellationToken = default) => SumAwaitAsyncCore<TSource>(source, selector, cancellationToken);
        public static ValueTask<double?> SumAsync<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, ValueTask<double?>> selector, CancellationToken cancellationToken = default) => SumAwaitAsyncCore<TSource>(source, selector, cancellationToken);
        public static ValueTask<decimal?> SumAsync<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, ValueTask<decimal?>> selector, CancellationToken cancellationToken = default) => SumAwaitAsyncCore<TSource>(source, selector, cancellationToken);
        public static ValueTask<int> SumAsync<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, ValueTask<int>> selector, CancellationToken cancellationToken = default) => SumAwaitAsyncCore<TSource>(source, selector, cancellationToken);
        public static ValueTask<long> SumAsync<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, ValueTask<long>> selector, CancellationToken cancellationToken = default) => SumAwaitAsyncCore<TSource>(source, selector, cancellationToken);
        public static ValueTask<float> SumAsync<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, ValueTask<float>> selector, CancellationToken cancellationToken = default) => SumAwaitAsyncCore<TSource>(source, selector, cancellationToken);
        public static ValueTask<double> SumAsync<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, ValueTask<double>> selector, CancellationToken cancellationToken = default) => SumAwaitAsyncCore<TSource>(source, selector, cancellationToken);
        public static ValueTask<decimal> SumAsync<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, ValueTask<decimal>> selector, CancellationToken cancellationToken = default) => SumAwaitAsyncCore<TSource>(source, selector, cancellationToken);
        public static ValueTask<int?> SumAsync<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, ValueTask<int?>> selector, CancellationToken cancellationToken = default) => SumAwaitAsyncCore<TSource>(source, selector, cancellationToken);
        public static IAsyncEnumerable<TSource> TakeWhile<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, ValueTask<bool>> predicate) => TakeWhileAwaitCore<TSource>(source, predicate);
        public static IAsyncEnumerable<TSource> TakeWhile<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, int, ValueTask<bool>> predicate) => TakeWhileAwaitCore<TSource>(source, predicate);
        public static IOrderedAsyncEnumerable<TSource> ThenBy<TSource, TKey>(this IOrderedAsyncEnumerable<TSource> source, Func<TSource, ValueTask<TKey>> keySelector) => ThenByAwaitCore<TSource, TKey>(source, keySelector);
        public static IOrderedAsyncEnumerable<TSource> ThenBy<TSource, TKey>(this IOrderedAsyncEnumerable<TSource> source, Func<TSource, ValueTask<TKey>> keySelector, IComparer<TKey> comparer) => ThenByAwaitCore<TSource, TKey>(source, keySelector, comparer);
        public static IOrderedAsyncEnumerable<TSource> ThenByDescending<TSource, TKey>(this IOrderedAsyncEnumerable<TSource> source, Func<TSource, ValueTask<TKey>> keySelector) => ThenByDescendingAwaitCore<TSource, TKey>(source, keySelector);
        public static IOrderedAsyncEnumerable<TSource> ThenByDescending<TSource, TKey>(this IOrderedAsyncEnumerable<TSource> source, Func<TSource, ValueTask<TKey>> keySelector, IComparer<TKey> comparer) => ThenByDescendingAwaitCore<TSource, TKey>(source, keySelector, comparer);
        public static ValueTask<Dictionary<TKey, TSource>> ToDictionaryAsync<TSource, TKey>(this IAsyncEnumerable<TSource> source, Func<TSource, ValueTask<TKey>> keySelector, CancellationToken cancellationToken = default) where TKey : notnull => ToDictionaryAwaitAsyncCore<TSource, TKey>(source, keySelector, cancellationToken);
        public static ValueTask<Dictionary<TKey, TSource>> ToDictionaryAsync<TSource, TKey>(this IAsyncEnumerable<TSource> source, Func<TSource, ValueTask<TKey>> keySelector, IEqualityComparer<TKey> comparer, CancellationToken cancellationToken = default) where TKey : notnull => ToDictionaryAwaitAsyncCore<TSource, TKey>(source, keySelector, comparer, cancellationToken);
        public static ValueTask<Dictionary<TKey, TElement>> ToDictionaryAsync<TSource, TKey, TElement>(this IAsyncEnumerable<TSource> source, Func<TSource, ValueTask<TKey>> keySelector, Func<TSource, ValueTask<TElement>> elementSelector, CancellationToken cancellationToken = default) where TKey : notnull => ToDictionaryAwaitAsyncCore<TSource, TKey, TElement>(source, keySelector, elementSelector, cancellationToken);
        public static ValueTask<Dictionary<TKey, TElement>> ToDictionaryAsync<TSource, TKey, TElement>(this IAsyncEnumerable<TSource> source, Func<TSource, ValueTask<TKey>> keySelector, Func<TSource, ValueTask<TElement>> elementSelector, IEqualityComparer<TKey> comparer, CancellationToken cancellationToken = default) where TKey : notnull => ToDictionaryAwaitAsyncCore<TSource, TKey, TElement>(source, keySelector, elementSelector, comparer, cancellationToken);
        public static ValueTask<ImmutableDictionary<TKey, TSource>> ToImmutableDictionaryAsync<TSource, TKey>(this IAsyncEnumerable<TSource> source, Func<TSource, ValueTask<TKey>> keySelector, CancellationToken cancellationToken = default) where TKey : notnull => ToImmutableDictionaryAwaitAsyncCore<TSource, TKey>(source, keySelector, cancellationToken);
        public static ValueTask<ImmutableDictionary<TKey, TValue>> ToImmutableDictionaryAsync<TSource, TKey, TValue>(this IAsyncEnumerable<TSource> source, Func<TSource, ValueTask<TKey>> keySelector, Func<TSource, ValueTask<TValue>> elementSelector, CancellationToken cancellationToken = default) where TKey : notnull => ToImmutableDictionaryAwaitAsyncCore<TSource, TKey, TValue>(source, keySelector, elementSelector, cancellationToken);
        public static ValueTask<ImmutableDictionary<TKey, TSource>> ToImmutableDictionaryAsync<TSource, TKey>(this IAsyncEnumerable<TSource> source, Func<TSource, ValueTask<TKey>> keySelector, IEqualityComparer<TKey>? keyComparer, CancellationToken cancellationToken = default) where TKey : notnull => ToImmutableDictionaryAwaitAsyncCore<TSource, TKey>(source, keySelector, keyComparer, cancellationToken);
        public static ValueTask<ImmutableDictionary<TKey, TValue>> ToImmutableDictionaryAsync<TSource, TKey, TValue>(this IAsyncEnumerable<TSource> source, Func<TSource, ValueTask<TKey>> keySelector, Func<TSource, ValueTask<TValue>> elementSelector, IEqualityComparer<TKey> keyComparer?, CancellationToken cancellationToken = default) where TKey : notnull => ToImmutableDictionaryAwaitAsyncCore<TSource, TKey, TValue>(source, keySelector, elementSelector, keyComparer, cancellationToken);
        public static ValueTask<ImmutableDictionary<TKey, TValue>> ToImmutableDictionaryAsync<TSource, TKey, TValue>(this IAsyncEnumerable<TSource> source, Func<TSource, ValueTask<TKey>> keySelector, Func<TSource, ValueTask<TValue>> elementSelector, IEqualityComparer<TKey> keyComparer?, IEqualityComparer<TValue> valueComparer?, CancellationToken cancellationToken = default) where TKey : notnull => ToImmutableDictionaryAwaitAsyncCore<TSource, TKey, TValue>(source, keySelector, elementSelector, keyComparer, valueComparer, cancellationToken);
        public static ValueTask<ImmutableSortedDictionary<TKey, TValue>> ToImmutableSortedDictionaryAsync<TSource, TKey, TValue>(this IAsyncEnumerable<TSource> source, Func<TSource, ValueTask<TKey>> keySelector, Func<TSource, ValueTask<TValue>> elementSelector, CancellationToken cancellationToken = default) => ToImmutableSortedDictionaryAwaitAsyncCore<TSource, TKey, TValue>(source, keySelector, elementSelector, cancellationToken);
        public static ValueTask<ImmutableSortedDictionary<TKey, TValue>> ToImmutableSortedDictionaryAsync<TSource, TKey, TValue>(this IAsyncEnumerable<TSource> source, Func<TSource, ValueTask<TKey>> keySelector, Func<TSource, ValueTask<TValue>> elementSelector, IComparer<TKey>? keyComparer, CancellationToken cancellationToken = default) => ToImmutableSortedDictionaryAwaitAsyncCore<TSource, TKey, TValue>(source, keySelector, elementSelector, keyComparer, cancellationToken);
        public static ValueTask<ImmutableSortedDictionary<TKey, TValue>> ToImmutableSortedDictionaryAsync<TSource, TKey, TValue>(this IAsyncEnumerable<TSource> source, Func<TSource, ValueTask<TKey>> keySelector, Func<TSource, ValueTask<TValue>> elementSelector, IComparer<TKey>? keyComparer, IEqualityComparer<TValue>? valueComparer, CancellationToken cancellationToken = default) => ToImmutableSortedDictionaryAwaitAsyncCore<TSource, TKey, TValue>(source, keySelector, elementSelector, keyComparer, valueComparer, cancellationToken);
        public static ValueTask<ILookup<TKey, TSource>> ToLookupAsync<TSource, TKey>(this IAsyncEnumerable<TSource> source, Func<TSource, ValueTask<TKey>> keySelector, CancellationToken cancellationToken = default) => ToLookupAwaitAsyncCore<TSource, TKey>(source, keySelector, cancellationToken);
        public static ValueTask<ILookup<TKey, TElement>> ToLookupAsync<TSource, TKey, TElement>(this IAsyncEnumerable<TSource> source, Func<TSource, ValueTask<TKey>> keySelector, Func<TSource, ValueTask<TElement>> elementSelector, CancellationToken cancellationToken = default) => ToLookupAwaitAsyncCore<TSource, TKey, TElement>(source, keySelector, elementSelector, cancellationToken);
        public static ValueTask<ILookup<TKey, TSource>> ToLookupAsync<TSource, TKey>(this IAsyncEnumerable<TSource> source, Func<TSource, ValueTask<TKey>> keySelector, IEqualityComparer<TKey> comparer, CancellationToken cancellationToken = default) => ToLookupAwaitAsyncCore<TSource, TKey>(source, keySelector, comparer, cancellationToken);
        public static ValueTask<ILookup<TKey, TElement>> ToLookupAsync<TSource, TKey, TElement>(this IAsyncEnumerable<TSource> source, Func<TSource, ValueTask<TKey>> keySelector, Func<TSource, ValueTask<TElement>> elementSelector, IEqualityComparer<TKey> comparer, CancellationToken cancellationToken = default) => ToLookupAwaitAsyncCore<TSource, TKey, TElement>(source, keySelector, elementSelector, comparer, cancellationToken);
        public static IAsyncEnumerable<TSource> Where<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, ValueTask<bool>> predicate) => WhereAwaitCore<TSource>(source, predicate);
        public static IAsyncEnumerable<TSource> Where<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, int, ValueTask<bool>> predicate) => WhereAwaitCore<TSource>(source, predicate);
        public static IAsyncEnumerable<TResult> Zip<TFirst, TSecond, TResult>(this IAsyncEnumerable<TFirst> first, IAsyncEnumerable<TSecond> second, Func<TFirst, TSecond, ValueTask<TResult>> selector) => ZipAwaitCore<TFirst, TSecond, TResult>(first, second, selector);

#if !NO_DEEP_CANCELLATION
        public static ValueTask<TSource> AggregateAsync<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, TSource, CancellationToken, ValueTask<TSource>> accumulator, CancellationToken cancellationToken = default) => AggregateAwaitWithCancellationAsyncCore<TSource>(source, accumulator, cancellationToken);
        public static ValueTask<TAccumulate> AggregateAsync<TSource, TAccumulate>(this IAsyncEnumerable<TSource> source, TAccumulate seed, Func<TAccumulate, TSource, CancellationToken, ValueTask<TAccumulate>> accumulator, CancellationToken cancellationToken = default) => AggregateAwaitWithCancellationAsyncCore<TSource, TAccumulate>(source, seed, accumulator, cancellationToken);
        public static ValueTask<TResult> AggregateAsync<TSource, TAccumulate, TResult>(this IAsyncEnumerable<TSource> source, TAccumulate seed, Func<TAccumulate, TSource, CancellationToken, ValueTask<TAccumulate>> accumulator, Func<TAccumulate, CancellationToken, ValueTask<TResult>> resultSelector, CancellationToken cancellationToken = default) => AggregateAwaitWithCancellationAsyncCore<TSource, TAccumulate, TResult>(source, seed, accumulator, resultSelector, cancellationToken);
        public static ValueTask<bool> AllAsync<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, ValueTask<bool>> predicate, CancellationToken cancellationToken = default) => AllAwaitWithCancellationAsyncCore<TSource>(source, predicate, cancellationToken);
        public static ValueTask<bool> AnyAsync<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, ValueTask<bool>> predicate, CancellationToken cancellationToken = default) => AnyAwaitWithCancellationAsyncCore<TSource>(source, predicate, cancellationToken);
        public static ValueTask<double> AverageAsync<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, ValueTask<int>> selector, CancellationToken cancellationToken = default) => AverageAwaitWithCancellationAsyncCore<TSource>(source, selector, cancellationToken);
        public static ValueTask<double> AverageAsync<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, ValueTask<long>> selector, CancellationToken cancellationToken = default) => AverageAwaitWithCancellationAsyncCore<TSource>(source, selector, cancellationToken);
        public static ValueTask<float> AverageAsync<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, ValueTask<float>> selector, CancellationToken cancellationToken = default) => AverageAwaitWithCancellationAsyncCore<TSource>(source, selector, cancellationToken);
        public static ValueTask<double> AverageAsync<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, ValueTask<double>> selector, CancellationToken cancellationToken = default) => AverageAwaitWithCancellationAsyncCore<TSource>(source, selector, cancellationToken);
        public static ValueTask<decimal> AverageAsync<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, ValueTask<decimal>> selector, CancellationToken cancellationToken = default) => AverageAwaitWithCancellationAsyncCore<TSource>(source, selector, cancellationToken);
        public static ValueTask<double?> AverageAsync<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, ValueTask<int?>> selector, CancellationToken cancellationToken = default) => AverageAwaitWithCancellationAsyncCore<TSource>(source, selector, cancellationToken);
        public static ValueTask<double?> AverageAsync<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, ValueTask<long?>> selector, CancellationToken cancellationToken = default) => AverageAwaitWithCancellationAsyncCore<TSource>(source, selector, cancellationToken);
        public static ValueTask<float?> AverageAsync<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, ValueTask<float?>> selector, CancellationToken cancellationToken = default) => AverageAwaitWithCancellationAsyncCore<TSource>(source, selector, cancellationToken);
        public static ValueTask<double?> AverageAsync<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, ValueTask<double?>> selector, CancellationToken cancellationToken = default) => AverageAwaitWithCancellationAsyncCore<TSource>(source, selector, cancellationToken);
        public static ValueTask<decimal?> AverageAsync<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, ValueTask<decimal?>> selector, CancellationToken cancellationToken = default) => AverageAwaitWithCancellationAsyncCore<TSource>(source, selector, cancellationToken);
        public static ValueTask<int> CountAsync<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, ValueTask<bool>> predicate, CancellationToken cancellationToken = default) => CountAwaitWithCancellationAsyncCore<TSource>(source, predicate, cancellationToken);
        public static ValueTask<TSource> FirstAsync<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, ValueTask<bool>> predicate, CancellationToken cancellationToken = default) => FirstAwaitWithCancellationAsyncCore<TSource>(source, predicate, cancellationToken);
        public static ValueTask<TSource?> FirstOrDefaultAsync<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, ValueTask<bool>> predicate, CancellationToken cancellationToken = default) => FirstOrDefaultAwaitWithCancellationAsyncCore<TSource>(source, predicate, cancellationToken);
        public static Task ForEachAsync<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, Task> action, CancellationToken cancellationToken) => ForEachAwaitWithCancellationAsyncCore<TSource>(source, action, cancellationToken);
        public static Task ForEachAsync<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, int, CancellationToken, Task> action, CancellationToken cancellationToken) => ForEachAwaitWithCancellationAsyncCore<TSource>(source, action, cancellationToken);
        public static IAsyncEnumerable<IAsyncGrouping<TKey, TSource>> GroupBy<TSource, TKey>(this IAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, ValueTask<TKey>> keySelector) => GroupByAwaitWithCancellationCore<TSource, TKey>(source, keySelector);
        public static IAsyncEnumerable<IAsyncGrouping<TKey, TSource>> GroupBy<TSource, TKey>(this IAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, ValueTask<TKey>> keySelector, IEqualityComparer<TKey> comparer) => GroupByAwaitWithCancellationCore<TSource, TKey>(source, keySelector, comparer);
        public static IAsyncEnumerable<IAsyncGrouping<TKey, TElement>> GroupBy<TSource, TKey, TElement>(this IAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, ValueTask<TKey>> keySelector, Func<TSource, CancellationToken, ValueTask<TElement>> elementSelector) => GroupByAwaitWithCancellationCore<TSource, TKey, TElement>(source, keySelector, elementSelector);
        public static IAsyncEnumerable<TResult> GroupBy<TSource, TKey, TResult>(this IAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, ValueTask<TKey>> keySelector, Func<TKey, IAsyncEnumerable<TSource>, CancellationToken, ValueTask<TResult>> resultSelector) => GroupByAwaitWithCancellationCore<TSource, TKey, TResult>(source, keySelector, resultSelector);
        public static IAsyncEnumerable<TResult> GroupBy<TSource, TKey, TElement, TResult>(this IAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, ValueTask<TKey>> keySelector, Func<TSource, CancellationToken, ValueTask<TElement>> elementSelector, Func<TKey, IAsyncEnumerable<TElement>, CancellationToken, ValueTask<TResult>> resultSelector) => GroupByAwaitWithCancellationCore<TSource, TKey, TElement, TResult>(source, keySelector, elementSelector, resultSelector);
        public static IAsyncEnumerable<IAsyncGrouping<TKey, TElement>> GroupBy<TSource, TKey, TElement>(this IAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, ValueTask<TKey>> keySelector, Func<TSource, CancellationToken, ValueTask<TElement>> elementSelector, IEqualityComparer<TKey> comparer) => GroupByAwaitWithCancellationCore<TSource, TKey, TElement>(source, keySelector, elementSelector, comparer);
        public static IAsyncEnumerable<TResult> GroupBy<TSource, TKey, TResult>(this IAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, ValueTask<TKey>> keySelector, Func<TKey, IAsyncEnumerable<TSource>, CancellationToken, ValueTask<TResult>> resultSelector, IEqualityComparer<TKey> comparer) => GroupByAwaitWithCancellationCore<TSource, TKey, TResult>(source, keySelector, resultSelector, comparer);
        public static IAsyncEnumerable<TResult> GroupBy<TSource, TKey, TElement, TResult>(this IAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, ValueTask<TKey>> keySelector, Func<TSource, CancellationToken, ValueTask<TElement>> elementSelector, Func<TKey, IAsyncEnumerable<TElement>, CancellationToken, ValueTask<TResult>> resultSelector, IEqualityComparer<TKey> comparer) => GroupByAwaitWithCancellationCore<TSource, TKey, TElement, TResult>(source, keySelector, elementSelector, resultSelector, comparer);
        public static IAsyncEnumerable<TResult> GroupJoin<TOuter, TInner, TKey, TResult>(this IAsyncEnumerable<TOuter> outer, IAsyncEnumerable<TInner> inner, Func<TOuter, CancellationToken, ValueTask<TKey>> outerKeySelector, Func<TInner, CancellationToken, ValueTask<TKey>> innerKeySelector, Func<TOuter, IAsyncEnumerable<TInner>, CancellationToken, ValueTask<TResult>> resultSelector) => GroupJoinAwaitWithCancellationCore<TOuter, TInner, TKey, TResult>(outer, inner, outerKeySelector, innerKeySelector, resultSelector);
        public static IAsyncEnumerable<TResult> GroupJoin<TOuter, TInner, TKey, TResult>(this IAsyncEnumerable<TOuter> outer, IAsyncEnumerable<TInner> inner, Func<TOuter, CancellationToken, ValueTask<TKey>> outerKeySelector, Func<TInner, CancellationToken, ValueTask<TKey>> innerKeySelector, Func<TOuter, IAsyncEnumerable<TInner>, CancellationToken, ValueTask<TResult>> resultSelector, IEqualityComparer<TKey> comparer) => GroupJoinAwaitWithCancellationCore<TOuter, TInner, TKey, TResult>(outer, inner, outerKeySelector, innerKeySelector, resultSelector, comparer);
        public static IAsyncEnumerable<TResult> Join<TOuter, TInner, TKey, TResult>(this IAsyncEnumerable<TOuter> outer, IAsyncEnumerable<TInner> inner, Func<TOuter, CancellationToken, ValueTask<TKey>> outerKeySelector, Func<TInner, CancellationToken, ValueTask<TKey>> innerKeySelector, Func<TOuter, TInner, CancellationToken, ValueTask<TResult>> resultSelector) => JoinAwaitWithCancellationCore<TOuter, TInner, TKey, TResult>(outer, inner, outerKeySelector, innerKeySelector, resultSelector);
        public static IAsyncEnumerable<TResult> Join<TOuter, TInner, TKey, TResult>(this IAsyncEnumerable<TOuter> outer, IAsyncEnumerable<TInner> inner, Func<TOuter, CancellationToken, ValueTask<TKey>> outerKeySelector, Func<TInner, CancellationToken, ValueTask<TKey>> innerKeySelector, Func<TOuter, TInner, CancellationToken, ValueTask<TResult>> resultSelector, IEqualityComparer<TKey> comparer) => JoinAwaitWithCancellationCore<TOuter, TInner, TKey, TResult>(outer, inner, outerKeySelector, innerKeySelector, resultSelector, comparer);
        public static ValueTask<TSource> LastAsync<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, ValueTask<bool>> predicate, CancellationToken cancellationToken = default) => LastAwaitWithCancellationAsyncCore<TSource>(source, predicate, cancellationToken);
        public static ValueTask<TSource?> LastOrDefaultAsync<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, ValueTask<bool>> predicate, CancellationToken cancellationToken = default) => LastOrDefaultAwaitWithCancellationAsyncCore<TSource>(source, predicate, cancellationToken);
        public static ValueTask<long> LongCountAsync<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, ValueTask<bool>> predicate, CancellationToken cancellationToken = default) => LongCountAwaitWithCancellationAsyncCore<TSource>(source, predicate, cancellationToken);
        public static ValueTask<long> MaxAsync<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, ValueTask<long>> selector, CancellationToken cancellationToken = default) => MaxAwaitWithCancellationAsyncCore<TSource>(source, selector, cancellationToken);
        public static ValueTask<long?> MaxAsync<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, ValueTask<long?>> selector, CancellationToken cancellationToken = default) => MaxAwaitWithCancellationAsyncCore<TSource>(source, selector, cancellationToken);
        public static ValueTask<float> MaxAsync<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, ValueTask<float>> selector, CancellationToken cancellationToken = default) => MaxAwaitWithCancellationAsyncCore<TSource>(source, selector, cancellationToken);
        public static ValueTask<float?> MaxAsync<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, ValueTask<float?>> selector, CancellationToken cancellationToken = default) => MaxAwaitWithCancellationAsyncCore<TSource>(source, selector, cancellationToken);
        public static ValueTask<double> MaxAsync<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, ValueTask<double>> selector, CancellationToken cancellationToken = default) => MaxAwaitWithCancellationAsyncCore<TSource>(source, selector, cancellationToken);
        public static ValueTask<double?> MaxAsync<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, ValueTask<double?>> selector, CancellationToken cancellationToken = default) => MaxAwaitWithCancellationAsyncCore<TSource>(source, selector, cancellationToken);
        public static ValueTask<decimal> MaxAsync<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, ValueTask<decimal>> selector, CancellationToken cancellationToken = default) => MaxAwaitWithCancellationAsyncCore<TSource>(source, selector, cancellationToken);
        public static ValueTask<decimal?> MaxAsync<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, ValueTask<decimal?>> selector, CancellationToken cancellationToken = default) => MaxAwaitWithCancellationAsyncCore<TSource>(source, selector, cancellationToken);
        public static ValueTask<TResult> MaxAsync<TSource, TResult>(this IAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, ValueTask<TResult>> selector, CancellationToken cancellationToken = default) => MaxAwaitWithCancellationAsyncCore<TSource, TResult>(source, selector, cancellationToken);
        public static ValueTask<int> MaxAsync<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, ValueTask<int>> selector, CancellationToken cancellationToken = default) => MaxAwaitWithCancellationAsyncCore<TSource>(source, selector, cancellationToken);
        public static ValueTask<int?> MaxAsync<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, ValueTask<int?>> selector, CancellationToken cancellationToken = default) => MaxAwaitWithCancellationAsyncCore<TSource>(source, selector, cancellationToken);
        public static ValueTask<double> MinAsync<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, ValueTask<double>> selector, CancellationToken cancellationToken = default) => MinAwaitWithCancellationAsyncCore<TSource>(source, selector, cancellationToken);
        public static ValueTask<double?> MinAsync<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, ValueTask<double?>> selector, CancellationToken cancellationToken = default) => MinAwaitWithCancellationAsyncCore<TSource>(source, selector, cancellationToken);
        public static ValueTask<decimal> MinAsync<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, ValueTask<decimal>> selector, CancellationToken cancellationToken = default) => MinAwaitWithCancellationAsyncCore<TSource>(source, selector, cancellationToken);
        public static ValueTask<decimal?> MinAsync<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, ValueTask<decimal?>> selector, CancellationToken cancellationToken = default) => MinAwaitWithCancellationAsyncCore<TSource>(source, selector, cancellationToken);
        public static ValueTask<int> MinAsync<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, ValueTask<int>> selector, CancellationToken cancellationToken = default) => MinAwaitWithCancellationAsyncCore<TSource>(source, selector, cancellationToken);
        public static ValueTask<int?> MinAsync<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, ValueTask<int?>> selector, CancellationToken cancellationToken = default) => MinAwaitWithCancellationAsyncCore<TSource>(source, selector, cancellationToken);
        public static ValueTask<long> MinAsync<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, ValueTask<long>> selector, CancellationToken cancellationToken = default) => MinAwaitWithCancellationAsyncCore<TSource>(source, selector, cancellationToken);
        public static ValueTask<long?> MinAsync<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, ValueTask<long?>> selector, CancellationToken cancellationToken = default) => MinAwaitWithCancellationAsyncCore<TSource>(source, selector, cancellationToken);
        public static ValueTask<float> MinAsync<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, ValueTask<float>> selector, CancellationToken cancellationToken = default) => MinAwaitWithCancellationAsyncCore<TSource>(source, selector, cancellationToken);
        public static ValueTask<float?> MinAsync<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, ValueTask<float?>> selector, CancellationToken cancellationToken = default) => MinAwaitWithCancellationAsyncCore<TSource>(source, selector, cancellationToken);
        public static ValueTask<TResult> MinAsync<TSource, TResult>(this IAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, ValueTask<TResult>> selector, CancellationToken cancellationToken = default) => MinAwaitWithCancellationAsyncCore<TSource, TResult>(source, selector, cancellationToken);
        public static IOrderedAsyncEnumerable<TSource> OrderBy<TSource, TKey>(this IAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, ValueTask<TKey>> keySelector) => OrderByAwaitWithCancellationCore<TSource, TKey>(source, keySelector);
        public static IOrderedAsyncEnumerable<TSource> OrderBy<TSource, TKey>(this IAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, ValueTask<TKey>> keySelector, IComparer<TKey> comparer) => OrderByAwaitWithCancellationCore<TSource, TKey>(source, keySelector, comparer);
        public static IOrderedAsyncEnumerable<TSource> OrderByDescending<TSource, TKey>(this IAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, ValueTask<TKey>> keySelector) => OrderByDescendingAwaitWithCancellationCore<TSource, TKey>(source, keySelector);
        public static IOrderedAsyncEnumerable<TSource> OrderByDescending<TSource, TKey>(this IAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, ValueTask<TKey>> keySelector, IComparer<TKey> comparer) => OrderByDescendingAwaitWithCancellationCore<TSource, TKey>(source, keySelector, comparer);
        public static IAsyncEnumerable<TResult> Select<TSource, TResult>(this IAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, ValueTask<TResult>> selector) => SelectAwaitWithCancellationCore<TSource, TResult>(source, selector);
        public static IAsyncEnumerable<TResult> Select<TSource, TResult>(this IAsyncEnumerable<TSource> source, Func<TSource, int, CancellationToken, ValueTask<TResult>> selector) => SelectAwaitWithCancellationCore<TSource, TResult>(source, selector);
        public static IAsyncEnumerable<TResult> SelectMany<TSource, TResult>(this IAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, ValueTask<IAsyncEnumerable<TResult>>> selector) => SelectManyAwaitWithCancellationCore<TSource, TResult>(source, selector);
        public static IAsyncEnumerable<TResult> SelectMany<TSource, TResult>(this IAsyncEnumerable<TSource> source, Func<TSource, int, CancellationToken, ValueTask<IAsyncEnumerable<TResult>>> selector) => SelectManyAwaitWithCancellationCore<TSource, TResult>(source, selector);
        public static IAsyncEnumerable<TResult> SelectMany<TSource, TCollection, TResult>(this IAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, ValueTask<IAsyncEnumerable<TCollection>>> collectionSelector, Func<TSource, TCollection, CancellationToken, ValueTask<TResult>> resultSelector) => SelectManyAwaitWithCancellationCore<TSource, TCollection, TResult>(source, collectionSelector, resultSelector);
        public static IAsyncEnumerable<TResult> SelectMany<TSource, TCollection, TResult>(this IAsyncEnumerable<TSource> source, Func<TSource, int, CancellationToken, ValueTask<IAsyncEnumerable<TCollection>>> collectionSelector, Func<TSource, TCollection, CancellationToken, ValueTask<TResult>> resultSelector) => SelectManyAwaitWithCancellationCore<TSource, TCollection, TResult>(source, collectionSelector, resultSelector);
        public static ValueTask<TSource> SingleAsync<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, ValueTask<bool>> predicate, CancellationToken cancellationToken = default) => SingleAwaitWithCancellationAsyncCore<TSource>(source, predicate, cancellationToken);
        public static ValueTask<TSource?> SingleOrDefaultAsync<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, ValueTask<bool>> predicate, CancellationToken cancellationToken = default) => SingleOrDefaultAwaitWithCancellationAsyncCore<TSource>(source, predicate, cancellationToken);
        public static IAsyncEnumerable<TSource> SkipWhile<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, ValueTask<bool>> predicate) => SkipWhileAwaitWithCancellationCore<TSource>(source, predicate);
        public static IAsyncEnumerable<TSource> SkipWhile<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, int, CancellationToken, ValueTask<bool>> predicate) => SkipWhileAwaitWithCancellationCore<TSource>(source, predicate);
        public static ValueTask<int?> SumAsync<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, ValueTask<int?>> selector, CancellationToken cancellationToken = default) => SumAwaitWithCancellationAsyncCore<TSource>(source, selector, cancellationToken);
        public static ValueTask<long?> SumAsync<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, ValueTask<long?>> selector, CancellationToken cancellationToken = default) => SumAwaitWithCancellationAsyncCore<TSource>(source, selector, cancellationToken);
        public static ValueTask<float?> SumAsync<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, ValueTask<float?>> selector, CancellationToken cancellationToken = default) => SumAwaitWithCancellationAsyncCore<TSource>(source, selector, cancellationToken);
        public static ValueTask<double?> SumAsync<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, ValueTask<double?>> selector, CancellationToken cancellationToken = default) => SumAwaitWithCancellationAsyncCore<TSource>(source, selector, cancellationToken);
        public static ValueTask<decimal?> SumAsync<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, ValueTask<decimal?>> selector, CancellationToken cancellationToken = default) => SumAwaitWithCancellationAsyncCore<TSource>(source, selector, cancellationToken);
        public static ValueTask<int> SumAsync<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, ValueTask<int>> selector, CancellationToken cancellationToken = default) => SumAwaitWithCancellationAsyncCore<TSource>(source, selector, cancellationToken);
        public static ValueTask<long> SumAsync<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, ValueTask<long>> selector, CancellationToken cancellationToken = default) => SumAwaitWithCancellationAsyncCore<TSource>(source, selector, cancellationToken);
        public static ValueTask<float> SumAsync<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, ValueTask<float>> selector, CancellationToken cancellationToken = default) => SumAwaitWithCancellationAsyncCore<TSource>(source, selector, cancellationToken);
        public static ValueTask<double> SumAsync<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, ValueTask<double>> selector, CancellationToken cancellationToken = default) => SumAwaitWithCancellationAsyncCore<TSource>(source, selector, cancellationToken);
        public static ValueTask<decimal> SumAsync<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, ValueTask<decimal>> selector, CancellationToken cancellationToken = default) => SumAwaitWithCancellationAsyncCore<TSource>(source, selector, cancellationToken);
        public static IAsyncEnumerable<TSource> TakeWhile<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, ValueTask<bool>> predicate) => TakeWhileAwaitWithCancellationCore<TSource>(source, predicate);
        public static IAsyncEnumerable<TSource> TakeWhile<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, int, CancellationToken, ValueTask<bool>> predicate) => TakeWhileAwaitWithCancellationCore<TSource>(source, predicate);
        public static IOrderedAsyncEnumerable<TSource> ThenBy<TSource, TKey>(this IOrderedAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, ValueTask<TKey>> keySelector) => ThenByAwaitWithCancellationCore<TSource, TKey>(source, keySelector);
        public static IOrderedAsyncEnumerable<TSource> ThenBy<TSource, TKey>(this IOrderedAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, ValueTask<TKey>> keySelector, IComparer<TKey> comparer) => ThenByAwaitWithCancellationCore<TSource, TKey>(source, keySelector, comparer);
        public static IOrderedAsyncEnumerable<TSource> ThenByDescending<TSource, TKey>(this IOrderedAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, ValueTask<TKey>> keySelector) => ThenByDescendingAwaitWithCancellationCore<TSource, TKey>(source, keySelector);
        public static IOrderedAsyncEnumerable<TSource> ThenByDescending<TSource, TKey>(this IOrderedAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, ValueTask<TKey>> keySelector, IComparer<TKey> comparer) => ThenByDescendingAwaitWithCancellationCore<TSource, TKey>(source, keySelector, comparer);
        public static ValueTask<Dictionary<TKey, TSource>> ToDictionaryAsync<TSource, TKey>(this IAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, ValueTask<TKey>> keySelector, CancellationToken cancellationToken = default) where TKey : notnull => ToDictionaryAwaitWithCancellationAsyncCore<TSource, TKey>(source, keySelector, cancellationToken);
        public static ValueTask<Dictionary<TKey, TSource>> ToDictionaryAsync<TSource, TKey>(this IAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, ValueTask<TKey>> keySelector, IEqualityComparer<TKey> comparer, CancellationToken cancellationToken = default) where TKey : notnull => ToDictionaryAwaitWithCancellationAsyncCore<TSource, TKey>(source, keySelector, comparer, cancellationToken);
        public static ValueTask<Dictionary<TKey, TElement>> ToDictionaryAsync<TSource, TKey, TElement>(this IAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, ValueTask<TKey>> keySelector, Func<TSource, CancellationToken, ValueTask<TElement>> elementSelector, CancellationToken cancellationToken = default) where TKey : notnull => ToDictionaryAwaitWithCancellationAsyncCore<TSource, TKey, TElement>(source, keySelector, elementSelector, cancellationToken);
        public static ValueTask<Dictionary<TKey, TElement>> ToDictionaryAsync<TSource, TKey, TElement>(this IAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, ValueTask<TKey>> keySelector, Func<TSource, CancellationToken, ValueTask<TElement>> elementSelector, IEqualityComparer<TKey> comparer, CancellationToken cancellationToken = default) where TKey : notnull => ToDictionaryAwaitWithCancellationAsyncCore<TSource, TKey, TElement>(source, keySelector, elementSelector, comparer, cancellationToken);
        public static ValueTask<ImmutableDictionary<TKey, TSource>> ToImmutableDictionaryAsync<TSource, TKey>(this IAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, ValueTask<TKey>> keySelector, CancellationToken cancellationToken = default) where TKey : notnull => ToImmutableDictionaryAwaitWithCancellationAsyncCore<TSource, TKey>(source, keySelector, cancellationToken);
        public static ValueTask<ImmutableDictionary<TKey, TValue>> ToImmutableDictionaryAsync<TSource, TKey, TValue>(this IAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, ValueTask<TKey>> keySelector, Func<TSource, CancellationToken, ValueTask<TValue>> elementSelector, CancellationToken cancellationToken = default) where TKey : notnull => ToImmutableDictionaryAwaitWithCancellationAsyncCore<TSource, TKey, TValue>(source, keySelector, elementSelector, cancellationToken);
        public static ValueTask<ImmutableDictionary<TKey, TSource>> ToImmutableDictionaryAsync<TSource, TKey>(this IAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, ValueTask<TKey>> keySelector, IEqualityComparer<TKey>? keyComparer, CancellationToken cancellationToken = default) where TKey : notnull => ToImmutableDictionaryAwaitWithCancellationAsyncCore<TSource, TKey>(source, keySelector, keyComparer, cancellationToken);
        public static ValueTask<ImmutableDictionary<TKey, TValue>> ToImmutableDictionaryAsync<TSource, TKey, TValue>(this IAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, ValueTask<TKey>> keySelector, Func<TSource, CancellationToken, ValueTask<TValue>> elementSelector, IEqualityComparer<TKey>? keyComparer, CancellationToken cancellationToken = default) where TKey : notnull => ToImmutableDictionaryAwaitWithCancellationAsyncCore<TSource, TKey, TValue>(source, keySelector, elementSelector, keyComparer, cancellationToken);
        public static ValueTask<ImmutableDictionary<TKey, TValue>> ToImmutableDictionaryAsync<TSource, TKey, TValue>(this IAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, ValueTask<TKey>> keySelector, Func<TSource, CancellationToken, ValueTask<TValue>> elementSelector, IEqualityComparer<TKey>? keyComparer, IEqualityComparer<TValue>? valueComparer, CancellationToken cancellationToken = default) where TKey : notnull => ToImmutableDictionaryAwaitWithCancellationAsyncCore<TSource, TKey, TValue>(source, keySelector, elementSelector, keyComparer, valueComparer, cancellationToken);
        public static ValueTask<ImmutableSortedDictionary<TKey, TValue>> ToImmutableSortedDictionaryAsync<TSource, TKey, TValue>(this IAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, ValueTask<TKey>> keySelector, Func<TSource, CancellationToken, ValueTask<TValue>> elementSelector, CancellationToken cancellationToken = default) where TKey : notnull => ToImmutableSortedDictionaryAwaitWithCancellationAsyncCore<TSource, TKey, TValue>(source, keySelector, elementSelector, cancellationToken);
        public static ValueTask<ImmutableSortedDictionary<TKey, TValue>> ToImmutableSortedDictionaryAsync<TSource, TKey, TValue>(this IAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, ValueTask<TKey>> keySelector, Func<TSource, CancellationToken, ValueTask<TValue>> elementSelector, IComparer<TKey>? keyComparer, CancellationToken cancellationToken = default) where TKey : notnull => ToImmutableSortedDictionaryAwaitWithCancellationAsyncCore<TSource, TKey, TValue>(source, keySelector, elementSelector, keyComparer, cancellationToken);
        public static ValueTask<ImmutableSortedDictionary<TKey, TValue>> ToImmutableSortedDictionaryAsync<TSource, TKey, TValue>(this IAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, ValueTask<TKey>> keySelector, Func<TSource, CancellationToken, ValueTask<TValue>> elementSelector, IComparer<TKey>? keyComparer, IEqualityComparer<TValue>? valueComparer, CancellationToken cancellationToken = default) where TKey : notnull => ToImmutableSortedDictionaryAwaitWithCancellationAsyncCore<TSource, TKey, TValue>(source, keySelector, elementSelector, keyComparer, valueComparer, cancellationToken);
        public static ValueTask<ILookup<TKey, TSource>> ToLookupAsync<TSource, TKey>(this IAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, ValueTask<TKey>> keySelector, CancellationToken cancellationToken = default) => ToLookupAwaitWithCancellationAsyncCore<TSource, TKey>(source, keySelector, cancellationToken);
        public static ValueTask<ILookup<TKey, TElement>> ToLookupAsync<TSource, TKey, TElement>(this IAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, ValueTask<TKey>> keySelector, Func<TSource, CancellationToken, ValueTask<TElement>> elementSelector, CancellationToken cancellationToken = default) => ToLookupAwaitWithCancellationAsyncCore<TSource, TKey, TElement>(source, keySelector, elementSelector, cancellationToken);
        public static ValueTask<ILookup<TKey, TSource>> ToLookupAsync<TSource, TKey>(this IAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, ValueTask<TKey>> keySelector, IEqualityComparer<TKey> comparer, CancellationToken cancellationToken = default) => ToLookupAwaitWithCancellationAsyncCore<TSource, TKey>(source, keySelector, comparer, cancellationToken);
        public static ValueTask<ILookup<TKey, TElement>> ToLookupAsync<TSource, TKey, TElement>(this IAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, ValueTask<TKey>> keySelector, Func<TSource, CancellationToken, ValueTask<TElement>> elementSelector, IEqualityComparer<TKey> comparer, CancellationToken cancellationToken = default) => ToLookupAwaitWithCancellationAsyncCore<TSource, TKey, TElement>(source, keySelector, elementSelector, comparer, cancellationToken);
        public static IAsyncEnumerable<TSource> Where<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, ValueTask<bool>> predicate) => WhereAwaitWithCancellationCore<TSource>(source, predicate);
        public static IAsyncEnumerable<TSource> Where<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, int, CancellationToken, ValueTask<bool>> predicate) => WhereAwaitWithCancellationCore<TSource>(source, predicate);
        public static IAsyncEnumerable<TResult> Zip<TFirst, TSecond, TResult>(this IAsyncEnumerable<TFirst> first, IAsyncEnumerable<TSecond> second, Func<TFirst, TSecond, CancellationToken, ValueTask<TResult>> selector) => ZipAwaitWithCancellationCore<TFirst, TSecond, TResult>(first, second, selector);
#endif
#else
        /// <summary>
        /// Applies an accumulator function over an async-enumerable sequence, returning the result of the aggregation as a single element in the result sequence.
        /// </summary>
        /// <typeparam name="TSource">The type of elements in the source sequence.</typeparam>
        /// <param name="source">An async-enumerable sequence to aggregate over.</param>
        /// <param name="accumulator">An asynchronous accumulator function to be invoked and awaited on each element.</param>
        /// <param name="cancellationToken">An optional cancellation token to be used for cancelling the sequence at any time.</param>
        /// <returns>A ValueTask containing the final accumulator value.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> or <paramref name="accumulator"/> is <see langword="null"/>.</exception>
        /// <exception cref="InvalidOperationException">The source sequence is empty.</exception>
        /// <remarks>The return type of this operator differs from the corresponding operator on IEnumerable in order to retain asynchronous behavior.</remarks>
        public static ValueTask<TSource> AggregateAwaitAsync<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, TSource, ValueTask<TSource>> accumulator, CancellationToken cancellationToken = default) => AggregateAwaitAsyncCore<TSource>(source, accumulator, cancellationToken);

        /// <summary>
        /// Applies an accumulator function over an async-enumerable sequence, returning the result of the aggregation as a single element in the result sequence. The specified seed value is used as the initial accumulator value.
        /// </summary>
        /// <typeparam name="TSource">The type of elements in the source sequence.</typeparam>
        /// <typeparam name="TAccumulate">The type of the result of aggregation.</typeparam>
        /// <param name="source">An async-enumerable sequence to aggregate over.</param>
        /// <param name="seed">The initial accumulator value.</param>
        /// <param name="accumulator">An asynchronous accumulator function to be invoked and awaited on each element.</param>
        /// <param name="cancellationToken">An optional cancellation token to be used for cancelling the sequence at any time.</param>
        /// <returns>A ValueTask containing the final accumulator value.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> or <paramref name="accumulator"/> is <see langword="null"/>.</exception>
        /// <remarks>The return type of this operator differs from the corresponding operator on IEnumerable in order to retain asynchronous behavior.</remarks>
        public static ValueTask<TAccumulate> AggregateAwaitAsync<TSource, TAccumulate>(this IAsyncEnumerable<TSource> source, TAccumulate seed, Func<TAccumulate, TSource, ValueTask<TAccumulate>> accumulator, CancellationToken cancellationToken = default) => AggregateAwaitAsyncCore<TSource, TAccumulate>(source, seed, accumulator, cancellationToken);

        /// <summary>
        /// Applies an accumulator function over an async-enumerable sequence, returning the result of the aggregation as a single element in the result sequence. The specified seed value is used as the initial accumulator value,
        /// and the specified result selector is used to select the result value.
        /// </summary>
        /// <typeparam name="TSource">The type of elements in the source sequence.</typeparam>
        /// <typeparam name="TAccumulate">The type of the accumulator value.</typeparam>
        /// <typeparam name="TResult">The type of the resulting value.</typeparam>
        /// <param name="source">An async-enumerable sequence to aggregate over.</param>
        /// <param name="seed">The initial accumulator value.</param>
        /// <param name="accumulator">An asynchronous accumulator function to be invoked and awaited on each element.</param>
        /// <param name="resultSelector">An asynchronous transform function to transform the final accumulator value into the result value.</param>
        /// <param name="cancellationToken">An optional cancellation token to be used for cancelling the sequence at any time.</param>
        /// <returns>A ValueTask containing the value obtained by applying the result selector to the final accumulator value.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> or <paramref name="accumulator"/> or <paramref name="resultSelector"/> is <see langword="null"/>.</exception>
        public static ValueTask<TResult> AggregateAwaitAsync<TSource, TAccumulate, TResult>(this IAsyncEnumerable<TSource> source, TAccumulate seed, Func<TAccumulate, TSource, ValueTask<TAccumulate>> accumulator, Func<TAccumulate, ValueTask<TResult>> resultSelector, CancellationToken cancellationToken = default) => AggregateAwaitAsyncCore<TSource, TAccumulate, TResult>(source, seed, accumulator, resultSelector, cancellationToken);

        /// <summary>
        /// Determines whether all elements in an async-enumerable sequence satisfy a condition.
        /// </summary>
        /// <typeparam name="TSource">The type of element in the sequence.</typeparam>
        /// <param name="source">An async-enumerable sequence whose elements to apply the predicate to.</param>
        /// <param name="predicate">An asynchronous predicate to apply to each element of the source sequence.</param>
        /// <param name="cancellationToken">An optional cancellation token to be used for cancelling the sequence at any time.</param>
        /// <returns>A ValueTask containing a value indicating whether all elements in the sequence pass the test in the specified predicate.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> or <paramref name="predicate"/> is <see langword="null"/>.</exception>
        /// <remarks>The return type of this operator differs from the corresponding operator on IEnumerable in order to retain asynchronous behavior.</remarks>
        public static ValueTask<bool> AllAwaitAsync<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, ValueTask<bool>> predicate, CancellationToken cancellationToken = default) => AllAwaitAsyncCore<TSource>(source, predicate, cancellationToken);

        /// <summary>
        /// Determines whether any element in an async-enumerable sequence satisfies a condition.
        /// </summary>
        /// <typeparam name="TSource">The type of element in the sequence.</typeparam>
        /// <param name="source">An async-enumerable sequence whose elements to apply the predicate to.</param>
        /// <param name="predicate">An asynchronous predicate to apply to each element of the source sequence.</param>
        /// <param name="cancellationToken">An optional cancellation token to be used for cancelling the sequence at any time.</param>
        /// <returns>A ValueTask containing a value indicating whether any elements in the source sequence pass the test in the specified predicate.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> or <paramref name="predicate"/> is <see langword="null"/>.</exception>
        /// <remarks>The return type of this operator differs from the corresponding operator on IEnumerable in order to retain asynchronous behavior.</remarks>
        public static ValueTask<bool> AnyAwaitAsync<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, ValueTask<bool>> predicate, CancellationToken cancellationToken = default) => AnyAwaitAsyncCore<TSource>(source, predicate, cancellationToken);

        /// <summary>
        /// Computes the average of an async-enumerable sequence of <see cref="int"/> values that are obtained by invoking an asynchronous transform function on each element of the source sequence and awaiting the result.
        /// </summary>
        /// <typeparam name="TSource">The type of elements in the source sequence.</typeparam>
        /// <param name="source">An async-enumerable sequence of values to compute the average of.</param>
        /// <param name="selector">A transform function to invoke and await on each element of the source sequence.</param>
        /// <param name="cancellationToken">An optional cancellation token for cancelling the sequence at any time.</param>
        /// <returns>A ValueTask containing the average of the sequence of values.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> or <paramref name="selector"/> is <see langword="null"/>.</exception>
        /// <exception cref="InvalidOperationException">The source sequence is empty.</exception>
        /// <remarks>The return type of this operator differs from the corresponding operator on IEnumerable in order to retain asynchronous behavior.</remarks>
        public static ValueTask<double> AverageAwaitAsync<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, ValueTask<int>> selector, CancellationToken cancellationToken = default) => AverageAwaitAsyncCore<TSource>(source, selector, cancellationToken);

        /// <summary>
        /// Computes the average of an async-enumerable sequence of <see cref="long"/> values that are obtained by invoking an asynchronous transform function on each element of the source sequence and awaiting the result.
        /// </summary>
        /// <typeparam name="TSource">The type of elements in the source sequence.</typeparam>
        /// <param name="source">An async-enumerable sequence of values to compute the average of.</param>
        /// <param name="selector">A transform function to invoke and await on each element of the source sequence.</param>
        /// <param name="cancellationToken">An optional cancellation token for cancelling the sequence at any time.</param>
        /// <returns>A ValueTask containing the average of the sequence of values.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> or <paramref name="selector"/> is <see langword="null"/>.</exception>
        /// <exception cref="InvalidOperationException">The source sequence is empty.</exception>
        /// <remarks>The return type of this operator differs from the corresponding operator on IEnumerable in order to retain asynchronous behavior.</remarks>
        public static ValueTask<double> AverageAwaitAsync<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, ValueTask<long>> selector, CancellationToken cancellationToken = default) => AverageAwaitAsyncCore<TSource>(source, selector, cancellationToken);

        /// <summary>
        /// Computes the average of an async-enumerable sequence of <see cref="float"/> values that are obtained by invoking an asynchronous transform function on each element of the source sequence and awaiting the result.
        /// </summary>
        /// <typeparam name="TSource">The type of elements in the source sequence.</typeparam>
        /// <param name="source">An async-enumerable sequence of values to compute the average of.</param>
        /// <param name="selector">A transform function to invoke and await on each element of the source sequence.</param>
        /// <param name="cancellationToken">An optional cancellation token for cancelling the sequence at any time.</param>
        /// <returns>A ValueTask containing the average of the sequence of values.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> or <paramref name="selector"/> is <see langword="null"/>.</exception>
        /// <exception cref="InvalidOperationException">The source sequence is empty.</exception>
        /// <remarks>The return type of this operator differs from the corresponding operator on IEnumerable in order to retain asynchronous behavior.</remarks>
        public static ValueTask<float> AverageAwaitAsync<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, ValueTask<float>> selector, CancellationToken cancellationToken = default) => AverageAwaitAsyncCore<TSource>(source, selector, cancellationToken);

        /// <summary>
        /// Computes the average of an async-enumerable sequence of <see cref="double"/> values that are obtained by invoking an asynchronous transform function on each element of the source sequence and awaiting the result.
        /// </summary>
        /// <typeparam name="TSource">The type of elements in the source sequence.</typeparam>
        /// <param name="source">An async-enumerable sequence of values to compute the average of.</param>
        /// <param name="selector">A transform function to invoke and await on each element of the source sequence.</param>
        /// <param name="cancellationToken">An optional cancellation token for cancelling the sequence at any time.</param>
        /// <returns>A ValueTask containing the average of the sequence of values.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> or <paramref name="selector"/> is <see langword="null"/>.</exception>
        /// <exception cref="InvalidOperationException">The source sequence is empty.</exception>
        /// <remarks>The return type of this operator differs from the corresponding operator on IEnumerable in order to retain asynchronous behavior.</remarks>
        public static ValueTask<double> AverageAwaitAsync<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, ValueTask<double>> selector, CancellationToken cancellationToken = default) => AverageAwaitAsyncCore<TSource>(source, selector, cancellationToken);

        /// <summary>
        /// Computes the average of an async-enumerable sequence of <see cref="decimal"/> values that are obtained by invoking an asynchronous transform function on each element of the source sequence and awaiting the result.
        /// </summary>
        /// <typeparam name="TSource">The type of elements in the source sequence.</typeparam>
        /// <param name="source">An async-enumerable sequence of values to compute the average of.</param>
        /// <param name="selector">A transform function to invoke and await on each element of the source sequence.</param>
        /// <param name="cancellationToken">An optional cancellation token for cancelling the sequence at any time.</param>
        /// <returns>A ValueTask containing the average of the sequence of values.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> or <paramref name="selector"/> is <see langword="null"/>.</exception>
        /// <exception cref="InvalidOperationException">The source sequence is empty.</exception>
        /// <remarks>The return type of this operator differs from the corresponding operator on IEnumerable in order to retain asynchronous behavior.</remarks>
        public static ValueTask<decimal> AverageAwaitAsync<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, ValueTask<decimal>> selector, CancellationToken cancellationToken = default) => AverageAwaitAsyncCore<TSource>(source, selector, cancellationToken);

        /// <summary>
        /// Computes the average of an async-enumerable sequence of <see cref="Nullable{Int}"/> values that are obtained by invoking an asynchronous transform function on each element of the source sequence and awaiting the result.
        /// </summary>
        /// <typeparam name="TSource">The type of elements in the source sequence.</typeparam>
        /// <param name="source">An async-enumerable sequence of values to compute the average of.</param>
        /// <param name="selector">A transform function to invoke and await on each element of the source sequence.</param>
        /// <param name="cancellationToken">An optional cancellation token for cancelling the sequence at any time.</param>
        /// <returns>A ValueTask containing the average of the sequence of values, or <see langword="null"/> if the source sequence is empty.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> or <paramref name="selector"/> is <see langword="null"/>.</exception>
        /// <remarks>The return type of this operator differs from the corresponding operator on IEnumerable in order to retain asynchronous behavior.</remarks>
        public static ValueTask<double?> AverageAwaitAsync<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, ValueTask<int?>> selector, CancellationToken cancellationToken = default) => AverageAwaitAsyncCore<TSource>(source, selector, cancellationToken);

        /// <summary>
        /// Computes the average of an async-enumerable sequence of <see cref="Nullable{Long}"/> values that are obtained by invoking an asynchronous transform function on each element of the source sequence and awaiting the result.
        /// </summary>
        /// <typeparam name="TSource">The type of elements in the source sequence.</typeparam>
        /// <param name="source">An async-enumerable sequence of values to compute the average of.</param>
        /// <param name="selector">A transform function to invoke and await on each element of the source sequence.</param>
        /// <param name="cancellationToken">An optional cancellation token for cancelling the sequence at any time.</param>
        /// <returns>A ValueTask containing the average of the sequence of values, or <see langword="null"/> if the source sequence is empty.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> or <paramref name="selector"/> is <see langword="null"/>.</exception>
        /// <remarks>The return type of this operator differs from the corresponding operator on IEnumerable in order to retain asynchronous behavior.</remarks>
        public static ValueTask<double?> AverageAwaitAsync<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, ValueTask<long?>> selector, CancellationToken cancellationToken = default) => AverageAwaitAsyncCore<TSource>(source, selector, cancellationToken);

        /// <summary>
        /// Computes the average of an async-enumerable sequence of <see cref="Nullable{Float}"/> values that are obtained by invoking an asynchronous transform function on each element of the source sequence and awaiting the result.
        /// </summary>
        /// <typeparam name="TSource">The type of elements in the source sequence.</typeparam>
        /// <param name="source">An async-enumerable sequence of values to compute the average of.</param>
        /// <param name="selector">A transform function to invoke and await on each element of the source sequence.</param>
        /// <param name="cancellationToken">An optional cancellation token for cancelling the sequence at any time.</param>
        /// <returns>A ValueTask containing the average of the sequence of values, or <see langword="null"/> if the source sequence is empty.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> or <paramref name="selector"/> is <see langword="null"/>.</exception>
        /// <remarks>The return type of this operator differs from the corresponding operator on IEnumerable in order to retain asynchronous behavior.</remarks>
        public static ValueTask<float?> AverageAwaitAsync<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, ValueTask<float?>> selector, CancellationToken cancellationToken = default) => AverageAwaitAsyncCore<TSource>(source, selector, cancellationToken);

        /// <summary>
        /// Computes the average of an async-enumerable sequence of <see cref="Nullable{Double}"/> values that are obtained by invoking an asynchronous transform function on each element of the source sequence and awaiting the result.
        /// </summary>
        /// <typeparam name="TSource">The type of elements in the source sequence.</typeparam>
        /// <param name="source">An async-enumerable sequence of values to compute the average of.</param>
        /// <param name="selector">A transform function to invoke and await on each element of the source sequence.</param>
        /// <param name="cancellationToken">An optional cancellation token for cancelling the sequence at any time.</param>
        /// <returns>A ValueTask containing the average of the sequence of values, or <see langword="null"/> if the source sequence is empty.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> or <paramref name="selector"/> is <see langword="null"/>.</exception>
        /// <remarks>The return type of this operator differs from the corresponding operator on IEnumerable in order to retain asynchronous behavior.</remarks>
        public static ValueTask<double?> AverageAwaitAsync<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, ValueTask<double?>> selector, CancellationToken cancellationToken = default) => AverageAwaitAsyncCore<TSource>(source, selector, cancellationToken);

        /// <summary>
        /// Computes the average of an async-enumerable sequence of <see cref="Nullable{Decimal}"/> values that are obtained by invoking an asynchronous transform function on each element of the source sequence and awaiting the result.
        /// </summary>
        /// <typeparam name="TSource">The type of elements in the source sequence.</typeparam>
        /// <param name="source">An async-enumerable sequence of values to compute the average of.</param>
        /// <param name="selector">A transform function to invoke and await on each element of the source sequence.</param>
        /// <param name="cancellationToken">An optional cancellation token for cancelling the sequence at any time.</param>
        /// <returns>A ValueTask containing the average of the sequence of values, or <see langword="null"/> if the source sequence is empty.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> or <paramref name="selector"/> is <see langword="null"/>.</exception>
        /// <remarks>The return type of this operator differs from the corresponding operator on IEnumerable in order to retain asynchronous behavior.</remarks>
        public static ValueTask<decimal?> AverageAwaitAsync<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, ValueTask<decimal?>> selector, CancellationToken cancellationToken = default) => AverageAwaitAsyncCore<TSource>(source, selector, cancellationToken);

        /// <summary>
        /// Counts the elements in an async-enumerable sequence that satisfy a condition.
        /// </summary>
        /// <typeparam name="TSource">Type of elements in the source sequence.</typeparam>
        /// <param name="source">A sequence of elements to count.</param>
        /// <param name="predicate">An asynchronous predicate to apply to each element in the source sequence.</param>
        /// <param name="cancellationToken">An optional cancellation token for cancelling the sequence at any time.</param>
        /// <returns>A ValueTask containing the number of elements in the sequence that satisfy the predicate.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> or <paramref name="predicate"/> is <see langword="null"/>.</exception>
        public static ValueTask<int> CountAwaitAsync<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, ValueTask<bool>> predicate, CancellationToken cancellationToken = default) => CountAwaitAsyncCore<TSource>(source, predicate, cancellationToken);

        /// <summary>
        /// Returns the first element of an async-enumerable sequence that satisfies the condition in the predicate.
        /// </summary>
        /// <typeparam name="TSource">The type of elements in the sequence.</typeparam>
        /// <param name="source">Source async-enumerable sequence.</param>
        /// <param name="predicate">An asynchronous predicate that will be invoked and awaited for each element in the sequence.</param>
        /// <param name="cancellationToken">An optional cancellation token for cancelling the sequence at any time.</param>
        /// <returns>A ValueTask containing the first element in the sequence that satisfies the predicate.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> or <paramref name="predicate"/> is <see langword="null"/>.</exception>
        /// <exception cref="InvalidOperationException">No element satisfies the condition in the predicate. -or- The source sequence is empty.</exception>
        public static ValueTask<TSource> FirstAwaitAsync<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, ValueTask<bool>> predicate, CancellationToken cancellationToken = default) => FirstAwaitAsyncCore<TSource>(source, predicate, cancellationToken);

        /// <summary>
        /// Returns the first element of an async-enumerable sequence that satisfies the condition in the predicate, or a default value if no element satisfies the condition in the predicate.
        /// </summary>
        /// <typeparam name="TSource">The type of element in the sequence.</typeparam>
        /// <param name="source">Source async-enumerable sequence.</param>
        /// <param name="predicate">An asynchronous predicate to invoke and await on each element of the sequence.</param>
        /// <param name="cancellationToken">An optional cancellation token for cancelling the sequence at any time.</param>
        /// <returns>A ValueTask containing the first element in the sequence that satisfies the predicate, or a default value if no element satisfies the predicate.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> or <paramref name="predicate"/> is <see langword="null"/>.</exception>
        public static ValueTask<TSource?> FirstOrDefaultAwaitAsync<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, ValueTask<bool>> predicate, CancellationToken cancellationToken = default) => FirstOrDefaultAwaitAsyncCore<TSource>(source, predicate, cancellationToken);

        /// <summary>
        /// Invokes and awaits an asynchronous action on each element in the source sequence, and returns a task that is signaled when the sequence terminates.
        /// </summary>
        /// <typeparam name="TSource">Type of elements in the sequence.</typeparam>
        /// <param name="source">Source sequence.</param>
        /// <param name="action">Asynchronous action to invoke and await for each element in the source sequence.</param>
        /// <param name="cancellationToken">Optional cancellation token for cancelling the sequence at any time.</param>
        /// <returns>Task that signals the termination of the sequence.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> or <paramref name="action"/> is <see langword="null"/>.</exception>
        public static Task ForEachAwaitAsync<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, Task> action, CancellationToken cancellationToken = default) => ForEachAwaitAsyncCore<TSource>(source, action, cancellationToken);

        /// <summary>
        /// Invokes and awaits an asynchronous action on each element in the source sequence, incorporating the element's index, and returns a task that is signaled when the sequence terminates.
        /// </summary>
        /// <typeparam name="TSource">Type of elements in the sequence.</typeparam>
        /// <param name="source">Source sequence.</param>
        /// <param name="action">Asynchronous action to invoke and await for each element in the source sequence; the second parameter represents the index of the element.</param>
        /// <param name="cancellationToken">Optional cancellation token for cancelling the sequence at any time.</param>
        /// <returns>Task that signals the termination of the sequence.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> or <paramref name="action"/> is <see langword="null"/>.</exception>
        public static Task ForEachAwaitAsync<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, int, Task> action, CancellationToken cancellationToken = default) => ForEachAwaitAsyncCore<TSource>(source, action, cancellationToken);

        /// <summary>
        /// Groups the elements of an async-enumerable sequence according to a specified key selector function.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements in the source sequence.</typeparam>
        /// <typeparam name="TKey">The type of the grouping key computed for each element in the source sequence.</typeparam>
        /// <param name="source">An async-enumerable sequence whose elements to group.</param>
        /// <param name="keySelector">An asynchronous function to extract the key for each element.</param>
        /// <returns>A sequence of async-enumerable groups, each of which corresponds to a unique key value, containing all elements that share that same key value.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> or <paramref name="keySelector"/> is <see langword="null"/>.</exception>
        public static IAsyncEnumerable<IAsyncGrouping<TKey, TSource>> GroupByAwait<TSource, TKey>(this IAsyncEnumerable<TSource> source, Func<TSource, ValueTask<TKey>> keySelector) => GroupByAwaitCore<TSource, TKey>(source, keySelector);

        /// <summary>
        /// Groups the elements of an async-enumerable sequence according to a specified key selector function and comparer.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements in the source sequence.</typeparam>
        /// <typeparam name="TKey">The type of the grouping key computed for each element in the source sequence.</typeparam>
        /// <param name="source">An async-enumerable sequence whose elements to group.</param>
        /// <param name="keySelector">An asynchronous function to extract the key for each element.</param>
        /// <param name="comparer">An equality comparer to compare keys with.</param>
        /// <returns>A sequence of async-enumerable groups, each of which corresponds to a unique key value, containing all elements that share that same key value.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> or <paramref name="keySelector"/> or <paramref name="comparer"/> is <see langword="null"/>.</exception>
        public static IAsyncEnumerable<IAsyncGrouping<TKey, TSource>> GroupByAwait<TSource, TKey>(this IAsyncEnumerable<TSource> source, Func<TSource, ValueTask<TKey>> keySelector, IEqualityComparer<TKey> comparer) => GroupByAwaitCore<TSource, TKey>(source, keySelector, comparer);

        /// <summary>
        /// Groups the elements of an async-enumerable sequence and selects the resulting elements by using a specified function.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements in the source sequence.</typeparam>
        /// <typeparam name="TKey">The type of the grouping key computed for each element in the source sequence.</typeparam>
        /// <typeparam name="TElement">The type of the elements within the groups computed for each element in the source sequence.</typeparam>
        /// <param name="source">An async-enumerable sequence whose elements to group.</param>
        /// <param name="keySelector">An asynchronous function to extract the key for each element.</param>
        /// <param name="elementSelector">An asynchronous function to map each source element to an element in an async-enumerable group.</param>
        /// <returns>A sequence of async-enumerable groups, each of which corresponds to a unique key value, containing all elements that share that same key value.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> or <paramref name="keySelector"/> or <paramref name="elementSelector"/> is <see langword="null"/>.</exception>
        public static IAsyncEnumerable<IAsyncGrouping<TKey, TElement>> GroupByAwait<TSource, TKey, TElement>(this IAsyncEnumerable<TSource> source, Func<TSource, ValueTask<TKey>> keySelector, Func<TSource, ValueTask<TElement>> elementSelector) => GroupByAwaitCore<TSource, TKey, TElement>(source, keySelector, elementSelector);

        /// <summary>
        /// Groups the elements of an async-enumerable sequence according to a specified key selector function, and then applies a result selector function to each group.
        /// </summary>
        /// <typeparam name="TSource">Type of element in the source sequence.</typeparam>
        /// <typeparam name="TKey">Type of the grouping key computed for each element in the source sequence.</typeparam>
        /// <typeparam name="TResult">The result type returned by the result selector function.</typeparam>
        /// <param name="source">An async-enumerable sequence whose elements to group.</param>
        /// <param name="keySelector">An asynchronous function to extract the key for each element.</param>
        /// <param name="resultSelector">An asynchronous function to transform each group into the result type.</param>
        /// <returns>An async-enumerable sequence of results obtained by invoking and awaiting the result-selector function on each group.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> or <paramref name="keySelector"/> or <paramref name="resultSelector"/> is <see langword="null"/>.</exception>
        public static IAsyncEnumerable<TResult> GroupByAwait<TSource, TKey, TResult>(this IAsyncEnumerable<TSource> source, Func<TSource, ValueTask<TKey>> keySelector, Func<TKey, IAsyncEnumerable<TSource>, ValueTask<TResult>> resultSelector) => GroupByAwaitCore<TSource, TKey, TResult>(source, keySelector, resultSelector);

        /// <summary>
        /// Groups the elements of an async-enumerable sequence according to a specified key-selector function, applies an element selector to each element of each group, then applies a result selector to each transformed group.
        /// </summary>
        /// <typeparam name="TSource">The type of element in the source sequence.</typeparam>
        /// <typeparam name="TKey">The type of the grouping key computed for each element in the source sequence.</typeparam>
        /// <typeparam name="TElement">The type of element computed by the element selector.</typeparam>
        /// <typeparam name="TResult">The type of the final result, computed by applying the result selector to each transformed group of elements.</typeparam>
        /// <param name="source">An async-enumerable sequence whose elements to group.</param>
        /// <param name="keySelector">An asynchronous function to extract the key for each element.</param>
        /// <param name="elementSelector">An asynchronous function to apply to each element of each group. </param>
        /// <param name="resultSelector">An asynchronous function to transform each group into the result type.</param>
        /// <returns>An async-enumerable sequence of results obtained by invoking the result selector function on each group and awaiting the result.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> or <paramref name="keySelector"/> or <paramref name="elementSelector"/> or <paramref name="resultSelector"/> is <see langword="null"/>.</exception>
        public static IAsyncEnumerable<TResult> GroupByAwait<TSource, TKey, TElement, TResult>(this IAsyncEnumerable<TSource> source, Func<TSource, ValueTask<TKey>> keySelector, Func<TSource, ValueTask<TElement>> elementSelector, Func<TKey, IAsyncEnumerable<TElement>, ValueTask<TResult>> resultSelector) => GroupByAwaitCore<TSource, TKey, TElement, TResult>(source, keySelector, elementSelector, resultSelector);

        /// <summary>
        /// Groups the elements of an async-enumerable sequence and selects the resulting elements by using a specified function.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements in the source sequence.</typeparam>
        /// <typeparam name="TKey">The type of the grouping key computed for each element in the source sequence.</typeparam>
        /// <typeparam name="TElement">The type of the elements within the groups computed for each element in the source sequence.</typeparam>
        /// <param name="source">An async-enumerable sequence whose elements to group.</param>
        /// <param name="keySelector">An asynchronous function to extract the key for each element.</param>
        /// <param name="elementSelector">An asynchronous function to map each source element to an element in an async-enumerable group.</param>
        /// <param name="comparer">An equality comparer to use to compare keys.</param>
        /// <returns>A sequence of async-enumerable groups, each of which corresponds to a unique key value, containing all elements that share that same key value.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> or <paramref name="keySelector"/> or <paramref name="elementSelector"/> or <paramref name="comparer"/> is <see langword="null"/>.</exception>
        public static IAsyncEnumerable<IAsyncGrouping<TKey, TElement>> GroupByAwait<TSource, TKey, TElement>(this IAsyncEnumerable<TSource> source, Func<TSource, ValueTask<TKey>> keySelector, Func<TSource, ValueTask<TElement>> elementSelector, IEqualityComparer<TKey> comparer) => GroupByAwaitCore<TSource, TKey, TElement>(source, keySelector, elementSelector, comparer);

        /// <summary>
        /// Groups the elements of an async-enumerable sequence according to a specified key selector function, and then applies a result selector function to each group.
        /// </summary>
        /// <typeparam name="TSource">Type of element in the source sequence.</typeparam>
        /// <typeparam name="TKey">Type of the grouping key computed for each element in the source sequence.</typeparam>
        /// <typeparam name="TResult">The result type returned by the result selector function.</typeparam>
        /// <param name="source">An async-enumerable sequence whose elements to group.</param>
        /// <param name="keySelector">An asynchronous function to extract the key for each element.</param>
        /// <param name="resultSelector">An asynchronous function to transform each group into the result type.</param>
        /// <param name="comparer">An equality comparer to use to compare keys.</param>
        /// <returns>An async-enumerable sequence of results obtained by invoking and awaiting the result-selector function on each group.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> or <paramref name="keySelector"/> or <paramref name="resultSelector"/> or <paramref name="comparer"/> is <see langword="null"/>.</exception>
        public static IAsyncEnumerable<TResult> GroupByAwait<TSource, TKey, TResult>(this IAsyncEnumerable<TSource> source, Func<TSource, ValueTask<TKey>> keySelector, Func<TKey, IAsyncEnumerable<TSource>, ValueTask<TResult>> resultSelector, IEqualityComparer<TKey> comparer) => GroupByAwaitCore<TSource, TKey, TResult>(source, keySelector, resultSelector, comparer);

        /// <summary>
        /// Groups the elements of an async-enumerable sequence according to a specified key-selector function, applies an element selector to each element of each group, then applies a result selector to each transformed group.
        /// </summary>
        /// <typeparam name="TSource">The type of element in the source sequence.</typeparam>
        /// <typeparam name="TKey">The type of the grouping key computed for each element in the source sequence.</typeparam>
        /// <typeparam name="TElement">The type of element computed by the element selector.</typeparam>
        /// <typeparam name="TResult">The type of the final result, computed by applying the result selector to each transformed group of elements.</typeparam>
        /// <param name="source">An async-enumerable sequence whose elements to group.</param>
        /// <param name="keySelector">An asynchronous function to extract the key for each element.</param>
        /// <param name="elementSelector">An asynchronous function to apply to each element of each group. </param>
        /// <param name="resultSelector">An asynchronous function to transform each group into the result type.</param>
        /// <param name="comparer">An equality comparer to use to compare keys.</param>
        /// <returns>An async-enumerable sequence of results obtained by invoking the result selector function on each group and awaiting the result.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> or <paramref name="keySelector"/> or <paramref name="elementSelector"/> or <paramref name="resultSelector"/> or <paramref name="comparer"/> is <see langword="null"/>.</exception>
        public static IAsyncEnumerable<TResult> GroupByAwait<TSource, TKey, TElement, TResult>(this IAsyncEnumerable<TSource> source, Func<TSource, ValueTask<TKey>> keySelector, Func<TSource, ValueTask<TElement>> elementSelector, Func<TKey, IAsyncEnumerable<TElement>, ValueTask<TResult>> resultSelector, IEqualityComparer<TKey> comparer) => GroupByAwaitCore<TSource, TKey, TElement, TResult>(source, keySelector, elementSelector, resultSelector, comparer);
        public static IAsyncEnumerable<TResult> GroupJoinAwait<TOuter, TInner, TKey, TResult>(this IAsyncEnumerable<TOuter> outer, IAsyncEnumerable<TInner> inner, Func<TOuter, ValueTask<TKey>> outerKeySelector, Func<TInner, ValueTask<TKey>> innerKeySelector, Func<TOuter, IAsyncEnumerable<TInner>, ValueTask<TResult>> resultSelector) => GroupJoinAwaitCore<TOuter, TInner, TKey, TResult>(outer, inner, outerKeySelector, innerKeySelector, resultSelector);
        public static IAsyncEnumerable<TResult> GroupJoinAwait<TOuter, TInner, TKey, TResult>(this IAsyncEnumerable<TOuter> outer, IAsyncEnumerable<TInner> inner, Func<TOuter, ValueTask<TKey>> outerKeySelector, Func<TInner, ValueTask<TKey>> innerKeySelector, Func<TOuter, IAsyncEnumerable<TInner>, ValueTask<TResult>> resultSelector, IEqualityComparer<TKey> comparer) => GroupJoinAwaitCore<TOuter, TInner, TKey, TResult>(outer, inner, outerKeySelector, innerKeySelector, resultSelector, comparer);
        public static IAsyncEnumerable<TResult> JoinAwait<TOuter, TInner, TKey, TResult>(this IAsyncEnumerable<TOuter> outer, IAsyncEnumerable<TInner> inner, Func<TOuter, ValueTask<TKey>> outerKeySelector, Func<TInner, ValueTask<TKey>> innerKeySelector, Func<TOuter, TInner, ValueTask<TResult>> resultSelector) => JoinAwaitCore<TOuter, TInner, TKey, TResult>(outer, inner, outerKeySelector, innerKeySelector, resultSelector);
        public static IAsyncEnumerable<TResult> JoinAwait<TOuter, TInner, TKey, TResult>(this IAsyncEnumerable<TOuter> outer, IAsyncEnumerable<TInner> inner, Func<TOuter, ValueTask<TKey>> outerKeySelector, Func<TInner, ValueTask<TKey>> innerKeySelector, Func<TOuter, TInner, ValueTask<TResult>> resultSelector, IEqualityComparer<TKey> comparer) => JoinAwaitCore<TOuter, TInner, TKey, TResult>(outer, inner, outerKeySelector, innerKeySelector, resultSelector, comparer);

        /// <summary>
        /// Returns the last element of an async-enumerable sequence that satisfies the condition in the predicate.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements in the source sequence.</typeparam>
        /// <param name="source">Source async-enumerable sequence.</param>
        /// <param name="predicate">An asynchronous predicate function to evaluate for elements in the source sequence.</param>
        /// <param name="cancellationToken">The optional cancellation token to be used for cancelling the sequence at any time.</param>
        /// <returns>ValueTask containing the last element in the async-enumerable sequence that satisfies the condition in the predicate.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> or <paramref name="predicate"/> is null.</exception>
        /// <exception cref="InvalidOperationException">(Asynchronous) No element satisfies the condition in the predicate. -or- The source sequence is empty.</exception>
        public static ValueTask<TSource> LastAwaitAsync<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, ValueTask<bool>> predicate, CancellationToken cancellationToken = default) => LastAwaitAsyncCore<TSource>(source, predicate, cancellationToken);

        /// <summary>
        /// Returns the last element of an async-enumerable sequence that satisfies the condition in the predicate, or a default value if no such element exists.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements in the source sequence.</typeparam>
        /// <param name="source">Source async-enumerable sequence.</param>
        /// <param name="predicate">An asynchronous predicate function to evaluate for elements in the source sequence.</param>
        /// <param name="cancellationToken">The optional cancellation token to be used for cancelling the sequence at any time.</param>
        /// <returns>ValueTask containing the last element in the async-enumerable sequence that satisfies the condition in the predicate, or a default value if no such element exists.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> or <paramref name="predicate"/> is null.</exception>
        public static ValueTask<TSource?> LastOrDefaultAwaitAsync<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, ValueTask<bool>> predicate, CancellationToken cancellationToken = default) => LastOrDefaultAwaitAsyncCore<TSource>(source, predicate, cancellationToken);

        /// <summary>
        /// Returns an async-enumerable sequence containing a <see cref="long" /> that represents the number of elements in the specified async-enumerable sequence that satisfy a condition.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements in the source sequence.</typeparam>
        /// <param name="source">An async-enumerable sequence that contains elements to be counted.</param>
        /// <param name="predicate">An asynchronous predicate to test each element for a condition.</param>
        /// <param name="cancellationToken">The optional cancellation token to be used for cancelling the sequence at any time.</param>
        /// <returns>An async-enumerable sequence containing a single element with a number that represents how many elements in the input sequence satisfy the condition in the predicate function.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> or <paramref name="predicate"/> is null.</exception>
        /// <remarks>The return type of this operator differs from the corresponding operator on IEnumerable in order to retain asynchronous behavior.</remarks>
        public static ValueTask<long> LongCountAwaitAsync<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, ValueTask<bool>> predicate, CancellationToken cancellationToken = default) => LongCountAwaitAsyncCore<TSource>(source, predicate, cancellationToken);

        /// <summary>
        /// Invokes and awaits a transform function on each element of a sequence and returns the maximum <see cref="long"/> value.
        /// </summary>
        /// <typeparam name="TSource">Type of elements in the source sequence.</typeparam>
        /// <param name="source">The source sequence.</param>
        /// <param name="selector">An asynchronous transform function to invoke and await on each element of the source.</param>
        /// <param name="cancellationToken">The optional cancellation token to be usef for cancelling the sequence at any time.</param>
        /// <returns>A ValueTask containing the maximum value in the sequence.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> or <paramref name="selector"/> is <see langword="null"/>.</exception>
        public static ValueTask<long> MaxAwaitAsync<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, ValueTask<long>> selector, CancellationToken cancellationToken = default) => MaxAwaitAsyncCore<TSource>(source, selector, cancellationToken);

        /// <summary>
        /// Invokes and awaits a transform function on each element of a sequence and returns the maximum <see cref="Nullable{Long}"/> value.
        /// </summary>
        /// <typeparam name="TSource">Type of elements in the source sequence.</typeparam>
        /// <param name="source">The source sequence.</param>
        /// <param name="selector">An asynchronous transform function to invoke and await on each element of the source.</param>
        /// <param name="cancellationToken">The optional cancellation token to be usef for cancelling the sequence at any time.</param>
        /// <returns>A ValueTask containing the maximum value in the sequence.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> or <paramref name="selector"/> is <see langword="null"/>.</exception>
        public static ValueTask<long?> MaxAwaitAsync<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, ValueTask<long?>> selector, CancellationToken cancellationToken = default) => MaxAwaitAsyncCore<TSource>(source, selector, cancellationToken);

        /// <summary>
        /// Invokes and awaits a transform function on each element of a sequence and returns the maximum <see cref="float"/> value.
        /// </summary>
        /// <typeparam name="TSource">Type of elements in the source sequence.</typeparam>
        /// <param name="source">The source sequence.</param>
        /// <param name="selector">An asynchronous transform function to invoke and await on each element of the source.</param>
        /// <param name="cancellationToken">The optional cancellation token to be usef for cancelling the sequence at any time.</param>
        /// <returns>A ValueTask containing the maximum value in the sequence.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> or <paramref name="selector"/> is <see langword="null"/>.</exception>
        public static ValueTask<float> MaxAwaitAsync<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, ValueTask<float>> selector, CancellationToken cancellationToken = default) => MaxAwaitAsyncCore<TSource>(source, selector, cancellationToken);

        /// <summary>
        /// Invokes and awaits a transform function on each element of a sequence and returns the maximum <see cref="Nullable{Float}"/> value.
        /// </summary>
        /// <typeparam name="TSource">Type of elements in the source sequence.</typeparam>
        /// <param name="source">The source sequence.</param>
        /// <param name="selector">An asynchronous transform function to invoke and await on each element of the source.</param>
        /// <param name="cancellationToken">The optional cancellation token to be usef for cancelling the sequence at any time.</param>
        /// <returns>A ValueTask containing the maximum value in the sequence.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> or <paramref name="selector"/> is <see langword="null"/>.</exception>
        public static ValueTask<float?> MaxAwaitAsync<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, ValueTask<float?>> selector, CancellationToken cancellationToken = default) => MaxAwaitAsyncCore<TSource>(source, selector, cancellationToken);

        /// <summary>
        /// Invokes and awaits a transform function on each element of a sequence and returns the maximum <see cref="double"/> value.
        /// </summary>
        /// <typeparam name="TSource">Type of elements in the source sequence.</typeparam>
        /// <param name="source">The source sequence.</param>
        /// <param name="selector">An asynchronous transform function to invoke and await on each element of the source.</param>
        /// <param name="cancellationToken">The optional cancellation token to be usef for cancelling the sequence at any time.</param>
        /// <returns>A ValueTask containing the maximum value in the sequence.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> or <paramref name="selector"/> is <see langword="null"/>.</exception>
        public static ValueTask<double> MaxAwaitAsync<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, ValueTask<double>> selector, CancellationToken cancellationToken = default) => MaxAwaitAsyncCore<TSource>(source, selector, cancellationToken);

        /// <summary>
        /// Invokes and awaits a transform function on each element of a sequence and returns the maximum <see cref="Nullable{Double}"/> value.
        /// </summary>
        /// <typeparam name="TSource">Type of elements in the source sequence.</typeparam>
        /// <param name="source">The source sequence.</param>
        /// <param name="selector">An asynchronous transform function to invoke and await on each element of the source.</param>
        /// <param name="cancellationToken">The optional cancellation token to be usef for cancelling the sequence at any time.</param>
        /// <returns>A ValueTask containing the maximum value in the sequence.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> or <paramref name="selector"/> is <see langword="null"/>.</exception>
        public static ValueTask<double?> MaxAwaitAsync<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, ValueTask<double?>> selector, CancellationToken cancellationToken = default) => MaxAwaitAsyncCore<TSource>(source, selector, cancellationToken);

        /// <summary>
        /// Invokes and awaits a transform function on each element of a sequence and returns the maximum <see cref="decimal"/> value.
        /// </summary>
        /// <typeparam name="TSource">Type of elements in the source sequence.</typeparam>
        /// <param name="source">The source sequence.</param>
        /// <param name="selector">An asynchronous transform function to invoke and await on each element of the source.</param>
        /// <param name="cancellationToken">The optional cancellation token to be usef for cancelling the sequence at any time.</param>
        /// <returns>A ValueTask containing the maximum value in the sequence.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> or <paramref name="selector"/> is <see langword="null"/>.</exception>
        public static ValueTask<decimal> MaxAwaitAsync<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, ValueTask<decimal>> selector, CancellationToken cancellationToken = default) => MaxAwaitAsyncCore<TSource>(source, selector, cancellationToken);

        /// <summary>
        /// Invokes and awaits a transform function on each element of a sequence and returns the maximum <see cref="Nullable{Decimal}"/> value.
        /// </summary>
        /// <typeparam name="TSource">Type of elements in the source sequence.</typeparam>
        /// <param name="source">The source sequence.</param>
        /// <param name="selector">An asynchronous transform function to invoke and await on each element of the source.</param>
        /// <param name="cancellationToken">The optional cancellation token to be usef for cancelling the sequence at any time.</param>
        /// <returns>A ValueTask containing the maximum value in the sequence.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> or <paramref name="selector"/> is <see langword="null"/>.</exception>
        public static ValueTask<decimal?> MaxAwaitAsync<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, ValueTask<decimal?>> selector, CancellationToken cancellationToken = default) => MaxAwaitAsyncCore<TSource>(source, selector, cancellationToken);

        /// <summary>
        /// Invokes and awaits a transform function on each element of a sequence and returns the maximum value.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements in the source sequence.</typeparam>
        /// <typeparam name="TResult">The type of the objects derived from the elements in the source sequence to determine the maximum of.</typeparam>
        /// <param name="source">An async-enumerable sequence to determine the minimum element of.</param>
        /// <param name="selector">An asynchronous transform function to invoke and await on each element.</param>
        /// <param name="cancellationToken">The optional cancellation token to be used for cancelling the sequence at any time.</param>
        /// <returns>A ValueTask containing a single element with the value that corresponds to the maximum element in the source sequence.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> or <paramref name="selector"/> is null.</exception>
        /// <remarks>The return type of this operator differs from the corresponding operator on IEnumerable in order to retain asynchronous behavior.</remarks>
        public static ValueTask<TResult> MaxAwaitAsync<TSource, TResult>(this IAsyncEnumerable<TSource> source, Func<TSource, ValueTask<TResult>> selector, CancellationToken cancellationToken = default) => MaxAwaitAsyncCore<TSource, TResult>(source, selector, cancellationToken);

        /// <summary>
        /// Returns the maximum <see cref="int"/> value in an async-enumerable sequence.
        /// </summary>
        /// <typeparam name="TSource">Type of elements in the source sequence.</typeparam>
        /// <param name="source">The source sequence.</param>
        /// <param name="selector">An asynchronous transform function to invoke and await on each element of the source.</param>
        /// <param name="cancellationToken">The optional cancellation token to be usef for cancelling the sequence at any time.</param>
        /// <returns>A ValueTask containing the maximum value in the sequence.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> or <paramref name="selector"/> is <see langword="null"/>.</exception>
        public static ValueTask<int> MaxAwaitAsync<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, ValueTask<int>> selector, CancellationToken cancellationToken = default) => MaxAwaitAsyncCore<TSource>(source, selector, cancellationToken);

        /// <summary>
        /// Returns the maximum <see cref="Nullable{Int}"/> value in an async-enumerable sequence.
        /// </summary>
        /// <typeparam name="TSource">Type of elements in the source sequence.</typeparam>
        /// <param name="source">The source sequence.</param>
        /// <param name="selector">An asynchronous transform function to invoke and await on each element of the source.</param>
        /// <param name="cancellationToken">The optional cancellation token to be usef for cancelling the sequence at any time.</param>
        /// <returns>A ValueTask containing the maximum value in the sequence.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> or <paramref name="selector"/> is <see langword="null"/>.</exception>
        public static ValueTask<int?> MaxAwaitAsync<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, ValueTask<int?>> selector, CancellationToken cancellationToken = default) => MaxAwaitAsyncCore<TSource>(source, selector, cancellationToken);

        /// <summary>
        /// Invokes and awaits a transform function on each element of a sequence and returns the minimum <see cref="double"/> value.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements in the source sequence.</typeparam>
        /// <param name="source">An async-enumerable sequence to determine the minimum element of.</param>
        /// <param name="selector">An asynchronous transform function to invoke and await on each element.</param>
        /// <param name="cancellationToken">The optional cancellation token to be used for cancelling the sequence at any time.</param>
        /// <returns>A ValueTask sequence containing a single element with the value that corresponds to the minimum element in the source sequence.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> or <paramref name="selector"/> is null.</exception>
        /// <remarks>The return type of this operator differs from the corresponding operator on IEnumerable in order to retain asynchronous behavior.</remarks>
        public static ValueTask<double> MinAwaitAsync<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, ValueTask<double>> selector, CancellationToken cancellationToken = default) => MinAwaitAsyncCore<TSource>(source, selector, cancellationToken);

        /// <summary>
        /// Invokes and awaits a transform function on each element of a sequence and returns the minimum <see cref="Nullable{Double}"/> value.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements in the source sequence.</typeparam>
        /// <param name="source">An async-enumerable sequence to determine the minimum element of.</param>
        /// <param name="selector">An asynchronous transform function to invoke and await on each element.</param>
        /// <param name="cancellationToken">The optional cancellation token to be used for cancelling the sequence at any time.</param>
        /// <returns>A ValueTask sequence containing a single element with the value that corresponds to the minimum element in the source sequence.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> or <paramref name="selector"/> is null.</exception>
        /// <remarks>The return type of this operator differs from the corresponding operator on IEnumerable in order to retain asynchronous behavior.</remarks>
        public static ValueTask<double?> MinAwaitAsync<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, ValueTask<double?>> selector, CancellationToken cancellationToken = default) => MinAwaitAsyncCore<TSource>(source, selector, cancellationToken);

        /// <summary>
        /// Invokes and awaits a transform function on each element of a sequence and returns the minimum <see cref="decimal"/> value.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements in the source sequence.</typeparam>
        /// <param name="source">An async-enumerable sequence to determine the minimum element of.</param>
        /// <param name="selector">An asynchronous transform function to invoke and await on each element.</param>
        /// <param name="cancellationToken">The optional cancellation token to be used for cancelling the sequence at any time.</param>
        /// <returns>A ValueTask sequence containing a single element with the value that corresponds to the minimum element in the source sequence.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> or <paramref name="selector"/> is null.</exception>
        /// <remarks>The return type of this operator differs from the corresponding operator on IEnumerable in order to retain asynchronous behavior.</remarks>
        public static ValueTask<decimal> MinAwaitAsync<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, ValueTask<decimal>> selector, CancellationToken cancellationToken = default) => MinAwaitAsyncCore<TSource>(source, selector, cancellationToken);

        /// <summary>
        /// Invokes and awaits a transform function on each element of a sequence and returns the minimum <see cref="Nullable{Decimal}"/> value.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements in the source sequence.</typeparam>
        /// <param name="source">An async-enumerable sequence to determine the minimum element of.</param>
        /// <param name="selector">An asynchronous transform function to invoke and await on each element.</param>
        /// <param name="cancellationToken">The optional cancellation token to be used for cancelling the sequence at any time.</param>
        /// <returns>A ValueTask sequence containing a single element with the value that corresponds to the minimum element in the source sequence.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> or <paramref name="selector"/> is null.</exception>
        /// <remarks>The return type of this operator differs from the corresponding operator on IEnumerable in order to retain asynchronous behavior.</remarks>
        public static ValueTask<decimal?> MinAwaitAsync<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, ValueTask<decimal?>> selector, CancellationToken cancellationToken = default) => MinAwaitAsyncCore<TSource>(source, selector, cancellationToken);

        /// <summary>
        /// Invokes and awaits a transform function on each element of a sequence and returns the minimum <see cref="int"/> value.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements in the source sequence.</typeparam>
        /// <param name="source">An async-enumerable sequence to determine the minimum element of.</param>
        /// <param name="selector">An asynchronous transform function to invoke and await on each element.</param>
        /// <param name="cancellationToken">The optional cancellation token to be used for cancelling the sequence at any time.</param>
        /// <returns>A ValueTask sequence containing a single element with the value that corresponds to the minimum element in the source sequence.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> or <paramref name="selector"/> is null.</exception>
        /// <remarks>The return type of this operator differs from the corresponding operator on IEnumerable in order to retain asynchronous behavior.</remarks>
        public static ValueTask<int> MinAwaitAsync<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, ValueTask<int>> selector, CancellationToken cancellationToken = default) => MinAwaitAsyncCore<TSource>(source, selector, cancellationToken);

        /// <summary>
        /// Invokes and awaits a transform function on each element of a sequence and returns the minimum <see cref="Nullable{Int}"/> value.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements in the source sequence.</typeparam>
        /// <param name="source">An async-enumerable sequence to determine the minimum element of.</param>
        /// <param name="selector">An asynchronous transform function to invoke and await on each element.</param>
        /// <param name="cancellationToken">The optional cancellation token to be used for cancelling the sequence at any time.</param>
        /// <returns>A ValueTask sequence containing a single element with the value that corresponds to the minimum element in the source sequence.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> or <paramref name="selector"/> is null.</exception>
        /// <remarks>The return type of this operator differs from the corresponding operator on IEnumerable in order to retain asynchronous behavior.</remarks>
        public static ValueTask<int?> MinAwaitAsync<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, ValueTask<int?>> selector, CancellationToken cancellationToken = default) => MinAwaitAsyncCore<TSource>(source, selector, cancellationToken);

        /// <summary>
        /// Invokes and awaits a transform function on each element of a sequence and returns the minimum <see cref="long"/> value.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements in the source sequence.</typeparam>
        /// <param name="source">An async-enumerable sequence to determine the minimum element of.</param>
        /// <param name="selector">An asynchronous transform function to invoke and await on each element.</param>
        /// <param name="cancellationToken">The optional cancellation token to be used for cancelling the sequence at any time.</param>
        /// <returns>A ValueTask sequence containing a single element with the value that corresponds to the minimum element in the source sequence.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> or <paramref name="selector"/> is null.</exception>
        /// <remarks>The return type of this operator differs from the corresponding operator on IEnumerable in order to retain asynchronous behavior.</remarks>
        public static ValueTask<long> MinAwaitAsync<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, ValueTask<long>> selector, CancellationToken cancellationToken = default) => MinAwaitAsyncCore<TSource>(source, selector, cancellationToken);

        /// <summary>
        /// Invokes and awaits a transform function on each element of a sequence and returns the minimum <see cref="Nullable{Long}"/> value.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements in the source sequence.</typeparam>
        /// <param name="source">An async-enumerable sequence to determine the minimum element of.</param>
        /// <param name="selector">An asynchronous transform function to invoke and await on each element.</param>
        /// <param name="cancellationToken">The optional cancellation token to be used for cancelling the sequence at any time.</param>
        /// <returns>A ValueTask sequence containing a single element with the value that corresponds to the minimum element in the source sequence.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> or <paramref name="selector"/> is null.</exception>
        /// <remarks>The return type of this operator differs from the corresponding operator on IEnumerable in order to retain asynchronous behavior.</remarks>
        public static ValueTask<long?> MinAwaitAsync<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, ValueTask<long?>> selector, CancellationToken cancellationToken = default) => MinAwaitAsyncCore<TSource>(source, selector, cancellationToken);

        /// <summary>
        /// Invokes and awaits a transform function on each element of a sequence and returns the minimum <see cref="float"/> value.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements in the source sequence.</typeparam>
        /// <param name="source">An async-enumerable sequence to determine the minimum element of.</param>
        /// <param name="selector">An asynchronous transform function to invoke and await on each element.</param>
        /// <param name="cancellationToken">The optional cancellation token to be used for cancelling the sequence at any time.</param>
        /// <returns>A ValueTask sequence containing a single element with the value that corresponds to the minimum element in the source sequence.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> or <paramref name="selector"/> is null.</exception>
        /// <remarks>The return type of this operator differs from the corresponding operator on IEnumerable in order to retain asynchronous behavior.</remarks>
        public static ValueTask<float> MinAwaitAsync<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, ValueTask<float>> selector, CancellationToken cancellationToken = default) => MinAwaitAsyncCore<TSource>(source, selector, cancellationToken);

        /// <summary>
        /// Invokes and awaits a transform function on each element of a sequence and returns the minimum <see cref="Nullable{Float}"/> value.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements in the source sequence.</typeparam>
        /// <param name="source">An async-enumerable sequence to determine the minimum element of.</param>
        /// <param name="selector">An asynchronous transform function to invoke and await on each element.</param>
        /// <param name="cancellationToken">The optional cancellation token to be used for cancelling the sequence at any time.</param>
        /// <returns>A ValueTask sequence containing a single element with the value that corresponds to the minimum element in the source sequence.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> or <paramref name="selector"/> is null.</exception>
        /// <remarks>The return type of this operator differs from the corresponding operator on IEnumerable in order to retain asynchronous behavior.</remarks>
        public static ValueTask<float?> MinAwaitAsync<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, ValueTask<float?>> selector, CancellationToken cancellationToken = default) => MinAwaitAsyncCore<TSource>(source, selector, cancellationToken);

        /// <summary>
        /// Invokes and awaits a transform function on each element of a sequence and returns the minimum value.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements in the source sequence.</typeparam>
        /// <typeparam name="TResult">The type of the objects derived from the elements in the source sequence to determine the minimum of.</typeparam>
        /// <param name="source">An async-enumerable sequence to determine the minimum element of.</param>
        /// <param name="selector">An asynchronous transform function to invoke and await on each element.</param>
        /// <param name="cancellationToken">The optional cancellation token to be used for cancelling the sequence at any time.</param>
        /// <returns>A ValueTask sequence containing a single element with the value that corresponds to the minimum element in the source sequence.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> or <paramref name="selector"/> is null.</exception>
        /// <remarks>The return type of this operator differs from the corresponding operator on IEnumerable in order to retain asynchronous behavior.</remarks>
        public static ValueTask<TResult> MinAwaitAsync<TSource, TResult>(this IAsyncEnumerable<TSource> source, Func<TSource, ValueTask<TResult>> selector, CancellationToken cancellationToken = default) => MinAwaitAsyncCore<TSource, TResult>(source, selector, cancellationToken);

        /// <summary>
        /// Sorts the elements of a sequence in ascending order according to a key obtained by invoking a transform function on each element and awaiting the result.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of source.</typeparam>
        /// <typeparam name="TKey">The type of the key returned by keySelector.</typeparam>
        /// <param name="source">An async-enumerable sequence of values to order.</param>
        /// <param name="keySelector">An asynchronous function to extract a key from an element.</param>
        /// <returns>An ordered async-enumerable sequence whose elements are sorted according to a key.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> or <paramref name="keySelector"/> is null.</exception>
        public static IOrderedAsyncEnumerable<TSource> OrderByAwait<TSource, TKey>(this IAsyncEnumerable<TSource> source, Func<TSource, ValueTask<TKey>> keySelector) => OrderByAwaitCore<TSource, TKey>(source, keySelector);

        /// <summary>
        /// Sorts the elements of a sequence in ascending order by using a specified comparer. The keys are obtained by invoking the transform function on each element and awaiting the result.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of source.</typeparam>
        /// <typeparam name="TKey">The type of the key returned by keySelector.</typeparam>
        /// <param name="source">An async-enumerable sequence of values to order.</param>
        /// <param name="keySelector">An asynchronous function to extract a key from an element.</param>
        /// <param name="comparer">A comparer to compare keys.</param>
        /// <returns>An ordered async-enumerable sequence whose elements are sorted according to a key.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> or <paramref name="keySelector"/> is null.</exception>
        public static IOrderedAsyncEnumerable<TSource> OrderByAwait<TSource, TKey>(this IAsyncEnumerable<TSource> source, Func<TSource, ValueTask<TKey>> keySelector, IComparer<TKey> comparer) => OrderByAwaitCore<TSource, TKey>(source, keySelector, comparer);

        /// <summary>
        /// Sorts the elements of a sequence in descending order according to a key obtained by invoking a transform function on each element and awaiting the result.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of source.</typeparam>
        /// <typeparam name="TKey">The type of the key returned by keySelector.</typeparam>
        /// <param name="source">An async-enumerable sequence of values to order.</param>
        /// <param name="keySelector">An asynchronous function to extract a key from an element.</param>
        /// <returns>An ordered async-enumerable sequence whose elements are sorted in descending order according to a key.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> or <paramref name="keySelector"/> is null.</exception>
        public static IOrderedAsyncEnumerable<TSource> OrderByDescendingAwait<TSource, TKey>(this IAsyncEnumerable<TSource> source, Func<TSource, ValueTask<TKey>> keySelector) => OrderByDescendingAwaitCore<TSource, TKey>(source, keySelector);

        /// <summary>
        /// Sorts the elements of a sequence in descending order by using a specified comparer. The keys are obtained by invoking the transform function on each element and awaiting the result.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of source.</typeparam>
        /// <typeparam name="TKey">The type of the key returned by keySelector.</typeparam>
        /// <param name="source">An async-enumerable sequence of values to order.</param>
        /// <param name="keySelector">An asynchronous function to extract a key from an element.</param>
        /// <param name="comparer">A comparer to compare keys.</param>
        /// <returns>An ordered async-enumerable sequence whose elements are sorted in descending order according to a key.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> or <paramref name="keySelector"/> is null.</exception>
        public static IOrderedAsyncEnumerable<TSource> OrderByDescendingAwait<TSource, TKey>(this IAsyncEnumerable<TSource> source, Func<TSource, ValueTask<TKey>> keySelector, IComparer<TKey> comparer) => OrderByDescendingAwaitCore<TSource, TKey>(source, keySelector, comparer);

        /// <summary>
        /// Projects each element of an async-enumerable sequence into a new form by applying an asynchronous selector function to each member of the source sequence and awaiting the result.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements in the source sequence.</typeparam>
        /// <typeparam name="TResult">The type of the elements in the result sequence, obtained by running the selector function for each element in the source sequence and awaiting the result.</typeparam>
        /// <param name="source">A sequence of elements to invoke a transform function on.</param>
        /// <param name="selector">An asynchronous transform function to apply to each source element.</param>
        /// <returns>An async-enumerable sequence whose elements are the result of invoking the transform function on each element of the source sequence and awaiting the result.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> or <paramref name="selector"/> is null.</exception>
        public static IAsyncEnumerable<TResult> SelectAwait<TSource, TResult>(this IAsyncEnumerable<TSource> source, Func<TSource, ValueTask<TResult>> selector) => SelectAwaitCore<TSource, TResult>(source, selector);

        /// <summary>
        /// Projects each element of an async-enumerable sequence into a new form by applying an asynchronous selector function that incorporates each element's index to each element of the source sequence and awaiting the result.
        /// </summary>
        /// <typeparam name="TSource">The type of elements in the source sequence.</typeparam>
        /// <typeparam name="TResult">The type of elements in the result sequence, obtained by running the selector function for each element and its index, and awaiting the result.</typeparam>
        /// <param name="source">A sequence of elements to invoke a transform function on.</param>
        /// <param name="selector">An asynchronous transform function to apply to each source element; the second parameter represents the index of the element.</param>
        /// <returns>An async-enumerable sequence whose elements are the result of invoking the transform function on each element and its index of the source sequence and awaiting the result.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> or <paramref name="selector"/> is null.</exception>
        public static IAsyncEnumerable<TResult> SelectAwait<TSource, TResult>(this IAsyncEnumerable<TSource> source, Func<TSource, int, ValueTask<TResult>> selector) => SelectAwaitCore<TSource, TResult>(source, selector);

        /// <summary>
        /// Projects each element of an async-enumerable sequence into an async-enumerable sequence and merges the resulting async-enumerable sequences into one async-enumerable sequence.
        /// </summary>
        /// <typeparam name="TSource">The type of elements in the source sequence.</typeparam>
        /// <typeparam name="TResult">The type of elements in the projected inner sequences and the merged result sequence.</typeparam>
        /// <param name="source">An async-enumerable sequence of elements to project.</param>
        /// <param name="selector">An asynchronous selector function to apply to each element of the source sequence.</param>
        /// <returns>An async-enumerable sequence whose elements are the result of invoking the one-to-many transform function on each element of the source sequence and awaiting the result.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> or <paramref name="selector"/> is null.</exception>
        public static IAsyncEnumerable<TResult> SelectManyAwait<TSource, TResult>(this IAsyncEnumerable<TSource> source, Func<TSource, ValueTask<IAsyncEnumerable<TResult>>> selector) => SelectManyAwaitCore<TSource, TResult>(source, selector);

        /// <summary>
        /// Projects each element of an async-enumerable sequence into an async-enumerable sequence by incorporating the element's index and merges the resulting async-enumerable sequences into an async-enumerable sequence.
        /// </summary>
        /// <typeparam name="TSource">The type of elements in the source sequence.</typeparam>
        /// <typeparam name="TResult">The type of elements in the projected inner sequences and the merged result sequence.</typeparam>
        /// <param name="source">An async-enumerable sequence of elements to project.</param>
        /// <param name="selector">An asynchronous selector function to apply to each element; the second parameter represents the index of the element.</param>
        /// <returns>An async-enumerable sequence who's elements are the result of invoking the one-to-many transform function on each element of the source sequence and awaiting the result.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> or <paramref name="selector"/> is null.</exception>
        public static IAsyncEnumerable<TResult> SelectManyAwait<TSource, TResult>(this IAsyncEnumerable<TSource> source, Func<TSource, int, ValueTask<IAsyncEnumerable<TResult>>> selector) => SelectManyAwaitCore<TSource, TResult>(source, selector);

        /// <summary>
        /// Projects each element of an async-enumerable sequence to an async-enumerable sequence by awaiting the result of a transform function, invokes the result selector for each of the source elements and each of the corrasponding inner-sequence's elements and awaits the result, and merges the results into one async-enumerable sequence.
        /// </summary>
        /// <typeparam name="TSource">The type of elements in the source sequence.</typeparam>
        /// <typeparam name="TCollection">The type of elements in the projected intermediate sequences.</typeparam>
        /// <typeparam name="TResult">The type of elements in the result sequence.</typeparam>
        /// <param name="source">An async-enumerable sequence of elements to project.</param>
        /// <param name="collectionSelector">An asynchronous transform function to apply to each source element.</param>
        /// <param name="resultSelector">An asynchronous transform function to apply to each element of the intermediate sequence.</param>
        /// <returns>An async-enumerable sequence whose elements are the result of invoking the one-to-many transform function <paramref name="collectionSelector"/> on each element of the input sequence, awaiting the result, applying <paramref name="resultSelector"/> to each element of the intermediate sequences along with their corrasponding source element and awaiting the result.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/>, <paramref name="collectionSelector"/>, or <paramref name="resultSelector"/> is <see langword="null"/>.</exception>
        public static IAsyncEnumerable<TResult> SelectManyAwait<TSource, TCollection, TResult>(this IAsyncEnumerable<TSource> source, Func<TSource, ValueTask<IAsyncEnumerable<TCollection>>> collectionSelector, Func<TSource, TCollection, ValueTask<TResult>> resultSelector) => SelectManyAwaitCore<TSource, TCollection, TResult>(source, collectionSelector, resultSelector);

        /// <summary>
        /// Projects each element of an async-enumerable sequence to an async-enumerable sequence by awaiting the result of a transform function that incorporates each element's index,
        /// invokes the result selector for the source element and each of the corrasponding inner-sequence's elements and awaits the result, and merges the results into one async-enumerable sequence.
        /// </summary>
        /// <typeparam name="TSource">The type of elements in the source sequence.</typeparam>
        /// <typeparam name="TCollection">The type of elements in the projected intermediate sequences.</typeparam>
        /// <typeparam name="TResult">The type of elements in the result sequence.</typeparam>
        /// <param name="source">An async-enumerable sequence of elements to project.</param>
        /// <param name="collectionSelector">An asynchronous transform function to apply to each source element; the second parameter represents the index of the element.</param>
        /// <param name="resultSelector">An asynchronous transform function to apply to each element of the intermediate sequence.</param>
        /// <returns>An async-enumerable sequence whose elements are the result of invoking the one-to-many transform function <paramref name="collectionSelector"/> on each element of the input sequence, awaiting the result, applying <paramref name="resultSelector"/> to each element of the intermediate sequences olong with their corrasponding source element and awaiting the result.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/>, <paramref name="collectionSelector"/>, or <paramref name="resultSelector"/> is <see langword="null"/>.</exception>
        public static IAsyncEnumerable<TResult> SelectManyAwait<TSource, TCollection, TResult>(this IAsyncEnumerable<TSource> source, Func<TSource, int, ValueTask<IAsyncEnumerable<TCollection>>> collectionSelector, Func<TSource, TCollection, ValueTask<TResult>> resultSelector) => SelectManyAwaitCore<TSource, TCollection, TResult>(source, collectionSelector, resultSelector);

        /// <summary>
        /// Returns the only element of an async-enumerable sequence that satisfies the condition in the asynchronous predicate, and reports an exception if there is not exactly one element in the async-enumerable sequence that matches the predicate.
        /// </summary>
        /// <typeparam name="TSource">The type of elements in the source sequence.</typeparam>
        /// <param name="source">Source async-enumerable sequence.</param>
        /// <param name="predicate">An asynchronous predicate that will be applied to each element of the source sequence.</param>
        /// <param name="cancellationToken">The optional cancellation token to be used for cancelling the sequence at any time.</param>
        /// <returns>ValueTask containing the only element in the async-enumerable sequence that satisfies the condition in the asynchronous predicate.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> or <paramref name="predicate"/> is null.</exception>
        /// <exception cref="InvalidOperationException">(Asynchronous) No element satisfies the condition in the predicate. -or- More than one element satisfies the condition in the predicate. -or- The source sequence is empty.</exception>
        public static ValueTask<TSource> SingleAwaitAsync<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, ValueTask<bool>> predicate, CancellationToken cancellationToken = default) => SingleAwaitAsyncCore<TSource>(source, predicate, cancellationToken);

        /// <summary>
        /// Returns the only element of an async-enumerable sequence that satisfies the condition in the asynchronous predicate, or a default value if no such element exists, and reports an exception if there is more than one element in the async-enumerable sequence that matches the predicate.
        /// </summary>
        /// <typeparam name="TSource">The type of elements in the source sequence.</typeparam>
        /// <param name="source">Source async-enumerable sequence.</param>
        /// <param name="predicate">An asynchronous predicate that will be applied to each element of the source sequence. </param>
        /// <param name="cancellationToken">The optional cancellation token to be used for cancelling the sequence at any time.</param>
        /// <returns>ValueTask containing the only element in the async-enumerable sequence that satisfies the condition in the asynchronous predicate, or a default value if no such element exists.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> or <paramref name="predicate"/> is null.</exception>
        /// <exception cref="InvalidOperationException">(Asynchronous) More than one element satisfies the condition in the predicate.</exception>
        public static ValueTask<TSource?> SingleOrDefaultAwaitAsync<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, ValueTask<bool>> predicate, CancellationToken cancellationToken = default) => SingleOrDefaultAwaitAsyncCore<TSource>(source, predicate, cancellationToken);

        /// <summary>
        /// Bypasses elements in an async-enumerable sequence as long as a condition is true, and then returns the remaining elements.
        /// </summary>
        /// <typeparam name="TSource">The type of elements in the source sequence.</typeparam>
        /// <param name="source">An async-enumerable sequence to return elements from.</param>
        /// <param name="predicate">An asynchronous function to test each element for a condition.</param>
        /// <returns>An async-enumerable sequence containing the elements in the source sequence starting at the first element that does not pass the test specified by the predicate.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> or <paramref name="predicate"/> is <see langword="null"/>.</exception>
        public static IAsyncEnumerable<TSource> SkipWhileAwait<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, ValueTask<bool>> predicate) => SkipWhileAwaitCore<TSource>(source, predicate);

        /// <summary>
        /// Bypasses elements in an async-enumerable sequence as long as a condition is true, and then returns the remaining elements.
        /// The index of the element is used by the predicate.
        /// </summary>
        /// <typeparam name="TSource">The type of elements in the source sequence.</typeparam>
        /// <param name="source">An async-enumerable sequence to return elements from.</param>
        /// <param name="predicate">An asynchronous function to test each element for a condition; the second parameter of the function represents the index of the element.</param>
        /// <returns>An async-enumerable sequence containing the elements in the source sequence starting at the first element that does not pass the test specified by the predicate.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> or <paramref name="predicate"/> is <see langword="null"/>.</exception>
        public static IAsyncEnumerable<TSource> SkipWhileAwait<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, int, ValueTask<bool>> predicate) => SkipWhileAwaitCore<TSource>(source, predicate);
        
        /// <summary>
        /// Computes the sum of a sequence of <see cref="Nullable{Long}"/> values that are obtained by invoking a transform function on each element of the source sequence and awaiting the result.
        /// </summary>
        /// <typeparam name="TSource">The type of elements in the source sequence.</typeparam>
        /// <param name="source">A sequence of values that are used to calculate a sum.</param>
        /// <param name="selector">An asynchronous transform function to apply to each element.</param>
        /// <param name="cancellationToken">The optional cancellation token to be used for cancelling the sequence at any time.</param>
        /// <returns>A ValueTask containing the sum of the values in the source sequence.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> or <paramref name="selector"/> is <see langword="null"/>.</exception>
        /// <remarks>The return type of this operator differs from the corresponding operator on IEnumerable in order to retain asynchronous behavior.</remarks>
        public static ValueTask<long?> SumAwaitAsync<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, ValueTask<long?>> selector, CancellationToken cancellationToken = default) => SumAwaitAsyncCore<TSource>(source, selector, cancellationToken);

        /// <summary>
        /// Computes the sum of a sequence of <see cref="Nullable{Float}"/> values that are obtained by invoking a transform function on each element of the source sequence and awaiting the result.
        /// </summary>
        /// <typeparam name="TSource">The type of elements in the source sequence.</typeparam>
        /// <param name="source">A sequence of values that are used to calculate a sum.</param>
        /// <param name="selector">An asynchronous transform function to apply to each element.</param>
        /// <param name="cancellationToken">The optional cancellation token to be used for cancelling the sequence at any time.</param>
        /// <returns>A ValueTask containing the sum of the values in the source sequence.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> or <paramref name="selector"/> is <see langword="null"/>.</exception>
        /// <remarks>The return type of this operator differs from the corresponding operator on IEnumerable in order to retain asynchronous behavior.</remarks>
        public static ValueTask<float?> SumAwaitAsync<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, ValueTask<float?>> selector, CancellationToken cancellationToken = default) => SumAwaitAsyncCore<TSource>(source, selector, cancellationToken);

        /// <summary>
        /// Computes the sum of a sequence of <see cref="Nullable{Double}"/> values that are obtained by invoking a transform function on each element of the source sequence and awaiting the result.
        /// </summary>
        /// <typeparam name="TSource">The type of elements in the source sequence.</typeparam>
        /// <param name="source">A sequence of values that are used to calculate a sum.</param>
        /// <param name="selector">An asynchronous transform function to apply to each element.</param>
        /// <param name="cancellationToken">The optional cancellation token to be used for cancelling the sequence at any time.</param>
        /// <returns>A ValueTask containing the sum of the values in the source sequence.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> or <paramref name="selector"/> is <see langword="null"/>.</exception>
        /// <remarks>The return type of this operator differs from the corresponding operator on IEnumerable in order to retain asynchronous behavior.</remarks>
        public static ValueTask<double?> SumAwaitAsync<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, ValueTask<double?>> selector, CancellationToken cancellationToken = default) => SumAwaitAsyncCore<TSource>(source, selector, cancellationToken);

        /// <summary>
        /// Computes the sum of a sequence of <see cref="Nullable{Decimal}"/> values that are obtained by invoking a transform function on each element of the source sequence and awaiting the result.
        /// </summary>
        /// <typeparam name="TSource">The type of elements in the source sequence.</typeparam>
        /// <param name="source">A sequence of values that are used to calculate a sum.</param>
        /// <param name="selector">An asynchronous transform function to apply to each element.</param>
        /// <param name="cancellationToken">The optional cancellation token to be used for cancelling the sequence at any time.</param>
        /// <returns>A ValueTask containing the sum of the values in the source sequence.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> or <paramref name="selector"/> is <see langword="null"/>.</exception>
        /// <remarks>The return type of this operator differs from the corresponding operator on IEnumerable in order to retain asynchronous behavior.</remarks>
        public static ValueTask<decimal?> SumAwaitAsync<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, ValueTask<decimal?>> selector, CancellationToken cancellationToken = default) => SumAwaitAsyncCore<TSource>(source, selector, cancellationToken);

        /// <summary>
        /// Computes the sum of a sequence of <see cref="int"/> values that are obtained by invoking a transform function on each element of the source sequence and awaiting the result.
        /// </summary>
        /// <typeparam name="TSource">The type of elements in the source sequence.</typeparam>
        /// <param name="source">A sequence of values that are used to calculate a sum.</param>
        /// <param name="selector">An asynchronous transform function to apply to each element.</param>
        /// <param name="cancellationToken">The optional cancellation token to be used for cancelling the sequence at any time.</param>
        /// <returns>A ValueTask containing the sum of the values in the source sequence.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> or <paramref name="selector"/> is <see langword="null"/>.</exception>
        /// <remarks>The return type of this operator differs from the corresponding operator on IEnumerable in order to retain asynchronous behavior.</remarks>
        public static ValueTask<int> SumAwaitAsync<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, ValueTask<int>> selector, CancellationToken cancellationToken = default) => SumAwaitAsyncCore<TSource>(source, selector, cancellationToken);

        /// <summary>
        /// Computes the sum of a sequence of <see cref="long"/> values that are obtained by invoking a transform function on each element of the source sequence and awaiting the result.
        /// </summary>
        /// <typeparam name="TSource">The type of elements in the source sequence.</typeparam>
        /// <param name="source">A sequence of values that are used to calculate a sum.</param>
        /// <param name="selector">An asynchronous transform function to apply to each element.</param>
        /// <param name="cancellationToken">The optional cancellation token to be used for cancelling the sequence at any time.</param>
        /// <returns>A ValueTask containing the sum of the values in the source sequence.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> or <paramref name="selector"/> is <see langword="null"/>.</exception>
        /// <remarks>The return type of this operator differs from the corresponding operator on IEnumerable in order to retain asynchronous behavior.</remarks>
        public static ValueTask<long> SumAwaitAsync<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, ValueTask<long>> selector, CancellationToken cancellationToken = default) => SumAwaitAsyncCore<TSource>(source, selector, cancellationToken);

        /// <summary>
        /// Computes the sum of a sequence of <see cref="float"/> values that are obtained by invoking a transform function on each element of the source sequence and awaiting the result.
        /// </summary>
        /// <typeparam name="TSource">The type of elements in the source sequence.</typeparam>
        /// <param name="source">A sequence of values that are used to calculate a sum.</param>
        /// <param name="selector">An asynchronous transform function to apply to each element.</param>
        /// <param name="cancellationToken">The optional cancellation token to be used for cancelling the sequence at any time.</param>
        /// <returns>A ValueTask containing the sum of the values in the source sequence.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> or <paramref name="selector"/> is <see langword="null"/>.</exception>
        /// <remarks>The return type of this operator differs from the corresponding operator on IEnumerable in order to retain asynchronous behavior.</remarks>
        public static ValueTask<float> SumAwaitAsync<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, ValueTask<float>> selector, CancellationToken cancellationToken = default) => SumAwaitAsyncCore<TSource>(source, selector, cancellationToken);

        /// <summary>
        /// Computes the sum of a sequence of <see cref="double"/> values that are obtained by invoking a transform function on each element of the source sequence and awaiting the result.
        /// </summary>
        /// <typeparam name="TSource">The type of elements in the source sequence.</typeparam>
        /// <param name="source">A sequence of values that are used to calculate a sum.</param>
        /// <param name="selector">An asynchronous transform function to apply to each element.</param>
        /// <param name="cancellationToken">The optional cancellation token to be used for cancelling the sequence at any time.</param>
        /// <returns>A ValueTask containing the sum of the values in the source sequence.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> or <paramref name="selector"/> is <see langword="null"/>.</exception>
        /// <remarks>The return type of this operator differs from the corresponding operator on IEnumerable in order to retain asynchronous behavior.</remarks>
        public static ValueTask<double> SumAwaitAsync<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, ValueTask<double>> selector, CancellationToken cancellationToken = default) => SumAwaitAsyncCore<TSource>(source, selector, cancellationToken);

        /// <summary>
        /// Computes the sum of a sequence of <see cref="decimal"/> values that are obtained by invoking a transform function on each element of the source sequence and awaiting the result.
        /// </summary>
        /// <typeparam name="TSource">The type of elements in the source sequence.</typeparam>
        /// <param name="source">A sequence of values that are used to calculate a sum.</param>
        /// <param name="selector">An asynchronous transform function to apply to each element.</param>
        /// <param name="cancellationToken">The optional cancellation token to be used for cancelling the sequence at any time.</param>
        /// <returns>A ValueTask containing the sum of the values in the source sequence.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> or <paramref name="selector"/> is <see langword="null"/>.</exception>
        /// <remarks>The return type of this operator differs from the corresponding operator on IEnumerable in order to retain asynchronous behavior.</remarks>
        public static ValueTask<decimal> SumAwaitAsync<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, ValueTask<decimal>> selector, CancellationToken cancellationToken = default) => SumAwaitAsyncCore<TSource>(source, selector, cancellationToken);

        /// <summary>
        /// Computes the sum of a sequence of <see cref="Nullable{Int}"/> values that are obtained by invoking a transform function on each element of the source sequence and awaiting the result.
        /// </summary>
        /// <typeparam name="TSource">The type of elements in the source sequence.</typeparam>
        /// <param name="source">A sequence of values that are used to calculate a sum.</param>
        /// <param name="selector">An asynchronous transform function to apply to each element.</param>
        /// <param name="cancellationToken">The optional cancellation token to be used for cancelling the sequence at any time.</param>
        /// <returns>A ValueTask containing the sum of the values in the source sequence.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> or <paramref name="selector"/> is <see langword="null"/>.</exception>
        /// <remarks>The return type of this operator differs from the corresponding operator on IEnumerable in order to retain asynchronous behavior.</remarks>
        public static ValueTask<int?> SumAwaitAsync<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, ValueTask<int?>> selector, CancellationToken cancellationToken = default) => SumAwaitAsyncCore<TSource>(source, selector, cancellationToken);

        /// <summary>
        /// Returns elements from an async-enumerable sequence as long as a specified condition is true.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements in the source sequence.</typeparam>
        /// <param name="source">A sequence to return elements from.</param>
        /// <param name="predicate">An asynchronous predicate to test each element for a condition.</param>
        /// <returns>An async-enumerable sequence that contains the elements from the input sequence that occur before the element at which the test no longer passes.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> or <paramref name="predicate"/> is null.</exception>
        public static IAsyncEnumerable<TSource> TakeWhileAwait<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, ValueTask<bool>> predicate) => TakeWhileAwaitCore<TSource>(source, predicate);

        /// <summary>
        /// Returns elements from an async-enumerable sequence as long as a specified condition is true.
        /// The element's index is used in the logic of the predicate function.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements in the source sequence.</typeparam>
        /// <param name="source">A sequence to return elements from.</param>
        /// <param name="predicate">An asynchronous function to test each element for a condition; the second parameter of the function represents the index of the source element.</param>
        /// <returns>An async-enumerable sequence that contains the elements from the input sequence that occur before the element at which the test no longer passes.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> or <paramref name="predicate"/> is null.</exception>
        public static IAsyncEnumerable<TSource> TakeWhileAwait<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, int, ValueTask<bool>> predicate) => TakeWhileAwaitCore<TSource>(source, predicate);

        /// <summary>
        /// Performs a subsequent ordering of the elements in a sequence in ascending order according to a key obtained by invoking a transform function on each element and awaiting the result.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of source.</typeparam>
        /// <typeparam name="TKey">The type of the key returned by keySelector.</typeparam>
        /// <param name="source">An ordered async-enumerable sequence that contains elements to sort.</param>
        /// <param name="keySelector">An asynchronous function to extract a key from each element.</param>
        /// <returns>An ordered async-enumerable whose elements are sorted according to a key.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> or <paramref name="keySelector"/> is null.</exception>
        public static IOrderedAsyncEnumerable<TSource> ThenByAwait<TSource, TKey>(this IOrderedAsyncEnumerable<TSource> source, Func<TSource, ValueTask<TKey>> keySelector) => ThenByAwaitCore<TSource, TKey>(source, keySelector);

        /// <summary>
        /// Performs a subsequent ordering of the elements in a sequence in ascending order by using a specified comparer. The keys are obtained by invoking a transform function on each element and awaiting the result.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of source.</typeparam>
        /// <typeparam name="TKey">The type of the key returned by keySelector.</typeparam>
        /// <param name="source">An ordered async-enumerable sequence that contains elements to sort.</param>
        /// <param name="keySelector">An asynchronous function to extract a key from each element.</param>
        /// <param name="comparer">A comparer to compare keys.</param>
        /// <returns>An ordered async-enumerable whose elements are sorted according to a key.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> or <paramref name="keySelector"/> is null.</exception>
        public static IOrderedAsyncEnumerable<TSource> ThenByAwait<TSource, TKey>(this IOrderedAsyncEnumerable<TSource> source, Func<TSource, ValueTask<TKey>> keySelector, IComparer<TKey> comparer) => ThenByAwaitCore<TSource, TKey>(source, keySelector, comparer);

        /// <summary>
        /// Performs a subsequent ordering of the elements in a sequence in descending order, according to a key obtained by invoking a transform function on each element and awaiting the result.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of source.</typeparam>
        /// <typeparam name="TKey">The type of the key returned by keySelector.</typeparam>
        /// <param name="source">An ordered async-enumerable sequence that contains elements to sort.</param>
        /// <param name="keySelector">An asynchronous function to extract a key from each element.</param>
        /// <returns>An ordered async-enumerable sequence whose elements are sorted in descending order according to a key.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> or <paramref name="keySelector"/> is null.</exception>
        public static IOrderedAsyncEnumerable<TSource> ThenByDescendingAwait<TSource, TKey>(this IOrderedAsyncEnumerable<TSource> source, Func<TSource, ValueTask<TKey>> keySelector) => ThenByDescendingAwaitCore<TSource, TKey>(source, keySelector);

        /// <summary>
        /// Performs a subsequent ordering of the elements in a sequence in descending order by using a specified comparer. The keys are obtained by invoking a transform function on each element and awaiting the result.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of source.</typeparam>
        /// <typeparam name="TKey">The type of the key returned by keySelector.</typeparam>
        /// <param name="source">An ordered async-enumerable sequence that contains elements to sort.</param>
        /// <param name="keySelector">An asynchronous function to extract a key from each element.</param>
        /// <param name="comparer">A comparer to compare keys.</param>
        /// <returns>An ordered async-enumerable sequence whose elements are sorted in descending order according to a key.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> or <paramref name="keySelector"/> is null.</exception>
        public static IOrderedAsyncEnumerable<TSource> ThenByDescendingAwait<TSource, TKey>(this IOrderedAsyncEnumerable<TSource> source, Func<TSource, ValueTask<TKey>> keySelector, IComparer<TKey> comparer) => ThenByDescendingAwaitCore<TSource, TKey>(source, keySelector, comparer);

        /// <summary>
        /// Creates a dictionary from an async-enumerable sequence by invoking a key-selector function on each element and awaiting the result.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements in the source sequence.</typeparam>
        /// <typeparam name="TKey">The type of the dictionary key computed for each element in the source sequence.</typeparam>
        /// <param name="source">An async-enumerable sequence to create a dictionary for.</param>
        /// <param name="keySelector">An asynchronous function to extract a key from each element.</param>
        /// <param name="cancellationToken">The optional cancellation token to be used for cancelling the sequence at any time.</param>
        /// <returns>A ValueTask containing a dictionary mapping unique key values onto the corresponding source sequence's element.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> or <paramref name="keySelector"/> is null.</exception>
        /// <remarks>The return type of this operator differs from the corresponding operator on IEnumerable in order to retain asynchronous behavior.</remarks>
        public static ValueTask<Dictionary<TKey, TSource>> ToDictionaryAwaitAsync<TSource, TKey>(this IAsyncEnumerable<TSource> source, Func<TSource, ValueTask<TKey>> keySelector, CancellationToken cancellationToken = default) where TKey : notnull => ToDictionaryAwaitAsyncCore<TSource, TKey>(source, keySelector, cancellationToken);

        /// <summary>
        /// Creates a dictionary from an async-enumerable sequence by invoking a key-selector function on each element and awaiting the result.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements in the source sequence.</typeparam>
        /// <typeparam name="TKey">The type of the dictionary key computed for each element in the source sequence.</typeparam>
        /// <param name="source">An async-enumerable sequence to create a dictionary for.</param>
        /// <param name="keySelector">An asynchronous function to extract a key from each element.</param>
        /// <param name="comparer">An equality comparer to compare keys.</param>
        /// <param name="cancellationToken">The optional cancellation token to be used for cancelling the sequence at any time.</param>
        /// <returns>A ValueTask containing a dictionary mapping unique key values onto the corresponding source sequence's element.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> or <paramref name="keySelector"/> or <paramref name="comparer"/> is null.</exception>
        /// <remarks>The return type of this operator differs from the corresponding operator on IEnumerable in order to retain asynchronous behavior.</remarks>
        public static ValueTask<Dictionary<TKey, TSource>> ToDictionaryAwaitAsync<TSource, TKey>(this IAsyncEnumerable<TSource> source, Func<TSource, ValueTask<TKey>> keySelector, IEqualityComparer<TKey> comparer, CancellationToken cancellationToken = default) where TKey : notnull => ToDictionaryAwaitAsyncCore<TSource, TKey>(source, keySelector, comparer, cancellationToken);

        /// <summary>
        /// Creates a dictionary from an async-enumerable sequence using the specified asynchronous key and element selector functions.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements in the source sequence.</typeparam>
        /// <typeparam name="TKey">The type of the dictionary key computed for each element in the source sequence.</typeparam>
        /// <typeparam name="TElement">The type of the dictionary value computed for each element in the source sequence.</typeparam>
        /// <param name="source">An async-enumerable sequence to create a dictionary for.</param>
        /// <param name="keySelector">An asynchronous function to extract a key from each element.</param>
        /// <param name="elementSelector">An asynchronous transform function to produce a result element value from each element.</param>
        /// <param name="cancellationToken">The optional cancellation token to be used for cancelling the sequence at any time.</param>
        /// <returns>A ValueTask containing a dictionary mapping unique key values onto the corresponding source sequence's element.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> or <paramref name="keySelector"/> or <paramref name="elementSelector"/> is null.</exception>
        /// <remarks>The return type of this operator differs from the corresponding operator on IEnumerable in order to retain asynchronous behavior.</remarks>
        public static ValueTask<Dictionary<TKey, TElement>> ToDictionaryAwaitAsync<TSource, TKey, TElement>(this IAsyncEnumerable<TSource> source, Func<TSource, ValueTask<TKey>> keySelector, Func<TSource, ValueTask<TElement>> elementSelector, CancellationToken cancellationToken = default) where TKey : notnull => ToDictionaryAwaitAsyncCore<TSource, TKey, TElement>(source, keySelector, elementSelector, cancellationToken);

        /// <summary>
        /// Creates a dictionary from an async-enumerable sequence using the specified asynchronous key and element selector functions.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements in the source sequence.</typeparam>
        /// <typeparam name="TKey">The type of the dictionary key computed for each element in the source sequence.</typeparam>
        /// <typeparam name="TElement">The type of the dictionary value computed for each element in the source sequence.</typeparam>
        /// <param name="source">An async-enumerable sequence to create a dictionary for.</param>
        /// <param name="keySelector">An asynchronous function to extract a key from each element.</param>
        /// <param name="elementSelector">An asynchronous transform function to produce a result element value from each element.</param>
        /// <param name="comparer">An equality comparer to compare keys.</param>
        /// <param name="cancellationToken">The optional cancellation token to be used for cancelling the sequence at any time.</param>
        /// <returns>A ValueTask containing a dictionary mapping unique key values onto the corresponding source sequence's element.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> or <paramref name="keySelector"/> or <paramref name="elementSelector"/> or <paramref name="comparer"/> is null.</exception>
        /// <remarks>The return type of this operator differs from the corresponding operator on IEnumerable in order to retain asynchronous behavior.</remarks>
        public static ValueTask<Dictionary<TKey, TElement>> ToDictionaryAwaitAsync<TSource, TKey, TElement>(this IAsyncEnumerable<TSource> source, Func<TSource, ValueTask<TKey>> keySelector, Func<TSource, ValueTask<TElement>> elementSelector, IEqualityComparer<TKey> comparer, CancellationToken cancellationToken = default) where TKey : notnull => ToDictionaryAwaitAsyncCore<TSource, TKey, TElement>(source, keySelector, elementSelector, comparer, cancellationToken);

        public static ValueTask<ImmutableDictionary<TKey, TSource>> ToImmutableDictionaryAwaitAsync<TSource, TKey>(this IAsyncEnumerable<TSource> source, Func<TSource, ValueTask<TKey>> keySelector, CancellationToken cancellationToken = default) where TKey : notnull => ToImmutableDictionaryAwaitAsyncCore<TSource, TKey>(source, keySelector, cancellationToken);
        public static ValueTask<ImmutableDictionary<TKey, TValue>> ToImmutableDictionaryAwaitAsync<TSource, TKey, TValue>(this IAsyncEnumerable<TSource> source, Func<TSource, ValueTask<TKey>> keySelector, Func<TSource, ValueTask<TValue>> elementSelector, CancellationToken cancellationToken = default) where TKey : notnull => ToImmutableDictionaryAwaitAsyncCore<TSource, TKey, TValue>(source, keySelector, elementSelector, cancellationToken);
        public static ValueTask<ImmutableDictionary<TKey, TSource>> ToImmutableDictionaryAwaitAsync<TSource, TKey>(this IAsyncEnumerable<TSource> source, Func<TSource, ValueTask<TKey>> keySelector, IEqualityComparer<TKey>? keyComparer, CancellationToken cancellationToken = default) where TKey : notnull => ToImmutableDictionaryAwaitAsyncCore<TSource, TKey>(source, keySelector, keyComparer, cancellationToken);
        public static ValueTask<ImmutableDictionary<TKey, TValue>> ToImmutableDictionaryAwaitAsync<TSource, TKey, TValue>(this IAsyncEnumerable<TSource> source, Func<TSource, ValueTask<TKey>> keySelector, Func<TSource, ValueTask<TValue>> elementSelector, IEqualityComparer<TKey>? keyComparer, CancellationToken cancellationToken = default) where TKey : notnull => ToImmutableDictionaryAwaitAsyncCore<TSource, TKey, TValue>(source, keySelector, elementSelector, keyComparer, cancellationToken);
        public static ValueTask<ImmutableDictionary<TKey, TValue>> ToImmutableDictionaryAwaitAsync<TSource, TKey, TValue>(this IAsyncEnumerable<TSource> source, Func<TSource, ValueTask<TKey>> keySelector, Func<TSource, ValueTask<TValue>> elementSelector, IEqualityComparer<TKey>? keyComparer, IEqualityComparer<TValue>? valueComparer, CancellationToken cancellationToken = default) where TKey : notnull => ToImmutableDictionaryAwaitAsyncCore<TSource, TKey, TValue>(source, keySelector, elementSelector, keyComparer, valueComparer, cancellationToken);
        public static ValueTask<ImmutableSortedDictionary<TKey, TValue>> ToImmutableSortedDictionaryAwaitAsync<TSource, TKey, TValue>(this IAsyncEnumerable<TSource> source, Func<TSource, ValueTask<TKey>> keySelector, Func<TSource, ValueTask<TValue>> elementSelector, CancellationToken cancellationToken = default) where TKey : notnull => ToImmutableSortedDictionaryAwaitAsyncCore<TSource, TKey, TValue>(source, keySelector, elementSelector, cancellationToken);
        public static ValueTask<ImmutableSortedDictionary<TKey, TValue>> ToImmutableSortedDictionaryAwaitAsync<TSource, TKey, TValue>(this IAsyncEnumerable<TSource> source, Func<TSource, ValueTask<TKey>> keySelector, Func<TSource, ValueTask<TValue>> elementSelector, IComparer<TKey>? keyComparer, CancellationToken cancellationToken = default) where TKey : notnull => ToImmutableSortedDictionaryAwaitAsyncCore<TSource, TKey, TValue>(source, keySelector, elementSelector, keyComparer, cancellationToken);
        public static ValueTask<ImmutableSortedDictionary<TKey, TValue>> ToImmutableSortedDictionaryAwaitAsync<TSource, TKey, TValue>(this IAsyncEnumerable<TSource> source, Func<TSource, ValueTask<TKey>> keySelector, Func<TSource, ValueTask<TValue>> elementSelector, IComparer<TKey>? keyComparer, IEqualityComparer<TValue>? valueComparer, CancellationToken cancellationToken = default) where TKey : notnull => ToImmutableSortedDictionaryAwaitAsyncCore<TSource, TKey, TValue>(source, keySelector, elementSelector, keyComparer, valueComparer, cancellationToken);

        /// <summary>
        /// Creates a lookup from an async-enumerable sequence by invoking a key-selector function on each element and awaiting the result.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements in the source sequence.</typeparam>
        /// <typeparam name="TKey">The type of the lookup key computed for each element in the source sequence.</typeparam>
        /// <param name="source">An async-enumerable sequence to create a lookup for.</param>
        /// <param name="keySelector">An asynchronous function to extract a key from each element.</param>
        /// <param name="cancellationToken">The optional cancellation token to be used for cancelling the sequence at any time.</param>
        /// <returns>A ValueTask containing a lookup mapping unique key values onto the corresponding source sequence's elements.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> or <paramref name="keySelector"/> is null.</exception>
        /// <remarks>The return type of this operator differs from the corresponding operator on IEnumerable in order to retain asynchronous behavior.</remarks>
        public static ValueTask<ILookup<TKey, TSource>> ToLookupAwaitAsync<TSource, TKey>(this IAsyncEnumerable<TSource> source, Func<TSource, ValueTask<TKey>> keySelector, CancellationToken cancellationToken = default) => ToLookupAwaitAsyncCore<TSource, TKey>(source, keySelector, cancellationToken);

        /// <summary>
        /// Creates a lookup from an async-enumerable sequence by invoking key and element selector functions on each source element and awaiting the results.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements in the source sequence.</typeparam>
        /// <typeparam name="TKey">The type of the lookup key computed for each element in the source sequence.</typeparam>
        /// <typeparam name="TElement">The type of the lookup value computed for each element in the source sequence.</typeparam>
        /// <param name="source">An async-enumerable sequence to create a lookup for.</param>
        /// <param name="keySelector">An asynchronous function to extract a key from each element.</param>
        /// <param name="elementSelector">An asynchronous transform function to produce a result element value from each element.</param>
        /// <param name="cancellationToken">The optional cancellation token to be used for cancelling the sequence at any time.</param>
        /// <returns>An async-enumerable sequence containing a single element with a lookup mapping unique key values onto the corresponding source sequence's elements.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> or <paramref name="keySelector"/> or <paramref name="elementSelector"/> is null.</exception>
        /// <remarks>The return type of this operator differs from the corresponding operator on IEnumerable in order to retain asynchronous behavior.</remarks>
        public static ValueTask<ILookup<TKey, TElement>> ToLookupAwaitAsync<TSource, TKey, TElement>(this IAsyncEnumerable<TSource> source, Func<TSource, ValueTask<TKey>> keySelector, Func<TSource, ValueTask<TElement>> elementSelector, CancellationToken cancellationToken = default) => ToLookupAwaitAsyncCore<TSource, TKey, TElement>(source, keySelector, elementSelector, cancellationToken);

        /// <summary>
        /// Creates a lookup from an async-enumerable sequence by invoking a key-selector function on each element and awaiting the result.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements in the source sequence.</typeparam>
        /// <typeparam name="TKey">The type of the lookup key computed for each element in the source sequence.</typeparam>
        /// <param name="source">An async-enumerable sequence to create a lookup for.</param>
        /// <param name="keySelector">An asynchronous function to extract a key from each element.</param>
        /// <param name="comparer">An equality comparer to compare keys.</param>
        /// <param name="cancellationToken">The optional cancellation token to be used for cancelling the sequence at any time.</param>
        /// <returns>A ValueTask containing a lookup mapping unique key values onto the corresponding source sequence's elements.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> or <paramref name="keySelector"/> or <paramref name="comparer"/> is null.</exception>
        /// <remarks>The return type of this operator differs from the corresponding operator on IEnumerable in order to retain asynchronous behavior.</remarks>
        public static ValueTask<ILookup<TKey, TSource>> ToLookupAwaitAsync<TSource, TKey>(this IAsyncEnumerable<TSource> source, Func<TSource, ValueTask<TKey>> keySelector, IEqualityComparer<TKey> comparer, CancellationToken cancellationToken = default) => ToLookupAwaitAsyncCore<TSource, TKey>(source, keySelector, comparer, cancellationToken);

        /// <summary>
        /// Creates a lookup from an async-enumerable sequence by invoking key and element selector functions on each source element and awaiting the results.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements in the source sequence.</typeparam>
        /// <typeparam name="TKey">The type of the lookup key computed for each element in the source sequence.</typeparam>
        /// <typeparam name="TElement">The type of the lookup value computed for each element in the source sequence.</typeparam>
        /// <param name="source">An async-enumerable sequence to create a lookup for.</param>
        /// <param name="keySelector">An asynchronous function to extract a key from each element.</param>
        /// <param name="elementSelector">An asynchronous transform function to produce a result element value from each source element.</param>
        /// <param name="comparer">An equality comparer to compare keys.</param>
        /// <param name="cancellationToken">The optional cancellation token to be used for cancelling the sequence at any time.</param>
        /// <returns>A ValueTask containing a lookup mapping unique key values onto the corresponding source sequence's elements.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> or <paramref name="keySelector"/> or <paramref name="elementSelector"/> or <paramref name="comparer"/> is null.</exception>
        /// <remarks>The return type of this operator differs from the corresponding operator on IEnumerable in order to retain asynchronous behavior.</remarks>
        public static ValueTask<ILookup<TKey, TElement>> ToLookupAwaitAsync<TSource, TKey, TElement>(this IAsyncEnumerable<TSource> source, Func<TSource, ValueTask<TKey>> keySelector, Func<TSource, ValueTask<TElement>> elementSelector, IEqualityComparer<TKey> comparer, CancellationToken cancellationToken = default) => ToLookupAwaitAsyncCore<TSource, TKey, TElement>(source, keySelector, elementSelector, comparer, cancellationToken);

        /// <summary>
        /// Filters the elements of an async-enumerable sequence based on an asynchronous predicate.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements in the source sequence.</typeparam>
        /// <param name="source">An async-enumerable sequence whose elements to filter.</param>
        /// <param name="predicate">An asynchronous predicate to test each source element for a condition.</param>
        /// <returns>An async-enumerable sequence that contains elements from the input sequence that satisfy the condition.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> or <paramref name="predicate"/> is null.</exception>
        public static IAsyncEnumerable<TSource> WhereAwait<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, ValueTask<bool>> predicate) => WhereAwaitCore<TSource>(source, predicate);

        /// <summary>
        /// Filters the elements of an async-enumerable sequence based on an asynchronous predicate that incorporates the element's index.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements in the source sequence.</typeparam>
        /// <param name="source">An async-enumerable sequence whose elements to filter.</param>
        /// <param name="predicate">An asynchronous predicate to test each source element for a condition; the second parameter of the function represents the index of the source element.</param>
        /// <returns>An async-enumerable sequence that contains elements from the input sequence that satisfy the condition.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> or <paramref name="predicate"/> is null.</exception>
        public static IAsyncEnumerable<TSource> WhereAwait<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, int, ValueTask<bool>> predicate) => WhereAwaitCore<TSource>(source, predicate);

        /// <summary>
        /// Merges two async-enumerable sequences into one async-enumerable sequence by combining their elements in a pairwise fashion.
        /// </summary>
        /// <typeparam name="TFirst">The type of the elements in the first source sequence.</typeparam>
        /// <typeparam name="TSecond">The type of the elements in the second source sequence.</typeparam>
        /// <typeparam name="TResult">The type of the elements in the result sequence, returned by the selector function.</typeparam>
        /// <param name="first">First async-enumerable source.</param>
        /// <param name="second">Second async-enumerable source.</param>
        /// <param name="selector">An asynchronous function to invoke and await for each consecutive pair of elements from the first and second source.</param>
        /// <returns>An async-enumerable sequence containing the result of pairwise combining the elements of the first and second source using the specified result selector function.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="first"/> or <paramref name="second"/> or <paramref name="selector"/> is null.</exception>
        public static IAsyncEnumerable<TResult> ZipAwait<TFirst, TSecond, TResult>(this IAsyncEnumerable<TFirst> first, IAsyncEnumerable<TSecond> second, Func<TFirst, TSecond, ValueTask<TResult>> selector) => ZipAwaitCore<TFirst, TSecond, TResult>(first, second, selector);

#if !NO_DEEP_CANCELLATION
        public static ValueTask<TSource> AggregateAwaitWithCancellationAsync<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, TSource, CancellationToken, ValueTask<TSource>> accumulator, CancellationToken cancellationToken = default) => AggregateAwaitWithCancellationAsyncCore<TSource>(source, accumulator, cancellationToken);
        public static ValueTask<TAccumulate> AggregateAwaitWithCancellationAsync<TSource, TAccumulate>(this IAsyncEnumerable<TSource> source, TAccumulate seed, Func<TAccumulate, TSource, CancellationToken, ValueTask<TAccumulate>> accumulator, CancellationToken cancellationToken = default) => AggregateAwaitWithCancellationAsyncCore<TSource, TAccumulate>(source, seed, accumulator, cancellationToken);
        public static ValueTask<TResult> AggregateAwaitWithCancellationAsync<TSource, TAccumulate, TResult>(this IAsyncEnumerable<TSource> source, TAccumulate seed, Func<TAccumulate, TSource, CancellationToken, ValueTask<TAccumulate>> accumulator, Func<TAccumulate, CancellationToken, ValueTask<TResult>> resultSelector, CancellationToken cancellationToken = default) => AggregateAwaitWithCancellationAsyncCore<TSource, TAccumulate, TResult>(source, seed, accumulator, resultSelector, cancellationToken);
        public static ValueTask<bool> AllAwaitWithCancellationAsync<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, ValueTask<bool>> predicate, CancellationToken cancellationToken = default) => AllAwaitWithCancellationAsyncCore<TSource>(source, predicate, cancellationToken);
        public static ValueTask<bool> AnyAwaitWithCancellationAsync<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, ValueTask<bool>> predicate, CancellationToken cancellationToken = default) => AnyAwaitWithCancellationAsyncCore<TSource>(source, predicate, cancellationToken);
        public static ValueTask<double> AverageAwaitWithCancellationAsync<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, ValueTask<int>> selector, CancellationToken cancellationToken = default) => AverageAwaitWithCancellationAsyncCore<TSource>(source, selector, cancellationToken);
        public static ValueTask<double> AverageAwaitWithCancellationAsync<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, ValueTask<long>> selector, CancellationToken cancellationToken = default) => AverageAwaitWithCancellationAsyncCore<TSource>(source, selector, cancellationToken);
        public static ValueTask<float> AverageAwaitWithCancellationAsync<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, ValueTask<float>> selector, CancellationToken cancellationToken = default) => AverageAwaitWithCancellationAsyncCore<TSource>(source, selector, cancellationToken);
        public static ValueTask<double> AverageAwaitWithCancellationAsync<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, ValueTask<double>> selector, CancellationToken cancellationToken = default) => AverageAwaitWithCancellationAsyncCore<TSource>(source, selector, cancellationToken);
        public static ValueTask<decimal> AverageAwaitWithCancellationAsync<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, ValueTask<decimal>> selector, CancellationToken cancellationToken = default) => AverageAwaitWithCancellationAsyncCore<TSource>(source, selector, cancellationToken);
        public static ValueTask<double?> AverageAwaitWithCancellationAsync<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, ValueTask<int?>> selector, CancellationToken cancellationToken = default) => AverageAwaitWithCancellationAsyncCore<TSource>(source, selector, cancellationToken);
        public static ValueTask<double?> AverageAwaitWithCancellationAsync<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, ValueTask<long?>> selector, CancellationToken cancellationToken = default) => AverageAwaitWithCancellationAsyncCore<TSource>(source, selector, cancellationToken);
        public static ValueTask<float?> AverageAwaitWithCancellationAsync<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, ValueTask<float?>> selector, CancellationToken cancellationToken = default) => AverageAwaitWithCancellationAsyncCore<TSource>(source, selector, cancellationToken);
        public static ValueTask<double?> AverageAwaitWithCancellationAsync<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, ValueTask<double?>> selector, CancellationToken cancellationToken = default) => AverageAwaitWithCancellationAsyncCore<TSource>(source, selector, cancellationToken);
        public static ValueTask<decimal?> AverageAwaitWithCancellationAsync<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, ValueTask<decimal?>> selector, CancellationToken cancellationToken = default) => AverageAwaitWithCancellationAsyncCore<TSource>(source, selector, cancellationToken);
        public static ValueTask<int> CountAwaitWithCancellationAsync<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, ValueTask<bool>> predicate, CancellationToken cancellationToken = default) => CountAwaitWithCancellationAsyncCore<TSource>(source, predicate, cancellationToken);
        public static ValueTask<TSource> FirstAwaitWithCancellationAsync<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, ValueTask<bool>> predicate, CancellationToken cancellationToken = default) => FirstAwaitWithCancellationAsyncCore<TSource>(source, predicate, cancellationToken);
        public static ValueTask<TSource?> FirstOrDefaultAwaitWithCancellationAsync<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, ValueTask<bool>> predicate, CancellationToken cancellationToken = default) => FirstOrDefaultAwaitWithCancellationAsyncCore<TSource>(source, predicate, cancellationToken);
        public static Task ForEachAwaitWithCancellationAsync<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, Task> action, CancellationToken cancellationToken) => ForEachAwaitWithCancellationAsyncCore<TSource>(source, action, cancellationToken);
        public static Task ForEachAwaitWithCancellationAsync<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, int, CancellationToken, Task> action, CancellationToken cancellationToken) => ForEachAwaitWithCancellationAsyncCore<TSource>(source, action, cancellationToken);
        public static IAsyncEnumerable<IAsyncGrouping<TKey, TSource>> GroupByAwaitWithCancellation<TSource, TKey>(this IAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, ValueTask<TKey>> keySelector) => GroupByAwaitWithCancellationCore<TSource, TKey>(source, keySelector);
        public static IAsyncEnumerable<IAsyncGrouping<TKey, TSource>> GroupByAwaitWithCancellation<TSource, TKey>(this IAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, ValueTask<TKey>> keySelector, IEqualityComparer<TKey> comparer) => GroupByAwaitWithCancellationCore<TSource, TKey>(source, keySelector, comparer);
        public static IAsyncEnumerable<IAsyncGrouping<TKey, TElement>> GroupByAwaitWithCancellation<TSource, TKey, TElement>(this IAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, ValueTask<TKey>> keySelector, Func<TSource, CancellationToken, ValueTask<TElement>> elementSelector) => GroupByAwaitWithCancellationCore<TSource, TKey, TElement>(source, keySelector, elementSelector);
        public static IAsyncEnumerable<TResult> GroupByAwaitWithCancellation<TSource, TKey, TResult>(this IAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, ValueTask<TKey>> keySelector, Func<TKey, IAsyncEnumerable<TSource>, CancellationToken, ValueTask<TResult>> resultSelector) => GroupByAwaitWithCancellationCore<TSource, TKey, TResult>(source, keySelector, resultSelector);
        public static IAsyncEnumerable<TResult> GroupByAwaitWithCancellation<TSource, TKey, TElement, TResult>(this IAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, ValueTask<TKey>> keySelector, Func<TSource, CancellationToken, ValueTask<TElement>> elementSelector, Func<TKey, IAsyncEnumerable<TElement>, CancellationToken, ValueTask<TResult>> resultSelector) => GroupByAwaitWithCancellationCore<TSource, TKey, TElement, TResult>(source, keySelector, elementSelector, resultSelector);
        public static IAsyncEnumerable<IAsyncGrouping<TKey, TElement>> GroupByAwaitWithCancellation<TSource, TKey, TElement>(this IAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, ValueTask<TKey>> keySelector, Func<TSource, CancellationToken, ValueTask<TElement>> elementSelector, IEqualityComparer<TKey> comparer) => GroupByAwaitWithCancellationCore<TSource, TKey, TElement>(source, keySelector, elementSelector, comparer);
        public static IAsyncEnumerable<TResult> GroupByAwaitWithCancellation<TSource, TKey, TResult>(this IAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, ValueTask<TKey>> keySelector, Func<TKey, IAsyncEnumerable<TSource>, CancellationToken, ValueTask<TResult>> resultSelector, IEqualityComparer<TKey> comparer) => GroupByAwaitWithCancellationCore<TSource, TKey, TResult>(source, keySelector, resultSelector, comparer);
        public static IAsyncEnumerable<TResult> GroupByAwaitWithCancellation<TSource, TKey, TElement, TResult>(this IAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, ValueTask<TKey>> keySelector, Func<TSource, CancellationToken, ValueTask<TElement>> elementSelector, Func<TKey, IAsyncEnumerable<TElement>, CancellationToken, ValueTask<TResult>> resultSelector, IEqualityComparer<TKey> comparer) => GroupByAwaitWithCancellationCore<TSource, TKey, TElement, TResult>(source, keySelector, elementSelector, resultSelector, comparer);
        public static IAsyncEnumerable<TResult> GroupJoinAwaitWithCancellation<TOuter, TInner, TKey, TResult>(this IAsyncEnumerable<TOuter> outer, IAsyncEnumerable<TInner> inner, Func<TOuter, CancellationToken, ValueTask<TKey>> outerKeySelector, Func<TInner, CancellationToken, ValueTask<TKey>> innerKeySelector, Func<TOuter, IAsyncEnumerable<TInner>, CancellationToken, ValueTask<TResult>> resultSelector) => GroupJoinAwaitWithCancellationCore<TOuter, TInner, TKey, TResult>(outer, inner, outerKeySelector, innerKeySelector, resultSelector);
        public static IAsyncEnumerable<TResult> GroupJoinAwaitWithCancellation<TOuter, TInner, TKey, TResult>(this IAsyncEnumerable<TOuter> outer, IAsyncEnumerable<TInner> inner, Func<TOuter, CancellationToken, ValueTask<TKey>> outerKeySelector, Func<TInner, CancellationToken, ValueTask<TKey>> innerKeySelector, Func<TOuter, IAsyncEnumerable<TInner>, CancellationToken, ValueTask<TResult>> resultSelector, IEqualityComparer<TKey> comparer) => GroupJoinAwaitWithCancellationCore<TOuter, TInner, TKey, TResult>(outer, inner, outerKeySelector, innerKeySelector, resultSelector, comparer);
        public static IAsyncEnumerable<TResult> JoinAwaitWithCancellation<TOuter, TInner, TKey, TResult>(this IAsyncEnumerable<TOuter> outer, IAsyncEnumerable<TInner> inner, Func<TOuter, CancellationToken, ValueTask<TKey>> outerKeySelector, Func<TInner, CancellationToken, ValueTask<TKey>> innerKeySelector, Func<TOuter, TInner, CancellationToken, ValueTask<TResult>> resultSelector) => JoinAwaitWithCancellationCore<TOuter, TInner, TKey, TResult>(outer, inner, outerKeySelector, innerKeySelector, resultSelector);
        public static IAsyncEnumerable<TResult> JoinAwaitWithCancellation<TOuter, TInner, TKey, TResult>(this IAsyncEnumerable<TOuter> outer, IAsyncEnumerable<TInner> inner, Func<TOuter, CancellationToken, ValueTask<TKey>> outerKeySelector, Func<TInner, CancellationToken, ValueTask<TKey>> innerKeySelector, Func<TOuter, TInner, CancellationToken, ValueTask<TResult>> resultSelector, IEqualityComparer<TKey> comparer) => JoinAwaitWithCancellationCore<TOuter, TInner, TKey, TResult>(outer, inner, outerKeySelector, innerKeySelector, resultSelector, comparer);
        public static ValueTask<TSource> LastAwaitWithCancellationAsync<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, ValueTask<bool>> predicate, CancellationToken cancellationToken = default) => LastAwaitWithCancellationAsyncCore<TSource>(source, predicate, cancellationToken);
        public static ValueTask<TSource?> LastOrDefaultAwaitWithCancellationAsync<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, ValueTask<bool>> predicate, CancellationToken cancellationToken = default) => LastOrDefaultAwaitWithCancellationAsyncCore<TSource>(source, predicate, cancellationToken);
        public static ValueTask<long> LongCountAwaitWithCancellationAsync<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, ValueTask<bool>> predicate, CancellationToken cancellationToken = default) => LongCountAwaitWithCancellationAsyncCore<TSource>(source, predicate, cancellationToken);
        public static ValueTask<long> MaxAwaitWithCancellationAsync<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, ValueTask<long>> selector, CancellationToken cancellationToken = default) => MaxAwaitWithCancellationAsyncCore<TSource>(source, selector, cancellationToken);
        public static ValueTask<long?> MaxAwaitWithCancellationAsync<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, ValueTask<long?>> selector, CancellationToken cancellationToken = default) => MaxAwaitWithCancellationAsyncCore<TSource>(source, selector, cancellationToken);
        public static ValueTask<float> MaxAwaitWithCancellationAsync<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, ValueTask<float>> selector, CancellationToken cancellationToken = default) => MaxAwaitWithCancellationAsyncCore<TSource>(source, selector, cancellationToken);
        public static ValueTask<float?> MaxAwaitWithCancellationAsync<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, ValueTask<float?>> selector, CancellationToken cancellationToken = default) => MaxAwaitWithCancellationAsyncCore<TSource>(source, selector, cancellationToken);
        public static ValueTask<double> MaxAwaitWithCancellationAsync<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, ValueTask<double>> selector, CancellationToken cancellationToken = default) => MaxAwaitWithCancellationAsyncCore<TSource>(source, selector, cancellationToken);
        public static ValueTask<double?> MaxAwaitWithCancellationAsync<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, ValueTask<double?>> selector, CancellationToken cancellationToken = default) => MaxAwaitWithCancellationAsyncCore<TSource>(source, selector, cancellationToken);
        public static ValueTask<decimal> MaxAwaitWithCancellationAsync<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, ValueTask<decimal>> selector, CancellationToken cancellationToken = default) => MaxAwaitWithCancellationAsyncCore<TSource>(source, selector, cancellationToken);
        public static ValueTask<decimal?> MaxAwaitWithCancellationAsync<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, ValueTask<decimal?>> selector, CancellationToken cancellationToken = default) => MaxAwaitWithCancellationAsyncCore<TSource>(source, selector, cancellationToken);
        public static ValueTask<TResult> MaxAwaitWithCancellationAsync<TSource, TResult>(this IAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, ValueTask<TResult>> selector, CancellationToken cancellationToken = default) => MaxAwaitWithCancellationAsyncCore<TSource, TResult>(source, selector, cancellationToken);
        public static ValueTask<int> MaxAwaitWithCancellationAsync<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, ValueTask<int>> selector, CancellationToken cancellationToken = default) => MaxAwaitWithCancellationAsyncCore<TSource>(source, selector, cancellationToken);
        public static ValueTask<int?> MaxAwaitWithCancellationAsync<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, ValueTask<int?>> selector, CancellationToken cancellationToken = default) => MaxAwaitWithCancellationAsyncCore<TSource>(source, selector, cancellationToken);
        public static ValueTask<double> MinAwaitWithCancellationAsync<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, ValueTask<double>> selector, CancellationToken cancellationToken = default) => MinAwaitWithCancellationAsyncCore<TSource>(source, selector, cancellationToken);
        public static ValueTask<double?> MinAwaitWithCancellationAsync<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, ValueTask<double?>> selector, CancellationToken cancellationToken = default) => MinAwaitWithCancellationAsyncCore<TSource>(source, selector, cancellationToken);
        public static ValueTask<decimal> MinAwaitWithCancellationAsync<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, ValueTask<decimal>> selector, CancellationToken cancellationToken = default) => MinAwaitWithCancellationAsyncCore<TSource>(source, selector, cancellationToken);
        public static ValueTask<decimal?> MinAwaitWithCancellationAsync<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, ValueTask<decimal?>> selector, CancellationToken cancellationToken = default) => MinAwaitWithCancellationAsyncCore<TSource>(source, selector, cancellationToken);
        public static ValueTask<int> MinAwaitWithCancellationAsync<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, ValueTask<int>> selector, CancellationToken cancellationToken = default) => MinAwaitWithCancellationAsyncCore<TSource>(source, selector, cancellationToken);
        public static ValueTask<int?> MinAwaitWithCancellationAsync<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, ValueTask<int?>> selector, CancellationToken cancellationToken = default) => MinAwaitWithCancellationAsyncCore<TSource>(source, selector, cancellationToken);
        public static ValueTask<long> MinAwaitWithCancellationAsync<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, ValueTask<long>> selector, CancellationToken cancellationToken = default) => MinAwaitWithCancellationAsyncCore<TSource>(source, selector, cancellationToken);
        public static ValueTask<long?> MinAwaitWithCancellationAsync<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, ValueTask<long?>> selector, CancellationToken cancellationToken = default) => MinAwaitWithCancellationAsyncCore<TSource>(source, selector, cancellationToken);
        public static ValueTask<float> MinAwaitWithCancellationAsync<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, ValueTask<float>> selector, CancellationToken cancellationToken = default) => MinAwaitWithCancellationAsyncCore<TSource>(source, selector, cancellationToken);
        public static ValueTask<float?> MinAwaitWithCancellationAsync<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, ValueTask<float?>> selector, CancellationToken cancellationToken = default) => MinAwaitWithCancellationAsyncCore<TSource>(source, selector, cancellationToken);
        public static ValueTask<TResult> MinAwaitWithCancellationAsync<TSource, TResult>(this IAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, ValueTask<TResult>> selector, CancellationToken cancellationToken = default) => MinAwaitWithCancellationAsyncCore<TSource, TResult>(source, selector, cancellationToken);
        public static IOrderedAsyncEnumerable<TSource> OrderByAwaitWithCancellation<TSource, TKey>(this IAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, ValueTask<TKey>> keySelector) => OrderByAwaitWithCancellationCore<TSource, TKey>(source, keySelector);
        public static IOrderedAsyncEnumerable<TSource> OrderByAwaitWithCancellation<TSource, TKey>(this IAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, ValueTask<TKey>> keySelector, IComparer<TKey> comparer) => OrderByAwaitWithCancellationCore<TSource, TKey>(source, keySelector, comparer);
        public static IOrderedAsyncEnumerable<TSource> OrderByDescendingAwaitWithCancellation<TSource, TKey>(this IAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, ValueTask<TKey>> keySelector) => OrderByDescendingAwaitWithCancellationCore<TSource, TKey>(source, keySelector);
        public static IOrderedAsyncEnumerable<TSource> OrderByDescendingAwaitWithCancellation<TSource, TKey>(this IAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, ValueTask<TKey>> keySelector, IComparer<TKey> comparer) => OrderByDescendingAwaitWithCancellationCore<TSource, TKey>(source, keySelector, comparer);
        public static IAsyncEnumerable<TResult> SelectAwaitWithCancellation<TSource, TResult>(this IAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, ValueTask<TResult>> selector) => SelectAwaitWithCancellationCore<TSource, TResult>(source, selector);
        public static IAsyncEnumerable<TResult> SelectAwaitWithCancellation<TSource, TResult>(this IAsyncEnumerable<TSource> source, Func<TSource, int, CancellationToken, ValueTask<TResult>> selector) => SelectAwaitWithCancellationCore<TSource, TResult>(source, selector);
        public static IAsyncEnumerable<TResult> SelectManyAwaitWithCancellation<TSource, TResult>(this IAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, ValueTask<IAsyncEnumerable<TResult>>> selector) => SelectManyAwaitWithCancellationCore<TSource, TResult>(source, selector);
        public static IAsyncEnumerable<TResult> SelectManyAwaitWithCancellation<TSource, TResult>(this IAsyncEnumerable<TSource> source, Func<TSource, int, CancellationToken, ValueTask<IAsyncEnumerable<TResult>>> selector) => SelectManyAwaitWithCancellationCore<TSource, TResult>(source, selector);
        public static IAsyncEnumerable<TResult> SelectManyAwaitWithCancellation<TSource, TCollection, TResult>(this IAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, ValueTask<IAsyncEnumerable<TCollection>>> collectionSelector, Func<TSource, TCollection, CancellationToken, ValueTask<TResult>> resultSelector) => SelectManyAwaitWithCancellationCore<TSource, TCollection, TResult>(source, collectionSelector, resultSelector);
        public static IAsyncEnumerable<TResult> SelectManyAwaitWithCancellation<TSource, TCollection, TResult>(this IAsyncEnumerable<TSource> source, Func<TSource, int, CancellationToken, ValueTask<IAsyncEnumerable<TCollection>>> collectionSelector, Func<TSource, TCollection, CancellationToken, ValueTask<TResult>> resultSelector) => SelectManyAwaitWithCancellationCore<TSource, TCollection, TResult>(source, collectionSelector, resultSelector);
        public static ValueTask<TSource> SingleAwaitWithCancellationAsync<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, ValueTask<bool>> predicate, CancellationToken cancellationToken = default) => SingleAwaitWithCancellationAsyncCore<TSource>(source, predicate, cancellationToken);
        public static ValueTask<TSource?> SingleOrDefaultAwaitWithCancellationAsync<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, ValueTask<bool>> predicate, CancellationToken cancellationToken = default) => SingleOrDefaultAwaitWithCancellationAsyncCore<TSource>(source, predicate, cancellationToken);
        public static IAsyncEnumerable<TSource> SkipWhileAwaitWithCancellation<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, ValueTask<bool>> predicate) => SkipWhileAwaitWithCancellationCore<TSource>(source, predicate);
        public static IAsyncEnumerable<TSource> SkipWhileAwaitWithCancellation<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, int, CancellationToken, ValueTask<bool>> predicate) => SkipWhileAwaitWithCancellationCore<TSource>(source, predicate);
        public static ValueTask<int?> SumAwaitWithCancellationAsync<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, ValueTask<int?>> selector, CancellationToken cancellationToken = default) => SumAwaitWithCancellationAsyncCore<TSource>(source, selector, cancellationToken);
        public static ValueTask<long?> SumAwaitWithCancellationAsync<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, ValueTask<long?>> selector, CancellationToken cancellationToken = default) => SumAwaitWithCancellationAsyncCore<TSource>(source, selector, cancellationToken);
        public static ValueTask<float?> SumAwaitWithCancellationAsync<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, ValueTask<float?>> selector, CancellationToken cancellationToken = default) => SumAwaitWithCancellationAsyncCore<TSource>(source, selector, cancellationToken);
        public static ValueTask<double?> SumAwaitWithCancellationAsync<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, ValueTask<double?>> selector, CancellationToken cancellationToken = default) => SumAwaitWithCancellationAsyncCore<TSource>(source, selector, cancellationToken);
        public static ValueTask<decimal?> SumAwaitWithCancellationAsync<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, ValueTask<decimal?>> selector, CancellationToken cancellationToken = default) => SumAwaitWithCancellationAsyncCore<TSource>(source, selector, cancellationToken);
        public static ValueTask<int> SumAwaitWithCancellationAsync<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, ValueTask<int>> selector, CancellationToken cancellationToken = default) => SumAwaitWithCancellationAsyncCore<TSource>(source, selector, cancellationToken);
        public static ValueTask<long> SumAwaitWithCancellationAsync<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, ValueTask<long>> selector, CancellationToken cancellationToken = default) => SumAwaitWithCancellationAsyncCore<TSource>(source, selector, cancellationToken);
        public static ValueTask<float> SumAwaitWithCancellationAsync<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, ValueTask<float>> selector, CancellationToken cancellationToken = default) => SumAwaitWithCancellationAsyncCore<TSource>(source, selector, cancellationToken);
        public static ValueTask<double> SumAwaitWithCancellationAsync<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, ValueTask<double>> selector, CancellationToken cancellationToken = default) => SumAwaitWithCancellationAsyncCore<TSource>(source, selector, cancellationToken);
        public static ValueTask<decimal> SumAwaitWithCancellationAsync<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, ValueTask<decimal>> selector, CancellationToken cancellationToken = default) => SumAwaitWithCancellationAsyncCore<TSource>(source, selector, cancellationToken);
        public static IAsyncEnumerable<TSource> TakeWhileAwaitWithCancellation<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, ValueTask<bool>> predicate) => TakeWhileAwaitWithCancellationCore<TSource>(source, predicate);
        public static IAsyncEnumerable<TSource> TakeWhileAwaitWithCancellation<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, int, CancellationToken, ValueTask<bool>> predicate) => TakeWhileAwaitWithCancellationCore<TSource>(source, predicate);
        public static IOrderedAsyncEnumerable<TSource> ThenByAwaitWithCancellation<TSource, TKey>(this IOrderedAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, ValueTask<TKey>> keySelector) => ThenByAwaitWithCancellationCore<TSource, TKey>(source, keySelector);
        public static IOrderedAsyncEnumerable<TSource> ThenByAwaitWithCancellation<TSource, TKey>(this IOrderedAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, ValueTask<TKey>> keySelector, IComparer<TKey> comparer) => ThenByAwaitWithCancellationCore<TSource, TKey>(source, keySelector, comparer);
        public static IOrderedAsyncEnumerable<TSource> ThenByDescendingAwaitWithCancellation<TSource, TKey>(this IOrderedAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, ValueTask<TKey>> keySelector) => ThenByDescendingAwaitWithCancellationCore<TSource, TKey>(source, keySelector);
        public static IOrderedAsyncEnumerable<TSource> ThenByDescendingAwaitWithCancellation<TSource, TKey>(this IOrderedAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, ValueTask<TKey>> keySelector, IComparer<TKey> comparer) => ThenByDescendingAwaitWithCancellationCore<TSource, TKey>(source, keySelector, comparer);
        public static ValueTask<Dictionary<TKey, TSource>> ToDictionaryAwaitWithCancellationAsync<TSource, TKey>(this IAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, ValueTask<TKey>> keySelector, CancellationToken cancellationToken = default) where TKey : notnull => ToDictionaryAwaitWithCancellationAsyncCore<TSource, TKey>(source, keySelector, cancellationToken);
        public static ValueTask<Dictionary<TKey, TSource>> ToDictionaryAwaitWithCancellationAsync<TSource, TKey>(this IAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, ValueTask<TKey>> keySelector, IEqualityComparer<TKey> comparer, CancellationToken cancellationToken = default) where TKey : notnull => ToDictionaryAwaitWithCancellationAsyncCore<TSource, TKey>(source, keySelector, comparer, cancellationToken);
        public static ValueTask<Dictionary<TKey, TElement>> ToDictionaryAwaitWithCancellationAsync<TSource, TKey, TElement>(this IAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, ValueTask<TKey>> keySelector, Func<TSource, CancellationToken, ValueTask<TElement>> elementSelector, CancellationToken cancellationToken = default) where TKey : notnull => ToDictionaryAwaitWithCancellationAsyncCore<TSource, TKey, TElement>(source, keySelector, elementSelector, cancellationToken);
        public static ValueTask<Dictionary<TKey, TElement>> ToDictionaryAwaitWithCancellationAsync<TSource, TKey, TElement>(this IAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, ValueTask<TKey>> keySelector, Func<TSource, CancellationToken, ValueTask<TElement>> elementSelector, IEqualityComparer<TKey> comparer, CancellationToken cancellationToken = default) where TKey : notnull => ToDictionaryAwaitWithCancellationAsyncCore<TSource, TKey, TElement>(source, keySelector, elementSelector, comparer, cancellationToken);
        public static ValueTask<ImmutableDictionary<TKey, TSource>> ToImmutableDictionaryAwaitWithCancellationAsync<TSource, TKey>(this IAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, ValueTask<TKey>> keySelector, CancellationToken cancellationToken = default) where TKey : notnull => ToImmutableDictionaryAwaitWithCancellationAsyncCore<TSource, TKey>(source, keySelector, cancellationToken);
        public static ValueTask<ImmutableDictionary<TKey, TValue>> ToImmutableDictionaryAwaitWithCancellationAsync<TSource, TKey, TValue>(this IAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, ValueTask<TKey>> keySelector, Func<TSource, CancellationToken, ValueTask<TValue>> elementSelector, CancellationToken cancellationToken = default) where TKey : notnull => ToImmutableDictionaryAwaitWithCancellationAsyncCore<TSource, TKey, TValue>(source, keySelector, elementSelector, cancellationToken);
        public static ValueTask<ImmutableDictionary<TKey, TSource>> ToImmutableDictionaryAwaitWithCancellationAsync<TSource, TKey>(this IAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, ValueTask<TKey>> keySelector, IEqualityComparer<TKey>? keyComparer, CancellationToken cancellationToken = default) where TKey : notnull => ToImmutableDictionaryAwaitWithCancellationAsyncCore<TSource, TKey>(source, keySelector, keyComparer, cancellationToken);
        public static ValueTask<ImmutableDictionary<TKey, TValue>> ToImmutableDictionaryAwaitWithCancellationAsync<TSource, TKey, TValue>(this IAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, ValueTask<TKey>> keySelector, Func<TSource, CancellationToken, ValueTask<TValue>> elementSelector, IEqualityComparer<TKey>? keyComparer, CancellationToken cancellationToken = default) where TKey : notnull => ToImmutableDictionaryAwaitWithCancellationAsyncCore<TSource, TKey, TValue>(source, keySelector, elementSelector, keyComparer, cancellationToken);
        public static ValueTask<ImmutableDictionary<TKey, TValue>> ToImmutableDictionaryAwaitWithCancellationAsync<TSource, TKey, TValue>(this IAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, ValueTask<TKey>> keySelector, Func<TSource, CancellationToken, ValueTask<TValue>> elementSelector, IEqualityComparer<TKey>? keyComparer, IEqualityComparer<TValue>? valueComparer, CancellationToken cancellationToken = default) where TKey : notnull => ToImmutableDictionaryAwaitWithCancellationAsyncCore<TSource, TKey, TValue>(source, keySelector, elementSelector, keyComparer, valueComparer, cancellationToken);
        public static ValueTask<ImmutableSortedDictionary<TKey, TValue>> ToImmutableSortedDictionaryAwaitWithCancellationAsync<TSource, TKey, TValue>(this IAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, ValueTask<TKey>> keySelector, Func<TSource, CancellationToken, ValueTask<TValue>> elementSelector, CancellationToken cancellationToken = default) where TKey : notnull => ToImmutableSortedDictionaryAwaitWithCancellationAsyncCore<TSource, TKey, TValue>(source, keySelector, elementSelector, cancellationToken);
        public static ValueTask<ImmutableSortedDictionary<TKey, TValue>> ToImmutableSortedDictionaryAwaitWithCancellationAsync<TSource, TKey, TValue>(this IAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, ValueTask<TKey>> keySelector, Func<TSource, CancellationToken, ValueTask<TValue>> elementSelector, IComparer<TKey>? keyComparer, CancellationToken cancellationToken = default) where TKey : notnull => ToImmutableSortedDictionaryAwaitWithCancellationAsyncCore<TSource, TKey, TValue>(source, keySelector, elementSelector, keyComparer, cancellationToken);
        public static ValueTask<ImmutableSortedDictionary<TKey, TValue>> ToImmutableSortedDictionaryAwaitWithCancellationAsync<TSource, TKey, TValue>(this IAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, ValueTask<TKey>> keySelector, Func<TSource, CancellationToken, ValueTask<TValue>> elementSelector, IComparer<TKey>? keyComparer, IEqualityComparer<TValue>? valueComparer, CancellationToken cancellationToken = default) where TKey : notnull => ToImmutableSortedDictionaryAwaitWithCancellationAsyncCore<TSource, TKey, TValue>(source, keySelector, elementSelector, keyComparer, valueComparer, cancellationToken);
        public static ValueTask<ILookup<TKey, TSource>> ToLookupAwaitWithCancellationAsync<TSource, TKey>(this IAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, ValueTask<TKey>> keySelector, CancellationToken cancellationToken = default) => ToLookupAwaitWithCancellationAsyncCore<TSource, TKey>(source, keySelector, cancellationToken);
        public static ValueTask<ILookup<TKey, TElement>> ToLookupAwaitWithCancellationAsync<TSource, TKey, TElement>(this IAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, ValueTask<TKey>> keySelector, Func<TSource, CancellationToken, ValueTask<TElement>> elementSelector, CancellationToken cancellationToken = default) => ToLookupAwaitWithCancellationAsyncCore<TSource, TKey, TElement>(source, keySelector, elementSelector, cancellationToken);
        public static ValueTask<ILookup<TKey, TSource>> ToLookupAwaitWithCancellationAsync<TSource, TKey>(this IAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, ValueTask<TKey>> keySelector, IEqualityComparer<TKey> comparer, CancellationToken cancellationToken = default) => ToLookupAwaitWithCancellationAsyncCore<TSource, TKey>(source, keySelector, comparer, cancellationToken);
        public static ValueTask<ILookup<TKey, TElement>> ToLookupAwaitWithCancellationAsync<TSource, TKey, TElement>(this IAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, ValueTask<TKey>> keySelector, Func<TSource, CancellationToken, ValueTask<TElement>> elementSelector, IEqualityComparer<TKey> comparer, CancellationToken cancellationToken = default) => ToLookupAwaitWithCancellationAsyncCore<TSource, TKey, TElement>(source, keySelector, elementSelector, comparer, cancellationToken);
        public static IAsyncEnumerable<TSource> WhereAwaitWithCancellation<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, ValueTask<bool>> predicate) => WhereAwaitWithCancellationCore<TSource>(source, predicate);
        public static IAsyncEnumerable<TSource> WhereAwaitWithCancellation<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, int, CancellationToken, ValueTask<bool>> predicate) => WhereAwaitWithCancellationCore<TSource>(source, predicate);
        public static IAsyncEnumerable<TResult> ZipAwaitWithCancellation<TFirst, TSecond, TResult>(this IAsyncEnumerable<TFirst> first, IAsyncEnumerable<TSecond> second, Func<TFirst, TSecond, CancellationToken, ValueTask<TResult>> selector) => ZipAwaitWithCancellationCore<TFirst, TSecond, TResult>(first, second, selector);
#endif
#endif
    }
}

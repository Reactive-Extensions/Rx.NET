// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT License.
// See the LICENSE file in the project root for more information.

using System.Collections.Generic;
using System.Collections.Immutable;
using System.Threading;
using System.Threading.Tasks;
using static System.Linq.Async.System.Linq.FunctionalHelpers;

namespace System.Linq
{
    public static partial class AsyncEnumerable
    {
        /// <summary>
        /// Creates an immutable sorted dictionary from an async-enumerable sequence according to a specified key selector function, and an element selector function.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements in the source sequence.</typeparam>
        /// <typeparam name="TKey">The type of the dictionary key computed for each element in the source sequence.</typeparam>
        /// <typeparam name="TValue">The type of the dictionary value computed for each element in the source sequence.</typeparam>
        /// <param name="source">An async-enumerable sequence to create a dictionary for.</param>
        /// <param name="keySelector">A function to extract a key from each element.</param>
        /// <param name="elementSelector">A transform function to produce a result element value from each element.</param>
        /// <param name="cancellationToken">The optional cancellation token to be used for cancelling the sequence at any time.</param>
        /// <returns>An async-enumerable sequence containing a single element with a dictionary mapping unique key values onto the corresponding source sequence's element.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/>, <paramref name="keySelector"/>, or <paramref name="elementSelector"/> is null.</exception>
        /// <remarks>The return type of this operator differs from the corresponding operator on IEnumerable in order to retain asynchronous behavior.</remarks>
        public static ValueTask<ImmutableSortedDictionary<TKey, TValue>> ToImmutableSortedDictionaryAsync<TSource, TKey, TValue>(this IAsyncEnumerable<TSource> source, Func<TSource, TKey> keySelector, Func<TSource, TValue> elementSelector, CancellationToken cancellationToken = default) where TKey : notnull
            => source.ToImmutableSortedDictionaryAsync(keySelector, elementSelector, keyComparer: null, valueComparer: null, cancellationToken);

        /// <summary>
        /// Creates an immutable sorted dictionary from an async-enumerable sequence according to a specified key selector function, an element selector function, and a key comparer.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements in the source sequence.</typeparam>
        /// <typeparam name="TKey">The type of the dictionary key computed for each element in the source sequence.</typeparam>
        /// <typeparam name="TValue">The type of the dictionary value computed for each element in the source sequence.</typeparam>
        /// <param name="source">An async-enumerable sequence to create a dictionary for.</param>
        /// <param name="keySelector">A function to extract a key from each element.</param>
        /// <param name="elementSelector">A transform function to produce a result element value from each element.</param>
        /// <param name="keyComparer">A comparer to compare keys.</param>
        /// <param name="cancellationToken">The optional cancellation token to be used for cancelling the sequence at any time.</param>
        /// <returns>An async-enumerable sequence containing a single element with a dictionary mapping unique key values onto the corresponding source sequence's element.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/>, <paramref name="keySelector"/>, or <paramref name="elementSelector"/> is null.</exception>
        /// <remarks>The return type of this operator differs from the corresponding operator on IEnumerable in order to retain asynchronous behavior.</remarks>
        public static ValueTask<ImmutableSortedDictionary<TKey, TValue>> ToImmutableSortedDictionaryAsync<TSource, TKey, TValue>(this IAsyncEnumerable<TSource> source, Func<TSource, TKey> keySelector, Func<TSource, TValue> elementSelector, IComparer<TKey>? keyComparer, CancellationToken cancellationToken = default) where TKey : notnull
            => source.ToImmutableSortedDictionaryAsync(keySelector, elementSelector, keyComparer, valueComparer: null, cancellationToken);

        /// <summary>
        /// Creates an immutable sorted dictionary from an async-enumerable sequence of key/value pairs.
        /// </summary>
        /// <typeparam name="TKey">The type of the dictionary key computed for each element in the source sequence.</typeparam>
        /// <typeparam name="TValue">The type of the dictionary value computed for each element in the source sequence.</typeparam>
        /// <param name="source">An async-enumerable sequence to create a dictionary for.</param>
        /// <param name="cancellationToken">The optional cancellation token to be used for cancelling the sequence at any time.</param>
        /// <returns>An async-enumerable sequence containing a single element with a dictionary mapping unique key values onto the corresponding source sequence's element.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> is null.</exception>
        /// <remarks>The return type of this operator differs from the corresponding operator on IEnumerable in order to retain asynchronous behavior.</remarks>
        public static ValueTask<ImmutableSortedDictionary<TKey, TValue>> ToImmutableSortedDictionaryAsync<TKey, TValue>(this IAsyncEnumerable<KeyValuePair<TKey, TValue>> source, CancellationToken cancellationToken = default) where TKey : notnull
            => source.ToImmutableSortedDictionaryAsync(keySelector: Key, elementSelector: Value, keyComparer: null, valueComparer: null, cancellationToken);

        /// <summary>
        /// Creates an immutable sorted dictionary from an async-enumerable sequence of key/value pairs according to a specified key comparer.
        /// </summary>
        /// <typeparam name="TKey">The type of the dictionary key computed for each element in the source sequence.</typeparam>
        /// <typeparam name="TValue">The type of the dictionary value computed for each element in the source sequence.</typeparam>
        /// <param name="keyComparer">A comparer to compare keys.</param>
        /// <param name="source">An async-enumerable sequence to create a dictionary for.</param>
        /// <param name="cancellationToken">The optional cancellation token to be used for cancelling the sequence at any time.</param>
        /// <returns>An async-enumerable sequence containing a single element with a dictionary mapping unique key values onto the corresponding source sequence's element.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> is null.</exception>
        /// <remarks>The return type of this operator differs from the corresponding operator on IEnumerable in order to retain asynchronous behavior.</remarks>
        public static ValueTask<ImmutableSortedDictionary<TKey, TValue>> ToImmutableSortedDictionaryAsync<TKey, TValue>(this IAsyncEnumerable<KeyValuePair<TKey, TValue>> source, IComparer<TKey>? keyComparer, CancellationToken cancellationToken = default) where TKey : notnull
            => source.ToImmutableSortedDictionaryAsync(keySelector: Key, elementSelector: Value, keyComparer, valueComparer: null, cancellationToken);

        /// <summary>
        /// Creates an immutable sorted dictionary from an async-enumerable sequence of key/value pairs according to a specified key comparer and a value equality comparer.
        /// </summary>
        /// <typeparam name="TKey">The type of the dictionary key computed for each element in the source sequence.</typeparam>
        /// <typeparam name="TValue">The type of the dictionary value computed for each element in the source sequence.</typeparam>
        /// <param name="keyComparer">A comparer to compare keys.</param>
        /// <param name="valueComparer">An equality comparer to compare values.</param>
        /// <param name="source">An async-enumerable sequence to create a dictionary for.</param>
        /// <param name="cancellationToken">The optional cancellation token to be used for cancelling the sequence at any time.</param>
        /// <returns>An async-enumerable sequence containing a single element with a dictionary mapping unique key values onto the corresponding source sequence's element.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> is null.</exception>
        /// <remarks>The return type of this operator differs from the corresponding operator on IEnumerable in order to retain asynchronous behavior.</remarks>
        public static ValueTask<ImmutableSortedDictionary<TKey, TValue>> ToImmutableSortedDictionaryAsync<TKey, TValue>(this IAsyncEnumerable<KeyValuePair<TKey, TValue>> source, IComparer<TKey>? keyComparer, IEqualityComparer<TValue>? valueComparer, CancellationToken cancellationToken = default) where TKey : notnull
            => source.ToImmutableSortedDictionaryAsync(keySelector: Key, elementSelector: Value, keyComparer, valueComparer, cancellationToken);

        /// <summary>
        /// Creates an sorted immutable dictionary from an async-enumerable sequence according to a specified key selector function, an element selector function, a key comparer and a value equality comparer.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements in the source sequence.</typeparam>
        /// <typeparam name="TKey">The type of the dictionary key computed for each element in the source sequence.</typeparam>
        /// <typeparam name="TValue">The type of the dictionary value computed for each element in the source sequence.</typeparam>
        /// <param name="source">An async-enumerable sequence to create a dictionary for.</param>
        /// <param name="keySelector">A function to extract a key from each element.</param>
        /// <param name="elementSelector">A transform function to produce a result element value from each element.</param>
        /// <param name="keyComparer">A comparer to compare keys.</param>
        /// <param name="valueComparer">An equality comparer to compare values.</param>
        /// <param name="cancellationToken">The optional cancellation token to be used for cancelling the sequence at any time.</param>
        /// <returns>An async-enumerable sequence containing a single element with a dictionary mapping unique key values onto the corresponding source sequence's element.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/>, <paramref name="keySelector"/>, or <paramref name="elementSelector"/> is null.</exception>
        /// <remarks>The return type of this operator differs from the corresponding operator on IEnumerable in order to retain asynchronous behavior.</remarks>
        public static ValueTask<ImmutableSortedDictionary<TKey, TValue>> ToImmutableSortedDictionaryAsync<TSource, TKey, TValue>(
            this IAsyncEnumerable<TSource> source,
            Func<TSource, TKey> keySelector, Func<TSource, TValue> elementSelector,
            IComparer<TKey>? keyComparer, IEqualityComparer<TValue>? valueComparer,
            CancellationToken cancellationToken = default)
            where TKey : notnull
        {
            if (source == null)
                throw Error.ArgumentNull(nameof(source));
            if (keySelector == null)
                throw Error.ArgumentNull(nameof(keySelector));
            if (elementSelector == null)
                throw Error.ArgumentNull(nameof(elementSelector));

            return Core(source, keySelector, elementSelector, keyComparer, valueComparer, cancellationToken);

            static async ValueTask<ImmutableSortedDictionary<TKey, TValue>> Core(
                IAsyncEnumerable<TSource> source,
                Func<TSource, TKey> keySelector, Func<TSource, TValue> elementSelector,
                IComparer<TKey>? keyComparer, IEqualityComparer<TValue>? valueComparer,
                CancellationToken cancellationToken = default)
            {
                var builder = ImmutableSortedDictionary.CreateBuilder(keyComparer, valueComparer);

                await foreach (var item in source.WithCancellation(cancellationToken).ConfigureAwait(false))
                {
                    builder.Add(keySelector(item), elementSelector(item));
                }

                return builder.ToImmutable();
            }
        }

        private static ValueTask<ImmutableSortedDictionary<TKey, TValue>> ToImmutableSortedDictionaryAwaitAsyncCore<TSource, TKey, TValue>(this IAsyncEnumerable<TSource> source, Func<TSource, ValueTask<TKey>> keySelector, Func<TSource, ValueTask<TValue>> elementSelector, CancellationToken cancellationToken = default) where TKey : notnull
            => source.ToImmutableSortedDictionaryAwaitAsyncCore(keySelector, elementSelector, keyComparer: null, valueComparer: null, cancellationToken);

        private static ValueTask<ImmutableSortedDictionary<TKey, TValue>> ToImmutableSortedDictionaryAwaitAsyncCore<TSource, TKey, TValue>(this IAsyncEnumerable<TSource> source, Func<TSource, ValueTask<TKey>> keySelector, Func<TSource, ValueTask<TValue>> elementSelector, IComparer<TKey>? keyComparer, CancellationToken cancellationToken = default) where TKey : notnull
            => source.ToImmutableSortedDictionaryAwaitAsyncCore(keySelector, elementSelector, keyComparer, valueComparer: null, cancellationToken);

        private static ValueTask<ImmutableSortedDictionary<TKey, TValue>> ToImmutableSortedDictionaryAwaitAsyncCore<TSource, TKey, TValue>(
            this IAsyncEnumerable<TSource> source,
            Func<TSource, ValueTask<TKey>> keySelector, Func<TSource, ValueTask<TValue>> elementSelector,
            IComparer<TKey>? keyComparer, IEqualityComparer<TValue>? valueComparer,
            CancellationToken cancellationToken = default)
            where TKey : notnull
        {
            if (source == null)
                throw Error.ArgumentNull(nameof(source));
            if (keySelector == null)
                throw Error.ArgumentNull(nameof(keySelector));
            if (elementSelector == null)
                throw Error.ArgumentNull(nameof(elementSelector));

            return Core(source, keySelector, elementSelector, keyComparer, valueComparer, cancellationToken);

            static async ValueTask<ImmutableSortedDictionary<TKey, TValue>> Core(
                IAsyncEnumerable<TSource> source,
                Func<TSource, ValueTask<TKey>> keySelector, Func<TSource, ValueTask<TValue>> elementSelector,
                IComparer<TKey>? keyComparer, IEqualityComparer<TValue>? valueComparer,
                CancellationToken cancellationToken = default)
            {
                var builder = ImmutableSortedDictionary.CreateBuilder(keyComparer, valueComparer);

                await foreach (var item in source.WithCancellation(cancellationToken).ConfigureAwait(false))
                {
                    builder.Add(await keySelector(item).ConfigureAwait(false), await elementSelector(item).ConfigureAwait(false));
                }

                return builder.ToImmutable();
            }
        }

        private static ValueTask<ImmutableSortedDictionary<TKey, TValue>> ToImmutableSortedDictionaryAwaitWithCancellationAsyncCore<TSource, TKey, TValue>(this IAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, ValueTask<TKey>> keySelector, Func<TSource, CancellationToken, ValueTask<TValue>> elementSelector, CancellationToken cancellationToken = default) where TKey : notnull
            => source.ToImmutableSortedDictionaryAwaitWithCancellationAsyncCore(keySelector, elementSelector, keyComparer: null, valueComparer: null, cancellationToken);

        private static ValueTask<ImmutableSortedDictionary<TKey, TValue>> ToImmutableSortedDictionaryAwaitWithCancellationAsyncCore<TSource, TKey, TValue>(this IAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, ValueTask<TKey>> keySelector, Func<TSource, CancellationToken, ValueTask<TValue>> elementSelector, IComparer<TKey>? keyComparer, CancellationToken cancellationToken = default) where TKey : notnull
            => source.ToImmutableSortedDictionaryAwaitWithCancellationAsyncCore(keySelector, elementSelector, keyComparer, valueComparer: null, cancellationToken);

        private static ValueTask<ImmutableSortedDictionary<TKey, TValue>> ToImmutableSortedDictionaryAwaitWithCancellationAsyncCore<TSource, TKey, TValue>(
            this IAsyncEnumerable<TSource> source,
            Func<TSource, CancellationToken, ValueTask<TKey>> keySelector, Func<TSource, CancellationToken, ValueTask<TValue>> elementSelector,
            IComparer<TKey>? keyComparer, IEqualityComparer<TValue>? valueComparer,
            CancellationToken cancellationToken = default)
            where TKey : notnull
        {
            if (source == null)
                throw Error.ArgumentNull(nameof(source));
            if (keySelector == null)
                throw Error.ArgumentNull(nameof(keySelector));
            if (elementSelector == null)
                throw Error.ArgumentNull(nameof(elementSelector));

            return Core(source, keySelector, elementSelector, keyComparer, valueComparer, cancellationToken);

            static async ValueTask<ImmutableSortedDictionary<TKey, TValue>> Core(
                IAsyncEnumerable<TSource> source,
                Func<TSource, CancellationToken, ValueTask<TKey>> keySelector, Func<TSource, CancellationToken, ValueTask<TValue>> elementSelector,
                IComparer<TKey>? keyComparer, IEqualityComparer<TValue>? valueComparer,
                CancellationToken cancellationToken = default)
            {
                var builder = ImmutableSortedDictionary.CreateBuilder(keyComparer, valueComparer);

                await foreach (var item in source.WithCancellation(cancellationToken).ConfigureAwait(false))
                {
                    builder.Add(await keySelector(item, cancellationToken).ConfigureAwait(false), await elementSelector(item, cancellationToken).ConfigureAwait(false));
                }

                return builder.ToImmutable();
            }
        }
    }
}

// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT License.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Tests
{
    public class ToImmutableSortedDictionary : AsyncEnumerableTests
    {
        [Fact]
        public async Task ToImmutableSortedDictionary_Null()
        {
            await Assert.ThrowsAsync<ArgumentNullException>(() => AsyncEnumerable.ToImmutableSortedDictionaryAsync<int, int, int>(default, x => 0, x => 0).AsTask());
            await Assert.ThrowsAsync<ArgumentNullException>(() => AsyncEnumerable.ToImmutableSortedDictionaryAsync<int, int, int>(Return42, default, x => 0).AsTask());
            await Assert.ThrowsAsync<ArgumentNullException>(() => AsyncEnumerable.ToImmutableSortedDictionaryAsync<int, int, int>(Return42, x => 0, default).AsTask());

            await Assert.ThrowsAsync<ArgumentNullException>(() => AsyncEnumerable.ToImmutableSortedDictionaryAsync<int, int, int>(default, x => 0, x => 0, Comparer<int>.Default).AsTask());
            await Assert.ThrowsAsync<ArgumentNullException>(() => AsyncEnumerable.ToImmutableSortedDictionaryAsync(Return42, default, x => 0, Comparer<int>.Default).AsTask());
            await Assert.ThrowsAsync<ArgumentNullException>(() => AsyncEnumerable.ToImmutableSortedDictionaryAsync<int, int, int>(Return42, x => 0, default, Comparer<int>.Default).AsTask());

            await Assert.ThrowsAsync<ArgumentNullException>(() => AsyncEnumerable.ToImmutableSortedDictionaryAsync<int, int, int>(default, x => 0, x => 0, CancellationToken.None).AsTask());
            await Assert.ThrowsAsync<ArgumentNullException>(() => AsyncEnumerable.ToImmutableSortedDictionaryAsync<int, int, int>(Return42, default, x => 0, CancellationToken.None).AsTask());
            await Assert.ThrowsAsync<ArgumentNullException>(() => AsyncEnumerable.ToImmutableSortedDictionaryAsync<int, int, int>(Return42, x => 0, default, CancellationToken.None).AsTask());

            await Assert.ThrowsAsync<ArgumentNullException>(() => AsyncEnumerable.ToImmutableSortedDictionaryAsync<int, int, int>(default, x => 0, x => 0, Comparer<int>.Default, CancellationToken.None).AsTask());
            await Assert.ThrowsAsync<ArgumentNullException>(() => AsyncEnumerable.ToImmutableSortedDictionaryAsync(Return42, default, x => 0, Comparer<int>.Default, CancellationToken.None).AsTask());
            await Assert.ThrowsAsync<ArgumentNullException>(() => AsyncEnumerable.ToImmutableSortedDictionaryAsync<int, int, int>(Return42, x => 0, default, Comparer<int>.Default, CancellationToken.None).AsTask());
        }

        [Fact]
        public async Task ToImmutableSortedDictionary1Async()
        {
            var xs = new[] { 1, 4 }.ToAsyncEnumerable();
            var res = await xs.ToImmutableSortedDictionaryAsync(x => x % 2, x => x);
            Assert.True(res[0] == 4);
            Assert.True(res[1] == 1);
        }

        [Fact]
        public async Task ToImmutableSortedDictionary2Async()
        {
            var xs = new[] { 1, 4, 2 }.ToAsyncEnumerable();
            await AssertThrowsAsync<ArgumentException>(xs.ToImmutableSortedDictionaryAsync(x => x % 2, x => x).AsTask());
        }

        [Fact]
        public async Task ToImmutableSortedDictionary3Async()
        {
            var xs = new[] { 1, 4 }.ToAsyncEnumerable();
            var res = await xs.ToImmutableSortedDictionaryAsync(x => x % 2, x => x + 1);
            Assert.True(res[0] == 5);
            Assert.True(res[1] == 2);
        }

        [Fact]
        public async Task ToImmutableSortedDictionary4Async()
        {
            var xs = new[] { 1, 4, 2 }.ToAsyncEnumerable();
            await AssertThrowsAsync<ArgumentException>(xs.ToImmutableSortedDictionaryAsync(x => x % 2, x => x + 1).AsTask());
        }

        [Fact]
        public async Task ToImmutableSortedDictionary5Async()
        {
            var xs = new[] { 1, 4 }.ToAsyncEnumerable();
            var res = await xs.ToImmutableSortedDictionaryAsync(x => x % 2, x => x, new Eq());
            Assert.True(res[0] == 4);
            Assert.True(res[1] == 1);
        }

        [Fact]
        public async Task ToImmutableSortedDictionary6Async()
        {
            var xs = new[] { 1, 4, 2 }.ToAsyncEnumerable();
            await AssertThrowsAsync<ArgumentException>(xs.ToImmutableSortedDictionaryAsync(x => x % 2, x => x, new Eq()).AsTask());
        }

        [Fact]
        public async Task ToImmutableSortedDictionary7Async()
        {
            var xs = new[] { 1, 4 }.ToAsyncEnumerable();
            var res = await xs.ToImmutableSortedDictionaryAsync(x => x % 2, x => x, new Eq());
            Assert.True(res[0] == 4);
            Assert.True(res[1] == 1);
        }

        [Fact]
        public async Task ToImmutableSortedDictionary8Async()
        {
            var xs = new[] { 5, 8, 7, 1, 9 }.ToAsyncEnumerable();
            var res = await xs.ToImmutableSortedDictionaryAsync(x => x, x => x);
            Assert.Equal(new[] { 1, 5, 7, 8, 9 }, res.Keys);
        }

        private sealed class Eq : IComparer<int>
        {
            public int Compare(int x, int y) => Comparer<int>.Default.Compare(Math.Abs(x), Math.Abs(y));
        }
    }
}

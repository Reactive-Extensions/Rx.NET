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
    public class ToImmutableDictionary : AsyncEnumerableTests
    {
        [Fact]
        public async Task ToImmutableDictionary_Null()
        {
            await Assert.ThrowsAsync<ArgumentNullException>(() => AsyncEnumerable.ToImmutableDictionaryAsync<int, int>(default, x => 0).AsTask());
            await Assert.ThrowsAsync<ArgumentNullException>(() => AsyncEnumerable.ToImmutableDictionaryAsync(Return42, default(Func<int, int>)).AsTask());

            await Assert.ThrowsAsync<ArgumentNullException>(() => AsyncEnumerable.ToImmutableDictionaryAsync<int, int>(default, x => 0, EqualityComparer<int>.Default).AsTask());
            await Assert.ThrowsAsync<ArgumentNullException>(() => AsyncEnumerable.ToImmutableDictionaryAsync(Return42, default, EqualityComparer<int>.Default).AsTask());

            await Assert.ThrowsAsync<ArgumentNullException>(() => AsyncEnumerable.ToImmutableDictionaryAsync<int, int, int>(default, x => 0, x => 0).AsTask());
            await Assert.ThrowsAsync<ArgumentNullException>(() => AsyncEnumerable.ToImmutableDictionaryAsync<int, int, int>(Return42, default, x => 0).AsTask());
            await Assert.ThrowsAsync<ArgumentNullException>(() => AsyncEnumerable.ToImmutableDictionaryAsync<int, int, int>(Return42, x => 0, default).AsTask());

            await Assert.ThrowsAsync<ArgumentNullException>(() => AsyncEnumerable.ToImmutableDictionaryAsync<int, int, int>(default, x => 0, x => 0, EqualityComparer<int>.Default).AsTask());
            await Assert.ThrowsAsync<ArgumentNullException>(() => AsyncEnumerable.ToImmutableDictionaryAsync(Return42, default, x => 0, EqualityComparer<int>.Default).AsTask());
            await Assert.ThrowsAsync<ArgumentNullException>(() => AsyncEnumerable.ToImmutableDictionaryAsync<int, int, int>(Return42, x => 0, default, EqualityComparer<int>.Default).AsTask());

            await Assert.ThrowsAsync<ArgumentNullException>(() => AsyncEnumerable.ToImmutableDictionaryAsync<int, int>(default, x => 0, CancellationToken.None).AsTask());
            await Assert.ThrowsAsync<ArgumentNullException>(() => AsyncEnumerable.ToImmutableDictionaryAsync(Return42, default(Func<int, int>), CancellationToken.None).AsTask());

            await Assert.ThrowsAsync<ArgumentNullException>(() => AsyncEnumerable.ToImmutableDictionaryAsync<int, int>(default, x => 0, EqualityComparer<int>.Default, CancellationToken.None).AsTask());
            await Assert.ThrowsAsync<ArgumentNullException>(() => AsyncEnumerable.ToImmutableDictionaryAsync(Return42, default, EqualityComparer<int>.Default, CancellationToken.None).AsTask());

            await Assert.ThrowsAsync<ArgumentNullException>(() => AsyncEnumerable.ToImmutableDictionaryAsync<int, int, int>(default, x => 0, x => 0, CancellationToken.None).AsTask());
            await Assert.ThrowsAsync<ArgumentNullException>(() => AsyncEnumerable.ToImmutableDictionaryAsync<int, int, int>(Return42, default, x => 0, CancellationToken.None).AsTask());
            await Assert.ThrowsAsync<ArgumentNullException>(() => AsyncEnumerable.ToImmutableDictionaryAsync<int, int, int>(Return42, x => 0, default, CancellationToken.None).AsTask());

            await Assert.ThrowsAsync<ArgumentNullException>(() => AsyncEnumerable.ToImmutableDictionaryAsync<int, int, int>(default, x => 0, x => 0, EqualityComparer<int>.Default, CancellationToken.None).AsTask());
            await Assert.ThrowsAsync<ArgumentNullException>(() => AsyncEnumerable.ToImmutableDictionaryAsync(Return42, default, x => 0, EqualityComparer<int>.Default, CancellationToken.None).AsTask());
            await Assert.ThrowsAsync<ArgumentNullException>(() => AsyncEnumerable.ToImmutableDictionaryAsync<int, int, int>(Return42, x => 0, default, EqualityComparer<int>.Default, CancellationToken.None).AsTask());
        }

        [Fact]
        public async Task ToImmutableDictionary1Async()
        {
            var xs = new[] { 1, 4 }.ToAsyncEnumerable();
            var res = await xs.ToImmutableDictionaryAsync(x => x % 2);
            Assert.True(res[0] == 4);
            Assert.True(res[1] == 1);
        }

        [Fact]
        public async Task ToImmutableDictionary2Async()
        {
            var xs = new[] { 1, 4, 2 }.ToAsyncEnumerable();
            await AssertThrowsAsync<ArgumentException>(xs.ToImmutableDictionaryAsync(x => x % 2).AsTask());
        }

        [Fact]
        public async Task ToImmutableDictionary3Async()
        {
            var xs = new[] { 1, 4 }.ToAsyncEnumerable();
            var res = await xs.ToImmutableDictionaryAsync(x => x % 2, x => x + 1);
            Assert.True(res[0] == 5);
            Assert.True(res[1] == 2);
        }

        [Fact]
        public async Task ToImmutableDictionary4Async()
        {
            var xs = new[] { 1, 4, 2 }.ToAsyncEnumerable();
            await AssertThrowsAsync<ArgumentException>(xs.ToImmutableDictionaryAsync(x => x % 2, x => x + 1).AsTask());
        }

        [Fact]
        public async Task ToImmutableDictionary5Async()
        {
            var xs = new[] { 1, 4 }.ToAsyncEnumerable();
            var res = await xs.ToImmutableDictionaryAsync(x => x % 2, new Eq());
            Assert.True(res[0] == 4);
            Assert.True(res[1] == 1);
        }

        [Fact]
        public async Task ToImmutableDictionary6Async()
        {
            var xs = new[] { 1, 4, 2 }.ToAsyncEnumerable();
            await AssertThrowsAsync<ArgumentException>(xs.ToImmutableDictionaryAsync(x => x % 2, new Eq()).AsTask());
        }

        [Fact]
        public async Task ToImmutableDictionary7Async()
        {
            var xs = new[] { 1, 4 }.ToAsyncEnumerable();
            var res = await xs.ToImmutableDictionaryAsync(x => x % 2, x => x, new Eq());
            Assert.True(res[0] == 4);
            Assert.True(res[1] == 1);
        }

        private sealed class Eq : IEqualityComparer<int>
        {
            public bool Equals(int x, int y) => EqualityComparer<int>.Default.Equals(Math.Abs(x), Math.Abs(y));

            public int GetHashCode(int obj) => EqualityComparer<int>.Default.GetHashCode(Math.Abs(obj));
        }
    }
}

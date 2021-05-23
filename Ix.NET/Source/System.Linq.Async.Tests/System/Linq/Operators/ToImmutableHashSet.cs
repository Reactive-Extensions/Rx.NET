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
    public class ToImmutableHashSet : AsyncEnumerableTests
    {
        [Fact]
        public async Task ToImmutableHashSet_Null()
        {
            await Assert.ThrowsAsync<ArgumentNullException>(() => AsyncEnumerable.ToImmutableHashSetAsync<int>(default).AsTask());
            await Assert.ThrowsAsync<ArgumentNullException>(() => AsyncEnumerable.ToImmutableHashSetAsync<int>(default, CancellationToken.None).AsTask());

            await Assert.ThrowsAsync<ArgumentNullException>(() => AsyncEnumerable.ToImmutableHashSetAsync(default, EqualityComparer<int>.Default, CancellationToken.None).AsTask());
        }

        [Fact]
        public async Task ToImmutableHashSet_Simple()
        {
            var xs = new[] { 1, 2, 1, 2, 3, 4, 1, 2, 3, 4 };
            var res = xs.ToAsyncEnumerable().ToImmutableHashSetAsync();
            Assert.True((await res).OrderBy(x => x).SequenceEqual(new[] { 1, 2, 3, 4 }));
        }

        [Fact]
        public async Task ToImmutableHashSet_Comparer()
        {
            var xs = new[] { 1, 12, 11, 2, 3, 14, 1, 12, 13, 4 };
            var res = xs.ToAsyncEnumerable().ToImmutableHashSetAsync(new Eq());
            Assert.True((await res).OrderBy(x => x).SequenceEqual(new[] { 1, 3, 12, 14 }));
        }

        private class Eq : IEqualityComparer<int>
        {
            public bool Equals(int x, int y) => x % 10 == y % 10;

            public int GetHashCode(int obj) => obj % 10;
        }
    }
}

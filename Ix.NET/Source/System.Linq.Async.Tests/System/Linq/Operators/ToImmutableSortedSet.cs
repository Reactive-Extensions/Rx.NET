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
    public class ToImmutableSortedSet : AsyncEnumerableTests
    {
        [Fact]
        public async Task ToImmutableSortedSet_Null()
        {
            await Assert.ThrowsAsync<ArgumentNullException>(() => AsyncEnumerable.ToImmutableSortedSetAsync<int>(default).AsTask());
            await Assert.ThrowsAsync<ArgumentNullException>(() => AsyncEnumerable.ToImmutableSortedSetAsync<int>(default, CancellationToken.None).AsTask());

            await Assert.ThrowsAsync<ArgumentNullException>(() => AsyncEnumerable.ToImmutableSortedSetAsync(default, Comparer<int>.Default, CancellationToken.None).AsTask());
        }

        [Fact]
        public async Task ToImmutableSortedSet_Simple()
        {
            var xs = new[] { 1, 2, 1, 2, 3, 4, 1, 2, 3, 4 };
            var res = xs.ToAsyncEnumerable().ToImmutableSortedSetAsync();
            Assert.Equal(new[] { 1, 2, 3, 4 }, await res);
        }

        [Fact]
        public async Task ToImmutableSortedSet_Comparer()
        {
            var xs = new[] { 1, 12, 11, 2, 3, 14, 1, 12, 13, 4 };
            var res = xs.ToAsyncEnumerable().ToImmutableSortedSetAsync(new Eq());
            Assert.Equal(new[] { 1, 12, 3, 14 }, await res);
        }

        [Fact]
        public async Task ToImmutableSortedSet_Sorted()
        {
            var xs = new[] { 5, 8, 7, 1, 9 }.ToAsyncEnumerable();
            var res = await xs.ToImmutableSortedSetAsync();
            Assert.Equal(new[] { 1, 5, 7, 8, 9 }, res);
        }

        private sealed class Eq : IComparer<int>
        {
            public int Compare(int x, int y) => Comparer<int>.Default.Compare(x % 10, y % 10);
        }
    }
}

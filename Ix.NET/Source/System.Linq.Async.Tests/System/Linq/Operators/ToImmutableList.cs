// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT License.
// See the LICENSE file in the project root for more information.

using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Tests
{
    public class ToImmutableList : AsyncEnumerableTests
    {
        [Fact]
        public async Task ToImmutableList_Null()
        {
            await Assert.ThrowsAsync<ArgumentNullException>(() => AsyncEnumerable.ToImmutableListAsync<int>(default).AsTask());
            await Assert.ThrowsAsync<ArgumentNullException>(() => AsyncEnumerable.ToImmutableListAsync<int>(default, CancellationToken.None).AsTask());
        }

        [Fact]
        public async Task ToImmutableList_Simple()
        {
            var xs = new[] { 42, 25, 39 };
            var res = xs.ToAsyncEnumerable().ToImmutableListAsync();
            Assert.True((await res).SequenceEqual(xs));
        }

        [Fact]
        public async Task ToImmutableList_Empty()
        {
            var xs = AsyncEnumerable.Empty<int>();
            var res = xs.ToImmutableListAsync();
            Assert.True((await res).Count == 0);
        }

        [Fact]
        public async Task ToImmutableList_Throw()
        {
            var ex = new Exception("Bang!");
            var res = Throw<int>(ex).ToImmutableListAsync();
            await AssertThrowsAsync(res, ex);
        }
    }
}

namespace Holf.Tests
{
    using System;
    using System.Linq;

    using Xunit;

    public class Partition
    {
        [Fact]
        public void ShouldSplitOddsAndEvens()
        {
            // arrange
            var data = new[] { 1, 2, 3, 4, 5 };

            // act
            var result = data.Partition(i => i % 2);
            var odds = result[1];
            var evens = result[0];

            // assert
            Assert.Equal(odds.ToArray(), new[] { 1, 3, 5 });
            Assert.Equal(evens.ToArray(), new[] { 2, 4 });
        }

        [Fact]
        public void CannotPartitionWhenYieldDoesNotProducePositiveInteger()
        {
            // arrange
            var data = new[] { 1, 2, 3 };

            // act
            Assert.ThrowsDelegate code = () => data.Partition(i => -1);

            // assert
            Assert.Throws<IndexOutOfRangeException>(code);
        }
    }
}

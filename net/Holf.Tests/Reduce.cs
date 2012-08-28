namespace Holf.Tests
{
    using System.Collections.Generic;
    using System.Linq;

    using Xunit;

    using System;

    public class Reduce
    {
        private void Example()
        {
            var input = new[] { 1, 2, 3, 4, 5 };
            var result = input.First();
            foreach (var item in input.Skip(1))
            {
                if (result < item)
                {
                    result = item;
                }
            }
        }

        [Fact]
        public void ShouldSelectMaxItemInList()
        {
            // arrange
            var data = new[] { 1, 2, 5, 3, 4 };

            // act
            var result = data.Reduce((a, b) => a > b ? a : b);
            
            // assert
            Assert.Equal(5, result);
        }

        [Fact]
        public void ShouldReduceToNumber()
        {
            // arrange
            var data = new[] { 1, 2, 3 };

            // act
            var result = data.Reduce((agg, i) => (agg * 10) + i);

            // assert
            Assert.Equal(123, result);
        }

        [Fact]
        public void CannotReduceEmptyList()
        {
            // arrange
            var items = new int[0];

            // act
            Assert.ThrowsDelegate code = () => items.Reduce((a, b) => a);

            // assert
            Assert.Throws<ArgumentException>(code);
        }
    }
}

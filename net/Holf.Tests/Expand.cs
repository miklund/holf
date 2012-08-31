namespace Holf.Tests
{
    using System;
    using System.Linq;

    using Xunit;

    public class Expand
    {
        [Fact]
        public void ShouldCalculateAnnualTaxesOnAmount()
        {
            // arrange
            const double Data = 10000.0;

            // act
            var taxes = Data.Expand(i => i * 0.9); // 10%

            // assert
            Assert.Equal(new[] { 10000.0, 9000.0, 8100.0 }, taxes.Take(3));
        }

        [Fact]
        public void ShouldCalculateTheQubicOfEachInteger()
        {           
            // act
            var numbers = Tuple.Create(1, 1)
                .Expand(PowerOfTwo)
                .Map(t => t.Item2);

            // assert
            Assert.Equal(new[] { 1, 4, 9, 16, 25 }, numbers.Take(5).ToArray());
        }

        private static Tuple<int, int> PowerOfTwo(Tuple<int, int> number)
        {
            var next = number.Item1 + 1;
            return Tuple.Create(next, next * next);
        }
    }
}

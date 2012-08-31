namespace Holf.Tests
{
    using Xunit;

    public class Fold
    {
        [Fact]
        public void ShouldSumAllNumbers()
        {
            // arrange
            var data = new[] { 1, 2, 3 };

            // act
            var result = data.Fold((agg, i) => agg + i, 0);

            // assert
            Assert.Equal(6, result);
        }

        [Fact]
        public void ShouldMergeIntoSring()
        {
            // arrange
            var data = new[] { 1, 2, 3 };

            // act
            var result = data.Fold((agg, i) => agg + i, string.Empty);

            // assert
            Assert.Equal("123", result);
        }
    }
}

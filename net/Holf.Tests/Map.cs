namespace Holf.Tests
{
    using Xunit;

    public class Map
    {
        [Fact]
        public void ShouldDoubleEachNumberInList()
        {
            // arrange
            var data = new[] { 1, 2, 3 };

            // act
            var result = data.Map(i => i * 2);

            // assert
            Assert.Equal(new[] { 2, 4, 6 }, result);
        }

        [Fact]
        public void ShouldReturnFloatingPointNumbersInResult()
        {
            // arrange
            var data = new[] { 1, 3, 5 };

            // act
            var result = data.Map(i => i / 2.0);

            // assert
            Assert.Equal(new[] { 0.5, 1.5, 2.5 }, result);
        }

        [Fact]
        public void ShouldReturnNotPrimitiveTypeString()
        {
            // arrange
            var data = new[] { 1, 3, 5 };

            // act
            var result = data.Map(i => i.ToString());

            // assert
            Assert.Equal(new[] { "1", "3", "5" }, result);
        }

        [Fact]
        public void ShouldReturnEmptyListForEmptyInput()
        {
            // arrange
            var data = new int[0];

            // act
            var result = data.Map(i => i);

            // assert
            Assert.Equal(new int[0], result);
        }
    }
}

using Xunit;

namespace MapNavigator.Tests
{
    /// <summary>
    /// Tests against the <see cref="MapResult"/> class.
    /// </summary>
    public class MapResultTests
    {
        [Fact]
        public void ConstructorArgumentsGetAssigned()
        {
            var startPos = new GridCoordinate(2, 4);
            var endPos = new GridCoordinate(4, 6);

            var result = new MapResult(startPos, endPos);

            Assert.Equal(startPos, result.StartPosition);
            Assert.Equal(endPos, result.EndPosition);
        }

        [Fact]
        public void TestAbsolutePositionValue()
        {
            var startPos = new GridCoordinate();
            var endPos = new GridCoordinate(-10, -10);

            // Absolute should be 10, 10.
            var expected = new GridCoordinate(10, 10);

            var result = new MapResult(startPos, endPos);

            Assert.Equal(expected, result.AbsolutePositionDelta);
        }
    }
}

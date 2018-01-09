using Xunit;

namespace MapNavigator.Tests
{
    /// <summary>
    /// Tests against the <see cref="Trigonometry"/> class.
    /// </summary>
    public class TrigonometryTests
    {
        // Also serves as a test of if I remember high school math...

        [Theory]
        [InlineData(0, 0)]
        [InlineData(90, 1)]
        [InlineData(180, 0)]
        [InlineData(270, -1)]
        [InlineData(360, 0)]
        public void ValidateSinForDegrees(double degrees, double expected)
        {
            Assert.Equal(expected, Trigonometry.Sin(degrees));
        }

        [Theory]
        [InlineData(0, 1)]
        [InlineData(90, 0)]
        [InlineData(180, -1)]
        [InlineData(270, 0)]
        [InlineData(360, 1)]
        public void ValidateCosForDegrees(double degrees, double expected)
        {
            Assert.Equal(expected, Trigonometry.Cos(degrees));
        }
    }
}

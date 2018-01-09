using Xunit;

namespace MapNavigator.Tests
{
    /// <summary>
    /// Tests against the <see cref="InstructionSimulatorOptions"/> class.
    /// </summary>
    public class InstructionSimulatorOptionsTests
    {
        [Fact]
        public void ValidateDefaultsLocked()
        {
            var defaultOptions = new InstructionSimulatorOptions();

            Assert.Equal(1, defaultOptions.GridSpacing);
            Assert.Equal(90, defaultOptions.StartingDegree);
        }

        [Theory]
        [InlineData(73.2)]
        [InlineData(180.0)]
        [InlineData(-3)]
        public void TestDegreeConstructorOverload(double startingDegree)
        {
            var option = new InstructionSimulatorOptions(startingDegree: startingDegree);

            // Degree should be persisted.
            Assert.Equal(startingDegree, option.StartingDegree);

            // Grid spacing should always be set to null for this overload.
            Assert.Null(option.GridSpacing);
        }

        [Theory]
        [InlineData(1, 90)]
        [InlineData(1.5, 180)]
        [InlineData(24, 270)]
        [InlineData(8, -360)]
        public void TestFullConstructorWithValidData(double gridSpacing, double startingDegree)
        {
            var option = new InstructionSimulatorOptions(
                gridSpacing: gridSpacing,
                startingDegree: startingDegree);

            Assert.Equal(gridSpacing, option.GridSpacing);
            Assert.Equal(startingDegree, option.StartingDegree);
        }

        [Theory]
        [InlineData(1, 90.1)]
        [InlineData(1, 91)]
        [InlineData(1, 45)]
        [InlineData(1, .5)]
        public void TestFullConstructorWithInvalidData(double gridSpacing, double startingDegree)
        {
            // (All non-90 divisible degrees should throw an exception).
            Assert.Throws<MapNavigatorException>(
                () => new InstructionSimulatorOptions(
                    gridSpacing: gridSpacing,
                    startingDegree: startingDegree));
        }
    }
}

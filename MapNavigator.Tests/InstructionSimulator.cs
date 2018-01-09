using System.Collections.Generic;
using Xunit;

namespace MapNavigator.Tests
{
    /// <summary>
    /// Tests against the <see cref="InstructionSimulator"/> class.
    /// </summary>
    public class InstructionSimulatorTests
    {
        [Fact]
        public void AllowsNullOptionsParameter()
        {
            new InstructionSimulator(options: null);
        }

        [Fact]
        public void DefaultSimulateResults()
        {
            var sim = new InstructionSimulator();

            var leftOne = new RelativeInstruction(
                    direction: new RelativeDirection(90),
                    travelDistance: 1);

            // Three left turns + 1 step should get us at 0, -1
            var simList = new List<RelativeInstruction>
            {
                leftOne,
                leftOne,
                leftOne
            };

            var result = sim.Simulate(simList);

            // These are almost integration-worthy tests. But they are still useful.
            Assert.Equal(result.StartPosition, new GridCoordinate(0, 0));
            Assert.Equal(result.EndPosition, new GridCoordinate(0, -1));
        }

        [Theory]
        [InlineData(45)]
        [InlineData(32.45)]
        [InlineData(388.234)]
        public void DefaultSimulateBlocksNonNinetyAngles(double angle)
        {
            var sim = new InstructionSimulator();

            Assert.Throws<MapNavigatorException>(() => sim.Simulate(new List<RelativeInstruction>
            {
                new RelativeInstruction(
                    direction: new RelativeDirection(angle),
                    travelDistance: 1)
            }));
        }

        [Theory]
        [InlineData(34.2)]
        [InlineData(-4.23)]
        public void DefaultSimulateBlocksNonIntegerDistances(double distance)
        {
            var sim = new InstructionSimulator();

            Assert.Throws<MapNavigatorException>(() => sim.Simulate(new List<RelativeInstruction>
            {
                new RelativeInstruction(
                    direction: new RelativeDirection(90),
                    travelDistance: distance)
            }));
        }
    }
}

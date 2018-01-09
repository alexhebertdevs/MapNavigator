using System;
using Xunit;

namespace MapNavigator.Tests
{
    /// <summary>
    /// Tests against the <see cref="RelativeInstruction"/> class.
    /// </summary>
    public class RelativeInstructionTests
    {
        [Fact]
        public void ConstructorDoesNotAllowNullDirection()
        {
            Assert.Throws<ArgumentNullException>(() =>
                new RelativeInstruction(null, 0));
        }

        [Fact]
        public void ConstructorArgumentsGetAssigned()
        {
            var direction = new RelativeDirection(0);
            const double travelDistance = 38.3;

            var instruction = new RelativeInstruction(
                direction: direction,
                travelDistance: travelDistance);

            Assert.Equal(direction, instruction.Direction);
            Assert.Equal(travelDistance, instruction.TravelDistance);
        }
    }
}

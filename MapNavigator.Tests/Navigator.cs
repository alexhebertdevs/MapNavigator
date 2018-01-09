using Xunit;
using Moq;
using System;
using System.Collections.Generic;

namespace MapNavigator.Tests
{
    /// <summary>
    /// Tests against the <see cref="Navigator"/> class.
    /// </summary>
    public class NavigatorTests
    {
        [Fact]
        public void ConstructorThrowsArgumentNulls()
        {
            var parserMock = new Mock<IFullnstructionParser>(MockBehavior.Strict);
            var instructionSimMock = new Mock<IInstructionSimulator>(MockBehavior.Strict);

            // This should be safe.
            new Navigator(parserMock.Object, instructionSimMock.Object);

            Assert.Throws<ArgumentNullException>(() => new Navigator(null, instructionSimMock.Object));
            Assert.Throws<ArgumentNullException>(() => new Navigator(parserMock.Object, null));
        }

        [Fact]
        public void SimulateThrowsArgumentNull()
        {
            var parserMock = new Mock<IFullnstructionParser>(MockBehavior.Strict);
            var instructionSimMock = new Mock<IInstructionSimulator>(MockBehavior.Strict);

            var nav = new Navigator(parserMock.Object, instructionSimMock.Object);

            Assert.Throws<ArgumentNullException>(() => nav.Simulate(null));
        }

        [Theory]
        [InlineData("TestData!")]
        public void TestSimulate(string testInstructions)
        {
            var parserMock = new Mock<IFullnstructionParser>(MockBehavior.Strict);
            
            parserMock.Setup(x => x.ParseRawInstructions(testInstructions)).
                Returns(new List<RelativeInstruction>());

            var startPos = new GridCoordinate(8, 8);
            var endPos = new GridCoordinate(16, 16);

            var instructionSimMock = new Mock<IInstructionSimulator>(MockBehavior.Strict);
            instructionSimMock.Setup(x => x.Simulate(It.IsNotNull<IReadOnlyList<RelativeInstruction>>())).
                Returns(new MapResult(startPos, endPos));

            var nav = new Navigator(parserMock.Object, instructionSimMock.Object);

            var result = nav.Simulate(testInstructions);

            Assert.NotNull(result);

            parserMock.Verify(x => x.ParseRawInstructions(testInstructions), Times.Once);
            instructionSimMock.Verify(x => x.Simulate(It.IsNotNull<IReadOnlyList<RelativeInstruction>>()), Times.Once);
        }
    }
}

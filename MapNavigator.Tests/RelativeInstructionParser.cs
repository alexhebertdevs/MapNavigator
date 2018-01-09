using Moq;
using System;
using Xunit;

namespace MapNavigator.Tests
{
    /// <summary>
    /// Tests against the <see cref="RelativeInstructionParser"/> class.
    /// </summary>
    public class RelativeInstructionParserTests
    {
        [Fact]
        public void ConstructorThrowsArgumentNull()
        {
            Assert.Throws<ArgumentNullException>(() =>
                new RelativeInstructionParser(singleInstructionParser: null));
        }

        [Fact]
        public void ParserThrowsArgumentNull()
        {
            var instructionParserMock = new Mock<ISingleInstructionParser>(MockBehavior.Strict);

            var fullParser = new RelativeInstructionParser(instructionParserMock.Object);

            Assert.Throws<ArgumentNullException>(() =>
                fullParser.ParseRawInstructions(null));
        }

        [Theory]
        [InlineData("1,2,3,4,5", 5)]
        [InlineData("A,B,C", 3)]
        [InlineData("", 1)]
        [InlineData(",", 2)]
        public void TestParser(string instructions, int expectedCount)
        {
            var singleInstructionMock = new Mock<ISingleInstructionParser>(MockBehavior.Strict);

            var testInstruction = new RelativeInstruction(
                direction: new RelativeDirection(90),
                travelDistance: 1);

            singleInstructionMock.Setup(p => p.ParseSingleInstruction(It.IsNotNull<string>())).
                Returns(testInstruction);

            var fullParser = new RelativeInstructionParser(
                singleInstructionParser: singleInstructionMock.Object);

            var result = fullParser.ParseRawInstructions(instructions);

            // Make sure result is not null.
            Assert.NotNull(result);

            // Make sure the number of things we expected came back.
            Assert.Equal(expectedCount, result.Count);

            // Make sure the parser mock got called as many times as we expect.
            singleInstructionMock.Verify(p => p.ParseSingleInstruction(It.IsNotNull<string>()), Times.Exactly(expectedCount));

            // Make sure the items returned by the single parser
            // get included in the final output.
            foreach(var item in result)
            {
                Assert.Equal(testInstruction, item);
            }

        }
    }
}

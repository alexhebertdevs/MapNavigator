using System;
using Xunit;

namespace MapNavigator.Tests
{
    /// <summary>
    /// Tests against the <see cref="SingleNamedInstructionParser"/> class.
    /// </summary>
    public class SingleNamedInstructionParserTests
    {
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("   ")]
        public void ParseSingleInstructionThrowsArgumentException(string badInput)
        {
            var parser = new SingleNamedInstructionParser();

            Assert.Throws<ArgumentException>(
                () => parser.ParseSingleInstruction(badInput));
        }

        // I'm going to save more detailed parsing tests
        // involving this class
        // for full integration tests.
    }
}

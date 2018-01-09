using System;
using Xunit;

namespace MapNavigator.Tests
{
    /// <summary>
    /// Tests against the <see cref="NamedRelativeDirection"/> class.
    /// </summary>
    public class NamedRelativeDirectionTests
    {
        // I'm going to avoid testing specific parsings
        // in these tests, since that makes it harder to change
        // the string values. Not ideal.

        [Theory]
        [InlineData(90)]
        public void VerifyLeft(double expected)
        {
            var left = NamedRelativeDirection.Left;
            Assert.Equal(expected, left.DegreeInfluence);
            Assert.NotNull(left.Abbreviation);
            Assert.NotNull(left.Name);
        }

        [Theory]
        [InlineData(-90)]
        public void VerifyRight(double expected)
        {
            var right = NamedRelativeDirection.Right;
            Assert.Equal(expected, right.DegreeInfluence);
            Assert.NotNull(right.Abbreviation);
            Assert.NotNull(right.Name);
        }

        [Fact]
        public void ParserThrowsArgumentNull()
        {
            Assert.Throws<ArgumentNullException>(
                () => NamedRelativeDirection.Parse(null));
        }

        [Theory]
        [InlineData("!$@#")]
        [InlineData("NOT a valid input")]
        [InlineData("And other great tests")]
        public void UnexpectedInputsThrowException(string input)
        {
            Assert.Throws<MapNavigatorException>(() =>
                NamedRelativeDirection.Parse(input));
        }
    }
}

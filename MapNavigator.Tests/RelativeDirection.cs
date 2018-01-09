using Xunit;

namespace MapNavigator.Tests
{
    /// <summary>
    /// Tests against the <see cref="RelativeDirection"/> class.
    /// </summary>
    public class RelativeDirectionTests
    {
        [Theory]
        [InlineData(10)]
        [InlineData(-34)]
        [InlineData(53.25)]
        public void DegreeInfluenceMapsIn(double degreeInfluence)
        {
            var direction = new RelativeDirection(degreeInfluence: degreeInfluence);

            Assert.Equal(degreeInfluence, direction.DegreeInfluence);
        }

        [Theory]
        [InlineData(53.23)]
        [InlineData(-21)]
        public void TestToString(double degreeInfluence)
        {
            var direction = new RelativeDirection(degreeInfluence: degreeInfluence);
            Assert.Equal($"{degreeInfluence}°", direction.ToString());
        }
    }
}

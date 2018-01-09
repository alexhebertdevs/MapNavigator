using Xunit;
using System;

namespace MapNavigator.Tests
{
    /// <summary>
    /// Tests against the <see cref="GridCoordinate"/> struct.
    /// </summary>
    public class GridCoordinateTests
    {
        [Fact]
        public void StructRemainsAsValueType()
        {
            Assert.True(typeof(GridCoordinate).IsValueType);
        }

        /// <summary>
        /// Makes sure the default constructor gives a proper initialization.
        /// </summary>
        [Fact]
        public void DefaultConstructorDefaultsToZero()
        {
            var defaultCoordinate = new GridCoordinate();
            Assert.Equal(0, defaultCoordinate.XPosition);
            Assert.Equal(0, defaultCoordinate.YPosition);
        }

        /// <summary>
        /// Checks that values supplied in constructor persist to properties.
        /// </summary>
        [Theory]
        [InlineData(8, -4)]
        public void ParameterizedConstructor(double xPos, double yPos)
        {
            var paramaterizedGrid = new GridCoordinate(
                xPosition: xPos,
                yPosition: yPos);

            Assert.Equal(xPos, paramaterizedGrid.XPosition);
            Assert.Equal(yPos, paramaterizedGrid.YPosition);
        }

        [Theory]
        [InlineData(15)]
        public void TravelToStraightUp(double testDistance)
        {
            var startPos = new GridCoordinate();

            const double upDegree = 90;

            var finishPos = startPos.TravelTo(upDegree, testDistance);

            var expectedFinishPos = new GridCoordinate(
                xPosition: 0,
                yPosition: testDistance);

            Assert.Equal(expectedFinishPos, finishPos);
        }

        [Theory]
        [InlineData(44.3)]
        public void TravelToLeft(double testDistance)
        {
            var startPos = new GridCoordinate();

            const double leftDegree = 180;

            var finishPos = startPos.TravelTo(leftDegree, testDistance);

            var expectedFinishPos = new GridCoordinate(
                xPosition: -1 * testDistance,
                yPosition: 0);

            Assert.Equal(expectedFinishPos, finishPos);
        }

        [Theory]
        [InlineData(-23.52, -45.21)]
        public void ValidateAbsoluteComponents(double xPos, double yPos)
        {
            var absoluteCoord = new GridCoordinate(xPos, yPos).AbsoluteComponents();

            var expected = new GridCoordinate(Math.Abs(xPos), Math.Abs(yPos));

            Assert.Equal(expected, absoluteCoord);
        }

        [Theory]
        [InlineData(-3, -4, 5)]
        public void DistanceTraveledShortest(double xPos, double yPos, double expected)
        {
            // This should yield a distance of 5.
            var testCoord = new GridCoordinate(xPos, yPos);

            var shortestResult = testCoord.DistanceTraveledFromOrigin(DistanceCalculationMode.ShortestPath);

            Assert.Equal(expected, shortestResult);
        }

        [Theory]
        [InlineData(-3, -4)]
        public void DistanceTraveledXandYComponents(double xPos, double yPos)
        {
            var testCoord = new GridCoordinate(xPos, yPos);

            var addedResult = testCoord.DistanceTraveledFromOrigin(DistanceCalculationMode.XComponentPlusYComponent);

            double expected = Math.Abs(xPos) + Math.Abs(yPos);

            Assert.Equal(expected, addedResult);
        }

        [Theory]
        [InlineData(834.23, -232, -43.52, 34.555)]
        public void PlusOperator(double x1, double y1, double x2, double y2)
        {
            var point1 = new GridCoordinate(x1, y1);
            var point2 = new GridCoordinate(x2, y2);

            var expected = new GridCoordinate(x1 + x2, y1 + y2);

            Assert.Equal(expected, point1 + point2);
        }

        [Theory]
        [InlineData(77, -23.3, -90, 67.12)]
        public void MinusOperator(double x1, double y1, double x2, double y2)
        {
            var point1 = new GridCoordinate(x1, y1);
            var point2 = new GridCoordinate(x2, y2);

            var expected = new GridCoordinate(x1 - x2, y1 - y2);

            Assert.Equal(expected, point1 - point2);
        }

        [Theory]
        [InlineData(-3, 65.2)]
        public void VerifyToString(double xPos, double yPos)
        {
            var testCoord = new GridCoordinate(xPos, yPos);

            string expected = $"({xPos},{yPos})";

            Assert.Equal(expected, testCoord.ToString());
        }

    }
}

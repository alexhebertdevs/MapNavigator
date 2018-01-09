using System;

namespace MapNavigator
{
    /// <summary>
    /// A value type structure used to hold a coordinate on a grid system.
    /// </summary>
    public struct GridCoordinate
    {
        /// <summary>
        /// Required constructor to assign any non-0 coordinate values.
        /// </summary>
        /// <param name="xPosition"></param>
        /// <param name="yPosition"></param>
        public GridCoordinate(double xPosition, double yPosition)
        {
            XPosition = xPosition;
            YPosition = yPosition;
        }

        /// <summary>
        /// Holds the value for the x component of a coordinate.
        /// </summary>
        public double XPosition { get; }

        /// <summary>
        /// Holds the value for the y component of a coordinate.
        /// </summary>
        public double YPosition { get; }

        /// <summary>
        /// Returns the coordinate that results from traveling from the current coordinate, at the specified angle,
        /// for the specified distance.
        /// </summary>
        /// <param name="angleDegree"></param>
        /// <param name="distance"></param>
        /// <returns></returns>
        public GridCoordinate TravelTo(double angleDegree, double distance)
        {
            double xPosDelta = distance * Trigonometry.Cos(angleDegree);
            double yPosDelta = distance * Trigonometry.Sin(angleDegree);

            return new GridCoordinate(
                xPosition: XPosition + xPosDelta,
                yPosition: YPosition + yPosDelta);
        }

        /// <summary>
        /// Returns the current grid, but only includes the absolute values of its
        /// horizontal and vertical components.
        /// </summary>
        /// <returns></returns>
        public GridCoordinate AbsoluteComponents()
        {
            return new GridCoordinate(
                xPosition: Math.Abs(XPosition),
                yPosition: Math.Abs(YPosition));
        }

        /// <summary>
        /// Takes the current coordinate, and treats it as if it represents a complete trip from the origin.
        /// Calculates the distance from the origin the trip encompasses.
        /// </summary>
        /// <param name="mode"></param>
        /// <returns></returns>
        public double DistanceTraveledFromOrigin(DistanceCalculationMode mode)
        {
            double distance;

            // Treating "Shortest Path" as the option default.
            switch(mode)
            {
                case DistanceCalculationMode.XComponentPlusYComponent:
                    // Since the signs of the x and y matter, first get the absolute components of the current
                    // coordinate if this option is chosen before adding them.
                    var absolute = AbsoluteComponents();
                    distance = absolute.XPosition + absolute.YPosition;
                    break;
                case DistanceCalculationMode.ShortestPath:
                default:
                    distance = Trigonometry.Hypotenuse(XPosition, YPosition);
                    break;
            }

            return distance;
        }

        /// <summary>
        /// Provides a human-readable format of the coordinate.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
            => $"({XPosition},{YPosition})";

        /// <summary>
        /// Adds the horizontal and vertical components of two coordinates to produce a new coordinate.
        /// </summary>
        /// <param name="first"></param>
        /// <param name="second"></param>
        /// <returns></returns>
        public static GridCoordinate operator +(GridCoordinate first, GridCoordinate second)
        {
            return new GridCoordinate(
                xPosition: first.XPosition + second.XPosition,
                yPosition: first.YPosition + second.YPosition);
        }

        /// <summary>
        /// Subtracts the horizontal and vertical components of two coordinates to produce a new coordinate.
        /// </summary>
        /// <param name="first"></param>
        /// <param name="second"></param>
        /// <returns></returns>
        public static GridCoordinate operator -(GridCoordinate first, GridCoordinate second)
        {
            return new GridCoordinate(
                xPosition: first.XPosition - second.XPosition,
                yPosition: first.YPosition - second.YPosition);
        }

    }

    /// <summary>
    /// Supplies options to calculate a distance for a <see cref="GridCoordinate"/>.
    /// </summary>
    public enum DistanceCalculationMode
    {
        /// <summary>
        /// Distance is calculated using the shortest possible path from the origin to the <see cref="GridCoordinate"/>.
        /// </summary>
        ShortestPath,

        /// <summary>
        /// Distance is calculated by adding the absolute components of horizontal and vertical distance traveled only.
        /// </summary>
        XComponentPlusYComponent
    }
}

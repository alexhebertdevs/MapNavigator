using System;

namespace MapNavigator
{
    /// <summary>
    /// Contains useful methods and constants related to common trig functions.
    /// </summary>
    internal static class Trigonometry
    {
        /// <summary>
        /// Holds a constant value to refer to 90 degrees.
        /// </summary>
        public const double NinetyDegrees = 90;

        /// <summary>
        /// Performs a <see cref="Math.Cos(double)"/> operation on a degree instead of a radian.
        /// </summary>
        /// <param name="degrees"></param>
        /// <returns></returns>
        public static double Cos(double degrees)
            => Math.Cos(degrees.DegreesToRadians()).RoundTrigResult();

        /// <summary>
        /// Performs a <see cref="Math.Sin"/> operation on a degree instead of a radian.
        /// </summary>
        /// <param name="degrees"></param>
        /// <returns></returns>
        public static double Sin(double degrees)
            => Math.Sin(degrees.DegreesToRadians()).RoundTrigResult();

        /// <summary>
        /// Calculates the hypotenuse, given two sides of a triangle.
        /// </summary>
        /// <param name="side1"></param>
        /// <param name="side2"></param>
        /// <returns></returns>
        public static double Hypotenuse(double side1, double side2)
            => Math.Sqrt(side1 * side1 + side2 * side2).RoundTrigResult();

        /// <summary>
        /// Since trig doubles aren't exact, we will round results to a set number of
        /// tolerable decimal places to avoid getting back .999999999999 numbers.
        /// </summary>
        /// <param name="trigResult"></param>
        /// <returns></returns>
        private static double RoundTrigResult(this double trigResult)
            => Math.Round(trigResult, 8);

        /// <summary>
        /// Converts degrees to radians.
        /// </summary>
        /// <param name="degrees"></param>
        /// <returns></returns>
        private static double DegreesToRadians(this double degrees)
            => (degrees * (Math.PI / 180.0)).RoundTrigResult();
    }
}

namespace MapNavigator
{
    /// <summary>
    /// The bare components of what constitutes a relative map direction.
    /// </summary>
    public class RelativeDirection
    {
        /// <summary>
        /// Creates a new <see cref="RelativeDirection"/>, set to the specified degree angle.
        /// </summary>
        /// <param name="degreeInfluence"></param>
        public RelativeDirection(double degreeInfluence)
        {
            // I can't think of a valid way to validate a degree input.
            DegreeInfluence = degreeInfluence;
        }

        /// <summary>
        /// The number of degrees to turn (positive or negative) from a starting point before making a step.
        /// </summary>
        public double DegreeInfluence { get; }

        /// <summary>
        /// Returns a human-readable representation of the degree component of the direction.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
            => $"{DegreeInfluence}°";
    }
}

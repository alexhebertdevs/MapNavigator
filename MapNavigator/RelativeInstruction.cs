using System;

namespace MapNavigator
{
    /// <summary>
    /// Represents the data for a single relative map instruction.
    /// This instruction's direction component is relative to the 
    /// direction specified by the instruction preceding it.
    /// </summary>
    public class RelativeInstruction
    {
        /// <summary>
        /// Required constructor for an instruction.
        /// </summary>
        /// <param name="direction"></param>
        /// <param name="travelDistance"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public RelativeInstruction(RelativeDirection direction, double travelDistance)
        {
            // Do not allow null direction references.
            Direction = direction ?? throw new ArgumentNullException(nameof(direction));

            // Don't think range checking is needed here. I don't see the problem if there is hypothetically
            // a negative number of blocks requested.
            TravelDistance = travelDistance;
        }

        /// <summary>
        /// The direction that the instruction is using.
        /// </summary>
        public RelativeDirection Direction { get; }

        /// <summary>
        /// The distance to travel in the specified direction.
        /// (Represented as a double to allow greater flexibility for possible requirement changes).
        /// </summary>
        public double TravelDistance { get; }

        /// <summary>
        /// Provides a human-readable format for the <see cref="RelativeInstruction"/>.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
            => $"{Direction.ToString()}--{TravelDistance}";
    }
}

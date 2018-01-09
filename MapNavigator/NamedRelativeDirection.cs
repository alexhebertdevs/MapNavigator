using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace MapNavigator
{
    /// <summary>
    /// Contains the options available for named relative directions.
    /// </summary>
    public class NamedRelativeDirection : RelativeDirection
    {
        // This class is designed to be immutable, so there is no harm in using single instances of each direction.

        /// <summary>
        /// Represents turning Left.
        /// </summary>
        public static NamedRelativeDirection Left { get; } = new NamedRelativeDirection("L", nameof(Left), 90);

        /// <summary>
        /// Represents turning Right.
        /// </summary>
        public static NamedRelativeDirection Right { get; } = new NamedRelativeDirection("R", nameof(Right), -90);

        /// <summary>
        /// Holds a shortened version of the direction name, which will likely be used for parsing user input.
        /// </summary>
        public string Abbreviation { get; }


        /// <summary>
        /// The display-friendly name of the direction.
        /// </summary>
        public string Name { get; }


        /// <summary>
        /// Returns a collection of all available directions.
        /// </summary>
        private static IReadOnlyCollection<NamedRelativeDirection> AllDirections { get; } = GetAllDirections();

        /// <summary>
        /// Performs a case insensitive parsing of a <see cref="NamedRelativeDirection"/> from a string input.
        /// </summary>
        /// <param name="directionAbbreviation"></param>
        /// <returns></returns>
        /// <exception cref="MapNavigatorException"></exception>
        public static NamedRelativeDirection Parse(string directionAbbreviation)
        {
            if (directionAbbreviation == null)
                throw new ArgumentNullException(nameof(directionAbbreviation));

            var foundMatch =
                AllDirections.FirstOrDefault(d => d.Abbreviation.ToUpper() == directionAbbreviation.ToUpper());

            if(foundMatch == null)
            {
                throw new MapNavigatorException($"No direction was found matching value: {directionAbbreviation}");
            }

            return foundMatch;
        }

        /// <summary>
        /// Provides a human-readable output for this class.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
            => $"{Name}({Abbreviation})";

        /// <summary>
        /// Validates if a single character meets any validation rules for the parser.
        /// </summary>
        /// <param name="character"></param>
        /// <returns></returns>
        internal static bool ValidCharacterForParseInput(char character)
            => char.IsLetter(character);

        /// <summary>
        /// This should only get called once as a static initialization. No additional functions should call this.
        /// </summary>
        /// <returns></returns>
        private static IReadOnlyCollection<NamedRelativeDirection> GetAllDirections()
        {
            // This was banking on the idea that directions
            // would also need to be referenced explicitly in code
            // in some future implementation.

            // All static, public properties on this class are treated as valid
            // parse options for easy adjustment & access in code, if needed.

            var returnList = new List<NamedRelativeDirection>();

            var allProperties =
                typeof(NamedRelativeDirection).GetProperties(BindingFlags.Public | BindingFlags.Static | BindingFlags.GetProperty);

            foreach (var property in allProperties)
            {
                if (property.GetValue(obj: null) is NamedRelativeDirection direction)
                {
                    returnList.Add(direction);
                }
            }

            return returnList.AsReadOnly();
        }

        /// <summary>
        /// Private by design to force access through the available static properties.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="degreeInfluence"></param>
        /// <exception cref="ArgumentException"></exception>
        private NamedRelativeDirection(string abbreviation, string name, double degreeInfluence)
            : base(degreeInfluence)
        {
            if(abbreviation == null || !abbreviation.All(ValidCharacterForParseInput))
                throw new ArgumentException(message: $"Direction {nameof(abbreviation)} must not be null and be composed entirely of letters.");
         
            Abbreviation = abbreviation;

            // Input checking on name inputs.
            // This is basically to protect against possible future direction additions.
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException(message: $"Direction {nameof(name)} may not be null or whitespace.", paramName: nameof(name));

            Name = name;
        }
    }
}

using System;
using System.Text;

namespace MapNavigator
{
    /// <summary>
    /// Implements <see cref="ISingleInstructionParser"/>, and parses specifically named instructions into a common format.
    /// </summary>
    public class SingleNamedInstructionParser : ISingleInstructionParser
    {
        /// <summary>
        /// Takes a string and returns a single named instruction.
        /// </summary>
        /// <param name="singleInstruction"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="MapNavigatorException"></exception>
        public RelativeInstruction ParseSingleInstruction(string singleInstruction)
        {
            if (string.IsNullOrWhiteSpace(singleInstruction))
                throw new ArgumentException("A single instruction may not be just white space or null.");

            // Surrounding white space is unimportant.
            singleInstruction = singleInstruction.Trim();

            // Here's how I'm going to treat the messages:
            // 1) Parse through the message until we hit the first character that isn't a letter.
            // 2) All characters before this point should be the direction.
            // 3) All characters after this point should be the distance.

            // To avoid possible repeated string reassignments.
            var directionString = new StringBuilder();

            int? firstNonLetterIndex = default;

            // This could be done with Linq as well, but for this case,
            // I find the loop more readable (and efficient).
            for (int x = 0; x < singleInstruction.Length; x++)
            {
                char character = singleInstruction[x];

                if (NamedRelativeDirection.ValidCharacterForParseInput(character))
                {
                    directionString.Append(character);
                }
                else
                {
                    firstNonLetterIndex = x;
                    break;
                }
            }

            if (!firstNonLetterIndex.HasValue)
            {
                throw new MapNavigatorException($"While parsing instruction: ({singleInstruction}), no travel distance was found.");
            }

            // This should throw an exception if it fails.
            var firstCharAsDirection =
                NamedRelativeDirection.Parse(directionString.ToString());

            // Get all characters after the direction string. These should be able to be parsed into a number,
            // if the input is valid.
            var finalCharacters = singleInstruction.Substring(firstNonLetterIndex.Value);


            if (!double.TryParse(finalCharacters, out var distance))
            {
                // The piece after the direction is not a valid number.
                throw new MapNavigatorException($"Value: ({finalCharacters}) was assumed to be a number, but could not be parsed as one.");
            }

            return new RelativeInstruction(firstCharAsDirection, distance);
        }
    }
}

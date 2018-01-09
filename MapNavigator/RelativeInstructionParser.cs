using System;
using System.Collections.Generic;

namespace MapNavigator
{
    /// <summary>
    /// Provides a means to parse an entire set of map instructions.
    /// </summary>
    public class RelativeInstructionParser : IFullnstructionParser
    {
        /// <summary>
        /// Required constructor, requires a class capable of parsing a single instruction.
        /// </summary>
        /// <param name="singleInstructionParser"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public RelativeInstructionParser(ISingleInstructionParser singleInstructionParser)
        {
            InstructionParser = singleInstructionParser ?? throw new ArgumentNullException(nameof(singleInstructionParser));
        }

        /// <summary>
        /// Parses a series
        /// </summary>
        /// <param name="instructions"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public IReadOnlyList<RelativeInstruction> ParseRawInstructions(string instructions)
        {
            // Input checking.
            if (instructions == null)
                throw new ArgumentNullException(nameof(instructions));

            var currentDirections = new List<RelativeInstruction>();

            string[] splitString = instructions.Split(Delimiter);

            foreach (string split in splitString)
            {
                currentDirections.Add(InstructionParser.ParseSingleInstruction(split));
            }

            return currentDirections.AsReadOnly();
        }

        /// <summary>
        /// The delimiter between single instructions that this class is hard-coded to use.
        /// </summary>
        private const char Delimiter = ',';

        /// <summary>
        /// Holds the readonly reference to an instance capable of parsing a single instruction.
        /// </summary>
        private ISingleInstructionParser InstructionParser { get; }
    }
}

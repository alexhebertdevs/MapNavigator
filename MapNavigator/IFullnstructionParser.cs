using System.Collections.Generic;

namespace MapNavigator
{
    /// <summary>
    /// Defines the contract for an object to parse a complete map instruction set.
    /// </summary>
    public interface IFullnstructionParser
    {
        /// <summary>
        /// Takes a full instruction set, and returns a readonly collection of instructions.
        /// Should always return an initialized collection.
        /// </summary>
        /// <param name="rawInstructions"></param>
        /// <returns></returns>
        IReadOnlyList<RelativeInstruction> ParseRawInstructions(string rawInstructions);
    }
}

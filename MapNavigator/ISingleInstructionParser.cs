namespace MapNavigator
{
    /// <summary>
    /// Defines the contract for a class capable of interpreting a single map instruction.
    /// </summary>
    public interface ISingleInstructionParser
    {
        /// <summary>
        /// Parses a string that represents a single map instruction, and returns an object that conforms to <see cref="RelativeInstruction"/>.
        /// </summary>
        /// <param name="singleInstruction"></param>
        /// <returns></returns>
        RelativeInstruction ParseSingleInstruction(string singleInstruction);
    }
}

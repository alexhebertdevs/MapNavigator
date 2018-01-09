using System.Collections.Generic;

namespace MapNavigator
{
    /// <summary>
    /// Defines an object capable of taking a full enumerated instruction set and returning
    /// a result that may be interpreted by the user.
    /// </summary>
    public interface IInstructionSimulator
    {
        /// <summary>
        /// Simulates the provided instruction set, providing a <see cref="MapResult"/>
        /// as output in order to summarize the sequence that occurred.
        /// </summary>
        /// <param name="instructionSet"></param>
        /// <returns></returns>
        MapResult Simulate(IReadOnlyList<RelativeInstruction> instructionSet);
    }
}

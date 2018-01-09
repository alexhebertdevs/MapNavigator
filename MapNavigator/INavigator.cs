namespace MapNavigator
{
    /// <summary>
    /// Defines the contract for a full map simulator.
    /// </summary>
    public interface INavigator
    {
        /// <summary>
        /// Simulates instructions, if possible, and returns a result a user may interpret.
        /// </summary>
        /// <param name="instructions"></param>
        /// <returns></returns>
        MapResult Simulate(string instructions);
    }
}

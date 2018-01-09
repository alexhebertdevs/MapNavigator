using System;

namespace MapNavigator
{
    /// <summary>
    /// A top level class capable of wrapping all functions related to parsing
    /// map steps and returning a result.
    /// </summary>
    public class Navigator : INavigator
    {
        /// <summary>
        /// Required constructor.
        /// </summary>
        /// <param name="parser"></param>
        /// <param name="simulator"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public Navigator(IFullnstructionParser parser, IInstructionSimulator simulator)
        {
            Parser = parser ?? throw new ArgumentNullException(nameof(parser));
            Simulator = simulator ?? throw new ArgumentNullException(nameof(simulator));
        }

        /// <summary>
        /// Takes a set of instructions, and attempts to parse them
        /// and extract a result.
        /// </summary>
        /// <param name="instructions"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public MapResult Simulate(string instructions)
        {
            if (instructions == null)
                throw new ArgumentNullException(nameof(instructions));

            var parseResult = Parser.ParseRawInstructions(instructions);

            var simulationResult = Simulator.Simulate(parseResult);

            return simulationResult;
        }

        /// <summary>
        /// Readonly reference to a parser instance.
        /// </summary>
        private IFullnstructionParser Parser { get; }

        /// <summary>
        /// Readonly reference to a simulator instance.
        /// </summary>
        private IInstructionSimulator Simulator { get; }
    }
}

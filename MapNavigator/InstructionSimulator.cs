using System;
using System.Collections.Generic;

namespace MapNavigator
{
    /// <summary>
    /// This class will be responsible for simulating a set of relative instructions.
    /// </summary>
    public class InstructionSimulator : IInstructionSimulator
    {
        /// <summary>
        /// Creates a <see cref="InstructionSimulator"/> using the provided options.
        /// </summary>
        /// <param name="options"></param>
        public InstructionSimulator(InstructionSimulatorOptions options)
        {
            Options = options ?? new InstructionSimulatorOptions();
        }

        /// <summary>
        /// Creates a new <see cref="InstructionSimulator"/> using default options.
        /// </summary>
        public InstructionSimulator() : this(options: null) { }

        /// <summary>
        /// Takes an existing list of strongly typed instructions and returns a map result for interpretation.
        /// </summary>
        /// <param name="instructionSet"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="MapNavigatorException"></exception>
        public MapResult Simulate(IReadOnlyList<RelativeInstruction> instructionSet)
        {
            if (instructionSet == null)
                throw new ArgumentNullException(nameof(instructionSet));

            // The class constructor guarantees we don't need a null check here.
            double angleCurrentlyFacing = Options.StartingDegree;

            var startingPosition = new GridCoordinate();

            // This is an actual value copy, not a reference copy.
            var currentPosition = startingPosition;

            foreach (var instruction in instructionSet)
            {
                if (!InstructionConformsToOptions(instruction))
                {
                    throw new MapNavigatorException($"Instruction: ({instruction}) is not valid for the current settings.");
                }

                // First alter the current angle according to the next instruction.
                angleCurrentlyFacing += instruction.Direction.DegreeInfluence;

                // After we've turned, move in space the specified distance, and reset the current position.
                currentPosition = currentPosition.TravelTo(angleCurrentlyFacing, instruction.TravelDistance);
            }

            return new MapResult(startPosition: startingPosition, endPosition: currentPosition);
        }

        /// <summary>
        /// Holds a reference to the provided options.
        /// </summary>
        private InstructionSimulatorOptions Options { get; }

        /// <summary>
        /// Returns whether the provided <see cref="RelativeInstruction"/> is valid when paired with the current options state.
        /// </summary>
        /// <param name="instruction"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        private bool InstructionConformsToOptions(RelativeInstruction instruction)
        {
            if (instruction == null)
                throw new ArgumentNullException(nameof(instruction));

            // If the options have specified a grid spacing:
            if (Options.GridSpacing.HasValue)
            {         
                // 1) The direction to travel must be a multiple of 90 degrees.
                if(instruction.Direction.DegreeInfluence % Trigonometry.NinetyDegrees != 0)
                {
                    return false;
                }

                // And the travel distance must be a multiple of the grid spacing.
                // The grid spacing is guaranteed to be greater than 0...we shouldn't need divide by 0 checks.
                if(instruction.TravelDistance % Options.GridSpacing.Value != 0)
                {
                    return false;
                }
            }

            return true;
        }
    }
}

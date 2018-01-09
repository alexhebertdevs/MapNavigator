using System;

namespace MapNavigator
{
    /// <summary>
    /// Contains options with which to initialize the <see cref="InstructionSimulator"/> class.
    /// </summary>
    public class InstructionSimulatorOptions
    {
        // Option Defaults
        private const int DefaultGridSpacing = 1;

        private const double DefaultStartAngle = Trigonometry.NinetyDegrees;

        /// <summary>
        /// Provides a default <see cref="InstructionSimulatorOptions"/> instance.
        /// </summary>
        public InstructionSimulatorOptions() :
            this(gridSpacing: DefaultGridSpacing, startingDegree: DefaultStartAngle)
        { }

        /// <summary>
        /// Creates a new options instance at the specified starting degree without grid locking.
        /// </summary>
        /// <param name="startingDegree"></param>
        public InstructionSimulatorOptions(double startingDegree) :
            this(gridSpacing: null, startingDegree)
        { }

        /// <summary>
        /// Allows a grid with the specified spacing, at the specified starting degree.
        /// Starting degree must be divisible by 90 degrees to be valid.
        /// </summary>
        /// <param name="gridSpacing"></param>
        /// <param name="startingDegree"></param>
        public InstructionSimulatorOptions(double gridSpacing, double startingDegree) :
            this((double?)gridSpacing, startingDegree)
        { }

        /// <summary>
        /// Private, main entry point for all <see cref="InstructionSimulatorOptions"/> instances.
        /// </summary>
        /// <param name="gridSpacing"></param>
        /// <param name="startingDegree"></param>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="MapNavigatorException"></exception>
        private InstructionSimulatorOptions(double? gridSpacing, double startingDegree)
        {
            if (gridSpacing.HasValue && gridSpacing <= 0)
                throw new ArgumentException("Grid spacing value must be greater than 0.");

            GridSpacing = gridSpacing;

            // If grid spacing is supplied as an option, the angle must be locked to a multiple of 90 degrees.
            if (GridSpacing.HasValue)
            {
                if (startingDegree % Trigonometry.NinetyDegrees != 0)
                {
                    throw new MapNavigatorException($"Value: ({startingDegree}) is not valid because when a {nameof(gridSpacing)} parameter is provided, it must be divisible by {Trigonometry.NinetyDegrees}.");
                }
            }

            StartingDegree = startingDegree;
        }

        /// <summary>
        /// The angle at which the navigator will start facing initially. (90 degrees is due north, 180 degrees is west, etc).
        /// </summary>
        public double StartingDegree { get; }

        /// <summary>
        /// If null, instructions are not locked to a grid system.
        /// </summary>
        public double? GridSpacing { get; }
    }
}

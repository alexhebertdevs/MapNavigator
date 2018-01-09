namespace MapNavigator
{
    /// <summary>
    /// Encompasses the results of a map query.
    /// </summary>
    public class MapResult
    {
        /// <summary>
        /// Required constructor for <see cref="MapResult"/> class.
        /// </summary>
        /// <param name="startPosition"></param>
        /// <param name="endPosition"></param>
        public MapResult(GridCoordinate startPosition, GridCoordinate endPosition)
        {
            StartPosition = startPosition;
            EndPosition = endPosition;
            AbsolutePositionDelta = (EndPosition - StartPosition).AbsoluteComponents();
        }

        /// <summary>
        /// The starting position of the map result.
        /// </summary>
        public GridCoordinate StartPosition { get; }

        /// <summary>
        /// The ending position of the result.
        /// </summary>
        public GridCoordinate EndPosition { get; }

        /// <summary>
        /// The absolute components of the x and y coordinate components of the travel between start and finish.
        /// </summary>
        public GridCoordinate AbsolutePositionDelta { get; }
    }
}

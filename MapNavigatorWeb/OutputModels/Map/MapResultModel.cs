namespace MapNavigatorWeb.OutputModels.Map
{
    public class MapResultModel
    {
        /// <summary>
        /// The start position of the user.
        /// </summary>
        public string StartPosition { get; set; }

        /// <summary>
        /// The end position of the user.
        /// </summary>
        public string EndPosition { get; set; }

        /// <summary>
        /// The number of "blocks" the user would need to travel from start to finish,
        /// if the user was on a locked grid.
        /// </summary>
        public double? BlocksToTravel { get; set; }

        /// <summary>
        /// The exact distance the user could travel to the end destination by
        /// taking the shortest path.
        /// </summary>
        public double? AsTheCrowFliesDistance { get; set; }
    }
}

using MapNavigator;
using MapNavigatorWeb.OutputModels.Map;

namespace MapNavigatorWeb.OutputTranslators
{
    internal class MapTranslator : IMapTranslator
    {
        public MapResultModel Translate(MapResult mapResult)
        {
            return new MapResultModel
            {
                StartPosition = mapResult.StartPosition.ToString(),
                EndPosition = mapResult.EndPosition.ToString(),
                BlocksToTravel = mapResult.AbsolutePositionDelta.DistanceTraveledFromOrigin(DistanceCalculationMode.XComponentPlusYComponent),
                AsTheCrowFliesDistance = mapResult.AbsolutePositionDelta.DistanceTraveledFromOrigin(DistanceCalculationMode.ShortestPath)
            };
        }
    }
}

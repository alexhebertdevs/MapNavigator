using MapNavigator;
using MapNavigatorWeb.OutputModels.Map;
using MapNavigatorWeb.Controllers;

namespace MapNavigatorWeb.OutputTranslators
{
    /// <summary>
    /// Defines a translator, associated with the <see cref="MapController"/> to convert
    /// a service output to an output specific to this API.
    /// </summary>
    public interface IMapTranslator
    {
        MapResultModel Translate(MapResult mapResult);
    }
}

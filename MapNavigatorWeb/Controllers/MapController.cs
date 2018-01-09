using Microsoft.AspNetCore.Mvc;
using MapNavigator;
using System;
using MapNavigatorWeb.OutputTranslators;
using MapNavigatorWeb.InputModels.Map;
using MapNavigatorWeb.ActionFilters;

namespace MapNavigatorWeb.Controllers
{
    [Route("api/[controller]")]
    [ValidateModel]
    public class MapController : Controller
    {
        public MapController(INavigator navigator, IMapTranslator translator)
        {
            Map = navigator ?? throw new ArgumentNullException(nameof(navigator));
            Translator = translator ?? throw new ArgumentNullException(nameof(translator));
        }

        /// <summary>
        /// Readonly reference to a navigator instance.
        /// </summary>
        private INavigator Map { get; }

        /// <summary>
        /// Readonly reference to a translator instance.
        /// </summary>
        private IMapTranslator Translator { get; }


        // Note: Without context, it's hard to know the appropriate
        // Http Verb for this endpoint, since no resources are actually
        // being modified.

        // However, I chose to write this as a POST so that:
        // 1) Input length is not limited by query string length limitations AND
        // 2) It lends itself better to JSON input than embedding JSON in a query string, which the instructions ask for.

        // POST api/map

        /// <summary>
        /// Main controller action.
        /// </summary>
        /// <param name="inputs"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Post([FromBody]MapActionInputs inputs)
        {
            // There are cleaner ways to handle exceptions than this, but
            // for one endpoint, this is alright. The parser will
            // throw exceptions for malformed inputs.

            MapResult simulationResult;

            // Simple catch-all error handling for navigation errors.
            // This will pick up badly formatted inputs and should
            // give back readable error messages.
            try
            {
                simulationResult = Map.Simulate(inputs.Instructions);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }

            // Convert the raw navigation output to an api-specific model.
            var outputTranslation = Translator.Translate(simulationResult);

            return Ok(outputTranslation);
        }
    }
}

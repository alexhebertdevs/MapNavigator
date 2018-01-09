using MapNavigator;
using MapNavigatorWeb.Controllers;
using MapNavigatorWeb.InputModels.Map;
using MapNavigatorWeb.OutputModels.Map;
using MapNavigatorWeb.OutputTranslators;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace MapNavigatorWeb.Tests.IntegrationTests.Controllers
{
    /// <summary>
    /// Full integration tests related to <see cref="MapController"/>.
    /// </summary>
    public class MapControllerTests
    {
        [Theory]
        [InlineData(IntegrationTestString1, 10)]
        [InlineData(IntegrationTestString2, 209)]
        public void TestPostWithDefaultTests(string instructions, int expectedBlocksToTravel)
        {
            var inputs = new MapActionInputs
            {
                Instructions = instructions
            };

            INavigator navigator = new Navigator(
                parser: new RelativeInstructionParser(
                    singleInstructionParser: new SingleNamedInstructionParser()),
                simulator: new InstructionSimulator());

            IMapTranslator translator = new MapTranslator();

            var controller = new MapController(
                navigator: navigator,
                translator: translator);

            var result = controller.Post(
                inputs: inputs);

            // Make sure result is an okay response.
            var okResult = Assert.IsType<OkObjectResult>(result);

            // Check type of output and capture its value.
            var objOutput = Assert.IsType<MapResultModel>(okResult.Value);

            Assert.Equal(expectedBlocksToTravel, objOutput.BlocksToTravel);
        }

        #region Test Data

        private const string IntegrationTestString1 = "L3, R2, L5, R1, L1, L2";

        private const string IntegrationTestString2 = "L3, R2, L5, R1, L1, L2, L2, R1, R5, R1, L1, L2, R2, R4, L4, L3, L3, R5, L1, R3, L5, L2, R4, L5, R4, R2, L2, L1, R1, L3, L3, R2, R1, L4, L1, L1, R4, R5, R1, L2, L1, R188, R4, L3, R54, L4, R4, R74, R2, L4, R185, R1, R3, R5, L2, L3, R1, L1, L3, R3, R2, L3, L4, R1, L3, L5, L2, R2, L1, R2, R1, L4, R5, R4, L5, L5, L4, R5, R4, L5, L3, R4, R1, L5, L4, L3, R5, L5, L2, L4, R4, R4, R2, L1, L3, L2, R5, R4, L5, R1, R2, R5, L2, R4, R5, L2, L3, R3, L4, R3, L2, R1, R4, L5, R1, L5, L3, R4, L2, L2, L5, L5, R5, R2, L5, R1, L3, L2, L2, R3, L3, L4, R2, R3, L1, R2, L5, L3, R4, L4, R4, R3, L3, R1, L3, R5, L5, R1, R5, R3, L1";

        #endregion Test Data
    }
}

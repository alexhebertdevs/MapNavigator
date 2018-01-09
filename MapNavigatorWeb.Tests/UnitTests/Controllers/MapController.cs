using System;
using MapNavigator;
using MapNavigatorWeb.Controllers;
using MapNavigatorWeb.InputModels.Map;
using MapNavigatorWeb.OutputModels.Map;
using MapNavigatorWeb.OutputTranslators;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace MapNavigatorWeb.Tests.UnitTests.Controllers
{
    /// <summary>
    /// Tests against the <see cref="MapController"/> class.
    /// </summary>
    public class MapControllerTests
    {
        [Fact]
        public void ConstructorArgumentNullForArguments()
        {
            var navigatorMock = new Mock<INavigator>(MockBehavior.Strict);
            var translatorMapMock = new Mock<IMapTranslator>(MockBehavior.Strict);

            // This should not throw:
            new MapController(
                navigator: navigatorMock.Object,
                translator: translatorMapMock.Object);

            // But this should.
            Assert.Throws<ArgumentNullException>(
                () => new MapController(
                    navigator: null,
                    translator: translatorMapMock.Object));

            // And so should this.
            Assert.Throws<ArgumentNullException>(
                () => new MapController(
                    navigator: navigatorMock.Object,
                    translator: null));
        }

        [Fact]
        public void SuccessfulPostReturnsOkay()
        {
            var inputs = new MapActionInputs();

            var navMock = new Mock<INavigator>(MockBehavior.Strict);

            var navMockResult = new MapResult(
                startPosition: new GridCoordinate(),
                endPosition: new GridCoordinate());

            navMock.Setup(m => m.Simulate(inputs.Instructions)).
                Returns(navMockResult);

            var translatorMock = new Mock<IMapTranslator>(MockBehavior.Strict);

            var translatorResult = new MapResultModel();

            translatorMock.Setup(t => t.Translate(navMockResult)).
                Returns(translatorResult);

            var controller = new MapController(
                navigator: navMock.Object,
                translator: translatorMock.Object);

            var postResult = controller.Post(
                inputs: inputs);

            // Make sure api returns an "okay."
            var okResult = Assert.IsType<OkObjectResult>(postResult);

            // Make sure navigator got called exactly once with correct input.
            navMock.Verify(n => n.Simulate(inputs.Instructions), Times.Once);

            // Make sure translator got called exactly once with correct input.
            translatorMock.Verify(t => t.Translate(navMockResult), Times.Once);

            // Make sure the translator result got attached to the final output..
            Assert.Equal(translatorResult, okResult.Value);
        }

        [Fact]
        public void BadPostInputReturnsBadRequest()
        {
            var inputs = new MapActionInputs();

            var navMock = new Mock<INavigator>(MockBehavior.Strict);
            var translatorMock = new Mock<IMapTranslator>(MockBehavior.Strict);

            var thrownException = new Exception(
                message: "TestMessage");

            navMock.Setup(m => m.Simulate(inputs.Instructions)).
                Throws(thrownException);

            var controller = new MapController(
                navigator: navMock.Object,
                translator: translatorMock.Object);

            var controllerResult = controller.Post(inputs);

            // Make sure controller returns a bad request.
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(controllerResult);

            // Make sure the exception message got attached to the result.
            Assert.Equal(thrownException.Message, badRequestResult.Value);
        }
    }
}

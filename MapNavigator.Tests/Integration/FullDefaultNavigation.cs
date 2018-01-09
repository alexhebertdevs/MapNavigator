using Xunit;

namespace MapNavigator.Tests.Integration
{
    /// <summary>
    /// Contains some full system maps tests.
    /// </summary>
    public class FullDefaultNavigationTests
    {
        // Integration test data provided here: 
        // http://itrellisrecruiting.azurewebsites.net/

        [Theory]
        [InlineData(IntegrationTestString1, 10)]
        [InlineData(IntegrationTestString2, 209)]
        public void TestDefaultFullParser(string instructions, int expectedBlocks)
        {
            // Get all of the explicit implementations that will work to solve this test.
            var nav = new Navigator(
                parser: new RelativeInstructionParser(
                    singleInstructionParser: new SingleNamedInstructionParser()),
                simulator: new InstructionSimulator());

            var result = nav.Simulate(instructions);

            // Add the x & y component of the distance traveled to get the "blocks" asked for.
            double blocksCount = 
                result.AbsolutePositionDelta.
                DistanceTraveledFromOrigin(DistanceCalculationMode.XComponentPlusYComponent);

            Assert.Equal(expectedBlocks, blocksCount);
        }

        #region Test Data

        private const string IntegrationTestString1 = "L3, R2, L5, R1, L1, L2";

        private const string IntegrationTestString2 = "L3, R2, L5, R1, L1, L2, L2, R1, R5, R1, L1, L2, R2, R4, L4, L3, L3, R5, L1, R3, L5, L2, R4, L5, R4, R2, L2, L1, R1, L3, L3, R2, R1, L4, L1, L1, R4, R5, R1, L2, L1, R188, R4, L3, R54, L4, R4, R74, R2, L4, R185, R1, R3, R5, L2, L3, R1, L1, L3, R3, R2, L3, L4, R1, L3, L5, L2, R2, L1, R2, R1, L4, R5, R4, L5, L5, L4, R5, R4, L5, L3, R4, R1, L5, L4, L3, R5, L5, L2, L4, R4, R4, R2, L1, L3, L2, R5, R4, L5, R1, R2, R5, L2, R4, R5, L2, L3, R3, L4, R3, L2, R1, R4, L5, R1, L5, L3, R4, L2, L2, L5, L5, R5, R2, L5, R1, L3, L2, L2, R3, L3, L4, R2, R3, L1, R2, L5, L3, R4, L4, R4, R3, L3, R1, L3, R5, L5, R1, R5, R3, L1";

        #endregion Test Data
    }
}

using System;
using Xunit;

namespace MapNavigator.Tests
{
    /// <summary>
    /// Tests against the <see cref="MapNavigatorException"/> class.
    /// </summary>
    public class MapNavigatorExceptionTests
    {
        [Fact]
        public void IsException()
        {
            Assert.IsAssignableFrom<Exception>(new MapNavigatorException());
        }

        [Theory]
        [InlineData("Test message")]
        public void MessageIsPersisted(string message)
        {
            var exception = new MapNavigatorException(message);

            Assert.Equal(message, exception.Message);
        }
    }
}

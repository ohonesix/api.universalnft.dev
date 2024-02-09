using NSubstitute;

namespace UniversalNFT.dev.API.Tests.Services.Rules
{
    public class BirdjpgTest : BaseRuleTest
    {
        [Test]
        public async Task GivenNFT_ReturnsImageAsync()
        {
            // Arrange
            Token.URI = TestConstants.DirectImageUrl;

            // Act
            var result = await _classUnderTest.ProcessNFToken(Token);

            // Assert
            Assert.That(result, Is.EqualTo(TestConstants.DirectImageUrl));
        }
    }
}
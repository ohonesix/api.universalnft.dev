using NSubstitute;

namespace UniversalNFT.dev.API.Tests.Services.Rules
{
    public class FortunesTest : BaseRuleTest
    {
        [Test]
        public async Task GivenNFT_ReturnsImageAsync()
        {
            // Arrange
            Token.URI = TestConstants.MetaNormalisedIpfsUrl;

            var metaJson = @"{""name"":""Fortunes - Tier 1"",""description"":""1 of 1,850 Tier 1 Fortunes NFTs linked to the Fortunes Gaming Hub."",""external_url"":""https://www.fortunes.app/"",""attributes"":[{""trait_type"":""tier"",""value"":""1""}],""category"":""art"",""md5hash"":""fc45be252f87d86f4bf28b1e0ce9f970"",""is_explicit"":false,""content_type"":""video/mp4"",""image_url"":""" + TestConstants.IpfsDoubleWithFile + @""",""animation_url"":""ipfs://ipfs/bafybeiaffq25byvqbqi2v2k3kppf7zefnvgzi5rw6ijlqps4zpk3xbhfx4/animation.mp4""}";

            _mockHttpFacade.GetData(TestConstants.MetaNormalisedIpfsUrl).Returns(metaJson);

            // Act
            var result = await _classUnderTest.ProcessNFToken(Token);

            // Assert
            Assert.That(result, Is.EqualTo(TestConstants.IpfsDoubleWithFile));
        }
    }
}
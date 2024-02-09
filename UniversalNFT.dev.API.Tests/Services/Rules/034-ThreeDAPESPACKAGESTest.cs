using NSubstitute;

namespace UniversalNFT.dev.API.Tests.Services.Rules
{
    public class ThreeDAPESPACKAGESTest : BaseRuleTest
    {
        [Test]
        public async Task GivenNFT_ReturnsImageAsync()
        {
            // Arrange
            Token.URI = TestConstants.MetaNormalisedIpfsUrlWithFile;

            var metaJson = @"{""name"":""3DAPE PACKAGE 5"",""description"":""Purchaser of this ticket will be sent the following: 🍌 1 3DAPE NFT 🍌 1 Random Mutant Serum NFT"",""external_url"":""https://xrpl3dapes.com/"",""attributes"":[],""category"":""collectibles"",""md5hash"":""c2c26672a88311fabfb62ca639949c19"",""is_explicit"":false,""content_type"":""image/png"",""image_url"":""ipfs://ipfs/bafybeictaniw5pp26xbqsd4iom56eu37vgtjahifx7jrrhlzk7xvpvqy5q/image.jpeg"",""animation_url"":""ipfs://ipfs/bafybeictaniw5pp26xbqsd4iom56eu37vgtjahifx7jrrhlzk7xvpvqy5q/animation.png""}";

            _mockHttpFacade.GetData(TestConstants.MetaNormalisedIpfsUrlWithFile).Returns(metaJson);

            // Act
            var result = await _classUnderTest.ProcessNFToken(Token);

            // Assert
            Assert.That(result, Is.EqualTo("ipfs://ipfs/bafybeictaniw5pp26xbqsd4iom56eu37vgtjahifx7jrrhlzk7xvpvqy5q/image.jpeg"));
        }
    }
}
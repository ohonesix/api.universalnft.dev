using NSubstitute;

namespace UniversalNFT.dev.API.Tests.Services.Rules
{
    public class TheCoreApesClubTest : BaseRuleTest
    {
        [Test]
        public async Task GivenNFT_ReturnsImageAsync()
        {
            // Arrange
            Token.URI = TestConstants.MetaNormalisedIpfsUrlWithFile;

            var metaJson = @"{""name"":""CAC #0727"",""description"":""The Official APE project for the Coreum Blockchain. These NFTs offer holders the chance to support Coreum directly with their unique utility, offering a tokenized stake on our community validator. The collection is made up of 1,589 degenerate Apes all trying to connect to the network."",""external_url"":""https://coreapes.io/"",""attributes"":[{""trait_type"":""background"",""value"":""Bubblegum""},{""trait_type"":""skin"",""value"":""Gummy""},{""trait_type"":""outfit"",""value"":""Gold Scarf""},{""trait_type"":""eyes"",""value"":""Blue Eye Glasses""},{""trait_type"":""headwear"",""value"":""Solar System""},{""trait_type"":""mouth"",""value"":""Yummy""}],""category"":""collectibles"",""md5hash"":""ebc59067e055532d31eca0641416b717"",""is_explicit"":false,""content_type"":""image/png"",""image_url"":""ipfs://ipfs/bafybeibizbvibxbiclszk6fvznbx3af3e27itpj2g5ztsjhizcqagcrnge/image.png"",""animation_url"":""ipfs://ipfs/bafybeibizbvibxbiclszk6fvznbx3af3e27itpj2g5ztsjhizcqagcrnge/animation.png""}";

            _mockHttpFacade.GetData(TestConstants.MetaNormalisedIpfsUrlWithFile).Returns(metaJson);

            // Act
            var result = await _classUnderTest.ProcessNFToken(Token);

            // Assert
            Assert.That(result, Is.EqualTo("ipfs://ipfs/bafybeibizbvibxbiclszk6fvznbx3af3e27itpj2g5ztsjhizcqagcrnge/image.png"));
        }
    }
}
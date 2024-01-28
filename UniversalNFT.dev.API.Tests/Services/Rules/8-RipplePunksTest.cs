using NSubstitute;

namespace UniversalNFT.dev.API.Tests.Services.Rules
{
    public class RipplePunksTest : BaseRuleTest
    {
        [Test]
        public async Task GivenNFT_ReturnsImageAsync()
        {
            // Arrange
            Token.URI = TestConstants.MetaIpfsWithFile;

            var metaJson = @"{""attributes"":[{""description"":""Color"",""trait_type"":""Color"",""value"":""Blue""},{""description"":""Type"",""trait_type"":""Type"",""value"":""Female""},{""description"":""Accessory"",""trait_type"":""Accessory"",""value"":""2 Attributes""},{""description"":""Accessory"",""trait_type"":""Accessory"",""value"":""Black Lipstick""},{""description"":""Accessory"",""trait_type"":""Accessory"",""value"":""Stringy Hair""}],""collection"":{""name"":""RipplePunks"",""family"":""Family NFTs""},""video"":"""",""animation"":"""",""external_link"":"""",""audio"":"""",""name"":""RipplePunk #5502"",""image"":"""+ TestConstants.IpfsWithFile + @""",""taxon"":604,""description"":""Punks on the XRP Ledger. They have \""Ripple blue\"" properties and share the same attributes as their siblings on Ethereum."",""schema"":""ipfs://QmNpi8rcXEkohca8iXu7zysKKSJYqCvBJn3xJwga8jXqWU"",""nftType"":""art.v0"",""id"":""bf404836bad520f3717e3be065bab24d:1674499118204"",""file"":""""}";

            _mockHttpFacade.GetData(TestConstants.MetaNormalisedIpfsUrlWithFile).Returns(metaJson);

            // Act
            var result = await _classUnderTest.ProcessNFToken(Token);

            // Assert
            Assert.That(result, Is.EqualTo(TestConstants.IpfsWithFile));
        }
    }
}
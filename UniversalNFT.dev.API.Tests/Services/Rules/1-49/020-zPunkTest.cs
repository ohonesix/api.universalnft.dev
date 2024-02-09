using NSubstitute;

namespace UniversalNFT.dev.API.Tests.Services.Rules
{
    public class zPunkTest : BaseRuleTest
    {
        [Test]
        public async Task GivenNFT_ReturnsImageAsync()
        {
            // Arrange
            Token.URI = TestConstants.MetaIpfsWithFile;

            var metaJson = @"{""attributes"":[{""description"":""Color"",""trait_type"":""Color"",""value"":""Gray""},{""description"":""Type"",""trait_type"":""Type"",""value"":""Zombie Male""},{""description"":""Accessory"",""trait_type"":""Accessory"",""value"":""2 Attributes""},{""description"":""Accessory"",""trait_type"":""Accessory"",""value"":""Frumpy Hair""},{""description"":""Accessory"",""trait_type"":""Accessory"",""value"":""Chinstrap""}],""collection"":{""name"":""zPunks"",""family"":""zPUNKS""},""video"":"""",""animation"":"""",""external_link"":"""",""audio"":"""",""name"":""zPunk #613"",""image"":""ipfs://bafybeiald6xtbvebmbxfaxitdodsuhaxxn22mzkh7ntvkablxhefuuvdoi/1679248408995.png"",""taxon"":22914,""description"":""Zombie Punks on the XRPL "",""schema"":""ipfs://QmNpi8rcXEkohca8iXu7zysKKSJYqCvBJn3xJwga8jXqWU"",""nftType"":""art.v0"",""id"":""ce09b5d2bdcabc03c772d7739e83e92b:1679248408819"",""file"":""""}";

            _mockHttpFacade.GetData(TestConstants.MetaNormalisedIpfsUrlWithFile).Returns(metaJson);

            // Act
            var result = await _classUnderTest.ProcessNFToken(Token);

            // Assert
            Assert.That(result, Is.EqualTo("ipfs://bafybeiald6xtbvebmbxfaxitdodsuhaxxn22mzkh7ntvkablxhefuuvdoi/1679248408995.png"));
        }
    }
}
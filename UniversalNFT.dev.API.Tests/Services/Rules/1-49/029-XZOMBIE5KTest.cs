using NSubstitute;

namespace UniversalNFT.dev.API.Tests.Services.Rules
{
    public class XZOMBIE5KTest : BaseRuleTest
    {
        [Test]
        public async Task GivenNFT_ReturnsImageAsync()
        {
            // Arrange
            Token.URI = TestConstants.MetaNormalisedIpfsUrlWithFile;

            var metaJson = @"{""attributes"":[{""description"":""BACKGROUNDS"",""trait_type"":""BACKGROUNDS"",""value"":""Brooklyn streets overrun at night time""},{""description"":""BODY"",""trait_type"":""BODY"",""value"":""Transparent toxic  flesh""},{""description"":""CLOTHES"",""trait_type"":""CLOTHES"",""value"":""Skull zip top""},{""description"":""TEETH"",""trait_type"":""TEETH"",""value"":""Brain""},{""description"":""EYES"",""trait_type"":""EYES"",""value"":""Piercing grey ghost""},{""description"":""EARRINGS"",""trait_type"":""EARRINGS"",""value"":""Skull & chain earring""}],""collection"":{""name"":""XZOMBIE 5K"",""family"":""XZOMBIE""},""video"":"""",""animation"":"""",""external_link"":"""",""audio"":"""",""name"":""#456"",""image"":""https://bafybeigyetjx7lmezjbsc74ujurudekkfsa36wqtmps3j6kwt7tm2thufy.ipfs.w3s.link/1667241804953.png"",""taxon"":57,""description"":""XZOMBIE 5K"",""schema"":""ipfs://QmNpi8rcXEkohca8iXu7zysKKSJYqCvBJn3xJwga8jXqWU"",""nftType"":""art.v0"",""id"":""d98e0782de4df2c7da89fdc7cfb4e0fe:1667241804920"",""file"":""""}

";

            _mockHttpFacade.GetData(TestConstants.MetaNormalisedIpfsUrlWithFile).Returns(metaJson);

            // Act
            var result = await _classUnderTest.ProcessNFToken(Token);

            // Assert
            Assert.That(result, Is.EqualTo("https://bafybeigyetjx7lmezjbsc74ujurudekkfsa36wqtmps3j6kwt7tm2thufy.ipfs.w3s.link/1667241804953.png"));
        }
    }
}
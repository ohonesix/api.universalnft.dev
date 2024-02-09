using NSubstitute;

namespace UniversalNFT.dev.API.Tests.Services.Rules
{
    public class BoredApeFootballClubTest : BaseRuleTest
    {
        [Test]
        public async Task GivenNFT_ReturnsImageAsync()
        {
            // Arrange
            Token.URI = TestConstants.MetaIpfsWithFile;

            var metaJson = @"{""attributes"":[{""description"":""Background"",""trait_type"":""Background"",""value"":""COMMON""},{""description"":""Fur"",""trait_type"":""Fur"",""value"":""BROWN""},{""description"":""Shirt"",""trait_type"":""Shirt"",""value"":""INTER_91""},{""description"":""Eyes"",""trait_type"":""Eyes"",""value"":""WIDE_EYED""},{""description"":""Head"",""trait_type"":""Head"",""value"":""NONE""},{""description"":""Mouth"",""trait_type"":""Mouth"",""value"":""BORED_UNSHAVEN""}],""collection"":{""name"":"" Bored Ape Football Club"",""family"":""Bored Ape Football Club""},""video"":"""",""animation"":"""",""external_link"":"""",""audio"":"""",""name"":""BAFC #3923"",""image"":""ipfs://bafybeia4h2iun2jfznbxkk2bshmf5yshxlelocpondctrxxrzmkz7tsuca/1667206877240.png"",""taxon"":32,""description"":""The BAFC xls20 collection. BAFC is the first football community DAO. Built on the XRPL!"",""schema"":""ipfs://QmNpi8rcXEkohca8iXu7zysKKSJYqCvBJn3xJwga8jXqWU"",""nftType"":""art.v0"",""id"":""c4bc8b8390d58a5d9ae7b127af747d0b:1667206877226"",""file"":""""}";

            _mockHttpFacade.GetData(TestConstants.MetaNormalisedIpfsUrlWithFile).Returns(metaJson);

            // Act
            var result = await _classUnderTest.ProcessNFToken(Token);

            // Assert
            Assert.That(result, Is.EqualTo("ipfs://bafybeia4h2iun2jfznbxkk2bshmf5yshxlelocpondctrxxrzmkz7tsuca/1667206877240.png"));
        }
    }
}
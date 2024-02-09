using NSubstitute;

namespace UniversalNFT.dev.API.Tests.Services.Rules
{
    public class ZANIMALSTest : BaseRuleTest
    {
        [Test]
        public async Task GivenNFT_ReturnsImageAsync()
        {
            // Arrange
            Token.URI = TestConstants.MetaNormalisedIpfsUrlWithFile;

            var metaJson = @"{""attributes"":[{""description"":""BACKGROUND"",""trait_type"":""BACKGROUND"",""value"":""Meteor Shower""},{""description"":""BODY"",""trait_type"":""BODY"",""value"":""Icy Blue""},{""description"":""CLOTHES"",""trait_type"":""CLOTHES"",""value"":""Xrp hoodie _ Rat""},{""description"":""EYES"",""trait_type"":""EYES"",""value"":""Tinted biker goggles""},{""description"":""HATS"",""trait_type"":""HATS"",""value"":""Blue flat cap merch_""}],""collection"":{""name"":""ZANIMALS 5K"",""family"":""ZANIMAL""},""video"":"""",""animation"":"""",""external_link"":"""",""audio"":"""",""name"":""#2082"",""image"":""https://bafybeify5x2k7gwynjzpqanjvvcpkpk34piazge22z2dlepndxmrtn63ym.ipfs.w3s.link/1667248237551.png"",""taxon"":62,""description"":""ZANIMAL 5K COLLECTION"",""schema"":""ipfs://QmNpi8rcXEkohca8iXu7zysKKSJYqCvBJn3xJwga8jXqWU"",""nftType"":""art.v0"",""id"":""dd638ea5cc160275973953bd5c914379:1667248237531"",""file"":""""}";

            _mockHttpFacade.GetData(TestConstants.MetaNormalisedIpfsUrlWithFile).Returns(metaJson);

            // Act
            var result = await _classUnderTest.ProcessNFToken(Token);

            // Assert
            Assert.That(result, Is.EqualTo("https://bafybeify5x2k7gwynjzpqanjvvcpkpk34piazge22z2dlepndxmrtn63ym.ipfs.w3s.link/1667248237551.png"));
        }
    }
}
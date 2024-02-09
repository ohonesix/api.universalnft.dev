using NSubstitute;

namespace UniversalNFT.dev.API.Tests.Services.Rules
{
    public class TreeFacesTest : BaseRuleTest
    {
        [Test]
        public async Task GivenNFT_ReturnsImageAsync()
        {
            // Arrange
            Token.URI = TestConstants.MetaIpfs;

            var metaJson = @"{
  ""schema"": ""ipfs://bafkreidtjf2ihiwtptiyadjfmesplo555iy2jdcwhfr6hkenr2z3fvxn2y"",
  ""nftType"": ""art.v0"",
  ""name"": ""Tree Face 27"",
  ""description"": ""I know a lot of people that collect my art live in the US and Canada and they get so much more snow than I do living in Australia so this tree face is for them."",
  ""image"": ""ipfs://bafybeicyck37ahii4ljxuftdrnf3ukyruhvbmf3fjzvcwanup64wbmbh5m"",
  ""animation"": """",
  ""video"": """",
  ""audio"": """",
  ""3d_model"": """",
  ""collection"": {
    ""name"": ""Tree Faces""
  },
  ""attributes"": [],
  ""license"": ""None""
}";

            _mockHttpFacade.GetData(TestConstants.MetaNormalisedIpfsUrl).Returns(metaJson);

            // Act
            var result = await _classUnderTest.ProcessNFToken(Token);

            // Assert
            Assert.That(result, Is.EqualTo("ipfs://bafybeicyck37ahii4ljxuftdrnf3ukyruhvbmf3fjzvcwanup64wbmbh5m"));
        }
    }
}
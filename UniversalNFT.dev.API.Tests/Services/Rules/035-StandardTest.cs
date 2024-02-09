using NSubstitute;

namespace UniversalNFT.dev.API.Tests.Services.Rules
{
    public class StandardTest : BaseRuleTest
    {
        [Test]
        public async Task GivenNFT_ReturnsImageAsync()
        {
            // Arrange
            Token.URI = TestConstants.MetaIpfsWithFile;

            var metaJson = @"{
  ""name"": ""XBLADE #2531"",
  ""description"": ""Xblades are a collection of 8,888 unique powerful blades. Unique digital collectibles living on the XRPL. Your Xblade is your premium NFT that grants access to a number of things, such as XscapeNFT and StaykX utility, priority access to future XscapeNFT collections, and future airdrops. Visit xscapenft.com for more details."",
  ""image"": ""ipfs://QmSu8DCLvMhFF8WdwpStsDc9VutzYpkSCVHkWbcfdPLjkJ/2531.png"",
  ""attributes"": [
    {
      ""trait_type"": ""Effect"",
      ""value"": ""Scary Eyes""
    },
    {
      ""trait_type"": ""Blade"",
      ""value"": ""Lava Blade - Phosphorite""
    },
    {
      ""trait_type"": ""Hilt"",
      ""value"": ""Mystic Hilt""
    },
    {
      ""trait_type"": ""Handle"",
      ""value"": ""Poison Handle - Cobalt""
    },
    {
      ""trait_type"": ""Glow"",
      ""value"": ""Cerulean""
    },
    {
      ""trait_type"": ""Background"",
      ""value"": ""Limbo - Emerald""
    }
  ],
  ""collection"": {
    ""family"": ""Xblades"",
    ""name"": ""Standard""
  },
  ""edition"": 1
}";

            _mockHttpFacade.GetData(TestConstants.MetaNormalisedIpfsUrlWithFile).Returns(metaJson);

            // Act
            var result = await _classUnderTest.ProcessNFToken(Token);

            // Assert
            Assert.That(result, Is.EqualTo("ipfs://QmSu8DCLvMhFF8WdwpStsDc9VutzYpkSCVHkWbcfdPLjkJ/2531.png"));
        }
    }
}
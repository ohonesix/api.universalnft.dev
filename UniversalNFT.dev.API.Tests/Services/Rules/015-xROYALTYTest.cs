using NSubstitute;

namespace UniversalNFT.dev.API.Tests.Services.Rules
{
    public class xROYALTYTest : BaseRuleTest
    {
        [Test]
        public async Task GivenNFT_ReturnsImageAsync()
        {
            // Arrange
            Token.URI = TestConstants.MetaNormalisedIpfsUrlWithFile;

            var metaJson = @"{
  ""attributes"": [
    {
      ""trait_type"": ""Armor"",
      ""value"": ""Armor-RARE-Brown-Pants""
    },
    {
      ""trait_type"": ""Weapon"",
      ""value"": ""Weapon-ROYAL-Red""
    },
    {
      ""trait_type"": ""Hair-Color"",
      ""value"": ""Hair-Blonde""
    },
    {
      ""trait_type"": ""Accessory"",
      ""value"": ""Accessory-RARE-Yellow""
    },
    {
      ""trait_type"": ""Helmet"",
      ""value"": ""Helmet-EPIC""
    },
    {
      ""trait_type"": ""Sex"",
      ""value"": ""Female""
    },
    {
      ""trait_type"": ""Guild"",
      ""value"": ""Freedom""
    }
  ],
  ""collection"": {
    ""name"": ""xROYALTY"",
    ""family"": ""Collection""
  },
  ""video"": """",
  ""animation"": """",
  ""external_link"": """",
  ""audio"": """",
  ""name"": ""xROYALTY #1175"",
  ""image"": """ + TestConstants.NormalisedIpfsUrlWithFile + @""",
  ""taxon"": 32,
  ""description"": ""xROYALTY - Battle Against Our Own Consciousness"",
  ""schema"": ""ipfs://QmNpi8rcXEkohca8iXu7zysKKSJYqCvBJn3xJwga8jXqWU"",
  ""nftType"": ""art.v0"",
  ""external_url"": ""https://xroyalty.io/""
}";

            _mockHttpFacade.GetData(TestConstants.MetaNormalisedIpfsUrlWithFile).Returns(metaJson);

            // Act
            var result = await _classUnderTest.ProcessNFToken(Token);

            // Assert
            Assert.That(result, Is.EqualTo(TestConstants.NormalisedIpfsUrlWithFile));
        }
    }
}
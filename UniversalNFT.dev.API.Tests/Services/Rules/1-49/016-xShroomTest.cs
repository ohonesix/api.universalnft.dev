using NSubstitute;

namespace UniversalNFT.dev.API.Tests.Services.Rules
{
    public class xShroomTest : BaseRuleTest
    {
        [Test]
        public async Task GivenNFT_ReturnsImageAsync()
        {
            // Arrange
            Token.URI = TestConstants.MetaIpfsWithFile;

            var metaJson = @"{
  ""name"": ""xShroom #2887"",
  ""description"": ""xShrooms, Forage One. An 8k genesis collection by Jebzie."",
  ""image"": ""ipfs://bafybeibeuzupumno4jmqk2okzad6ca4yedfm7iil4usqqdcjjlec76kxbm/2887.png"",
  ""license"": ""CC0"",
  ""attributes"": [
    {
      ""trait_type"": ""Color"",
      ""value"": ""Orange""
    },
    {
      ""trait_type"": ""Stem"",
      ""value"": ""Tone 3""
    },
    {
      ""trait_type"": ""Natural"",
      ""value"": ""Pure""
    },
    {
      ""trait_type"": ""Eyes"",
      ""value"": ""Green Death""
    },
    {
      ""trait_type"": ""Headwear"",
      ""value"": ""Battery""
    },
    {
      ""trait_type"": ""Stemwear"",
      ""value"": ""Red Bow""
    }
  ]
}";

            _mockHttpFacade.GetData(TestConstants.MetaNormalisedIpfsUrlWithFile).Returns(metaJson);

            // Act
            var result = await _classUnderTest.ProcessNFToken(Token);

            // Assert
            Assert.That(result, Is.EqualTo("ipfs://bafybeibeuzupumno4jmqk2okzad6ca4yedfm7iil4usqqdcjjlec76kxbm/2887.png"));
        }
    }
}
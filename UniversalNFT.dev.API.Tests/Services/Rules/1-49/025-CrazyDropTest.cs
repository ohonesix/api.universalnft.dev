using NSubstitute;

namespace UniversalNFT.dev.API.Tests.Services.Rules
{
    public class CrazyDropTest : BaseRuleTest
    {
        [Test]
        public async Task GivenNFT_ReturnsImageAsync()
        {
            // Arrange
            Token.URI = TestConstants.MetaIpfsWithFile;

            var metaJson = @"{
  ""name"": ""Crazy Drop #1801"",
  ""image"": ""ipfs://bafybeig7jdxke66brrtjdhtpvzlpgqii7lvpfxueg24ffusy3ikfk5kg3m/1801.png"",
  ""collection"": {
    ""name"": ""Crazy Drops"",
    ""description"": ""Crazy Drops is the biggest NFT collection in the history of the XRP Ledger. The collection consists of 100,000 NFTs that combine utility and entertainment. Take part in regular quests, earn points, and receive rewards for your participation!"",
    ""image"": ""ipfs://bafybeiakvywvpj2a4ike56j7gbb4heedgt2u4f3t35cokzl55ek3f3wdki/0.png""
  },
  ""attributes"": [
    {
      ""trait_type"": ""Background"",
      ""value"": ""Underwater Violet""
    },
    {
      ""trait_type"": ""Legs"",
      ""value"": ""Yellow Walk""
    },
    {
      ""trait_type"": ""Right Hand"",
      ""value"": ""Victory""
    },
    {
      ""trait_type"": ""Body"",
      ""value"": ""Persian Green""
    },
    {
      ""trait_type"": ""Eyes"",
      ""value"": ""Harry Eyes""
    },
    {
      ""trait_type"": ""Mouth"",
      ""value"": ""Stick the tongue""
    },
    {
      ""trait_type"": ""Hats"",
      ""value"": ""Witch""
    },
    {
      ""trait_type"": ""Eyebrows"",
      ""value"": ""Common Thick""
    },
    {
      ""trait_type"": ""Left Hand"",
      ""value"": ""Common""
    }
  ]
}";

            _mockHttpFacade.GetData(TestConstants.MetaNormalisedIpfsUrlWithFile).Returns(metaJson);

            // Act
            var result = await _classUnderTest.ProcessNFToken(Token);

            // Assert
            Assert.That(result, Is.EqualTo("ipfs://bafybeig7jdxke66brrtjdhtpvzlpgqii7lvpfxueg24ffusy3ikfk5kg3m/1801.png"));
        }
    }
}
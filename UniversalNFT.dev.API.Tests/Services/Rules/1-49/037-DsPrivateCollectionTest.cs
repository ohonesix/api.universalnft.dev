using NSubstitute;

namespace UniversalNFT.dev.API.Tests.Services.Rules
{
    public class DsPrivateCollectionTest : BaseRuleTest
    {
        [Test]
        public async Task GivenNFT_ReturnsImageAsync()
        {
            // Arrange
            Token.URI = TestConstants.MetaNormalisedIpfsUrlWithFile;

            var metaJson = @"{

  ""dna"": ""01e57b3a6d51d8eea70871ec805fd238cac0e08b"",

  ""name"": ""XSpectar Private Collection #648"",

  ""description"": ""DS in Suit #803"",

  ""image"": ""https://nftstorage.link/ipfs/bafybeicrimxvmsug64f4n4v3blevbi2ouz2yvqxzlikq26tctby2dmfpzy/648.png"",

  ""imageHash"": null,

  ""edition"": 648,

  ""date"": 1658560014487,

  ""rarityScore"": 417.6116,

  ""collection"": {

    ""name"": ""Ds Private Collection"",

    ""family"": ""Dirk"",

    ""version"": ""1"",

    ""minRarityScore"": 359.6235,

    ""maxRarityScore"": 667.7357

  },

  ""ranking"": ""803"",

  ""createdAt"": ""2022-12-09T14:40:24Z"",

  ""attributes"": [

    {

      ""trait_type"": ""Background"",

      ""value"": ""Blue"",

      ""meta_data"": null,

      ""rarityScore"": 66.2460,

      ""chanceAsPercent"": 21.7540

    },

    {

      ""trait_type"": ""Open Shirt Suit"",

      ""value"": ""Open Shirt Suit Light Blue"",

      ""meta_data"": null,

      ""rarityScore"": 83.5091,

      ""chanceAsPercent"": 4.4909

    },

    {

      ""trait_type"": ""Open Shirt Suit Undershirt"",

      ""value"": ""Open Shirt Suit Undershirt White"",

      ""meta_data"": null,

      ""rarityScore"": 67.8530,

      ""chanceAsPercent"": 20.1470

    },

    {

      ""trait_type"": ""Extra Items"",

      ""value"": ""None"",

      ""meta_data"": null,

      ""rarityScore"": 48.9761,

      ""chanceAsPercent"": 39.0239

    },

    {

      ""trait_type"": ""Dirk Skin For Open Shirt Suit"",

      ""value"": ""Dirk Skin For Open Shirt Suit"",

      ""meta_data"": null,

      ""rarityScore"": 62.4575,

      ""chanceAsPercent"": 25.5425

    },

    {

      ""trait_type"": ""Hair"",

      ""value"": ""Slickedbackgrey"",

      ""meta_data"": null,

      ""rarityScore"": 58.9935,

      ""chanceAsPercent"": 29.0065

    },

    {

      ""trait_type"": ""Eye Wear"",

      ""value"": ""None"",

      ""meta_data"": null,

      ""rarityScore"": 39.4847,

      ""chanceAsPercent"": 48.5153

    },

    {

      ""trait_type"": ""Spectar Ring"",

      ""value"": ""None"",

      ""meta_data"": null,

      ""rarityScore"": -9.9083,

      ""chanceAsPercent"": 97.9083

    }

  ]

}";

            _mockHttpFacade.GetData(TestConstants.MetaNormalisedIpfsUrlWithFile).Returns(metaJson);

            // Act
            var result = await _classUnderTest.ProcessNFToken(Token);

            // Assert
            Assert.That(result, Is.EqualTo("https://nftstorage.link/ipfs/bafybeicrimxvmsug64f4n4v3blevbi2ouz2yvqxzlikq26tctby2dmfpzy/648.png"));
        }
    }
}
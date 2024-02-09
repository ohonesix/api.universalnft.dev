using NSubstitute;

namespace UniversalNFT.dev.API.Tests.Services.Rules
{
    public class xToadzXXLTest : BaseRuleTest
    {
        [Test]
        public async Task GivenNFT_ReturnsImageAsync()
        {
            // Arrange
            Token.URI = TestConstants.MetaIpfsWithFile;

            var metaJson = @"{
  ""nftType"": ""art.v0"",
  ""name"": ""xToadzXXL #4599"",
  ""description"": ""5,000 large body toadz living in pixel land. Ribbit ribbit!"",
  ""collection"": {
    ""name"": ""xToadzXXL"",
    ""family"": ""Toadz""
  },
  ""attributes"": [
    {
      ""trait_type"": ""Background"",
      ""description"": ""It's like Narnia but less copyrighted."",
      ""value"": ""PotOfGold""
    },
    {
      ""trait_type"": ""Large Body"",
      ""description"": ""Yay! Sweet, chewy, and pastel-colored for ultimate snacking."",
      ""value"": ""Skittles!""
    },
    {
      ""trait_type"": ""Eyes"",
      ""description"": ""Straight-up amphibious OG."",
      ""value"": ""Yellow""
    },
    {
      ""trait_type"": ""Mouth"",
      ""description"": ""This boy needs yoga and meditation."",
      ""value"": ""DirtyMouth""
    },
    {
      ""trait_type"": ""Hat"",
      ""description"": ""You're fashion-forward and a little bad-ass."",
      ""value"": ""Mohawk""
    },
    {
      ""trait_type"": ""Accessory"",
      ""description"": ""None"",
      ""value"": ""None""
    },
    {
      ""trait_type"": ""Music"",
      ""description"": ""BuyMeADrink"",
      ""value"": ""BuyMeADrink""
    }
  ],
  ""schema"": ""ipfs://QmNpi8rcXEkohca8iXu7zysKKSJYqCvBJn3xJwga8jXqWU"",
  ""video"": ""ipfs://bafybeiepsgif6ex6xvdhs7gmnwga57r4nccrrqey45rod6pa3entw6osoa/4599.mp4"",
  ""image"": ""ipfs://bafybeicu4usnaetfww476lup4q37gkzg4zop26s2tj5dh25fzkkb5in3c4/4599.png"",
  ""issuer"": ""rBwoDxDHHRkVq7jtTcjD9YG1TUFvpPvGXx""
}";

            _mockHttpFacade.GetData(TestConstants.MetaNormalisedIpfsUrlWithFile).Returns(metaJson);

            // Act
            var result = await _classUnderTest.ProcessNFToken(Token);

            // Assert
            Assert.That(result, Is.EqualTo("ipfs://bafybeicu4usnaetfww476lup4q37gkzg4zop26s2tj5dh25fzkkb5in3c4/4599.png"));
        }
    }
}
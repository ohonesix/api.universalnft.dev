using NSubstitute;

namespace UniversalNFT.dev.API.Tests.Services.Rules
{
    public class xToadzTest : BaseRuleTest
    {
        [Test]
        public async Task GivenNFT_ReturnsImageAsync()
        {
            // Arrange
            Token.URI = TestConstants.MetaIpfsWithFile;

            var metaJson = @"{
  ""nftType"": ""art.v0"",
  ""name"": ""xToadz #1079"",
  ""description"": ""10,000 small body toadz living in pixel land. Ribbit ribbit!"",
  ""collection"": {
    ""name"": ""xToadz"",
    ""family"": ""Toadz""
  },
  ""attributes"": [
    {
      ""trait_type"": ""Background"",
      ""description"": ""It's purple. Very conducive to lollipops and other grape-flavored treats."",
      ""value"": ""Purple""
    },
    {
      ""trait_type"": ""Small Body"",
      ""description"": ""Black and grey. Very minimal and sophisticated. Appropriate for formal events."",
      ""value"": ""BlackGrey""
    },
    {
      ""trait_type"": ""Eyes"",
      ""description"": ""You are literally the most beautiful thing I've ever seen."",
      ""value"": ""LuhYou""
    },
    {
      ""trait_type"": ""Mouth"",
      ""description"": ""From the streets of Tokyo, a Michelin Star ramen. The broth alone takes several weeks to prepare. Bouncy noodles. Veg and pork. 50,000 $RIBBITS if you can get a reservation (good luck)."",
      ""value"": ""MichelinStarRamen""
    },
    {
      ""trait_type"": ""Hat"",
      ""description"": ""You're a beautiful angel. [Cue heavenly music.]"",
      ""value"": ""Halo""
    },
    {
      ""trait_type"": ""Accessory"",
      ""description"": ""None"",
      ""value"": ""None""
    },
    {
      ""trait_type"": ""Music"",
      ""description"": ""AllTheRibbits"",
      ""value"": ""AllTheRibbits""
    }
  ],
  ""schema"": ""ipfs://QmNpi8rcXEkohca8iXu7zysKKSJYqCvBJn3xJwga8jXqWU"",
  ""video"": ""ipfs://bafybeiecovrmyejr534i4oef6umtsohr64mjaofpatw4muijrgmwv45w3a/1079.mp4"",
  ""image"": ""ipfs://bafybeibrdj7agseu6rxjumyrwaupklftac4ooohf7ao4sc3hggrjmknx5i/1079.png"",
  ""issuer"": ""rBwoDxDHHRkVq7jtTcjD9YG1TUFvpPvGXx""
}";

            _mockHttpFacade.GetData(TestConstants.MetaNormalisedIpfsUrlWithFile).Returns(metaJson);

            // Act
            var result = await _classUnderTest.ProcessNFToken(Token);

            // Assert
            Assert.That(result, Is.EqualTo("ipfs://bafybeibrdj7agseu6rxjumyrwaupklftac4ooohf7ao4sc3hggrjmknx5i/1079.png"));
        }
    }
}
using NSubstitute;

namespace UniversalNFT.dev.API.Tests.Services.Rules
{
    public class AAXRPCyborgsTest : BaseRuleTest
    {
        [Test]
        public async Task GivenNFT_ReturnsImageAsync()
        {
            // Arrange
            Token.URI = TestConstants.MetaIpfsWithFile;

            var metaJson = @"{
  ""schema"": ""ipfs://QmNpi8rcXEkohca8iXu7zysKKSJYqCvBJn3xJwga8jXqWU"",
  ""nftType"": ""art.v0"",
  ""name"": ""XRP Cyborg #222"",
  ""description"": ""The A.A. XRP Cyborgs are here to join and support the A.A. Family. The understanders of the future will not be humans but what we choose to call 'cyborgs' that will have designed and built themselves. DIE as a human or LIVE forever as an XRP Cyborg!"",
  ""image"": ""ipfs://bafybeibqve5d6zmtsdhlyo62iiibcmndthz6txekj5coxnfbyy7bfh7cda/222.png"",
  ""image_url"": ""ipfs://bafybeibqve5d6zmtsdhlyo62iiibcmndthz6txekj5coxnfbyy7bfh7cda/222.png"",
  ""category"": ""art"",
  ""content_type"": ""image/png"",
  ""collection"": {
    ""family"": ""Anonymous Astronauts XRPL"",
    ""name"": ""A.A. XRP Cyborgs""
  },
  ""attributes"": [],
  ""external_url"": ""https://www.anonymousnfts.xyz"",
  ""edition"": 222,
  ""date"": 1671667200000,
  ""compiler"": ""Anonymous Astronauts XRPL""
}";

            _mockHttpFacade.GetData(TestConstants.MetaNormalisedIpfsUrlWithFile).Returns(metaJson);

            // Act
            var result = await _classUnderTest.ProcessNFToken(Token);

            // Assert
            Assert.That(result, Is.EqualTo("ipfs://bafybeibqve5d6zmtsdhlyo62iiibcmndthz6txekj5coxnfbyy7bfh7cda/222.png"));
        }
    }
}
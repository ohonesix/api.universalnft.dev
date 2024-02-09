using NSubstitute;

namespace UniversalNFT.dev.API.Tests.Services.Rules
{
    public class XWalkersLiveAuctionTest : BaseRuleTest
    {
        [Test]
        public async Task GivenNFT_ReturnsImageAsync()
        {
            // Arrange
            Token.URI = TestConstants.MetaNormalisedIpfsUrlWithFile;

            var metaJson = @"{""name"":""XWO 0745"",""description"":""This exclusive 1 of 1 hand drawn original tier NFT was sold in the X Walkers Live Auction #26. Original Tier NFTs come with 2 points on the richlist."",""external_url"":""https://xrpwalkers.com/"",""attributes"":[],""category"":""art"",""md5hash"":""4d96fc267c767848f36640d74cf3cff8"",""is_explicit"":false,""content_type"":""image/png"",""image_url"":""ipfs://ipfs/bafybeibmxcyc7ajs25eyhriclgw6bjj4vdoumyizgy2c45ukq7ebqo4i64/image.jpeg"",""animation_url"":""ipfs://ipfs/bafybeibmxcyc7ajs25eyhriclgw6bjj4vdoumyizgy2c45ukq7ebqo4i64/animation.png""}";

            _mockHttpFacade.GetData(TestConstants.MetaNormalisedIpfsUrlWithFile).Returns(metaJson);

            // Act
            var result = await _classUnderTest.ProcessNFToken(Token);

            // Assert
            Assert.That(result, Is.EqualTo("ipfs://ipfs/bafybeibmxcyc7ajs25eyhriclgw6bjj4vdoumyizgy2c45ukq7ebqo4i64/image.jpeg"));
        }
    }
}
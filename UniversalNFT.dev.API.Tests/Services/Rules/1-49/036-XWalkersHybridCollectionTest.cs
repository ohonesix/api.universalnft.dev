using NSubstitute;

namespace UniversalNFT.dev.API.Tests.Services.Rules
{
    public class XWalkersHybridCollectionTest : BaseRuleTest
    {
        [Test]
        public async Task GivenNFT_ReturnsImageAsync()
        {
            // Arrange
            Token.URI = TestConstants.MetaNormalisedIpfsUrlWithFile;

            var metaJson = @"{""name"":""XWH 093"",""description"":""This NFT was sold as part of The X Walkers Hybrid Collection; reserved for Gen 1 and Gen2 OG holders. Hybrid NFTs give the holder additional utility on top of our monthly reward system. Holders of Hybrid NFTs get a share of an added 2.5% rewards pot from the Metaverse and Gaming revenues, 200XRP discount on our upcoming 3D and 3D animated NFTs, a free plot of land and voting rights on development in the Metaverse on our future project.  This GOLD TIER NFT is one of a total supply of 500 2D X Walker Gold NFTs. Gold tier NFTs are worth 5 points on our rich list system."",""external_url"":""https://xrpwalkers.com/"",""attributes"":[],""category"":""art"",""md5hash"":""24baf0566d26b462bc29723e79820830"",""is_explicit"":false,""content_type"":""image/png"",""image_url"":""ipfs://ipfs/bafybeiclp5msf7cw5ylauwqnnt7fehvnjdnnkc3kaay2sle3tqevtkcerm/image.jpeg"",""animation_url"":""ipfs://ipfs/bafybeiclp5msf7cw5ylauwqnnt7fehvnjdnnkc3kaay2sle3tqevtkcerm/animation.png""}";

            _mockHttpFacade.GetData(TestConstants.MetaNormalisedIpfsUrlWithFile).Returns(metaJson);

            // Act
            var result = await _classUnderTest.ProcessNFToken(Token);

            // Assert
            Assert.That(result, Is.EqualTo("ipfs://ipfs/bafybeiclp5msf7cw5ylauwqnnt7fehvnjdnnkc3kaay2sle3tqevtkcerm/image.jpeg"));
        }
    }
}
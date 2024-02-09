using NSubstitute;

namespace UniversalNFT.dev.API.Tests.Services.Rules
{
    public class xSPECTARTest : BaseRuleTest
    {
        [Test]
        public async Task GivenNFT_ReturnsImageAsync()
        {
            // Arrange
            Token.URI = TestConstants.MetaNormalisedIpfsUrl;

            var metaJson = @"{ ""schema"":""ipfs://QmNpi8rcXEkohca8iXu7zysKKSJYqCvBJn3xJwga8jXqWU"", ""nftType"":""art.v0"", ""name"":""Agent #4697"", ""description"":""Agent #4697, ranked 7693 of 8888 from the xSPECTAR Genesis Collection, is leading the way to the next frontier of innovation. This Agent is your digital access pass into our new world of infinite opportunities"", ""image"":""" +  TestConstants.NormalisedIpfsUrl +@""", ""collection"": { ""name"":""xSPECTAR"", ""family"":""8888"", ""version"":""1"", ""minRarityScore"": 281.45853, ""maxRarityScore"": 80105.53 }, ""attributes"":[{ ""trait_type"":""Avatar"", ""value"":""Default 1"", ""rarityScore"": 2.01223 },{ ""trait_type"":""Back"", ""value"":""Nothing"", ""rarityScore"": 1.01693 },{ ""trait_type"":""Background"", ""value"":""Triangles Cobalt Blue"", ""rarityScore"": 16.13067 },{ ""trait_type"":""Cheeks"", ""value"":""Star Freckles Orange Yellow"", ""rarityScore"": 4.52776 },{ ""trait_type"":""Clothing"", ""value"":""DenimJacket Hoodie Blue"", ""rarityScore"": 125.1831 },{ ""trait_type"":""Ears"", ""value"":""Earring Middle Gold"", ""rarityScore"": 22.38791 },{ ""trait_type"":""Eyeball"", ""value"":""Green"", ""rarityScore"": 4.47082 },{ ""trait_type"":""Eyebrow"", ""value"":""Piercing Barbell"", ""rarityScore"": 4.08832 },{ ""trait_type"":""Eyebrow Groom"", ""value"":""Default 01"", ""rarityScore"": 2.0992 },{ ""trait_type"":""Facial Hair"", ""value"":""Nothing"", ""rarityScore"": 1.95642 },{ ""trait_type"":""Fingers"", ""value"":""Vintage Index"", ""rarityScore"": 62.15385 },{ ""trait_type"":""Gender"", ""value"":""Female"", ""rarityScore"": 2 },{ ""trait_type"":""Glasses"", ""value"":""Nothing"", ""rarityScore"": 6.52091 },{ ""trait_type"":""Hair"", ""value"":""Buzz Cut"", ""rarityScore"": 1.95083 },{ ""trait_type"":""Hand"", ""value"":""Walkie Talkie"", ""rarityScore"": 76.62069 },{ ""trait_type"":""Head"", ""value"":""Fisherman Beanie Orange"", ""rarityScore"": 33.16418 },{ ""trait_type"":""Mouth"", ""value"":""Nothing"", ""rarityScore"": 1.01172 },{ ""trait_type"":""Neck"", ""value"":""Necklace Diamond Sticks"", ""rarityScore"": 21.52058 },{ ""trait_type"":""Nose"", ""value"":""Barbell Piercing"", ""rarityScore"": 9.99775 },{ ""trait_type"":""Shoulder"", ""value"":""Nothing"", ""rarityScore"": 1.11996 },{ ""trait_type"":""Wrist"", ""value"":""Bracelet 01 Gold"", ""rarityScore"": 13.18694 },{ ""trait_type"":""Rarity"", ""value"":""413.12076"", ""rarityScore"": 0 },{ ""trait_type"":""Ranking"", ""value"":""7693"", ""rarityScore"": 0 }], ""rarityScore"": 413.12076, ""ranking"": 7693, ""createdAt"" : ""2022-12-07T16:11:00Z"", ""altImageData"": { ""rawCentralisedUri"" : ""https://xspectarnfts.blob.core.windows.net/nfts/ea328950-e8d5-4ce8-a5d3-5c5fb7b40a52.png"", ""centralisedUri"" : ""https://xspectarnfts.blob.core.windows.net/nfts-medium/ea328950-e8d5-4ce8-a5d3-5c5fb7b40a52.jpg"", ""ipfs"" : { ""pinningService"" : ""FileBase"", ""rawImageCid"" : ""QmdTcXiCegFATE14kHpAW2MqpZyij2azfctytdPZ77ja2y"", ""rawImageHttpGatewayUri"" : ""https://ipfs.filebase.io/ipfs/QmdTcXiCegFATE14kHpAW2MqpZyij2azfctytdPZ77ja2y"", ""Cid"" : ""QmRAWJrSDVXB7UJfEzeXVAzNcGwkJ57erhUAsYuQ51TrDc"", ""httpGatewayUri"" : ""https://ipfs.filebase.io/ipfs/QmRAWJrSDVXB7UJfEzeXVAzNcGwkJ57erhUAsYuQ51TrDc"" } } }";

            _mockHttpFacade.GetData(TestConstants.MetaNormalisedIpfsUrl).Returns(metaJson);

            // Act
            var result = await _classUnderTest.ProcessNFToken(Token);

            // Assert
            Assert.That(result, Is.EqualTo(TestConstants.NormalisedIpfsUrl));
        }
    }
}
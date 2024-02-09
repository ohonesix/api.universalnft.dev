using NSubstitute;

namespace UniversalNFT.dev.API.Tests.Services.Rules
{
    public class ReaperFinancialsMarshallsTest : BaseRuleTest
    {
        [Test]
        public async Task GivenNFT_ReturnsImageAsync()
        {
            // Arrange
            Token.URI = TestConstants.MetaNormalisedIpfsUrlWithFile;

            var metaJson = @"{""name"":""Marshals #74"",""description"":""This card is a part of the third NFT collection for The Reaper Financial ($RPR). Minted on XLS-20 standard. Marshals series is about Reaper Financials connection to the Hard-slotted projects and building a strong XRPL alliance. Reaper character doing unexpected things! This is a Reaper Financial dedicated card, one of its ten variations. Revealed behind the 2nd Circle on 31 December 2022. DYOR Reaper Financial at: https:&#x2F;&#x2F;linktr.ee&#x2F;reaper_financial_llc DYOR The Reaper at: http:&#x2F;&#x2F;www.reaper.financial NFT Linktree: https:&#x2F;&#x2F;linktr.ee&#x2F;reaper_nfts Join Telegram at: http:&#x2F;&#x2F;t.me&#x2F;+AAMLQ9Lcm15lNDVh NFT artwork hand-drawn by Sumi Stik &#x2F; xSTIK Official: http:&#x2F;&#x2F;xrpstik.com"",""external_url"":""https://www.reaper.financial/"",""attributes"":[{""trait_type"":""units"",""value"":""4""},{""trait_type"":""original"",""value"":1,""max_value"":1}],""category"":""collectibles"",""md5hash"":""3f062c95dbe366f17abeada52c3a0a66"",""is_explicit"":false,""content_type"":""image/png"",""image_url"":""ipfs://ipfs/bafybeibwi3tk57nce4fvomsfayfykzyz3hqtubpdb7dwyt4zxipiz3dvpm/image.jpeg"",""animation_url"":""ipfs://ipfs/bafybeibwi3tk57nce4fvomsfayfykzyz3hqtubpdb7dwyt4zxipiz3dvpm/animation.png""}";

            _mockHttpFacade.GetData(TestConstants.MetaNormalisedIpfsUrlWithFile).Returns(metaJson);

            // Act
            var result = await _classUnderTest.ProcessNFToken(Token);

            // Assert
            Assert.That(result, Is.EqualTo("ipfs://ipfs/bafybeibwi3tk57nce4fvomsfayfykzyz3hqtubpdb7dwyt4zxipiz3dvpm/image.jpeg"));
        }
    }
}
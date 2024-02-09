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

            var metaJson = @"{""name"":""Marshals #90"",""description"":""This card is a part of the third NFT collection for The Reaper Financial ($RPR). Minted on XLS-20 standard. Marshals series is about Reaper Financials connection to the Hard-slotted projects and building a strong XRPL alliance. Reaper character doing unexpected things! This is a Reaper Financial dedicated card, one of its ten variations. Revealed behind the 2nd Circle on 24 December 2022. DYOR Reaper Financial at: https:&#x2F;&#x2F;linktr.ee&#x2F;reaper_financial_llc DYOR The Reaper at: http:&#x2F;&#x2F;www.reaper.financial NFT Linktree: https:&#x2F;&#x2F;linktr.ee&#x2F;reaper_nfts Join Telegram at: http:&#x2F;&#x2F;t.me&#x2F;+AAMLQ9Lcm15lNDVh NFT artwork hand-drawn by Sumi Stik &#x2F; xSTIK Official: http:&#x2F;&#x2F;xrpstik.com"",""external_url"":""https://www.reaper.financial/"",""attributes"":[{""trait_type"":""units"",""value"":""4""},{""trait_type"":""original"",""value"":1,""max_value"":1}],""category"":""collectibles"",""md5hash"":""eaf2f1a134d62542f414174a3418635b"",""is_explicit"":false,""content_type"":""image/png"",""image_url"":""" + TestConstants.IpfsDoubleWithFile + @""",""animation_url"":""ipfs://ipfs/bafybeihy4xvbn2bxcgzexpdpcozc6mg5o6hqrrw3pb6qgexjvtlslz7sla/animation.png""}";

            _mockHttpFacade.GetData(TestConstants.MetaNormalisedIpfsUrlWithFile).Returns(metaJson);

            // Act
            var result = await _classUnderTest.ProcessNFToken(Token);

            // Assert
            Assert.That(result, Is.EqualTo(TestConstants.IpfsDoubleWithFile));
        }
    }
}
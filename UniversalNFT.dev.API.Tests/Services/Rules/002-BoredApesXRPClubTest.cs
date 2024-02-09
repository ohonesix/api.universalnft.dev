using NSubstitute;

namespace UniversalNFT.dev.API.Tests.Services.Rules
{
    public class BoredApesXRPClubTest : BaseRuleTest
    {
        [Test]
        public async Task GivenNFT_ReturnsImageAsync()
        {
            // Arrange
            Token.URI = TestConstants.MetaIpfsRaw;

            var metaJson = @"{""name"":""Bored Apes XRP Club #1763"",""description"":""The Bored Apes XRP Club is a collection of 10,000 unique Bored Ape NFTs living on the XRP ledger."",""image"":"""+ TestConstants.MetaIpfsRawWithFile + @""",""edition"":""1763"",""date"":1667650922062,""creator"":""Bored Apes XRP Club"",""artist"":""Bored Apes XRP Club"",""attributes"":[{""trait_type"":""Background"",""value"":""Purple""},{""trait_type"":""Fur"",""value"":""Magnolia""},{""trait_type"":""Earring"",""value"":""Silver Ripple""},{""trait_type"":""Clothes"",""value"":""Leather Jacket""},{""trait_type"":""Mouth"",""value"":""S""},{""trait_type"":""Eyes"",""value"":""Shy""},{""trait_type"":""Headwear"",""value"":""Beret""}],""external_link"":""https://x-apes.com/"",""category"":""Collectables"",""collection"":{""name"":""Bored Apes XRP Club"",""family"":""art""}}";

            _mockHttpFacade.GetData(TestConstants.NormalisedIpfsUrl).Returns(metaJson);

            // Act
            var result = await _classUnderTest.ProcessNFToken(Token);

            // Assert
            Assert.That(result, Is.EqualTo(TestConstants.MetaIpfsRawWithFile));
        }
    }
}
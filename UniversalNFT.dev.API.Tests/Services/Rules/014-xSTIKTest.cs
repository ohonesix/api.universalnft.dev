using NSubstitute;

namespace UniversalNFT.dev.API.Tests.Services.Rules
{
    public class xSTIKTest : BaseRuleTest
    {
        [Test]
        public async Task GivenNFT_ReturnsImageAsync()
        {
            // Arrange
            Token.URI = TestConstants.MetaNormalisedIpfsUrlWithFile;

            var metaJson = @"{""name"":""x00372"",""category"":""collectibles"",""md5hash"":""d5d9f843d103c2996a239f5d5b0df328"",""content_type"":""image/png"",""description"":""Part of Original Tier Drop Zone 2 collection. Originally given to a Holder positioned between 102 and 301, or one of 100 spread out from 302 to 4000 in 6th of February 2022 xSTIK Richlist Snapshot."",""is_explicit"":false,""external_url"":""https://xrpstik.com"",""animation_url"":""ipfs://ipfs/bafybeifnwbg27fhmld5nrneg2tqun35fflxkklilo4q3w3czpta46bfvza/data.png"",""attributes"":[{""trait_type"":""original_tier"",""value"":""x00372""}],""image_url"":""ipfs://ipfs/bafybeibvgcxo3qldjq7pcpbkykzbfo3hsvyplikmb4kzm7246xppgatbrq/image.png""}";

            _mockHttpFacade.GetData(TestConstants.MetaNormalisedIpfsUrlWithFile).Returns(metaJson);

            // Act
            var result = await _classUnderTest.ProcessNFToken(Token);

            // Assert
            Assert.That(result, Is.EqualTo("ipfs://ipfs/bafybeibvgcxo3qldjq7pcpbkykzbfo3hsvyplikmb4kzm7246xppgatbrq/image.png"));
        }
    }
}
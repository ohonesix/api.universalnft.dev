using NSubstitute;

namespace UniversalNFT.dev.API.Tests.Services.Rules
{
    public class MagneticGirlsAutoMiningTest : BaseRuleTest
    {
        [Test]
        public async Task GivenNFT_ReturnsImageAsync()
        {
            // Arrange
            Token.URI = TestConstants.MetaNormalisedIpfsUrlWithFile;

            var metaJson = @"{""name"":""AutoCommon | S3#91"",""description"":""AutoCommon | S3#91\n\nThe main feature of these nfts is that they increase the income from automining, up to 120%!\n\nPS: stacks with all other types of rewards\n\nMore info on xmagnetic.org"",""external_url"":""https://xmagnetic.org/"",""attributes"":[{""trait_type"":""rarity"",""value"":""AutoCommon""}],""category"":""others"",""md5hash"":""ea18f3e4b4450f510d13c7808d4bb80d"",""is_explicit"":false,""content_type"":""image/png"",""image_url"":""ipfs://ipfs/bafybeifzjphgeflurhjbghbi4zgcvs36jx6llxno5733w4y36tek24ejr4/image.jpeg"",""animation_url"":""ipfs://ipfs/bafybeifzjphgeflurhjbghbi4zgcvs36jx6llxno5733w4y36tek24ejr4/animation.png""}";

            _mockHttpFacade.GetData(TestConstants.MetaNormalisedIpfsUrlWithFile).Returns(metaJson);

            // Act
            var result = await _classUnderTest.ProcessNFToken(Token);

            // Assert
            Assert.That(result, Is.EqualTo("ipfs://ipfs/bafybeifzjphgeflurhjbghbi4zgcvs36jx6llxno5733w4y36tek24ejr4/image.jpeg"));
        }
    }
}
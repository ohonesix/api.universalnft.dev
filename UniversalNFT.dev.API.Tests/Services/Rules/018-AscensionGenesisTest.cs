using NSubstitute;

namespace UniversalNFT.dev.API.Tests.Services.Rules
{
    public class AscensionGenesisTest : BaseRuleTest
    {
        [Test]
        public async Task GivenNFT_ReturnsImageAsync()
        {
            // Arrange
            Token.URI = TestConstants.MetaNormalisedIpfsUrlWithFile;

            var metaJson = @"{""name"":""Ascension Genesis #84"",""description"":""The first NFT collection for AscensionIndex.com ($ASC)\n\nThis series consists of 100 NFT cards. Holders of Ascension Genesis Collection NFT&#x27;s hold a lifetime pass to hang out with the Founders and Staff at all upcoming real-world conventions. Cards 10 &amp; below include a +1 for events. \n\nAll Ascension NFT&#x27;s grant 24 hours early-access to future limited Bullion Mints.\n\nUtility NFT Incentive Tokens (UNITs) paid in ASC from 50% of all future primary NFT sets."",""external_url"":""https://www.ascensionindex.com/"",""attributes"":[{""trait_type"":""units_plus_one_half"",""value"":""2""},{""trait_type"":""bullion_early_access"",""value"":""24""},{""trait_type"":""vip_access"",""value"":""1""},{""trait_type"":""original"",""value"":1,""max_value"":1}],""category"":""collectibles"",""md5hash"":""54318c0c0e59b67d5540f93b55ebe245"",""is_explicit"":false,""content_type"":""image/png"",""image_url"":""" + TestConstants.IpfsDoubleWithFile + @""",""animation_url"":""ipfs://ipfs/bafybeiboqeqooewnmbq2adm5xxuwswmhaas6yfjzodfmatbltudefxlvyu/animation.png""}";

            _mockHttpFacade.GetData(TestConstants.MetaNormalisedIpfsUrlWithFile).Returns(metaJson);

            // Act
            var result = await _classUnderTest.ProcessNFToken(Token);

            // Assert
            Assert.That(result, Is.EqualTo(TestConstants.IpfsDoubleWithFile));
        }
    }
}
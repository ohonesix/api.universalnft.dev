using NSubstitute;

namespace UniversalNFT.dev.API.Tests.Services.Rules
{
    public class MagneticGirlsTest : BaseRuleTest
    {
        [Test]
        public async Task GivenNFT_ReturnsImageAsync()
        {
            // Arrange
            Token.URI = TestConstants.MetaNormalisedIpfsUrlWithFile;

            var metaJson = @"{""name"":""Magnetic Girl Rare | S1#34"",""description"":""Magnetic Girl Rare | S1#34\n\nGirls provide incredible benefits in mining and other Magnetic tools!"",""external_url"":""https://xmagnetic.org"",""attributes"":[{""trait_type"":""rarity"",""value"":""Rare""},{""trait_type"":""background"",""value"":""Pink Stripes""},{""trait_type"":""body"",""value"":""White Skin""},{""trait_type"":""clothes"",""value"":""Swimsuit+leg Garters""},{""trait_type"":""face"",""value"":""Makeup 8""},{""trait_type"":""accessory"",""value"":""Masquerade Mask Violet""},{""trait_type"":""hair"",""value"":""Girl With Caret""}],""category"":""others"",""md5hash"":""91eb56c845b2526fde9f392b3b104d95"",""is_explicit"":true,""content_type"":""image/png"",""image_url"":""ipfs://ipfs/bafybeih55cfykhyjfogflwyos33sye44d4cjrgvz5lf643uw6u7hfy6gei/image.jpeg"",""animation_url"":""ipfs://ipfs/bafybeih55cfykhyjfogflwyos33sye44d4cjrgvz5lf643uw6u7hfy6gei/animation.png""}";

            _mockHttpFacade.GetData(TestConstants.MetaNormalisedIpfsUrlWithFile).Returns(metaJson);

            // Act
            var result = await _classUnderTest.ProcessNFToken(Token);

            // Assert
            Assert.That(result, Is.EqualTo("ipfs://ipfs/bafybeih55cfykhyjfogflwyos33sye44d4cjrgvz5lf643uw6u7hfy6gei/image.jpeg"));
        }
    }
}
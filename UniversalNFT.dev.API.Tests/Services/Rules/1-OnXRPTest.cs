using NSubstitute;

namespace UniversalNFT.dev.API.Tests.Services.Rules
{
    public class OnXRPTest : BaseRuleTest
    {
        [Test]
        public async Task GivenNFT_ReturnsImageAsync()
        {
            // Arrange
            var metaJson = "{\"dna\":\"705064feb136c1e9e265bbeb4e6e08bb89fcb49afc6d08d4baeddfdd06c92def\"," +
                "\"name\":\"XPUNKS #016\",\"description\":\"The XPUNK collection consists of 10,000 uniquely generated characters " +
                "on a 24-by 24-pixel canvas. They are distinguished by their colourful traits and trademark X-mouth that represents their " +
                "love for the XRPL.\",\"image\":\"" + TestConstants.MetaIpfs + "\"," +
                "\"imageHash\":\"70f23ea37fddcc5413feb9a58343e9afad255c810620c4398a52d13752cf3f35\",\"edition\":2372,\"date\":1667163538900," +
                "\"attributes\":[{\"trait_type\":\"Face\",\"value\":\"Male 7\"},{\"trait_type\":\"Eye Characteristics\",\"value\":\"Fresh\"}," +
                "{\"trait_type\":\"Eyes\",\"value\":\"Black Right\"},{\"trait_type\":\"Neck Wearable\",\"value\":\"No Attribute\"}," +
                "{\"trait_type\":\"Hair & Hats\",\"value\":\"Headphones Blue\"},{\"trait_type\":\"Facial Hair\",\"value\":\"Scruff\"}," +
                "{\"trait_type\":\"Eye Wearables\",\"value\":\"Aviator Sunglasses\"},{\"trait_type\":\"Mouth Attribute\",\"value\":\"No Attribute\"}," +
                "{\"trait_type\":\"Piercings\",\"value\":\"No Attribute\"}],\"schema\":\"ipfs://QmNpi8rcXEkohca8iXu7zysKKSJYqCvBJn3xJwga8jXqWU\"," +
                "\"nftType\":\"art.v0\",\"collection\":{\"name\":\"XPUNKS\",\"description\":\"The XPUNK collection consists of 10,000 " +
                "uniquely generated characters on a 24-by-24-pixel canvas. They are distinguished by their colorful traits and trademark " +
                "X-mouth that represents their love for the XRPL.\"}}";

            _mockOnXRPService.GetImageFromMetadata(Token.NFTokenID)
                .Returns(metaJson);

            // Act
            var result = await _classUnderTest.ProcessNFToken(Token);

            // Assert
            Assert.That(result, Is.EqualTo(TestConstants.MetaIpfs));
        }
    }
}
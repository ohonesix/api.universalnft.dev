using NSubstitute;

namespace UniversalNFT.dev.API.Tests.Services.Rules
{
    public class QueensQuestTest : BaseRuleTest
    {
        [Test]
        public async Task GivenNFT_ReturnsImageAsync()
        {
            // Arrange
            Token.URI = TestConstants.MetaIpfsWithFile;

            var metaJson = @"{
    ""name"": ""Puzzling"",
    ""description"": ""250 unique collection of 1/1 NFT with a broad story of the Queen’s life journey that involves characters, thoughts, emotions and feelings. Having her dominion and walking on her authority as the Queen.\n    She is a perfect balance of strength and passion. Sensitive is her heart \n    Strength is her mind. Her soul is absolute fire. You can’t burn what already been lit. Her flames will either warm you or it will suffocate you because  She is The Queen"",
    ""image"": ""ipfs://QmYbEHwF4r7purSTdiuWaq3uPp7eSHudU5JKfKARZUySeX/101.jpg"",
    ""edition"": 1,
    ""collection"": {
        ""name"": ""Queens Quest""
    },
    ""attributes"": [
        {
            ""trait_type"": ""Series"",
            ""value"": 4
        },
        {
            ""trait_type"": ""No"",
            ""value"": 19
        },
        {
            ""trait_type"": ""Discount"",
            ""value"": 2
        }
    ]
}";

            _mockHttpFacade.GetData(TestConstants.MetaNormalisedIpfsUrlWithFile).Returns(metaJson);

            // Act
            var result = await _classUnderTest.ProcessNFToken(Token);

            // Assert
            Assert.That(result, Is.EqualTo("ipfs://QmYbEHwF4r7purSTdiuWaq3uPp7eSHudU5JKfKARZUySeX/101.jpg"));
        }
    }
}
using NSubstitute;

namespace UniversalNFT.dev.API.Tests.Services.Rules
{
    public class XRPlOOtersTest : BaseRuleTest
    {
        [Test]
        public async Task GivenNFT_ReturnsImageAsync()
        {
            // Arrange
            Token.URI = TestConstants.MetaIpfsWithFile;

            var metaJson = @"{""attributes"":[{""description"":""1. Backgrounds"",""trait_type"":""1. Backgrounds"",""value"":""6""},{""description"":""2. Skin"",""trait_type"":""2. Skin"",""value"":""Blue Skin""},{""description"":""3. Eyes"",""trait_type"":""3. Eyes"",""value"":""Metal Glasses""},{""description"":""4. Mouth"",""trait_type"":""4. Mouth"",""value"":""Mouth 3""},{""description"":""5. Outfits"",""trait_type"":""5. Outfits"",""value"":""Neck Shirt Green rev""},{""description"":""6. Head"",""trait_type"":""6. Head"",""value"":""Blue Stripe Bandanna""}],""collection"":{""name"":""XRP lOOters"",""family"":""Club OOtopia""},""video"":"""",""animation"":"""",""external_link"":"""",""audio"":"""",""name"":""Square Head #3221"",""image"":""ipfs://bafybeibgrtrcqq6tr2rw4t5la4pqxhrscaoo2ovs3fnpbksc6wui5svdsu/1669165197932.png"",""taxon"":336,""description"":""This is Square Head"",""schema"":""ipfs://QmNpi8rcXEkohca8iXu7zysKKSJYqCvBJn3xJwga8jXqWU"",""nftType"":""art.v0"",""id"":""53fbef56dee30a65e488772c5f90da92:1669165197833"",""file"":""""}";

            _mockHttpFacade.GetData(TestConstants.MetaNormalisedIpfsUrlWithFile).Returns(metaJson);

            // Act
            var result = await _classUnderTest.ProcessNFToken(Token);

            // Assert
            Assert.That(result, Is.EqualTo("ipfs://bafybeibgrtrcqq6tr2rw4t5la4pqxhrscaoo2ovs3fnpbksc6wui5svdsu/1669165197932.png"));
        }
    }
}
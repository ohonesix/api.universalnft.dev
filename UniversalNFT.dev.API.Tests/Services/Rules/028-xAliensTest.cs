using NSubstitute;

namespace UniversalNFT.dev.API.Tests.Services.Rules
{
    public class xAliensTest : BaseRuleTest
    {
        [Test]
        public async Task GivenNFT_ReturnsImageAsync()
        {
            // Arrange
            Token.URI = TestConstants.MetaIpfs;

            var metaJson = @"{""schema"":""ipfs://QmNpi8rcXEkohca8iXu7zysKKSJYqCvBJn3xJwga8jXqWU"",""nftType"":""art.v0"",""name"":""xAliens #9238"",""artist"":""Toink Wright"",""description"":""xAliens is a collection of quality, hand curated, original artwork. It is the genesis collection of X City Labs."",""image"":""ipfs://Qmc2o8xUCHFwJiFZa7Ta9Wn2NP72ZoYQGC1RJiVAkV6edv"",""collection"":{""name"":""xAliens Genesis"",""family"":""xAliens""},""attributes"":[{""trait_type"":""Background"",""value"":""Phador Prime""},{""trait_type"":""Skin"",""value"":""Forest""},{""trait_type"":""Skin Effect"",""value"":""Tribal Lime Glow""},{""trait_type"":""Eyes"",""value"":""Glimmer Green""},{""trait_type"":""Clothing"",""value"":""Quantum Coder Coat""},{""trait_type"":""Necklace"",""value"":""None""},{""trait_type"":""Mouth"",""value"":""Smile""},{""trait_type"":""Eyewear"",""value"":""None""},{""trait_type"":""Head"",""value"":""Scientist Hair Black""},{""trait_type"":""X Power"",""value"":""None""}],""alternateSource"":{""image"":""https://ipfs.filebase.io/ipfs/Qmc2o8xUCHFwJiFZa7Ta9Wn2NP72ZoYQGC1RJiVAkV6edv""},""website"":""https://www.xaliensnft.com"",""twitter"":""https://twitter.com/xAliensNFT""}";

            _mockHttpFacade.GetData(TestConstants.MetaNormalisedIpfsUrl).Returns(metaJson);

            // Act
            var result = await _classUnderTest.ProcessNFToken(Token);

            // Assert
            Assert.That(result, Is.EqualTo("ipfs://Qmc2o8xUCHFwJiFZa7Ta9Wn2NP72ZoYQGC1RJiVAkV6edv"));
        }
    }
}
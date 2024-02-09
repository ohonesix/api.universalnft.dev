using UniversalNFT.dev.API.Services.IPFS;

namespace UniversalNFT.dev.API.Tests.Services.IPFS
{
    public class IPFSServiceTests
    {
        [TestCase(TestConstants.IpfsRawWithFile, TestConstants.NormalisedIpfsUrlWithFile)]
        [TestCase(TestConstants.IpfsWithFile, TestConstants.NormalisedIpfsUrlWithFile)]
        [TestCase(TestConstants.IpfsDoubleWithFile, TestConstants.NormalisedIpfsUrlWithFile)]
        public void GivenIpfsWithFile_ReturnsLinkWithFile(
            string input,
            string expected)
        {
            // Arrange

            // Act
            var result = IPFSService.NormaliseUrl(input);

            // Assert
            Assert.That(result, Is.EqualTo(expected));
        }

        [TestCase(TestConstants.IpfsRaw, TestConstants.NormalisedIpfsUrl)]
        [TestCase(TestConstants.Ipfs, TestConstants.NormalisedIpfsUrl)]
        public void GivenIpfs_ReturnsLink(
            string input,
            string expected)
        {
            // Arrange

            // Act
            var result = IPFSService.NormaliseUrl(input);

            // Assert
            Assert.That(result, Is.EqualTo(expected));
        }

        [TestCase(TestConstants.DirectImageUrl, TestConstants.DirectImageUrl)]
        [TestCase(TestConstants.NormalisedIpfsUrlWithFile, TestConstants.NormalisedIpfsUrlWithFile)]
        [TestCase(TestConstants.NormalisedIpfsUrl, TestConstants.NormalisedIpfsUrl)]
        public void GivenNormalisedUrl_ReturnsNormalisedLink(
            string input,
            string expected)
        {
            // Arrange

            // Act
            var result = IPFSService.NormaliseUrl(input);

            // Assert
            Assert.That(result, Is.EqualTo(expected));
        }
    }
}

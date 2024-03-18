using Microsoft.Extensions.Options;
using NSubstitute;
using UniversalNFT.dev.API.Models.DTO;
using UniversalNFT.dev.API.Services.AppSettings;
using UniversalNFT.dev.API.Services.Images;
using UniversalNFT.dev.API.Services.NFT;
using UniversalNFT.dev.API.Services.Rules;
using UniversalNFT.dev.API.Services.XRPL;

namespace UniversalNFT.dev.API.Tests.Services.NFT
{
    [TestFixture]
    public class NFTServiceTests
    {
        public required IXRPLService _mockXrplService;
        public required IRulesEngine _mockRulesEngine;
        public required IImageService _mockImageService;
        public required IOptions<ServerSettings> _mockOptions;
        public NFTService _nftService;

        [SetUp]
        public void SetUp()
        {
            // Create mock instances
            _mockXrplService = Substitute.For<IXRPLService>();
            _mockRulesEngine = Substitute.For<IRulesEngine>();
            _mockImageService = Substitute.For<IImageService>();
            _mockOptions = Options.Create(new ServerSettings
            {
                ServerExternalDomain = "https://api.universalnft.dev"
            });

            // Construct the service with the mocks
            _nftService = new NFTService(
                _mockXrplService,
                _mockRulesEngine,
                _mockImageService,
                _mockOptions);
        }

        [Test]
        public async Task GetNFT_WhenCalled_ReturnsValidNFTResponse()
        {
            // Arrange
            var nfTokenID = "SomeNfTokenId";
            var ownerWalletAddress = "SomeWalletAddress";
            var responseNFT = new RippledAccountNFToken();
            var expectedImageUrl = "http://ipfs.io/someimageurl";
            var expectedThumbnailFilename = "thumbnail.jpg";

            _mockXrplService.GetNFT(nfTokenID, ownerWalletAddress)
                .Returns(responseNFT);
            _mockRulesEngine.ProcessNFToken(responseNFT)
                .Returns(expectedImageUrl);
            _mockImageService.CreateThumbnail(expectedImageUrl, nfTokenID)
                .Returns(expectedThumbnailFilename);

            // Act
            var result = await _nftService.GetNFT(nfTokenID, ownerWalletAddress);

            // Assert
            Assert.IsNotNull(result);
            Assert.That(result.NFTokenID, Is.EqualTo(nfTokenID));
            Assert.That(result.OwnerAccount, Is.EqualTo(ownerWalletAddress));
            Assert.That(result.ImageThumbnailCacheUrl, Is.EqualTo($"https://api.universalnft.dev/v1.0/Image?file={expectedThumbnailFilename}"));
            Assert.That(result.ImageUrl, Is.EqualTo(expectedImageUrl));
        }

        [Test]
        public async Task GetNFT_WhenNFTokenDoesNotExist_ReturnsNull()
        {
            // Arrange
            var nfTokenID = "SomeNfTokenId";
            var ownerWalletAddress = "SomeWalletAddress";

            _mockXrplService.GetNFT(nfTokenID, ownerWalletAddress)
                .Returns(Task.FromResult<RippledAccountNFToken>(null));

            // Act
            var result = await _nftService.GetNFT(nfTokenID, ownerWalletAddress);

            // Assert
            Assert.IsNull(result);
        }

        [Test]
        public async Task GetGetArtv0_WhenCalled_ReturnsValidNFTResponse()
        {
            // Arrange
            var nfTokenID = "SomeNfTokenId";
            var ownerWalletAddress = "SomeWalletAddress";
            var responseNFT = new RippledAccountNFToken();
            var expectedImageUrl = "http://ipfs.io/someimageurl";

            _mockXrplService.GetNFT(nfTokenID, ownerWalletAddress)
                .Returns(responseNFT);
            _mockRulesEngine.ProcessNFToken(responseNFT)
                .Returns(expectedImageUrl);

            // Act
            var result = await _nftService.GetNFT(nfTokenID, ownerWalletAddress);

            // Assert
            Assert.IsNotNull(result);
            Assert.That(result.NFTokenID, Is.EqualTo(nfTokenID));
            Assert.That(result.OwnerAccount, Is.EqualTo(ownerWalletAddress));
            Assert.That(result.ImageUrl, Is.EqualTo(expectedImageUrl));
        }

        [Test]
        public async Task GetArtv0_WhenNFTokenDoesNotExist_ReturnsNull()
        {
            // Arrange
            var nfTokenID = "SomeNfTokenId";
            var ownerWalletAddress = "SomeWalletAddress";

            _mockXrplService.GetNFT(nfTokenID, ownerWalletAddress)
                .Returns(Task.FromResult<RippledAccountNFToken>(null));

            // Act
            var result = await _nftService.GetArtv0(nfTokenID, ownerWalletAddress);

            // Assert
            Assert.IsNull(result);
        }
    }
}

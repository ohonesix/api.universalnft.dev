using UniversalNFT.dev.API.Models.API;
using UniversalNFT.dev.API.Services.Images;
using UniversalNFT.dev.API.Services.IPFS;
using UniversalNFT.dev.API.Services.Rules;
using UniversalNFT.dev.API.Services.XRPL;

namespace UniversalNFT.dev.API.Services.NFT
{
    public class NFTService : INFTService
    {
        private readonly IXRPLService _xrplService;
        private readonly IRulesEngine _rulesEngine;
        private readonly IImageService _imageService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public NFTService(
            IXRPLService xrplService,
            IRulesEngine rulesEngine,
            IImageService imageService,
            IHttpContextAccessor httpContextAccessor
        )
        {
            _xrplService = xrplService;
            _rulesEngine = rulesEngine;
            _imageService = imageService;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<UniversalNFTResponseV1> GetNFT(
            string NFTokenID,
            string OwnerWalletAddress)
        {
            // Load the NFT from XRPL
            var responseNFToken = await _xrplService.GetNFT(NFTokenID, OwnerWalletAddress);
            if (responseNFToken == null)
                return null;

            // Download and extract the media URL
            var imageUrl = await _rulesEngine.ProcessNFToken(responseNFToken);
            if (string.IsNullOrWhiteSpace(imageUrl))
                return null;

            if (imageUrl.StartsWith("ipfs://"))
                imageUrl = IPFSService.NormaliseIPFSUrl(imageUrl);

            // We have an imageUrl extracted! Create the thumbnail if it doesn't exist
            // in our cache.
            var thumbnailFilename = await _imageService.CreateThumbnail(imageUrl, NFTokenID);

            return new UniversalNFTResponseV1
            {
                NFTokenID = NFTokenID,
                OwnerAccount = OwnerWalletAddress,
                ImageThumbnailCacheUrl = !string.IsNullOrWhiteSpace(thumbnailFilename)
                    ? $"https://{_httpContextAccessor.HttpContext.Request.Host}/v1.0/Image?file={thumbnailFilename}"
                    : string.Empty,
                ImageUrl = imageUrl,
                Timestamp = DateTime.UtcNow.ToString("O")
            };
        }
    }
}

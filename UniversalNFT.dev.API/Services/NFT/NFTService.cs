using Microsoft.Extensions.Options;
using UniversalNFT.dev.API.Models.API;
using UniversalNFT.dev.API.Services.AppSettings;
using UniversalNFT.dev.API.Services.Images;
using UniversalNFT.dev.API.Services.IPFS;
using UniversalNFT.dev.API.Services.Rules;
using UniversalNFT.dev.API.Services.XRPL;

namespace UniversalNFT.dev.API.Services.NFT
{
    public class NFTService : INFTService
    {
        private readonly ServerSettings _serverSettings;
        private readonly IXRPLService _xrplService;
        private readonly IRulesEngine _rulesEngine;
        private readonly IImageService _imageService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public NFTService(
            IXRPLService xrplService,
            IRulesEngine rulesEngine,
            IImageService imageService,
            IHttpContextAccessor httpContextAccessor,
            IOptions<ServerSettings> serverSettings)
        {
            _xrplService = xrplService;
            _rulesEngine = rulesEngine;
            _imageService = imageService;
            _httpContextAccessor = httpContextAccessor;
            _serverSettings = serverSettings.Value;
        }

        public async Task<UniversalNFTResponseV1> GetNFT(
            string NFTokenID,
            string OwnerWalletAddress)
        {
            try
            {
                // Load the NFT from XRPL
                var responseNFToken = await _xrplService.GetNFT(NFTokenID, OwnerWalletAddress);
                if (responseNFToken == null)
                    return null;

                // Download and extract the media URL
                var imageUrl = await _rulesEngine.ProcessNFToken(responseNFToken);
                if (string.IsNullOrWhiteSpace(imageUrl))
                    return null;

                imageUrl = IPFSService.NormaliseUrl(imageUrl);

                // We have an imageUrl extracted! Create the thumbnail if it doesn't exist
                // in our cache.
                var thumbnailFilename = await _imageService.CreateThumbnail(imageUrl, NFTokenID);

                return new UniversalNFTResponseV1
                {
                    NFTokenID = NFTokenID,
                    OwnerAccount = OwnerWalletAddress,
                    ImageThumbnailCacheUrl = !string.IsNullOrWhiteSpace(thumbnailFilename)
                        ? $"{_serverSettings.ServerExternalDomain}/v1.0/Image?file={thumbnailFilename}"
                        : string.Empty,
                    ImageUrl = imageUrl,
                    Timestamp = DateTime.UtcNow.ToString("O")
                };
            }
            catch (Exception ex)
            {
                // Log it if you care
            }

            return null;
        }
    }
}

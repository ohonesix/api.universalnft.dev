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

        public NFTService(
            IXRPLService xrplService,
            IRulesEngine rulesEngine,
            IImageService imageService,
            IOptions<ServerSettings> serverSettings)
        {
            _xrplService = xrplService;
            _rulesEngine = rulesEngine;
            _imageService = imageService;
            _serverSettings = serverSettings.Value;
        }

        /// <summary>
        /// Load a specific NFT from a given wallet and return it in our format
        /// </summary>
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

        /// <summary>
        /// Load all NFTs in a wallet and process them into our format
        /// </summary>
        public async Task<IEnumerable<UniversalNFTResponseV1>> GetAllNFTs(string OwnerWalletAddress)
        {
            var accountNftsResult = new List<UniversalNFTResponseV1>();
            try
            {
                // Load the NFT from XRPL
                var accountNfts = await _xrplService.GetAllNFTs(OwnerWalletAddress);
                if (accountNfts?.Any() != true)
                    return Enumerable.Empty<UniversalNFTResponseV1>();

                foreach (var accountNft in accountNfts)
                {
                    var imageUrl = await _rulesEngine.ProcessNFToken(accountNft) ?? string.Empty;
                    imageUrl = IPFSService.NormaliseUrl(imageUrl);

                    // We have an imageUrl extracted! Create the thumbnail if it doesn't exist
                    // in our cache.
                    var thumbnailFilename = await _imageService.CreateThumbnail(imageUrl, accountNft.NFTokenID);

                    accountNftsResult.Add(new UniversalNFTResponseV1
                    {
                        NFTokenID = accountNft.NFTokenID,
                        OwnerAccount = OwnerWalletAddress,
                        ImageThumbnailCacheUrl = !string.IsNullOrWhiteSpace(thumbnailFilename)
                        ? $"{_serverSettings.ServerExternalDomain}/v1.0/Image?file={thumbnailFilename}"
                        : string.Empty,
                        ImageUrl = imageUrl,
                        Timestamp = DateTime.UtcNow.ToString("O")
                    });
                }

                return accountNftsResult;
            }
            catch (Exception ex)
            {
                // Log it if you care
            }

            return Enumerable.Empty<UniversalNFTResponseV1>();
        }

        /// <summary>
        /// Load a specific NFT from a given wallet and return it in art.v0 format
        /// </summary>
        public async Task<Artv0Response> GetArtv0(
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

                return new Artv0Response
                {
                    name = NFTokenID,
                    image = imageUrl,
                    description = $"Converted with {_serverSettings.ServerExternalDomain} at {DateTime.UtcNow.ToString("O")}"
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

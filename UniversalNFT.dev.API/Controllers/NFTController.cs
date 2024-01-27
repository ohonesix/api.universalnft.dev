using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using UniversalNFT.dev.API.Models.API;
using UniversalNFT.dev.API.Services.Images;
using UniversalNFT.dev.API.Services.Rules;
using UniversalNFT.dev.API.Services.XRPL;

namespace UniversalNFT.dev.API.Controllers
{
    [ApiController]
    [Route("v1.0/NFT")]
    public class NFTController : ControllerBase
    {
        private readonly XRPLService _xrplService;
        private readonly IRulesEngine _rulesEngine;
        private readonly ImageService _imageService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public NFTController(
            XRPLService xrplService,
            IRulesEngine rulesEngine,
            ImageService imageService,
            IHttpContextAccessor httpContextAccessor)
        {
            _xrplService = xrplService;
            _rulesEngine = rulesEngine;
            _imageService = imageService;
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpGet]
        [SwaggerResponse(200, "The NFT is loaded and thumbnail cached sucessfully", typeof(UniversalNFTResponseV1))]
        [SwaggerResponse(404, "The NFT could not be loaded or has no associated image URL")]
        public async Task<IActionResult> Get(
            [SwaggerParameter("The NFTokenID value stored on the XRPL", Required = true)] string NFTokenID,
            [SwaggerParameter("The XRPL wallet address that owns this NFT", Required = true)] string OwnerWalletAddress)
        {
            // Load the NFT from XRPL
            var responseNFToken = await _xrplService.GetNFT(NFTokenID, OwnerWalletAddress);
            if (responseNFToken == null)
                return NotFound();

            // Download and extract the media URL
            var imageUrl = await _rulesEngine.ProcessNFToken(responseNFToken);
            if (string.IsNullOrWhiteSpace(imageUrl))
                return NotFound();

            // We have an imageUrl extracted! Create the thumbnail if it doesn't exist
            // in our cache.
            var thumbnailFilename = await _imageService.CreateThumbnail(imageUrl, NFTokenID);

            var responseModel = new UniversalNFTResponseV1
            {
                NFTokenID = NFTokenID,
                OwnerAccount = OwnerWalletAddress,
                ImageThumbnailCacheUrl = !string.IsNullOrWhiteSpace(thumbnailFilename)
                    ? $"https://{_httpContextAccessor.HttpContext.Request.Host}/v1.0/Image?file={thumbnailFilename}"
                    : string.Empty,
                ImageUrl = imageUrl,
                Timestamp = DateTime.UtcNow.ToString("O")
            };

            return new JsonResult(responseModel);
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using UniversalNFT.dev.API.Models.API;
using UniversalNFT.dev.API.Services.NFT;
using UniversalNFT.dev.API.SwaggerConfig;

namespace UniversalNFT.dev.API.Controllers
{
    /// <summary>
    /// Load and translate metadata for a specfic NFToken on the blockchain and return it in the specified format.
    /// </summary>
    [ApiController]
    [Route("v1.0/NFT")]
    public class NFTController : ControllerBase
    {
        private readonly INFTService _nftService;

        public NFTController(INFTService nftService)
        {
            _nftService = nftService;
        }

        /// <summary>
        /// Return the image URL in our UniversalNFTResponseV1 format.
        /// </summary>
        [HttpGet]
        [SwaggerResponse(200, "The NFT is loaded and thumbnail cached sucessfully", typeof(UniversalNFTResponseV1))]
        [SwaggerResponse(404, "The NFT could not be loaded or has no associated image URL")]
        public async Task<IActionResult> Get(
            [SwaggerParameter("The NFTokenID value stored on the XRPL", Required = true)]
            [SwaggerTryItOutDefaultValue("000803E8CEC1EB1B331D8A55E39D451DE8E13F59CF5509D5FA17E45000000527")]
                string NFTokenID,
            [SwaggerParameter("The XRPL wallet address that owns this NFT", Required = true)]
            [SwaggerTryItOutDefaultValue("rPpMSFxzjqJ6AGgEZ8kgbQeeo6UJvUkVmb")]
                string OwnerWalletAddress)
        {
            var response = await _nftService.GetNFT(NFTokenID, OwnerWalletAddress);

            if (response == null)
                return NotFound();

            return new JsonResult(response);
        }

        /// <summary>
        /// Return the image URL in XLS-24d "art.v0" format.
        /// </summary>
        [HttpGet("art.v0")]
        [SwaggerResponse(200, "The NFT is loaded and thumbnail cached sucessfully", typeof(Artv0Response))]
        [SwaggerResponse(404, "The NFT could not be loaded or has no associated image URL")]
        public async Task<IActionResult> GetAsArtv0(
            [SwaggerParameter("The NFTokenID value stored on the XRPL", Required = true)]
            [SwaggerTryItOutDefaultValue("000803E8CEC1EB1B331D8A55E39D451DE8E13F59CF5509D5FA17E45000000527")]
                string NFTokenID,
            [SwaggerParameter("The XRPL wallet address that owns this NFT", Required = true)]
            [SwaggerTryItOutDefaultValue("rPpMSFxzjqJ6AGgEZ8kgbQeeo6UJvUkVmb")]
                string OwnerWalletAddress)
        {
            var response = await _nftService.GetArtv0(NFTokenID, OwnerWalletAddress);

            if (response == null)
                return NotFound();

            return new JsonResult(response);
        }
    }
}

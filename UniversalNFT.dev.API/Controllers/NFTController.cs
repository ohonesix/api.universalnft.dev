using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using UniversalNFT.dev.API.Models.API;
using UniversalNFT.dev.API.Services.NFT;

namespace UniversalNFT.dev.API.Controllers
{
    [ApiController]
    [Route("v1.0/NFT")]
    public class NFTController : ControllerBase
    {
        private readonly INFTService _nftService;

        public NFTController(INFTService nftService)
        {
            _nftService = nftService;
        }

        [HttpGet]
        [SwaggerResponse(200, "The NFT is loaded and thumbnail cached sucessfully", typeof(UniversalNFTResponseV1))]
        [SwaggerResponse(404, "The NFT could not be loaded or has no associated image URL")]
        public async Task<IActionResult> Get(
            [SwaggerParameter("The NFTokenID value stored on the XRPL", Required = true)] string NFTokenID,
            [SwaggerParameter("The XRPL wallet address that owns this NFT", Required = true)] string OwnerWalletAddress)
        {
            var response = await _nftService.GetNFT(NFTokenID, OwnerWalletAddress);

            if (response == null)
                return NotFound();

            return new JsonResult(response);
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using UniversalNFT.dev.API.Models.API;
using UniversalNFT.dev.API.Services.NFT;
using UniversalNFT.dev.API.SwaggerConfig;

namespace UniversalNFT.dev.API.Controllers
{
    /// <summary>
    /// Return all NFTs in a specific wallet in our UniversalNFTResponseV1 format.
    /// </summary>
    [ApiController]
    [Route("v1.0/Wallet")]
    public class WalletController : ControllerBase
    {
        private readonly INFTService _nftService;

        public WalletController(INFTService nftService)
        {
            _nftService = nftService;
        }

        /// <summary>
        /// Return all NFTs in a wallet in our UniversalNFTResponseV1 format.
        /// </summary>
        [HttpGet]
        [SwaggerResponse(200, "The wallet NFTs are loaded and thumbnail cached sucessfully", typeof(IEnumerable<UniversalNFTResponseV1>))]
        [SwaggerResponse(404, "The wallet could not be found")]
        public async Task<IActionResult> Get(
            [SwaggerParameter("The XRPL wallet to load NFTs from", Required = true)]
            [SwaggerTryItOutDefaultValue("rPpMSFxzjqJ6AGgEZ8kgbQeeo6UJvUkVmb")]
        string OwnerWalletAddress)
        {
            var response = await _nftService.GetAllNFTs(OwnerWalletAddress);

            if (response == null)
                return NotFound();

            return new JsonResult(response);
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using UniversalNFT.dev.API.Services.Images;

namespace UniversalNFT.dev.API.Controllers
{
    [ApiController]
    [Route("v1.0/Image")]
    public class ImageController : ControllerBase
    {
        private readonly ImageService _imageService;

        public ImageController(ImageService imageService)
        {
            _imageService = imageService;
        }

        [HttpGet]
        [ApiExplorerSettings(IgnoreApi = true)]
        public async Task<IActionResult> GetThumbnail([SwaggerParameter("The thumbnail image file to load. Use the /NFT endpoint to generate this thumbnail url first.", Required = true)] string file)
        {
            // Get the extension from the URL
            var fileExtension = Path.GetExtension(file);

            // Check if the image already exists locally
            var localImagePath = Path.Combine(_imageService.LocalImagePath, file);
            if (System.IO.File.Exists(localImagePath))
            {
                // If the image already exists, read it from disk and return it
                var imageBytes = await System.IO.File.ReadAllBytesAsync(localImagePath);
                return File(imageBytes, _imageService.GetContentType(fileExtension));
            }

            return NotFound();
        }
    }
}

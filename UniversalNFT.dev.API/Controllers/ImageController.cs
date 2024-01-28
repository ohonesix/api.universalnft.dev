using Microsoft.AspNetCore.Mvc;
using UniversalNFT.dev.API.Services.Images;

namespace UniversalNFT.dev.API.Controllers
{
    [ApiController]
    [Route("v1.0/Image")]
    public class ImageController : ControllerBase
    {
        private readonly IImageService _imageService;

        public ImageController(IImageService imageService)
        {
            _imageService = imageService;
        }

        [HttpGet]
        [ApiExplorerSettings(IgnoreApi = true)]
        public async Task<IActionResult> GetThumbnail(string file)
        {
            // Get the extension from the URL
            var fileExtension = Path.GetExtension(file);

            // Check if the image already exists locally
            var localImagePath = Path.Combine(_imageService.GetLocalImagePath(), file);
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

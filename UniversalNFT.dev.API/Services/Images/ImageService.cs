using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;

namespace UniversalNFT.dev.API.Services.Images;

public class ImageService : IImageService
{
    private readonly HttpClient _httpClient;

    private readonly string LocalImagePath = Path.Combine(Directory.GetCurrentDirectory(), "ImageCache");

    public ImageService()
    {
        // Create local image path if it does not exist
        if (!Directory.Exists(LocalImagePath))
        {
            Directory.CreateDirectory(LocalImagePath);
        }

        _httpClient = new HttpClient();
    }

    public string GetLocalImagePath()
    {
        return LocalImagePath;
    }

    public async Task<string?> CreateThumbnail(string imageUrl, string nfTokenId)
    {
        try
        {
            // Get the extension from the URL and the filename is the ipfs hash
            var fileExtension = Path.GetExtension(imageUrl);
            var fileName = nfTokenId + fileExtension;

            // Check if the image already exists locally
            var localImagePath = Path.Combine(LocalImagePath, fileName);
            if (!System.IO.File.Exists(localImagePath))
            {
                // The image does not exist, download it from the URL
                var imageBytes = await _httpClient.GetByteArrayAsync(imageUrl);

                using (Image image = Image.Load(imageBytes))
                {
                    // Resize it
                    image.Mutate(x => x.Resize(250, 250));

                    // We have to use a MemoryStream to save if we don't have an image extention
                    if (string.IsNullOrWhiteSpace(fileExtension))
                    {
                        // Create a MemoryStream to save the resized image
                        using (var memoryStream = new MemoryStream())
                        {
                            // Save the resized image to the MemoryStream
                            image.Save(memoryStream, new SixLabors.ImageSharp.Formats.Png.PngEncoder());

                            // Reset the position of the MemoryStream to the beginning
                            memoryStream.Seek(0, SeekOrigin.Begin);

                            // We always save as png when we don't have an extension for the thumbnail
                            fileName = fileName + ".png";

                            // Save the resized image data from the MemoryStream to a new image file
                            await File.WriteAllBytesAsync(localImagePath + ".png", memoryStream.ToArray());
                        }
                    }
                    else
                    {
                        // Save the image to the local file system directly as we have an extention
                        await image.SaveAsync(localImagePath);
                    }
                }
            }

            return fileName;
        }
        catch (Exception ex)
        {
            // Log it if you care
        }

        return null;
    }

    public string GetContentType(string fileExtension)
    {
        switch (fileExtension.ToLower())
        {
            case ".jpg":
            case ".jpeg":
                return "image/jpeg";

            case ".png":
                return "image/png";

            case ".gif":
                return "image/gif";

            default:
                return "application/octet-stream";
        }
    }
}

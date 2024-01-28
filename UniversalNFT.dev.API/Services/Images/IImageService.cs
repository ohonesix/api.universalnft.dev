namespace UniversalNFT.dev.API.Services.Images
{
    public interface IImageService
    {
        Task<string?> CreateThumbnail(string imageUrl, string nfTokenId);

        string GetContentType(string fileExtension);

        string GetLocalImagePath();
    }
}
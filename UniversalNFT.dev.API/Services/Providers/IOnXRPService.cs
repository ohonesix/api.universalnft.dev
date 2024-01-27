
namespace UniversalNFT.dev.API.Services.Providers
{
    public interface IOnXRPService
    {
        Task<string?> GetImageFromMetadata(string nfTokenId);
    }
}
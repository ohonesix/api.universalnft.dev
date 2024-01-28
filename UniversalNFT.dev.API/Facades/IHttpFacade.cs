
namespace UniversalNFT.dev.API.Facades
{
    public interface IHttpFacade
    {
        Task<string?> GetData(string url);
    }
}
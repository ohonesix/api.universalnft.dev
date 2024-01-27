using System.Net.Http.Headers;

namespace UniversalNFT.dev.API.Services.Providers;

public class OnXRPService
{
    private readonly HttpClient _httpClient;

    private const string ApiUrl = "https://marketplace-api.onxrp.com/api/metadata/";

    public OnXRPService()
	{
        _httpClient = new HttpClient();
        _httpClient.DefaultRequestHeaders.Accept.Clear();
        _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
    }

    public async Task<string?> GetImageFromMetadata(string nfTokenId)
    {
        var response = await _httpClient.GetAsync(ApiUrl + nfTokenId);
        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadAsStringAsync();
        }

        return null;
    }
}
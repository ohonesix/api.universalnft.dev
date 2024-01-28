namespace UniversalNFT.dev.API.Facades
{
    public class HttpFacade : IHttpFacade
    {
        private readonly HttpClient _httpClient = new HttpClient();

        public async Task<string?> GetData(string url)
        {
            try
            {
                var response = await _httpClient.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsStringAsync();
                }
            }
            catch (Exception) { }

            return null;
        }

    }
}

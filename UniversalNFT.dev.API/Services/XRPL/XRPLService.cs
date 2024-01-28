using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using UniversalNFT.dev.API.Helpers;
using UniversalNFT.dev.API.Models.DTO;
using UniversalNFT.dev.API.Services.IPFS;

namespace UniversalNFT.dev.API.Services.XRPL;

public class XRPLService : IXRPLService
{
    private readonly ILogger<XRPLService> _logger;
    private readonly HttpClient _httpClient;

    private const string _rippledUrl = "wss://xrplcluster.com";

    public XRPLService(ILogger<XRPLService> logger)
    {
        _logger = logger;

        _httpClient = new HttpClient();
        _httpClient.DefaultRequestHeaders.Accept.Clear();
        _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
    }

    public async Task<RippledAccountNFToken?> GetNFT(string tokenID, string ownerAccount)
    {
        try
        {
            // Setup the request body to load account NFTs
            var body = @"{ ""method"": ""account_nfts"",
                            ""params"": [
                                            {
                                                ""account"": """ + ownerAccount + @""",
                                                ""ledger_index"": ""validated"",
                                                ""limit"": 1000
                                            }
                                        ]}";

            // Load account NFTs from Rippled
            var response = await _httpClient.PostAsync(_rippledUrl, new StringContent(body));
            if (response.IsSuccessStatusCode)
            {
                // Parse success response
                var data = await response.Content.ReadAsStringAsync();
                var resultObj = JsonSerializer.Deserialize<RippledAccountNFTsResponse>(data);

                // Check returned NFTs
                if (resultObj?.Result?.NFTs?.Length > 0)
                {
                    // Find specific NFT
                    var foundNft = resultObj.Result.NFTs?.Where(w => w.NFTokenID == tokenID).FirstOrDefault();

                    if (foundNft != null)
                    {
                        if (!string.IsNullOrWhiteSpace(foundNft.URI))
                        {
                            // convert the encoded image URL to something normal
                            var convertedUri = Encoding.UTF8.GetString(HexHelper.StringToByteArray(foundNft.URI));

                            // Normalise it
                            foundNft.URI = IPFSService.NormaliseIPFSUrl(convertedUri);
                        }

                        return foundNft;
                    }
                }
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting NFT in XRPLService");
        }

        return null;
    }
}

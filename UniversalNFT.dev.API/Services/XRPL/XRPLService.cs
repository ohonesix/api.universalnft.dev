using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Options;
using UniversalNFT.dev.API.Helpers;
using UniversalNFT.dev.API.Models.DTO;
using UniversalNFT.dev.API.Services.AppSettings;
using UniversalNFT.dev.API.Services.IPFS;

namespace UniversalNFT.dev.API.Services.XRPL;

public class XRPLService : IXRPLService
{
    private readonly XRPLSettings _xrplSettings;
    private readonly ILogger<XRPLService> _logger;
    private readonly HttpClient _httpClient;

    public XRPLService(IOptions<XRPLSettings> xrplSettings, ILogger<XRPLService> logger)
    {
        _xrplSettings = xrplSettings.Value;
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
            var response = await _httpClient.PostAsync(_xrplSettings.XRPLServerAddress, new StringContent(body));
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
                            foundNft.URI = IPFSService.NormaliseUrl(convertedUri);
                        }

                        return foundNft;
                    }
                }

                // Page if we have to keep going
                var seek = resultObj?.Result?.Marker;
                while (!string.IsNullOrWhiteSpace(seek))
                {
                    body = @"{ ""method"": ""account_nfts"",
                            ""params"": [
                                            {
                                                ""account"": """ + ownerAccount + @""",
                                                ""ledger_index"": ""validated"",
                                                ""limit"": 1000,
                                                ""marker"": """ + seek + @"""
                                            }
                                        ]}";

                    // Lets be kind to the free cluster servers, for better performance
                    // host your own Rippled node and disable with appsettings.json
                    // "XRPLSettings": { "EnableDelay" : false }
                    if (_xrplSettings.EnableDelay)
                        Thread.Sleep(200);

                    var seekResponse = await _httpClient.PostAsync(_xrplSettings.XRPLServerAddress,
                        new StringContent(body));
                    if (seekResponse.IsSuccessStatusCode)
                    {
                        // Parse success response
                        var seekData = await seekResponse.Content.ReadAsStringAsync();
                        var seekResultObj = JsonSerializer.Deserialize<RippledAccountNFTsResponse>(seekData);

                        // Check returned NFTs
                        if (seekResultObj?.Result?.NFTs?.Length > 0)
                        {
                            // Find specific NFT
                            var foundNft = seekResultObj.Result.NFTs?.Where(w => w.NFTokenID == tokenID).FirstOrDefault();

                            if (foundNft != null)
                            {
                                if (!string.IsNullOrWhiteSpace(foundNft.URI))
                                {
                                    // convert the encoded image URL to something normal
                                    var convertedUri = Encoding.UTF8.GetString(HexHelper.StringToByteArray(foundNft.URI));

                                    // Normalise it
                                    foundNft.URI = IPFSService.NormaliseUrl(convertedUri);
                                }

                                return foundNft;
                            }
                        }

                        seek = seekResultObj?.Result?.Marker;
                    }
                    else
                    {
                        // Don't carry on seeking on error
                        seek = null;
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

    public async Task<IEnumerable<RippledAccountNFToken>> GetAllNFTs(string ownerAccount)
    {
        var accountNfts = new List<RippledAccountNFToken>();

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
            var response = await _httpClient.PostAsync(_xrplSettings.XRPLServerAddress, new StringContent(body));
            if (response.IsSuccessStatusCode)
            {
                // Parse success response
                var data = await response.Content.ReadAsStringAsync();
                var resultObj = JsonSerializer.Deserialize<RippledAccountNFTsResponse>(data);

                foreach (var accountNft in resultObj?.Result?.NFTs ?? Enumerable.Empty<RippledAccountNFToken>())
                {
                    if (!string.IsNullOrWhiteSpace(accountNft.URI))
                    {
                        var convertedUri = Encoding.UTF8.GetString(HexHelper.StringToByteArray(accountNft.URI));
                        accountNft.URI = IPFSService.NormaliseUrl(convertedUri);
                    }
                    accountNfts.Add(accountNft);
                }

                // Page if we have to keep going
                var seek = resultObj?.Result?.Marker;
                while (!string.IsNullOrWhiteSpace(seek))
                {
                    body = @"{ ""method"": ""account_nfts"",
                        ""params"": [
                                        {
                                            ""account"": """ + ownerAccount + @""",
                                            ""ledger_index"": ""validated"",
                                            ""limit"": 1000,
                                            ""marker"": """ + seek + @"""
                                        }
                                    ]}";

                    // Lets be kind to the free cluster servers, for better performance
                    // host your own Rippled node and disable with appsettings.json
                    // "XRPLSettings": { "EnableDelay" : false }
                    if (_xrplSettings.EnableDelay)
                        Thread.Sleep(200);

                    var seekResponse = await _httpClient.PostAsync(_xrplSettings.XRPLServerAddress,
                        new StringContent(body));
                    if (seekResponse.IsSuccessStatusCode)
                    {
                        // Parse success response
                        var seekData = await seekResponse.Content.ReadAsStringAsync();
                        var seekResultObj = JsonSerializer.Deserialize<RippledAccountNFTsResponse>(seekData);

                        foreach (var seekAccountNft in seekResultObj?.Result?.NFTs ?? Enumerable.Empty<RippledAccountNFToken>())
                        {
                            if (!string.IsNullOrWhiteSpace(seekAccountNft.URI))
                            {
                                var convertedUri = Encoding.UTF8.GetString(HexHelper.StringToByteArray(seekAccountNft.URI));
                                seekAccountNft.URI = IPFSService.NormaliseUrl(convertedUri);
                            }
                            accountNfts.Add(seekAccountNft);
                        }

                        seek = seekResultObj?.Result?.Marker;
                    }
                    else
                    {
                        // Don't carry on seeking on error
                        seek = null;
                    }
                }
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting NFT in XRPLService");
        }

        return accountNfts;
    }
}

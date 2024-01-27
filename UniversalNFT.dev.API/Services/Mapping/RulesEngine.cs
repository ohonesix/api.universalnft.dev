using System;
using System.Text.RegularExpressions;
using UniversalNFT.dev.API.Models.DTO;
using UniversalNFT.dev.API.Services.IPFS;
using UniversalNFT.dev.API.Services.Providers;

namespace UniversalNFT.dev.API.Services.Mapping;

public class RulesEngine
{
    private readonly HttpClient _httpClient;
    private readonly OnXRPService _onXRPService;

    public RulesEngine(OnXRPService onXRPService)
	{
        _httpClient = new HttpClient();
        _onXRPService = onXRPService;
    }

    public async Task<string?> ProcessNFToken(RippledAccountNFToken nfToken)
    {
        // No URI on chain is most likely OnXRP's (bad) implementation of NFTs
        if (string.IsNullOrWhiteSpace(nfToken?.URI))
        {
            // Load the metadata from their API
            var data = await _onXRPService.GetImageFromMetadata(nfToken.NFTokenID);

            // Extract the image
            var imageFromMetadata = ExtractImageSynonymsUrl(data);
            if (!string.IsNullOrWhiteSpace(imageFromMetadata))
                return IPFSService.NormaliseIPFSUrl(imageFromMetadata);
        } 

        // Check if the on-chain URI is a direct link to the image (yay!)
        var uriAsHttp = ExtractHttpImageUrl(nfToken.URI);
        if (!string.IsNullOrWhiteSpace(uriAsHttp))
            return uriAsHttp;

        var uriAsIpfsFormatted = ExtractIpfsImageUrl(nfToken.URI);
        if (!string.IsNullOrWhiteSpace(uriAsIpfsFormatted))
            return IPFSService.NormaliseIPFSUrl(uriAsIpfsFormatted);

        // No on-chain link to the media directly.
        // Download the URI and see what it is
        var response = await _httpClient.GetAsync(nfToken.URI);
        if (response.IsSuccessStatusCode)
        {
            // Parse success response
            var data = await response.Content.ReadAsStringAsync();

            // Check if we have multiple ipfs urls
            if (CountIpfsUrls(data) > 1)
            {
                // We do, use smarter rules to try find image related fields
                var imageSynonymsUrl = ExtractImageSynonymsUrl(data);
                if (!string.IsNullOrWhiteSpace(imageSynonymsUrl))
                    return IPFSService.NormaliseIPFSUrl(imageSynonymsUrl);
            }

            // Attempt to extract an image url
            var httpImageUrl = ExtractHttpImageUrl(data);
            if (!string.IsNullOrWhiteSpace(httpImageUrl))
                return httpImageUrl;

            var ipfsFormattedImageUrl = ExtractIpfsImageUrl(data);
            if (!string.IsNullOrWhiteSpace(ipfsFormattedImageUrl))
                return IPFSService.NormaliseIPFSUrl(ipfsFormattedImageUrl);
        }

        return null;
    }

    private string ExtractHttpImageUrl(string input)
    {
        string pattern = @"(https?://\S+?\.(?:png|jpe?g|gif))";
        Regex regex = new Regex(pattern, RegexOptions.IgnoreCase);
        Match match = regex.Match(input);
        if (match.Success)
            return match.Value;

        return string.Empty;
    }   

    private string ExtractIpfsImageUrl(string input)
    {
        string pattern = @"(ipfs://\S+?\.(?:png|jpe?g|gif))";
        Regex regex = new Regex(pattern, RegexOptions.IgnoreCase);
        Match match = regex.Match(input);
        if (match.Success)
            return match.Value;

        return string.Empty;
    }

    private string ExtractImageSynonymsUrl(string input)
    {
        string pattern = @"(?:image|picture|image_url)\S+?(ipfs://\S+?\.(?:png|jpe?g|gif)|ipfs://\S+?"")";
        Regex regex = new Regex(pattern);
        Match match = regex.Match(input);
        if (match.Success)
            return match.Groups[1].Value.Contains('"') ? match.Groups[1].Value.Replace("\"", "") : match.Groups[1].Value;

        return string.Empty;
    }

    private int CountIpfsUrls(string input)
    {
        string pattern = @"ipfs://";
        return Regex.Matches(input, pattern)?.Count ?? 0;
    }
}



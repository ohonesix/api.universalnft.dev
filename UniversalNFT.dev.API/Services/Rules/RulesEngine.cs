using System.Text.RegularExpressions;
using UniversalNFT.dev.API.Facades;
using UniversalNFT.dev.API.Models.DTO;
using UniversalNFT.dev.API.Services.Images;
using UniversalNFT.dev.API.Services.IPFS;
using UniversalNFT.dev.API.Services.Providers;

namespace UniversalNFT.dev.API.Services.Rules;

public class RulesEngine : IRulesEngine
{
    private readonly IOnXRPService _onXRPService;
    private readonly IHttpFacade _httpFacade;

    public RulesEngine(
        IOnXRPService onXRPService,
        IHttpFacade httpFacade)
    {
        _onXRPService = onXRPService;
        _httpFacade = httpFacade;
    }

    public async Task<string?> ProcessNFToken(RippledAccountNFToken nfToken)
    {
        // No URI on chain is most likely OnXRP's (bad) implementation of NFTs
        if (string.IsNullOrWhiteSpace(nfToken?.URI))
        {
            // Load the metadata from their API
            var data = await _onXRPService.GetImageFromMetadata(nfToken.NFTokenID);

            // Extract the image
            var imageFromMetadata = ExtractIfpsImageSynonymsUrl(data);
            if (!string.IsNullOrWhiteSpace(imageFromMetadata))
                return imageFromMetadata;

            return null;
        }

        // Check if the on-chain URI is a direct link to the image (yay!)
        var uriAsHttp = ExtractHttpImageUrl(nfToken.URI);
        if (!string.IsNullOrWhiteSpace(uriAsHttp))
            return uriAsHttp;

        var uriAsIpfsFormatted = ExtractIpfsImageUrl(nfToken.URI);
        if (!string.IsNullOrWhiteSpace(uriAsIpfsFormatted))
            return uriAsIpfsFormatted;

        // No on-chain link to the media directly, normalise URI
        var onChainUrl = IPFSService.NormaliseUrl(nfToken.URI);

        // Download the URI and see what it is
        var metadata = await _httpFacade.GetData(onChainUrl);
        if (!string.IsNullOrWhiteSpace(metadata))
        {
            // Check if we have multiple ipfs urls
            if (IPFSService.CountIpfsUrls(metadata) > 1)
            {
                // We do, attempt to extract from JSON
                var imageSynonymsFromJson = JsonImageExtractor.ExtractImageUrl(metadata);
                if (!string.IsNullOrWhiteSpace(imageSynonymsFromJson))
                    return imageSynonymsFromJson;

                // Use smarter rules to try find image related fields
                var imageSynonymsUrl = ExtractIfpsImageSynonymsUrl(metadata);
                if (!string.IsNullOrWhiteSpace(imageSynonymsUrl))
                    return imageSynonymsUrl;
            }

            // Attempt to extract from JSON
            var imageFromJson = JsonImageExtractor.ExtractImageUrl(metadata);
            if (!string.IsNullOrWhiteSpace(imageFromJson))
                return imageFromJson;

            // Attempt to extract an image url
            var httpImageUrl = ExtractHttpImageUrl(metadata);
            if (!string.IsNullOrWhiteSpace(httpImageUrl))
                return httpImageUrl;

            // Attempt to extract formatted Ipfs url
            var ipfsFormattedImageUrl = ExtractIpfsImageUrl(metadata);
            if (!string.IsNullOrWhiteSpace(ipfsFormattedImageUrl))
                return ipfsFormattedImageUrl;
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

    private string ExtractIfpsImageSynonymsUrl(string input)
    {
        string pattern = @"(?:image|picture|image_url)\S+?(ipfs://\S+?\.(?:png|jpe?g|gif)|ipfs://\S+?"")";
        Regex regex = new Regex(pattern);
        Match match = regex.Match(input);
        if (match.Success)
            return match.Groups[1].Value.Contains('"') ? match.Groups[1].Value.Replace("\"", "") : match.Groups[1].Value;

        return string.Empty;
    }
}

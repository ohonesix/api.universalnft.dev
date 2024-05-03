using System.Text.RegularExpressions;

namespace UniversalNFT.dev.API.Services.IPFS;

public static class IPFSService
{
    public static string NormaliseUrl(string url)
    {
        // Is it well formed
        if (url.StartsWith("http"))
            return url;

        // Add ipfs to the start if it doesn't have it
        if (!url.StartsWith("ipfs://"))
            url = "ipfs://" + url;

        // Add /ipfs if it doesn't have it
        if (url.IndexOf("/ipfs/") == -1)
        {
            url = url.Replace("ipfs://", "ipfs://ipfs/");
        }

        // TODO: Ideally we could set the ipfs server the backend uses in appsettings
        url = url.Replace("ipfs://", "https://ipfs.io/");

        return url;
    }

    public static int CountIpfsUrls(string input)
    {
        string pattern = @"ipfs://";
        return Regex.Matches(input, pattern)?.Count ?? 0;
    }
}
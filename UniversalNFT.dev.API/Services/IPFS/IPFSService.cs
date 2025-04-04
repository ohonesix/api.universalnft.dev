﻿﻿﻿using System.Text.RegularExpressions;
using UniversalNFT.dev.API.Services.AppSettings;

namespace UniversalNFT.dev.API.Services.IPFS;

public static class IPFSService
{
    private static string _ipfsServerAddress = "https://ipfs.io/";

    public static void Initialize(IPFSSettings settings)
    {
        _ipfsServerAddress = settings.IPFSServerAddress;
    }

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

        url = url.Replace("ipfs://", _ipfsServerAddress);

        return url;
    }

    public static int CountIpfsUrls(string input)
    {
        string pattern = @"ipfs://";
        return Regex.Matches(input, pattern)?.Count ?? 0;
    }
}

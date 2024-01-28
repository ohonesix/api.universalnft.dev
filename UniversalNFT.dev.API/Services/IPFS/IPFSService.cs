namespace UniversalNFT.dev.API.Services.IPFS;

public static class IPFSService
{
    public static string NormaliseIPFSUrl(string url)
    {
        if (url.StartsWith("ipfs://"))
        {
            if (url.IndexOf("/ipfs/") == -1)
            {
                url = url.Replace("ipfs://", "ipfs://ipfs/");
            }

            url = url.Replace("ipfs://", "https://ipfs.io/");
        }

        return url;
    }
}
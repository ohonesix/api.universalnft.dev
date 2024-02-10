namespace UniversalNFT.dev.API.Services.CacheCleanup
{
    public class CacheFolderWatcherSettings
    {
        public long MaxFolderSizeInBytes { get; set; }

        public double PollingIntervalInMinutes { get; set; }
    }
}

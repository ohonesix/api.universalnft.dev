using Microsoft.Extensions.Options;
using UniversalNFT.dev.API.Services.AppSettings;

namespace UniversalNFT.dev.API.Services.ImageCacheCleanup;

public class ImageCleanupTask : BackgroundService
{
    private readonly TimeSpan _delay;
    private readonly string _localImagePath = Path.Combine(Directory.GetCurrentDirectory(), "ImageCache");
    private readonly long _maxFolderSize;

    public ImageCleanupTask(IOptions<CacheFolderWatcherSettings> settings)
    {
        _maxFolderSize = settings.Value.MaxFolderSizeInBytes;
        _delay = TimeSpan.FromMinutes(settings.Value.PollingIntervalInMinutes);
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            await CleanFolderIfNeeded();
            await Task.Delay(_delay, stoppingToken);
        }
    }

    private async Task CleanFolderIfNeeded()
    {
        try
        {
            var imageCacheDirectory = new DirectoryInfo(_localImagePath);
            while (CalculateFolderSize(imageCacheDirectory) > _maxFolderSize)
            {
                var files = imageCacheDirectory.GetFiles().OrderBy(f => f.LastWriteTime)?.Take(50)?.ToArray();
                if (files != null)
                {
                    foreach (var file in files)
                        file.Delete();
                }
                else
                {
                    // No more files to delete.
                    break;
                }
            }
        }
        catch (Exception ex)
        {
            //_logger.LogError(ex, "An error occurred when trying to clean the ImageCache folder.");
        }
    }

    private long CalculateFolderSize(DirectoryInfo imageCacheDirectory)
    {
        return imageCacheDirectory.GetFiles().Sum(file => file.Length);
    }
}

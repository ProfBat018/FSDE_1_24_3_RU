using Downloader.Services.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YoutubeExplode;

namespace Downloader.Services.Implementations;

class VideoService : IVideoService
{
    private readonly YoutubeClient _client = new();
    public async Task DownloadVideoAsync(string videoUrl, string outputPath)
    {
        var video = await _client.Videos.GetAsync(videoUrl);
        var streamManifest = await _client.Videos.Streams.GetManifestAsync(video.Id);

        var streamInfo = streamManifest
        .GetMuxedStreams()
        .OrderByDescending(s => s.VideoQuality)
        .First();

        await _client.Videos.Streams.DownloadAsync(streamInfo, outputPath);
    }
}

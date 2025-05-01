using Downloader.Services.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YoutubeExplode;
using YoutubeExplode.Videos.Streams;

namespace Downloader.Services.Implementations;

class AudioService : IAudioService
{
    private readonly YoutubeClient _client = new();
    public async Task DownloadAudioAsync(string videoUrl, string outputPath)
    {
        var video = await _client.Videos.GetAsync(videoUrl);
        var streamManifest = await _client.Videos.Streams.GetManifestAsync(video.Id);

        var audioStreamInfo = streamManifest
            .GetAudioOnlyStreams().GetWithHighestBitrate();

        if (audioStreamInfo != null)
        {
            await _client.Videos.Streams.DownloadAsync(audioStreamInfo, outputPath);
        }
    }
}

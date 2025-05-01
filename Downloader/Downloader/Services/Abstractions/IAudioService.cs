using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Downloader.Services.Abstractions;

interface IAudioService
{
    public Task DownloadAudioAsync(string videoUrl, string outputPath);
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Downloader.Services.Abstractions;

interface IVideoService
{
    public Task DownloadVideoAsync(string videoUrl, string outputPath);
}

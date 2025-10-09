using ImageService.Application.Interfaces;
using Microsoft.Extensions.Configuration;
using Minio;
using Minio.DataModel.Args;

namespace ImageService.Application.Classes;


public class MinioService : IMinioService
{
    private readonly IMinioClient _client;
    private const string Bucket = "images";

    public MinioService(IConfiguration config)
    {
        _client = new MinioClient()
            .WithEndpoint(config["Minio:Endpoint"]!)
            .WithCredentials(config["Minio:AccessKey"]!, config["Minio:SecretKey"]!)
            .Build();

        EnsureBucket().GetAwaiter().GetResult();
    }

    private async Task EnsureBucket()
    {
        var exists = await _client.BucketExistsAsync(new BucketExistsArgs().WithBucket(Bucket));
        if (!exists)
        {
            await _client.MakeBucketAsync(new MakeBucketArgs().WithBucket(Bucket));
        }
    }

    public async Task<string> UploadAsync(Stream fileStream, string fileName, string contentType)
    {
        var args = new PutObjectArgs()
            .WithBucket(Bucket)
            .WithObject(fileName)
            .WithStreamData(fileStream)
            .WithObjectSize(fileStream.Length)
            .WithContentType(contentType);

        await _client.PutObjectAsync(args);

        return $"{Bucket}/{fileName}";
    }

    public async Task DeleteAsync(string fileName)
    {
        await _client.RemoveObjectAsync(new RemoveObjectArgs().WithBucket(Bucket).WithObject(fileName));
    }
}
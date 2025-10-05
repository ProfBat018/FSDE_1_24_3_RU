using DocumentService.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Minio;
using Minio.DataModel.Args;

namespace DocumentService.Infrastructure.Storage;

public class MinioDocumentStorageService : IDocumentStorageService
{
    private readonly IMinioClient _minioClient;
    private readonly string _bucketName;
    private readonly ILogger<MinioDocumentStorageService> _logger;

    public MinioDocumentStorageService(IConfiguration config, ILogger<MinioDocumentStorageService> logger)
    {
        _bucketName = config["Minio:Bucket"] ?? "documents";
        _logger = logger;

        _minioClient = new MinioClient()
            .WithEndpoint(config["Minio:Endpoint"] ?? "localhost:9000")
            .WithCredentials(
                config["Minio:AccessKey"] ?? "minioadmin",
                config["Minio:SecretKey"] ?? "minioadmin")
            .Build();
    }

    public async Task<(string StoredFileName, string InternalUrl)> SaveAsync(IFormFile file, CancellationToken cancellationToken = default)
    {
        var extension = Path.GetExtension(file.FileName);
        var storedFileName = $"{Guid.NewGuid()}{extension}";

        await using var stream = file.OpenReadStream();

        var contentType = file.ContentType ?? "application/octet-stream";

        await _minioClient.PutObjectAsync(new PutObjectArgs()
                .WithBucket(_bucketName)
                .WithObject(storedFileName)
                .WithStreamData(stream)
                .WithObjectSize(file.Length)
                .WithContentType(contentType),
            cancellationToken);

        _logger.LogInformation("Saved file to MinIO: {File}", storedFileName);

        var internalUrl = $"minio://{_bucketName}/{storedFileName}";
        return (storedFileName, internalUrl);
    }
}
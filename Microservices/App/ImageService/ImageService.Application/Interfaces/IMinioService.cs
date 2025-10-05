namespace ImageService.Application.Interfaces;

public interface IMinioService
{
    Task<string> UploadAsync(Stream fileStream, string fileName, string contentType);
    Task DeleteAsync(string fileName);
}

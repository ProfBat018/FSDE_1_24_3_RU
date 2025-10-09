using System.Net.Mime;
using AutoMapper;
using ImageService.Application.Interfaces;
using ImageService.Contracts.DTOs;
using ImageService.Data;
using ImageService.Data.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using ImageService.Contracts.Response;


using AutoMapper;
using ImageService.Application.Interfaces;
using ImageService.Contracts.DTOs;
using ImageService.Data;
using ImageService.Data.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;


namespace ImageService.Application.Classes;


public class StoredImageService : IStoredImageService
{
    private readonly ImageDbContext _context;
    private readonly IMapper _mapper;
    private readonly IMinioService _minioService;

    public StoredImageService(ImageDbContext context, IMapper mapper, IMinioService minioService)
    {
        _context = context;
        _mapper = mapper;
        _minioService = minioService;
    }

    public async Task<Result<StoredImageDto>> UploadAsync(IFormFile file)
    {
        if (file == null || file.Length == 0)
            return Result<StoredImageDto>.Error("File is empty", 400);

        var ext = Path.GetExtension(file.FileName);
        var id = Guid.NewGuid();
        var fileName = $"{id}{ext}";

        using var stream = file.OpenReadStream();
        var objectPath = await _minioService.UploadAsync(stream, fileName, file.ContentType);

        var image = new StoredImage
        {
            Id = id,
            ImagePath = objectPath, 
            Extension = ext,
            SizeInBytes = file.Length,
            UploadedAt = DateTime.UtcNow
        };

        _context.Images.Add(image);
        await _context.SaveChangesAsync();

        return Result<StoredImageDto>.Success(_mapper.Map<StoredImageDto>(image));
    }

    public async Task<Result<StoredImageDto>> GetByIdAsync(Guid id)
    {
        var image = await _context.Images.FindAsync(id);
        if (image == null)
            return Result<StoredImageDto>.Error("Image not found", 404);

        return Result<StoredImageDto>.Success(_mapper.Map<StoredImageDto>(image));
    }

    public async Task<Result<string>> DeleteAsync(Guid id)
    {
        var image = await _context.Images.FindAsync(id);
        if (image == null)
            return Result<string>.Error("Image not found", 404);

        await _minioService.DeleteAsync(image.ImagePath);
        _context.Images.Remove(image);
        await _context.SaveChangesAsync();

        return Result<string>.Success("Image deleted");
    }
}

using Microsoft.AspNetCore.Mvc;
using ImageService.Application.Interfaces;
using ImageService.Contracts.DTOs;
using ImageService.Contracts.Response;

namespace ImageService.API.Controllers;

[ApiController]
[Route("api/images")]
public class ImageController : ControllerBase
{
    private readonly IStoredImageService _imageService;

    public ImageController(IStoredImageService imageService)
    {
        _imageService = imageService;
    }

    [HttpPost]
    public async Task<ActionResult<Result<StoredImageDto>>> Upload([FromForm] UploadImageDto dto)
    {
        var result = await _imageService.UploadAsync(dto.File);
        return StatusCode(result.IsSuccess ? 201 : result.ErrorCode ?? 400, result);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Result<StoredImageDto>>> Get(Guid id)
    {
        var result = await _imageService.GetByIdAsync(id);
        return StatusCode(result.IsSuccess ? 200 : result.ErrorCode ?? 404, result);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<Result<string>>> Delete(Guid id)
    {
        var result = await _imageService.DeleteAsync(id);
        return StatusCode(result.IsSuccess ? 200 : result.ErrorCode ?? 404, result);
    }
}

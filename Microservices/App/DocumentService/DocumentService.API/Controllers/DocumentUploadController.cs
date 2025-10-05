using DocumentService.Application.Classes;
using DocumentService.Contracts.DTOs;
using DocumentService.Contracts.Result;
using Microsoft.AspNetCore.Mvc;

namespace DocumentService.API.Controllers;

[ApiController]
[Route("api/documents")]
public class DocumentUploadController : ControllerBase
{
    private readonly DocumentUploadService _uploadService;

    public DocumentUploadController(DocumentUploadService uploadService)
    {
        _uploadService = uploadService;
    }

    [HttpPost("upload")]
    public async Task<ActionResult<Result<Guid>>> Upload([FromForm] DocumentUploadDto dto, CancellationToken ct)
    {
        var result = await _uploadService.UploadAsync(dto, ct);
        return result.IsSuccess ? Ok(result) : StatusCode(500, result);
    }
}
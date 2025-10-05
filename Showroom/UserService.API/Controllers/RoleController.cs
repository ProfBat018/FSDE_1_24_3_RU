using Microsoft.AspNetCore.Mvc;
using UserService.API.DTOs.Requests;
using UserService.API.DTOs.Response;
using UserService.API.Services.Interfaces;

namespace UserService.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RoleController : ControllerBase
{
    private readonly IRoleService _roleService;

    public RoleController(IRoleService roleService)
    {
        _roleService = roleService;
    }

    [HttpGet("All/{page}/{pageSize}")]
    public async Task<IActionResult> GetAllRolesAsync(int page=1, int pageSize=15) 
        => Ok(await _roleService.GetAllRolesAsync(page, pageSize));
    

    [HttpGet("{id}")]
    public async Task<IActionResult> GetRoleByIdAsync(string id) 
        => Ok(await _roleService.GetRoleByIdAsync(id));
    

    [HttpPost("Upsert")]
    public async Task<IActionResult> UpsertRoleAsync(UpsertRoleRequestDTO request)
        => Ok(await _roleService.UpsertRoleAsync(request));
    

    [HttpDelete("Remove")]
    public async Task<IActionResult> RemoveRoleAsync(string id)
        => Ok(await _roleService.RemoveRoleAsync(id));
    
}
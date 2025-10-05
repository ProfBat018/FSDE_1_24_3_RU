using System.Text.Json;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using UserService.Application.Interfaces;
using UserService.Contracts.Response;
using UserService.Contracts.DTOs;
using UserService.Data;
using UserService.Data.Entities;

namespace UserService.Application.Classes;

public class UserService : IUserService
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;
    private readonly IEventPublisher _eventPublisher;
    
    public UserService(AppDbContext context, IMapper mapper, IEventPublisher eventPublisher)
    {
        _context = context;
        _mapper = mapper;
        _eventPublisher = eventPublisher;
    }

    public async Task<Result<IEnumerable<UserDto>>> GetAllAsync()
    {
        var users = await _context.Users.AsNoTracking().ToListAsync();
        var dtos = _mapper.Map<IEnumerable<UserDto>>(users);
        return Result<IEnumerable<UserDto>>.Success(dtos);
    }

    public async Task<Result<UserDto>> GetByIdAsync(string id)
    {
        var user = await _context.Users.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
        if (user == null)
            return Result<UserDto>.Error("User not found", 404);

        var dto = _mapper.Map<UserDto>(user);
        return Result<UserDto>.Success(dto);
    }

    public async Task<Result<UserDto>> CreateAsync(CreateUserDto dto)
    {
        if (await _context.Users.AnyAsync(x => x.Id == dto.Id))
            return Result<UserDto>.Error("User with this ID already exists", 409);

        if (await _context.Users.AnyAsync(x => x.Email == dto.Email))
            return Result<UserDto>.Error("User with this email already exists", 409);

        var user = _mapper.Map<User>(dto);
        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        await _eventPublisher.PublishAsync("user.created", new AuditEventDto
        {
            EventType = "UserCreated",
            Source = "UserService",
            Target = user.Id,
            Description = "New user registered.",
            Metadata = JsonSerializer.Serialize(user),
            CreatedAt = DateTime.UtcNow
        });

        
        var result = _mapper.Map<UserDto>(user);
        return Result<UserDto>.Success(result);
    }

    public async Task<Result<UserDto>> UpdateAsync(string id, CreateUserDto dto)
    {
        if (id != dto.Id)
            return Result<UserDto>.Error("ID mismatch", 400);

        var user = await _context.Users.FindAsync(id);
        if (user == null)
            return Result<UserDto>.Error("User not found", 404);

        user.Name = dto.Name;
        user.Surname = dto.Surname;
        user.ImageId = dto.ImageId;

        _context.Users.Update(user);
        
        await _context.SaveChangesAsync();

        await _eventPublisher.PublishAsync("user.updated", new AuditEventDto
        {
            EventType = "UserUpdated",
            Source = "UserService",
            Target = user.Id,
            Description = "user Updated.",
            Metadata = JsonSerializer.Serialize(user),
            CreatedAt = DateTime.UtcNow
        });
        
        var updated = _mapper.Map<UserDto>(user);
        return Result<UserDto>.Success(updated);
    }

    public async Task<Result<string>> DeleteAsync(string id)
    {
        var user = await _context.Users.FindAsync(id);
        if (user == null)
            return Result<string>.Error("User not found", 404);

        _context.Users.Remove(user);
        await _context.SaveChangesAsync();

        await _eventPublisher.PublishAsync("user.deleted", new AuditEventDto
        {
            EventType = "Userdeleted",
            Source = "UserService",
            Target = user.Id,
            Description = "user deleted.",
            Metadata = JsonSerializer.Serialize(user),
            CreatedAt = DateTime.UtcNow
        });
        return Result<string>.Success("User deleted");
    }

 

    public async Task<Result<UserDto>> GetByEmailAsync(string email)
    {
        var user = await _context.Users
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Email == email);

        if (user == null)
            return Result<UserDto>.Error("User not found", 404);

        var dto = _mapper.Map<UserDto>(user);
        return Result<UserDto>.Success(dto);
    }
    
}

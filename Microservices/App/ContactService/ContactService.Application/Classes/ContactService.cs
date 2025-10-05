
using System.Text.Json;
using AutoMapper;
using ContactService.Application.Classes;
using ContactService.Application.Interfaces;
using ContactService.Contracts.DTOs;
using ContactService.Contracts.Response;
using ContactService.Data;
using ContactService.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace ContactService.Application.Services;

public class ContactService : IContactService
{
    private readonly ContactDbContext _context;
    private readonly IMapper _mapper;
    private readonly IEventPublisher _eventPublisher;
    public ContactService(ContactDbContext context, IMapper mapper, IEventPublisher eventPublisher)
    {
        _context = context;
        _mapper = mapper;
        _eventPublisher = eventPublisher;
    }

    public async Task<Result<ContactDto>> GetByUserIdAsync(string userId)
    {
        var contact = await _context.Contacts.AsNoTracking().FirstOrDefaultAsync(x => x.UserId == userId);
        if (contact == null)
            return Result<ContactDto>.Error("Contact not found", 404);

        return Result<ContactDto>.Success(_mapper.Map<ContactDto>(contact));
    }

    public async Task<Result<ContactDto>> CreateOrUpdateAsync(string userId, UpsertContactDto dto)
    {
        var contact = await _context.Contacts.FindAsync(userId);

        if (contact == null)
        {
            contact = new Contact
            {
                UserId = userId,
                PhoneNumber = dto.PhoneNumber,
                Email = dto.Email,
                PhoneVerified = false,
                EmailVerified = false
            };
            _context.Contacts.Add(contact);
        }
        else
        {
            contact.PhoneNumber = dto.PhoneNumber;
            contact.Email = dto.Email;
            _context.Contacts.Update(contact);
        }

        await _context.SaveChangesAsync();
        await _eventPublisher.PublishAsync("contact.upserted", new AuditEventDto
        {
            EventType = "ContactUpserted",
            Source = "ContactService",
            Target = userId,
            Description = "contact upserted.",
            Metadata = JsonSerializer.Serialize(contact),
            CreatedAt = DateTime.UtcNow
        });
        return Result<ContactDto>.Success(_mapper.Map<ContactDto>(contact));
    }

    public async Task<Result<string>> DeleteAsync(string userId)
    {
        var contact = await _context.Contacts.FindAsync(userId);
        if (contact == null)
            return Result<string>.Error("Contact not found", 404);

        _context.Contacts.Remove(contact);
        await _context.SaveChangesAsync();
        await _eventPublisher.PublishAsync("contact.deleted", new AuditEventDto
        {
            EventType = "ContactDeleted",
            Source = "ContactService",
            Target = userId,
            Description = "contact deleted.",
            Metadata = JsonSerializer.Serialize(contact),
            CreatedAt = DateTime.UtcNow
        });
        return Result<string>.Success("Contact deleted");
    }

    public async Task<Result<string>> VerifyPhoneAsync(string userId)
    {
        var contact = await _context.Contacts.FindAsync(userId);
        if (contact == null)
            return Result<string>.Error("Contact not found", 404);

        contact.PhoneVerified = true;
        await _context.SaveChangesAsync();
        await _eventPublisher.PublishAsync("contact.verified", new AuditEventDto
        {
            EventType = "ContactVerified",
            Source = "ContactService",
            Target = userId,
            Description = "contact phone verified.",
            Metadata = JsonSerializer.Serialize(contact),
            CreatedAt = DateTime.UtcNow
        });
        return Result<string>.Success("Phone verified");
    }

    public async Task<Result<string>> VerifyEmailAsync(string userId)
    {
        var contact = await _context.Contacts.FindAsync(userId);
        if (contact == null)
            return Result<string>.Error("Contact not found", 404);

        contact.EmailVerified = true;
        await _context.SaveChangesAsync();
        await _eventPublisher.PublishAsync("contact.verified", new AuditEventDto
        {
            EventType = "ContactVerified",
            Source = "ContactService",
            Target = userId,
            Description = "contact email verified.",
            Metadata = JsonSerializer.Serialize(contact),
            CreatedAt = DateTime.UtcNow
        });
        return Result<string>.Success("Email verified");
    }
}
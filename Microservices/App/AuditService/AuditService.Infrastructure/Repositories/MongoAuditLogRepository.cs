using AuditService.Application.Interfaces;
using AuditService.Contracts.DTOs;
using AuditService.Data.Entities;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace AuditService.Infrastructure.Repositories;

public class MongoAuditLogRepository : IAuditLogRepository
{
    private readonly IMongoCollection<AuditLog> _collection;

    public MongoAuditLogRepository(IConfiguration config)
    {
        var client = new MongoClient(config.GetConnectionString("MongoDb"));
        var db = client.GetDatabase(config["MongoSettings:Database"] ?? "AuditDb");
        _collection = db.GetCollection<AuditLog>("AuditLogs");
    }

    public async Task SaveAsync(AuditEventDto dto, CancellationToken cancellationToken = default)
    {
        var entity = new AuditLog
        {
            Id = Guid.NewGuid(),
            EventType = dto.EventType,
            Source = dto.Source,
            Target = dto.Target,
            Description = dto.Description,
            Metadata = dto.Metadata, 
            CreatedAt = dto.CreatedAt
        };

        await _collection.InsertOneAsync(entity, cancellationToken: cancellationToken);
    }
}
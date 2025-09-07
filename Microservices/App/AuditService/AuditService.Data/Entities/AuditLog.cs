using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace AuditService.Data.Entities;

public class AuditLog
{
    [BsonRepresentation(BsonType.String)]
    public Guid Id { get; set; }
    public string EventType { get; set; } = default!;
    public string Source { get; set; } = default!;
    public string Target { get; set; } = default!;
    public string? Description { get; set; }
    public string Metadata { get; set; }

    public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;
}
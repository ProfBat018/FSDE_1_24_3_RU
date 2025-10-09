namespace AuditService.Contracts.DTOs;

public record AuditEventDto(
    string EventType,
    string Source,
    string Target,
    string? Description,
    string Metadata,
    DateTimeOffset CreatedAt
);
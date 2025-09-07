namespace ContactService.Contracts.DTOs;

public record AuditEventDto
{
    public string EventType;
    public string Source;
    public string Target;
    public  string? Description;
    public string Metadata;
    public   DateTimeOffset CreatedAt;
}
using AuditService.Contracts.DTOs;

namespace AuditService.Application.Interfaces;

public interface IAuditLogRepository
{
    Task SaveAsync(AuditEventDto auditEvent, CancellationToken cancellationToken = default);
}
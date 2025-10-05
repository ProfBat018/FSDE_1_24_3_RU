namespace AuthApi.Core.DTOs.Request;

public record UpsertRoleRequestDTO(string Name, string? Id =null);
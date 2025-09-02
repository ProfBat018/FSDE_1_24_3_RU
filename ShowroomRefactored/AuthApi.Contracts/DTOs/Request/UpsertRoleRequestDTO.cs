namespace AuthApi.Contracts.DTOs.Request;

public record UpsertRoleRequestDTO(string Name, string? Id =null);
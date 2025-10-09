namespace UserService.API.DTOs.Requests;

// Свойтсво Name используется как Name и как NewName     
public record UpsertRoleRequestDTO(string Name, string? Id =null);

    // Name и Id - для обновления и в Name мы пишем новое имя 
    // Name - только Имя для добавления, потому что у него пока нет Id 
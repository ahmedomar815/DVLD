namespace Api.Dtos.Roles.Responses;

public sealed record RoleDetailsDto(string Id, string Name, bool IsDeleted, IEnumerable<string> Permissions);

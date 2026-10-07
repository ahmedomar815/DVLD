namespace Api.Dtos.Roles.Requests;

public sealed record RoleDto(
    [property: Required, MinLength(1)] string Name,
    IEnumerable<string>? Permissions);

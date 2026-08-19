namespace DVLD.Errors;

public record RoleErrors
{
    public static Error DuplicateName => new Error("Role.DuplicateName", "A Applicaton Role with this name already exists.", StatusCodes.Status409Conflict);
    public static Error InvalidPermissions => new Error("Role.InvalidPermissions", "InvalidPermissions", StatusCodes.Status400BadRequest);
    public static Error RoleNotFound => new Error("Role.NotFound", "The Role Not Found", StatusCodes.Status404NotFound);
}

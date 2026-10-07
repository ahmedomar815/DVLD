namespace Api.Filters;
public sealed class PermissionRequirment(string permission) : IAuthorizationRequirement
{
    public string Permission { get; } = permission;
}

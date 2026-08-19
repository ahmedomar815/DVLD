using Microsoft.AspNetCore.Authorization;

namespace DVLD.Authentication.Filters;

public sealed class PermissionRequirment(string permission) : IAuthorizationRequirement
{
    public string Permission { get; } = permission;
}

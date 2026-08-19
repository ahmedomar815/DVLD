using Microsoft.AspNetCore.Authorization;

namespace DVLD.Authentication.Filters;

public sealed class HasPermissionAttribute : AuthorizeAttribute
{
    public HasPermissionAttribute(string permission)
    {
        Policy = $"{PermissionAuthorizationPolicyProvider.PolicyPrefix}{permission}";
    }

}

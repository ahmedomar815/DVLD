using DVLD.Abstractions.Consts;
using Microsoft.AspNetCore.Authorization;

namespace DVLD.Authentication.Filters;

public class PermissionAuthorizationHandler : AuthorizationHandler<PermissionRequirment>
{
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionRequirment requirement)
    {
        var hasPermission = context.User.Claims.Any(x => x.Value == requirement.Permission && x.Type == Permissions.Type);
        if (!hasPermission) return Task.CompletedTask;
        context.Succeed(requirement);

        return Task.CompletedTask;
    }
}

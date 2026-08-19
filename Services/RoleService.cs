using DVLD.Contracts.ApplicationRole;
using Mapster;
using Microsoft.AspNetCore.Identity;
using Org.BouncyCastle.Asn1.Ocsp;

namespace DVLD.Services;

public class RoleService(RoleManager<ApplicationRole> roleManager, ApplicationDbContext context) : IRoleService
{
    private readonly RoleManager<ApplicationRole> _roleManager = roleManager;
    private readonly ApplicationDbContext _context = context;


    public async Task<IEnumerable<RoleResponse>> GetAll(CancellationToken cancellationToken)
    {
        return await _roleManager.Roles.Where(x => !x.IsDeleted).ProjectToType<RoleResponse>().ToListAsync(cancellationToken);
    }
    public async Task<Result<RoleDetailsResponse>>GetRole(string rollId,CancellationToken cancellationToken)
    {
        if (await _roleManager.FindByIdAsync(rollId) is not { } role)
            return Result.Failure<RoleDetailsResponse>(RoleErrors.RoleNotFound);
        var permission = await _roleManager.GetClaimsAsync(role);
        var response = new RoleDetailsResponse(role.Id, role.Name!, role.IsDeleted, permission.Select(x=>x.Value));
        return Result.Success<RoleDetailsResponse>(response);
    }

     public async Task<Result<RoleDetailsResponse>>CreateAsync(RoleRequest request,CancellationToken cancellationToken)
     {
        var roleIsExist = await _roleManager.RoleExistsAsync(request.Name);
        if (roleIsExist)
            return Result.Failure<RoleDetailsResponse>(RoleErrors.DuplicateName);
        var allowedPermissions = Permissions.GetAll();
        if( request.Permissions.Except(allowedPermissions).Any())
        return Result.Failure<RoleDetailsResponse>(RoleErrors.InvalidPermissions);
        var role = new ApplicationRole
        {
            Name = request.Name,
            ConcurrencyStamp = Guid.CreateVersion7().ToString(),
        };
        var result = await _roleManager.CreateAsync(role);
        if(result.Succeeded)
        {
            var permissions = request.Permissions.Select(x => new IdentityRoleClaim<string> { ClaimType = Permissions.Type, ClaimValue = x, RoleId = role.Id });
            await _context.AddRangeAsync(permissions);
            await _context.SaveChangesAsync();
            var response = new RoleDetailsResponse(role.Id, role.Name, role.IsDeleted, permissions.Select(x => x.ClaimValue!));
            return Result.Success(response);

        }
        var error = result.Errors.First();
        return Result.Failure<RoleDetailsResponse>(new Error(error.Code, error.Description, StatusCodes.Status400BadRequest));
    }

    public async Task<Result>UpdateAsync(string rollId,RoleRequest request)
    {
        if (await _roleManager.FindByIdAsync(rollId) is not { } role)
            return Result.Failure(RoleErrors.RoleNotFound);
        var roleIsExist = await _roleManager.Roles.AnyAsync(x => x.Name == rollId && x.Id != rollId);
        if (roleIsExist)
            return Result.Failure(RoleErrors.DuplicateName);
        var allowedPermissions = Permissions.GetAll();
        if (request.Permissions.Except(allowedPermissions).Any())
            return Result.Failure(RoleErrors.InvalidPermissions);


        role.Name = request.Name;
        var result = await _roleManager.UpdateAsync(role);
        if(result.Succeeded)
        {
            var currentPermissions=await _context.RoleClaims.Where
                (x=>x.RoleId == role.Id).Select(x=>x.ClaimValue!).ToListAsync();

            var newPermission = request.Permissions.Except(currentPermissions)
                .Select(x => new IdentityRoleClaim<string>
                { ClaimType = Permissions.Type, ClaimValue = x, RoleId = role.Id });

            var removePermission = currentPermissions.Except(request.Permissions);
            await _context.RoleClaims
               .Where(x => x.RoleId == rollId && removePermission.Contains(x.ClaimValue))
               .ExecuteDeleteAsync();
            await _context.AddRangeAsync(newPermission);
            await _context.SaveChangesAsync();
            return Result.Success();
        }
        var error = result.Errors.First();
        return Result.Failure<RoleDetailsResponse>(new Error(error.Code, error.Description, StatusCodes.Status400BadRequest));
    }
    public async Task<Result> ToggleStatusAsync(string rollId)
    {
        if (await _roleManager.FindByIdAsync(rollId) is not { } role)
            return Result.Failure(RoleErrors.RoleNotFound);

        role.IsDeleted = !role.IsDeleted;
        await _roleManager.UpdateAsync(role);
        return Result.Success();
    }
}

using DVLD.Contracts.ApplicationRole;

namespace DVLD.Services;

public interface IRoleService
{
    Task<IEnumerable<RoleResponse>> GetAll(CancellationToken cancellationToken);
    Task<Result<RoleDetailsResponse>> GetRole(string roleId, CancellationToken cancellationToken);
    Task<Result<RoleDetailsResponse>> CreateAsync(RoleRequest request, CancellationToken cancellationToken);
    Task<Result> UpdateAsync(string roleId, RoleRequest request);
    Task<Result> ToggleStatusAsync(string roleId);
}

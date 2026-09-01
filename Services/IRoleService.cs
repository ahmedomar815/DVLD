using DVLD.Contracts.ApplicationRole;

namespace DVLD.Services;

public interface IRoleService
{
    Task<IEnumerable<RoleResponse>> GetAllAsync(CancellationToken cancellationToken);
    Task<Result<RoleDetailsResponse>> GetAsync(string roleId, CancellationToken cancellationToken);
    Task<Result<RoleDetailsResponse>> CreateAsync(RoleRequest request, CancellationToken cancellationToken);
    Task<Result> UpdateAsync(string roleId, RoleRequest request, CancellationToken cancellationToken);
    Task<Result> ToggleStatusAsync(string roleId, CancellationToken cancellationToken);
}

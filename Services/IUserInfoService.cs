using DVLD.Contracts.User;

namespace DVLD.Services;

public interface IUserInfoService
{
    Task<Result<UserResponse>> GetInfoAsync(string userId, CancellationToken cancellationToken);
    Task<Result> ChangePasswordAsync(string currentPassword, string newPassword, CancellationToken cancellationToken);
}

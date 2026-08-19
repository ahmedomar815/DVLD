using DVLD.Contracts.User;

namespace DVLD.Services;

public interface IUserService

{
    Task<Result> CreateAsync(UserRequest request, CancellationToken cancellationToken);
    Task<Result<UserResponse>> GetAsync(string userId, CancellationToken cancellationToken);
    Task<Result<UserResponse>> UpdateAsync(string userId, UserRequest request, CancellationToken cancellationToken);

}

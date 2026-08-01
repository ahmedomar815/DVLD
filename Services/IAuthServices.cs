using DVLD.Contracts.Authentication;

namespace DVLD.Services;

public interface IAuthServices
{
    Task<Result<AuthResponse>> GetTokenAsync(string Email, string Password, CancellationToken cancellationToken);
    Task<Result> RevokeRefreshTokenAsync(string accessToken, string refreshToken, CancellationToken cancellationToken = default);
    Task<Result<AuthResponse>> RefreshTokenAsync(string accessToken, string refreshToken, CancellationToken cancellationToken = default);
}

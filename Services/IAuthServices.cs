using DVLD.Contracts.Authentication;

namespace DVLD.Services;

public interface IAuthServices
{
    Task<Result<LoginResponse>> GetTokenAsync(string Email, string Password, CancellationToken cancellationToken);
    Task<Result> RevokeRefreshTokensync(string accessToken, string refrshToken, CancellationToken cancellationToken);
    Task<Result<RefreshTokenResponse>> GetRefreshTokenAsync(string accessToken, string refreshToken, CancellationToken cancellationToken = default);
}

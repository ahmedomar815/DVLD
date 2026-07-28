using DVLD.Auth;
using DVLD.Contracts.Authentication;
using DVLD.Persistence;
using System.Security.Cryptography;

namespace DVLD.Services;

public class AuthServices(ApplicationDbContext context,IJwtProvider jwtProvider
    ):IAuthServices
{
    
    private readonly ApplicationDbContext _context = context;
    private readonly IJwtProvider _jwtProvider = jwtProvider;
    private readonly int _refreshTokenExpiryDays = 30;

    public async Task<Result<LoginResponse>> GetTokenAsync(string Email, string Password,CancellationToken cancellationToken)
    {
        

        if (await _context.Users.FirstOrDefaultAsync(x => x.Email == Email&&x.Password==Password,cancellationToken) is not { } user)
        {
            return Result.Failure<LoginResponse>(UserErrors.InvalidCredentials);
        }
        
        
            var (Token,ExpressIn) = _jwtProvider.GenerateToken(user);
            var token = GenerateRefreshToken();
            var response = new LoginResponse(user.FirstName, user.SecondName,user.ThirdName,user.FourthName
                , user.Email!, user.Id,Token, ExpressIn, token);
            
            var refreshToken = new RefreshToken { Token = token ,ExpiresOn = DateTime.UtcNow.AddDays(7), };
             user.RefreshTokens.Add(refreshToken);
           await _context.SaveChangesAsync();
            return Result.Success(response);
       

    }
    public async Task<Result> RevokeRefreshTokensync(string accessToken, string refrshToken, CancellationToken cancellationToken = default)
    {
        var userId = _jwtProvider.ValidateToken(accessToken);
        if (userId is null) return Result.Failure(UserErrors.InvalidRefreshToken);

        var user = await _context.Users
            .Include(u => u.RefreshTokens.Where(rt => rt.Token == refrshToken && rt.IsActive))
            .FirstOrDefaultAsync(u => u.Id == userId, cancellationToken);

        var userRefreshToken = user?.RefreshTokens.FirstOrDefault();
        if (userRefreshToken is null) return Result.Failure(UserErrors.InvalidRefreshToken);

        userRefreshToken.RevokedOn = DateTime.UtcNow;
        await _context.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
    public async Task<Result<RefreshTokenResponse>> GetRefreshTokenAsync(string accessToken, string refreshToken, CancellationToken cancellationToken = default)
    {
        var userId = _jwtProvider.GetUserIdFromExpiredToken(accessToken);
        if (userId is null) return Result.Failure<RefreshTokenResponse>(UserErrors.InvalidRefreshToken);
        var user = await _context.Users
        .Include(x => x.RefreshTokens.Where(rt => rt.Token == refreshToken))
        .FirstOrDefaultAsync(x => x.Id == userId, cancellationToken);
        if (user is null) return Result.Failure<RefreshTokenResponse>(UserErrors.InvalidRefreshToken);
        var userrefreshToken = user!.RefreshTokens.FirstOrDefault(x=>x.IsActive);
        if (userrefreshToken is null) return Result.Failure<RefreshTokenResponse>(UserErrors.InvalidRefreshToken );
        userrefreshToken.RevokedOn = DateTime.UtcNow;
        var token = _jwtProvider.GenerateToken(user!);
        var stringToken = GenerateRefreshToken();
        var refreshTokenEntity = new RefreshToken
        {
            Token = stringToken,
            ExpiresOn = DateTime.UtcNow.AddDays(_refreshTokenExpiryDays),
        };
        var response= new RefreshTokenResponse(token.Token, token.ExpressIn, stringToken);
        user.RefreshTokens.Add(refreshTokenEntity);
       await _context.SaveChangesAsync(cancellationToken);
        return Result.Success<RefreshTokenResponse>(response);
    }
    private static string GenerateRefreshToken()
    {
        return Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));
    }

    
}

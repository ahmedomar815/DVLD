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
        

        if (await _context.Users.FirstOrDefaultAsync(x => x.Email == Email,cancellationToken) is not { } user)
        {
            return Result.Failure<LoginResponse>(UserErrors.InvalidCredentials);
        }
        var result = await _context.Users.AnyAsync(x => x.Password == Password);
        if (result)
        {
            var (Token,ExpressIn) = _jwtProvider.GenerateToken(user);
            var token = GenerateRefreshToken();
            var response = new LoginResponse(user.FirstName, user.SecondName,user.ThirdName,user.FourthName
                , user.Email!, user.Id,Token, ExpressIn, token);
            
            var refreshToken = new RefreshToken { Token = token ,ExpiresOn = DateTime.UtcNow.AddDays(7), };
             user.RefreshTokens.Add(refreshToken);
           await _context.SaveChangesAsync();
            return Result.Success(response);
        }
        return Result.Failure<LoginResponse>(UserErrors.InvalidCredentials);


    }
    public async Task<Result> RevokeRefreshTokensync(string accessToken , string refrshToken, CancellationToken cancellationToken=default)
    {
        var userId = _jwtProvider.ValidateToken(accessToken);
        if (userId is null) return Result.Failure(UserErrors.InvalidCredentials);
        var user= await _context.Users.Include(x=>x.RefreshTokens).FirstOrDefaultAsync(x=>x.Id==userId, cancellationToken);
        if (userId is null) return Result.Failure(UserErrors.InvalidCredentials );
        var userrefreshToken =  user!.RefreshTokens.FirstOrDefault(x => x.Token == refrshToken&&x.IsActive);
        if (userrefreshToken is null) return Result.Failure(UserErrors.InvalidCredentials with { Description="refresh Token already revoked"});
        userrefreshToken.RevokedOn = DateTime.UtcNow;
        await _context.SaveChangesAsync();
        return Result.Success();
    }
    public async Task<Result<RefreshTokenResponse>> GetRefreshTokenAsync(string accessToken, string refreshToken, CancellationToken cancellationToken = default)
    {
        var userId = _jwtProvider.ValidateToken(accessToken);
        if (userId is null) return Result.Failure<RefreshTokenResponse>(UserErrors.InvalidCredentials);
        var user = await _context.Users.Include(x => x.RefreshTokens).FirstOrDefaultAsync(x => x.Id == userId, cancellationToken);
        if (userId is null) return Result.Failure<RefreshTokenResponse>(UserErrors.InvalidCredentials);
        var userrefreshToken = user!.RefreshTokens.FirstOrDefault(x => x.Token == refreshToken && x.IsActive);
        if (userrefreshToken is null) return Result.Failure<RefreshTokenResponse>(UserErrors.InvalidCredentials with { Description="refreshToken is not active"});
        var token = _jwtProvider.GenerateToken(user!);
        var stringToken = GenerateRefreshToken();
        var refreshTokenEntity = new RefreshToken
        {
            Token = stringToken,
            ExpiresOn = DateTime.UtcNow.AddDays(_refreshTokenExpiryDays),
        };
        var response= new RefreshTokenResponse(token.Token, token.ExpressIn, stringToken);
        user.RefreshTokens.Add(refreshTokenEntity);
       await _context.SaveChangesAsync();
        return Result.Success(response);
    }
    private static string GenerateRefreshToken()
    {
        return Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));
    }

    
}

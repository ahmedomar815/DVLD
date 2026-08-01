using DVLD.Auth;
using DVLD.Contracts.Authentication;
using Microsoft.AspNetCore.Identity;
using System.Security.Cryptography;

namespace DVLD.Services;

public class AuthServices(ApplicationDbContext context
    ,IJwtProvider jwtProvider
    ,UserManager<ApplicationUser> userManager
    ,SignInManager<ApplicationUser> signInManager 
    ) :IAuthServices
{
    
    private readonly ApplicationDbContext _context = context;
    private readonly IJwtProvider _jwtProvider = jwtProvider;
    private readonly UserManager<ApplicationUser> _userManager = userManager;
    private readonly SignInManager<ApplicationUser> _signInManager = signInManager;
    private readonly int _refreshTokenExpiryDays = 30;

    public async Task<Result<AuthResponse>> GetTokenAsync(string email, string password, CancellationToken cancellationToken)
    {


        var user = await _userManager.FindByEmailAsync(email);
        if (user is null)
            return Result.Failure<AuthResponse>(UserErrors.InvalidCredentials); 
        if(user.IsDisabled)
            return Result.Failure<AuthResponse>(UserErrors.UserDisabled);

        var result = await _signInManager.PasswordSignInAsync(
            user, password, isPersistent: false, lockoutOnFailure: true);

        if (result.IsLockedOut)
            return Result.Failure<AuthResponse>(UserErrors.UserLockedout);

        if (!result.Succeeded)
            return Result.Failure<AuthResponse>(UserErrors.InvalidCredentials);

        var (Token, ExpressIn) = _jwtProvider.GenerateToken(user);
        var refreshTokenValue = GenerateRefreshToken();

        var refreshToken = new RefreshToken
        {
            Token = refreshTokenValue,
            ExpiresOn = DateTime.UtcNow.AddDays(7),
            ApplicationUserId=user.Id
        };
        user.RefreshTokens.Add(refreshToken);
    
        await _context.SaveChangesAsync();

        var response = new AuthResponse(
            user.FirstName, user.SecondName, user.ThirdName, user.FourthName,
            user.Email!, user.Id, Token, ExpressIn, refreshTokenValue);

        return Result.Success(response);


    }
    public async Task<Result> RevokeRefreshTokenAsync(string accessToken, string refreshToken, CancellationToken cancellationToken = default)
     {
    var userId = _jwtProvider.ValidateToken(accessToken);
    if (userId is null) 
        return Result.Failure(UserErrors.InvalidRefreshToken);

    var storedToken = await _context.RefreshTokens
        .FirstOrDefaultAsync(rt => 
            rt.Token == refreshToken && 
            rt.ApplicationUserId == userId &&
            rt.RevokedOn == null && 
            rt.ExpiresOn > DateTime.UtcNow, 
            cancellationToken);

    if (storedToken is null) 
        return Result.Failure(UserErrors.InvalidRefreshToken);

    storedToken.RevokedOn = DateTime.UtcNow;
    await _context.SaveChangesAsync(cancellationToken);

    return Result.Success();
    }
    public async Task<Result<AuthResponse>> RefreshTokenAsync(string accessToken, string refreshToken, CancellationToken cancellationToken = default)
    {
        var userId = _jwtProvider.ValidateToken(accessToken);
        if (userId is null) return Result.Failure<AuthResponse>(UserErrors.InvalidRefreshToken);
        var user = await _userManager.FindByIdAsync(userId);
        if (user is null) return Result.Failure<AuthResponse>(UserErrors.InvalidCredentials);
        if (user.IsDisabled)
            return Result.Failure<AuthResponse>(UserErrors.UserDisabled);
        if (user.LockoutEnd > DateTime.UtcNow)
            return Result.Failure<AuthResponse>(UserErrors.UserLockedout);
        var userrefreshToken = _context.RefreshTokens.FirstOrDefault(x => x.Token == refreshToken && x.IsActive);
        if (userrefreshToken is null) return Result.Failure<AuthResponse>(UserErrors.InvalidRefreshToken);
        userrefreshToken.RevokedOn = DateTime.UtcNow;
        var (newAccessToken, ExpressIn) = _jwtProvider.GenerateToken(user);
        var newRefreshToken = GenerateRefreshToken();
        var refreshTokenExiration = DateTime.UtcNow.AddDays(_refreshTokenExpiryDays);
        user.RefreshTokens.Add(new RefreshToken { Token = newRefreshToken, ExpiresOn = refreshTokenExiration });
        await _userManager.UpdateAsync(user);
        var response = new AuthResponse(
            user.FirstName, user.SecondName, user.ThirdName, user.FourthName,
            user.Email!, user.Id, newAccessToken, ExpressIn, newRefreshToken);

        return Result.Success<AuthResponse>(response);
    }
    private static string GenerateRefreshToken()
    {
        return Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));
    }

   
}

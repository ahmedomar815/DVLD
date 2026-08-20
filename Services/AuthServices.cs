using DVLD.Auth;
using DVLD.Contracts.Authentication;
using Microsoft.AspNetCore.Identity;
using Org.BouncyCastle.Tls.Crypto.Impl;
using System.Security.Cryptography;

namespace DVLD.Services;

public class AuthServices(ApplicationDbContext context
    ,IJwtProvider jwtProvider
    ,UserManager<ApplicationUser> userManager
    ,SignInManager<ApplicationUser> signInManager 
    , RoleManager<ApplicationRole> roleManager
    ) :IAuthServices
{
    
    private readonly ApplicationDbContext _context = context;
    private readonly IJwtProvider _jwtProvider = jwtProvider;
    private readonly UserManager<ApplicationUser> _userManager = userManager;
    private readonly RoleManager<ApplicationRole> _roleManager = roleManager;
    private readonly SignInManager<ApplicationUser> _signInManager = signInManager;
    private readonly int _refreshTokenExpiryDays = 30;

    public async Task<Result<AuthResponse>> GetTokenAsync(
        string email,
        string password,
        CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByEmailAsync(email);

        if (user is null)
            return Result.Failure<AuthResponse>(
                UserErrors.InvalidCredentials);

        if (user.IsDisabled)
            return Result.Failure<AuthResponse>(
                UserErrors.UserDisabled);

        var result = await _signInManager.PasswordSignInAsync(
            user,
            password,
            isPersistent: false,
            lockoutOnFailure: true);

        if (!result.Succeeded)
        {
            if (result.IsLockedOut)
                return Result.Failure<AuthResponse>(
                    UserErrors.UserLockedout);

            return Result.Failure<AuthResponse>(
                UserErrors.InvalidCredentials);
        }

        var (userRoles, userPermissions) =
            await GetUserRolesAndPermissions(
                user,
                cancellationToken);

        var (token, expiresIn) =
            _jwtProvider.GenerateToken(
                user,
                userRoles,
                userPermissions);

        var refreshTokenValue = GenerateRefreshToken();

        var refreshToken = new RefreshToken
        {
            Token = refreshTokenValue,
            ExpiresOn = DateTime.UtcNow.AddDays(_refreshTokenExpiryDays),
            ApplicationUserId = user.Id
        };

        user.RefreshTokens.Add(refreshToken);

        var updateResult = await _userManager.UpdateAsync(user);

        

        var response = new AuthResponse(
            user.FirstName,
            user.SecondName,
            user.ThirdName,
            user.FourthName,
            user.Email!,
            user.Id,
            token,
            expiresIn,
            refreshTokenValue);

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
        var (userRoles, userPermissions) =
         await GetUserRolesAndPermissions(
             user,
             cancellationToken);
        var userRefreshToken = await _context.RefreshTokens
     .FirstOrDefaultAsync( x => x.Token == refreshToken && 
     x.ApplicationUserId == user.Id &&x.RevokedOn == null && x.ExpiresOn > DateTime.UtcNow,
         cancellationToken);

        if (userRefreshToken is null) return Result.Failure<AuthResponse>(UserErrors.InvalidRefreshToken);
        userRefreshToken.RevokedOn = DateTime.UtcNow;
        var (newAccessToken, ExpressIn) = _jwtProvider.GenerateToken(user,userRoles,userPermissions);
        var newRefreshToken = GenerateRefreshToken();
        var refreshTokenExiration = DateTime.UtcNow.AddDays(_refreshTokenExpiryDays);
        user.RefreshTokens.Add(new RefreshToken { Token = newRefreshToken, ExpiresOn = refreshTokenExiration });
        await _userManager.UpdateAsync(user);
        var response = new AuthResponse(
            user.FirstName, user.SecondName, user.ThirdName, user.FourthName,
            user.Email!, user.Id, newAccessToken, ExpressIn, newRefreshToken);

        return Result.Success<AuthResponse>(response);
    }
    private async Task<(IEnumerable<string>roles,IEnumerable<string>permissions)>GetUserRolesAndPermissions(ApplicationUser user, CancellationToken cancellationToken)
    {
        var userRoles = await _userManager.GetRolesAsync(user);
        var userPermissions = await _context.Roles.Join(_context.RoleClaims, r => r.Id, rc => rc.RoleId, (Role, Claim) => new { Role, Claim })
             .Where(x => userRoles.Contains(x.Role.Name!))
             .Select(x => x.Claim.ClaimValue!)
             .Distinct()
             .ToListAsync(cancellationToken);

        return (userRoles, userPermissions);
    }
    private static string GenerateRefreshToken()
    {
        return Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));
    }

   
}

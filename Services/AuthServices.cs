using DVLD.Auth;
using DVLD.Contracts.Authentication;
using DVLD.Helpers;
using Hangfire;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.WebUtilities;
using Org.BouncyCastle.Tls.Crypto.Impl;
using System.Security.Cryptography;
using System.Text;

namespace DVLD.Services;

public class AuthServices(ApplicationDbContext context
    ,IJwtProvider jwtProvider
    ,UserManager<ApplicationUser> userManager
    ,SignInManager<ApplicationUser> signInManager 
    , RoleManager<ApplicationRole> roleManager
    ,IEmailSender emailSender
    ,IHttpContextAccessor httpContextAccessor
    ) :IAuthServices
{
    
    private readonly ApplicationDbContext _context = context;
    private readonly IJwtProvider _jwtProvider = jwtProvider;
    private readonly UserManager<ApplicationUser> _userManager = userManager;
    private readonly RoleManager<ApplicationRole> _roleManager = roleManager;
    private readonly IEmailSender _emailSender = emailSender;
    private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;
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


    public async Task<Result> ForgetPassword(string email)
    {
        if (await _userManager.FindByEmailAsync(email) is not { } user)
            return Result.Failure(UserErrors.UserNotFound);

        if (user.IsDisabled)
            return Result.Failure(UserErrors.UserDisabled);
        if (await _userManager.IsLockedOutAsync(user))
            return Result.Failure(UserErrors.UserLockedout);
        var code = await _userManager.GeneratePasswordResetTokenAsync(user);
        code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
        await SendEmailForgetPassword(code,user.GetFullName(),email);
        return Result.Success();
    }
    public async Task<Result> ResetPassword(string email, string token, string newPassword)
    {
        if (await _userManager.FindByEmailAsync(email) is not { } user)
            return Result.Failure(UserErrors.UserNotFound);

        string decodedToken;
        try
        {
            decodedToken = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(token));
        }
        catch (FormatException)
        {
            return Result.Failure(UserErrors.InvalidCredentials with { Description = "Token Reset Password is Invalid" });
        }

        var result = await _userManager.ResetPasswordAsync(user, decodedToken, newPassword);

        if (result.Succeeded)
            return Result.Success();

        var error = result.Errors.First();
        return Result.Failure(new Error(error.Code, error.Description, StatusCodes.Status400BadRequest));
    }
    private async Task SendEmailForgetPassword(string code, string name, string email)
    {
        var origin = _httpContextAccessor.HttpContext?.Request.Headers.Origin;
        var placeholderValues = new Dictionary<string, string>
        {
            {"Name",name },
            {"Url",$"{origin}/auth/forgetPassword?userEmail={email}&code={code}" }

        };
        var body = EmailBodyBuilder.GenerateEmailBody("forgot-password-template", placeholderValues);
        BackgroundJob.Enqueue(() => _emailSender.SendEmailAsync(email, "Forget Password ", body));
    }
    private async Task<(IEnumerable<string> roles, IEnumerable<string> permissions)> GetUserRolesAndPermissions(ApplicationUser user, CancellationToken cancellationToken)
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

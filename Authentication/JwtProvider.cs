using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace DVLD.Auth;

public class JwtProvider(IOptions<JwtOptions> options,ILogger<JwtProvider> logger) : IJwtProvider
{
    private readonly JwtOptions _options = options.Value;
    private readonly ILogger<JwtProvider> _logger = logger;

    public (string Token, int ExpressIn) GenerateToken(ApplicationUser user)
    {
        

        Claim[] claims = new Claim[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id),
            new Claim(JwtRegisteredClaimNames.Name,user.FirstName),
            new Claim(JwtRegisteredClaimNames.Email,user.Email!),
            new  Claim(JwtRegisteredClaimNames.FamilyName,user.SecondName),
            new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString())

        };
         var symetricSecurityKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_options.Key));
        
        var singingCredentials = new SigningCredentials(symetricSecurityKey, SecurityAlgorithms.HmacSha256);

        var Token= new JwtSecurityToken(
            issuer: _options.Issuer,
            audience: _options.Audience,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(_options.ExpiryMinutes),
            signingCredentials: singingCredentials
            );
        return (new JwtSecurityTokenHandler().WriteToken(Token), _options.ExpiryMinutes);
    }


    public string? GetUserIdFromExpiredToken(string token)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.Key));

        try
        {
            var principal = tokenHandler.ValidateToken(token, new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = symmetricSecurityKey,
                ValidateIssuer = true,
                ValidIssuer = _options.Issuer,
                ValidateAudience = true,
                ValidAudience = _options.Audience,
                ValidateLifetime = false 
            }, out var validatedToken);

            if (validatedToken is not JwtSecurityToken jwtToken ||
                !jwtToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
            {
                return null;
            }

            return principal.Claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Sub)?.Value;
        }
        catch (SecurityTokenException ex)
        {
            _logger.LogWarning(ex, "Failed to extract claims from expired token");
            return null;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unexpected error while extracting claims from expired token");
            return null;
        }
    }

  

    public string ? ValidateToken (string Token)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var SymetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.Key));
        try
        {
            tokenHandler.ValidateToken(Token, new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = SymetricSecurityKey,
                ValidateIssuer = true,
                ValidIssuer = _options.Issuer,
                ValidateAudience = true,
                ValidAudience = _options.Audience,
                ClockSkew = TimeSpan.Zero,
                ValidateLifetime = true
            }, out SecurityToken validatedToken);
            var JwtToken = (JwtSecurityToken)validatedToken;
            return JwtToken.Claims.First(x => x.Type == JwtRegisteredClaimNames.Sub).Value;
        }
        catch(Exception ex)
        {
            _logger.LogError(ex, "Unexpected error while validating token");
            return null;
        }

    }
}

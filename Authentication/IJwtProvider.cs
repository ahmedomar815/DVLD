namespace DVLD.Auth;

public interface IJwtProvider

{
    (string Token, int ExpressIn) GenerateToken(ApplicationUser user,IEnumerable<string>roles,IEnumerable<string>permissions);
    string? ValidateToken(string Token);
    string? GetUserIdFromExpiredToken(string token);
}


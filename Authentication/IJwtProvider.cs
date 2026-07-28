namespace DVLD.Auth;

public interface IJwtProvider

{
    (string Token, int ExpressIn) GenerateToken(User user);
    string? ValidateToken(string Token);
    string? GetUserIdFromExpiredToken(string token);
}


namespace DVLD.Contracts.Authentication;

public record RefreshTokenResponse(string Token,int ExpireIn,string RefreshToken);


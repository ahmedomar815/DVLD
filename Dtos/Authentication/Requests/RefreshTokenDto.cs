namespace Api.Dtos.Authentication.Requests;

public sealed record RefreshTokenDto(
    [property: Required] string AccessToken,
    [property: Required] string RefreshToken);

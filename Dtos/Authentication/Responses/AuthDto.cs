namespace Api.Dtos.Authentication.Responses;

public sealed record AuthDto(
    string FirstName,
    string SecondName,
    string ThirdName,
    string FourthName,
    string Email,
    string Id,
    string Token,
    int ExpiresIn,
    string RefreshToken);

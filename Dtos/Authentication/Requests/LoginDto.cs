namespace Api.Dtos.Authentication.Requests;

public sealed record LoginDto(
    [property: Required, EmailAddress] string Email,
    [property: Required] string Password);

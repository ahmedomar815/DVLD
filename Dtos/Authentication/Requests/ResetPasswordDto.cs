namespace Api.Dtos.Authentication.Requests;

public sealed record ResetPasswordDto(
    [property: Required, EmailAddress] string Email,
    [property: Required] string Token,
    [property: Required, MinLength(8)] string NewPassword);

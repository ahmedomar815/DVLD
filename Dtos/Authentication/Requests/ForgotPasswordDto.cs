namespace Api.Dtos.Authentication.Requests;

public sealed record ForgotPasswordDto([property: Required, EmailAddress] string Email);

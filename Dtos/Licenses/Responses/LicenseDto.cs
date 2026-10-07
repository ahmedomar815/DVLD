namespace Api.Dtos.Licenses.Responses;
public sealed record LicenseDto(
    string LicenseNumber,
    DateOnly IssueDate,
    DateOnly ExpiryDate,
    string LicenseName,
    string Status);

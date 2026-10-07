namespace Api.Dtos.Applications.Responses;
public sealed record ApplicationDto(
    string Id,
    string Status,
    decimal PaidFees,
    string ApplicationTypeName,
    ApplicationUserDto Applicant);

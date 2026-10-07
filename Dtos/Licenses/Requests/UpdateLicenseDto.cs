namespace Api.Dtos.Licenses.Requests;
public sealed record UpdateLicenseDto(
    int LicenseTypeId,
    string DriverId,
    string ApplicationId,
    string Notes,
    decimal PaidFees,
    IssueReason IssueReason);

namespace Api.Dtos.Licenses.Requests;
public sealed record CreateLicenseDto(
    string LicenseNumber,
    string ApplicationId,
    int LicenseTypeId,
    string Notes,
    string DriverId,
    decimal PaidFees,
    IssueReason IssueReason);

namespace DVLD.Contracts.License;

public record LicenseUpdateRequest(  int LicenseTypeId, string DriverId, string ApplicationId, string Notes,decimal PaidFees,IssueReason IssueReason);

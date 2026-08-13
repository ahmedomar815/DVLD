namespace DVLD.Contracts.LicenseService;

public record LicenseRequest (string LicenseNumber,string ApplicationId, int LicenseTypeId, string Notes, string DriverId, decimal PaidFees, IssueReason IssueReason);

using DVLD.Contracts.LicenseType;

namespace DVLD.Contracts.License;

public record LicneseResponse(string LicenseNumber, DateOnly IssueDate, DateOnly ExpiryDate, string LicenseName, string Status);

namespace DVLD.Contracts.LicenseType;

public record LicenseTypeResponse(string Name, string Description, int MinimumAllowedAge, int DefaultValidityLength, decimal Fees);

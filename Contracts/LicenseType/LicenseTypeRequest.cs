namespace DVLD.Contracts.LicenseType;

public record LicenseTypeRequest(string Name, string Description, int MinimumAllowedAge, int DefaultValidityLength, decimal Fees);

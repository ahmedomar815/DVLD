namespace DVLD.Contracts.LicenseType;

public record LicenseTypeResponse(int Id, string Name, string Description, int MinimumAllowedAge, int DefaultValidityLength, decimal Fees);
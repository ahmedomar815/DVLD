namespace DVLD.Contracts.LicenseType;

public record LicenseTypeRequest(string Name, string Description, int MinmumAllowedAge, int DefaultVaildityLength, decimal fees);

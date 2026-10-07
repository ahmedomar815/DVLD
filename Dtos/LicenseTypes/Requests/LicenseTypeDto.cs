namespace Api.Dtos.LicenseTypes.Requests;
public sealed record LicenseTypeDto(
    string Name,
    string Description,
    int MinimumAllowedAge,
    int DefaultValidityLength,
    decimal Fees);

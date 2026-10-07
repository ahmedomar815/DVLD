namespace Api.Dtos.LicenseTypes.Responses;
public sealed record LicenseTypeDto(
    int Id,
    string Name,
    string Description,
    int MinimumAllowedAge,
    int DefaultValidityLength,
    decimal Fees);

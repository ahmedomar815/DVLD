namespace Api.Dtos.Drivers.Responses;
public sealed record DriverDto(
    string Id,
    Api.Dtos.Applications.Responses.ApplicationUserDto User,
    IEnumerable<Api.Dtos.Licenses.Responses.LicenseDto> Licenses);

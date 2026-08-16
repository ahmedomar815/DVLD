using DVLD.Contracts.License;

namespace DVLD.Contracts.Driver;

public record DriverResponse(
    string Id,
   UserResponse UserResponse,
   IEnumerable<LicneseResponse> LicenseResponses

);

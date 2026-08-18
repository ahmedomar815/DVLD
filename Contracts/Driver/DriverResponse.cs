using DVLD.Contracts.License;

namespace DVLD.Contracts.Driver;

public record DriverResponse(
    string Id,
   ApplicaitonUserResponse UserResponse,
   IEnumerable<LicneseResponse> LicenseResponses

);

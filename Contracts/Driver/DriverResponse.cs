using DVLD.Contracts.License;
using DVLD.Contracts.User;

namespace DVLD.Contracts.Driver;

public record DriverResponse(
    string Id,
   UserResponse UserResponse,
   IEnumerable<LicneseResponse> LicenseResponses

);

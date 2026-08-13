using DVLD.Contracts.License;
using DVLD.Contracts.LicenseService;

namespace DVLD.Services;

public interface ILicenseService
{
    Task<Result<LicneseResponse>> GetAyncId(string licenseNumber);
    Task<Result<LicneseResponse>> CreateAsync(string userId, LicenseRequest request, CancellationToken cancellationToken);
}

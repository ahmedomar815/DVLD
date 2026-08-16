using DVLD.Contracts.License;
using DVLD.Contracts.LicenseService;

namespace DVLD.Services;

public interface ILicenseService
{
    Task<Result<LicneseResponse>> GetAyncId(string licenseNumber);
    Task<Result<LicneseResponse>> CreateAsync(LicenseRequest request, CancellationToken cancellationToken);
    Task<Result> UpdateAsync(string LicenseNumber, LicenseUpdateRequest request, CancellationToken cancellationToken);
    Task<Result<LicneseResponse>> RenewAsync(string LicenseNumber, CancellationToken cancellationToken);
    Task<Result> Disable(string LicenseNumber, CancellationToken cancellationToken);


}

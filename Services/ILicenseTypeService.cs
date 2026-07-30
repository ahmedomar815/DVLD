using DVLD.Contracts.LicenseType;

namespace DVLD.Services;

public interface ILicenseTypeService
{
    Task<Result<LicenseTypeResponse>> GetAsync(int licenseTypeId, CancellationToken cancellationToken);
    Task<Result<LicenseTypeResponse>> CreateAsync(LicenseTypeRequest request, CancellationToken cancellationToken);
    Task<Result> UpdateAsync(int licenseTypeId, LicenseTypeRequest request, CancellationToken cancellationToken);
    Task<Result<IEnumerable<LicenseTypeResponse>>> GetAllAsync(CancellationToken cancellationToken);
}

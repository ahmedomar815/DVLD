using DVLD.Contracts.DrivingLicenseApplication;

namespace DVLD.Services;

public interface IDrivingLicenseApplicationService
{
    Task<Result<DrivingLicenseApplicationResponse>> GetAsync(string drivingLicenseApplicationId, CancellationToken cancellationToken);
    Task<Result> CreateAsync(DrivingLicenseApplicaitonRequest request, CancellationToken cancellationToken);
}


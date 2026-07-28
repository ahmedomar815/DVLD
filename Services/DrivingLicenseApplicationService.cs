using DVLD.Contracts.DrivingLicenseApplication;
using DVLD.Contracts.LicenseType;
using DVLD.Persistence;
using Mapster;

namespace DVLD.Services;

public class DrivingLicenseApplicationService(ApplicationDbContext context):IDrivingLicenseApplicationService
{
    private readonly ApplicationDbContext _context = context;

  
        public async Task<Result<DrivingLicenseApplicationResponse>> GetAsync(string drivingLicenseApplicationId, CancellationToken cancellationToken)
    {
        var response = await _context.DrivingLicenseApplications
            .Where(x => x.Id == drivingLicenseApplicationId)
            .Select(x => new DrivingLicenseApplicationResponse(
                new ApplicationResponse(
                    x.Application.Status.ToString(),
                    x.Application.PaidFees,
                    x.Application.ApplicationType.Name,
                    new UserResponse(
                        x.Application.User.Id,
                        x.Application.User.FirstName,
                        x.Application.User.SecondName,
                        x.Application.User.ThirdName,
                        x.Application.User.FourthName,
                        x.Application.User.Email,
                        x.Application.User.NationalId
                    )
                ),
                new LicenseTypeResponse(
                    x.LicenseType.Name,
                    x.LicenseType.Description,
                    x.LicenseType.MinimumAllowedAge,
                    x.LicenseType.DefaultValidityLength,
                    x.LicenseType.Fees
                )
            ))
            .FirstOrDefaultAsync(cancellationToken);

        if (response is null)
            return Result.Failure<DrivingLicenseApplicationResponse>(DrivingLicenseApplicationErros.NotFound);

        return Result.Success<DrivingLicenseApplicationResponse>(response);
    }
    
    public async Task<Result> CreateAsync(DrivingLicenseApplicaitonRequest request, CancellationToken cancellationToken)
    {
        var applicationIsExist = await _context.Applications
            .AnyAsync(x => x.Id == request.applicationId, cancellationToken);
        if (!applicationIsExist)
            return Result.Failure(ApplicationErrors.NotFound);

        var licenseTypeExist = await _context.LicenseTypes
            .AnyAsync(x => x.Id == request.LicenseTypeId, cancellationToken);
        if (!licenseTypeExist)
            return Result.Failure(LicenseTypeErrors.NotFound);

        var drivingLicenseApplication = request.Adapt<DrivingLicenseApplication>();

        await _context.DrivingLicenseApplications.AddAsync(drivingLicenseApplication, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }

}

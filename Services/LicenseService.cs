using DVLD.Contracts.License;
using DVLD.Contracts.LicenseService;
using Mapster;
using DVLD.Entities;

namespace DVLD.Services;

public class LicenseService(ApplicationDbContext context):ILicenseService
{
    private readonly ApplicationDbContext _context = context;


    public async Task<Result<LicneseResponse>> GetAyncId(string licenseNumber)
    {
        
        var licenseResponse = await GetLicenseQuery().FirstOrDefaultAsync(x => x.LicenseNumber == licenseNumber);
        if(licenseResponse is null) return Result.Failure<LicneseResponse>(LicenseErrors.NotFound);
        return Result.Success<LicneseResponse>(licenseResponse!);
    }
    public async Task<Result<LicneseResponse>> CreateAsync(string userId,LicenseRequest request,CancellationToken cancellationToken)
    {
        var LicneseNumberIsExist= await _context.Licenses.AnyAsync(x => x.LicenseNumber == request.LicenseNumber, cancellationToken);
        if (LicneseNumberIsExist) return Result.Failure<LicneseResponse>(LicenseErrors.DubplicatedLicenseNumber);
        var ApplicationIdIsExist = await _context.Applications.AnyAsync(x => x.Id == request.ApplicationId, cancellationToken);
        if(!ApplicationIdIsExist) return Result.Failure<LicneseResponse>(ApplicationErrors.NotFound);
        var DriverIdIsExist = await _context.Drivers.AnyAsync(x => x.Id == request.DriverId, cancellationToken);
        if(!DriverIdIsExist) return Result.Failure<LicneseResponse>(DriverErrors.NotFound);
        var licenseType = await _context.LicenseTypes.FirstOrDefaultAsync(x => x.Id == request.LicenseTypeId, cancellationToken);
        if (licenseType is null) return Result.Failure<LicneseResponse>(LicenseTypeErrors.NotFound);

        var license=request.Adapt<License>();
        license.CreatedByUserId = userId;
        license.IssueDate = DateOnly.FromDateTime(DateTime.Now);
        license.ExpiryDate = DateOnly.FromDateTime(DateTime.Now.AddYears(licenseType.DefaultValidityLength));
        await _context.Licenses.AddAsync(license, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);


        var licenseResponse= await GetLicenseQuery().FirstOrDefaultAsync(x => x.LicenseNumber == license.LicenseNumber);
        return Result.Success<LicneseResponse>(licenseResponse!);

    }

   

    private IQueryable<LicneseResponse> GetLicenseQuery()
    {
        return _context.Licenses.Select(x => new LicneseResponse(x.LicenseNumber, x.IssueDate, x.ExpiryDate, x.LicenseType.Name, new LicenseUserResponse(x.Driver.User.FirstName, x.Driver.User.SecondName, x.Driver.User.ThirdName, x.Driver.User.FourthName, x.Driver.User.NationalId)));
    }
}

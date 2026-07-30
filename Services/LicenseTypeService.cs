using DVLD.Contracts.LicenseType;
using DVLD.Persistence;
using Mapster;

namespace DVLD.Services;

public class LicenseTypeService(ApplicationDbContext context): ILicenseTypeService
{
    private readonly ApplicationDbContext _context = context;


    public async Task<Result<IEnumerable<LicenseTypeResponse>>> GetAllAsync(CancellationToken cancellationToken)
    {
        var response = await _context.LicenseTypes
            .AsNoTracking()
            .ProjectToType<LicenseTypeResponse>()
            .ToListAsync(cancellationToken);

        return Result.Success<IEnumerable<LicenseTypeResponse>>(response);
    }
    public async Task<Result<LicenseTypeResponse>> GetAsync(int licenseTypeId, CancellationToken cancellationToken)
    {
        var licenseType = await _context.LicenseTypes
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == licenseTypeId, cancellationToken);

        if (licenseType is null)
            return Result.Failure<LicenseTypeResponse>(LicenseTypeErrors.NotFound);

        return Result.Success(licenseType.Adapt<LicenseTypeResponse>());


    }
    public async Task<Result<LicenseTypeResponse>> CreateAsync(LicenseTypeRequest request, CancellationToken cancellation)
    {
        var isExist = await _context.LicenseTypes.AnyAsync(x => x.Name == request.Name.Trim(), cancellation);
        if (isExist) return Result.Failure<LicenseTypeResponse>(LicenseTypeErrors.DuplicateName);

        var licenseType = request.Adapt<LicenseType>();

        await _context.LicenseTypes.AddAsync(licenseType, cancellation);
        await _context.SaveChangesAsync(cancellation);

        return Result.Success(licenseType.Adapt<LicenseTypeResponse>());
    }

    public async Task<Result> UpdateAsync(int licenseTypeId, LicenseTypeRequest request, CancellationToken cancellation)
    {
        if (await _context.LicenseTypes.FirstOrDefaultAsync(x => x.Id == licenseTypeId, cancellation) is not { } licenseType)
            return Result.Failure(LicenseTypeErrors.NotFound);

        var isExist = await _context.LicenseTypes.AnyAsync(
            x => x.Name == request.Name.Trim() && licenseTypeId != x.Id,
            cancellation);
        if (isExist)
            return Result.Failure(LicenseTypeErrors.DuplicateName);

        request.Adapt(licenseType);

        await _context.SaveChangesAsync(cancellation);
        return Result.Success();
    }
}

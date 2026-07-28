using DVLD.Contracts.LicenseType;
using DVLD.Persistence;
using Mapster;

namespace DVLD.Services;

public class LicenseTypeService(ApplicationDbContext context)
{
    private readonly ApplicationDbContext _context = context;

    public async Task<Result>CreateAsync(LicenseTypeRequest request ,CancellationToken cancellation )
    {
        var isExist = await _context.LicenseTypes.AnyAsync(x => x.Name == request.Name);
        if (isExist) return Result.Failure(LicenseTypeErrors.DublicatedName);
        var licenseType=request.Adapt<LicenseType>();
         await _context.LicenseTypes.AddAsync(licenseType);
        return Result.Success();
    }
}

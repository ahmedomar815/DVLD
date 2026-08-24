using DVLD.Contracts.LicenseType;
using DVLD.Persistence;
using Mapster;
using Microsoft.Extensions.Caching.Distributed;
using System.Text;
using System.Text.Json;

namespace DVLD.Services;

public class LicenseTypeService(ApplicationDbContext context,IDistributedCache cache): ILicenseTypeService
{
    private readonly ApplicationDbContext _context = context;
    private readonly IDistributedCache _cache = cache;

    public async Task<Result<IEnumerable<LicenseTypeResponse>>> GetAllAsync(
    CancellationToken cancellationToken)
    {
        var bytes = await _cache.GetAsync("LicensesTypes", cancellationToken);

        if (bytes is not null)
        {
            var cachedResponse =
                JsonSerializer.Deserialize<IEnumerable<LicenseTypeResponse>>(bytes);

            return Result.Success(cachedResponse!);
        }

        var response = await _context.LicenseTypes
            .AsNoTracking()
            .ProjectToType<LicenseTypeResponse>()
            .ToListAsync(cancellationToken);

        var json = JsonSerializer.Serialize(response);
        bytes = Encoding.UTF8.GetBytes(json);

        await _cache.SetAsync(
            "LicensesTypes",
            bytes, new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromDays(180)
            },
            cancellationToken);

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
    public async Task<Result<LicenseTypeResponse>> CreateAsync(LicenseTypeRequest request, CancellationToken cancellationToken)
    {
        var isExist = await _context.LicenseTypes.AnyAsync(x => x.Name == request.Name.Trim(), cancellationToken);
        if (isExist) return Result.Failure<LicenseTypeResponse>(LicenseTypeErrors.DuplicateName);

        var licenseType = request.Adapt<LicenseType>();

        await _context.LicenseTypes.AddAsync(licenseType, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        await _cache.RemoveAsync("LicensesTypes", cancellationToken);
        return Result.Success(licenseType.Adapt<LicenseTypeResponse>());
    }

    public async Task<Result> UpdateAsync(int licenseTypeId, LicenseTypeRequest request, CancellationToken cancellationToken)
    {
        if (await _context.LicenseTypes.FirstOrDefaultAsync(x => x.Id == licenseTypeId, cancellationToken) is not { } licenseType)
            return Result.Failure(LicenseTypeErrors.NotFound);

        var isExist = await _context.LicenseTypes.AnyAsync(
            x => x.Name == request.Name.Trim() && licenseTypeId != x.Id,
            cancellationToken);
        if (isExist)
            return Result.Failure(LicenseTypeErrors.DuplicateName);

        request.Adapt(licenseType);

        await _context.SaveChangesAsync(cancellationToken);
        await _cache.RemoveAsync("LicensesTypes", cancellationToken);
        return Result.Success();
    }
}

using DVLD.Contracts.TestType;
using Mapster;
using Microsoft.Extensions.Caching.Distributed;
using System.Text;
using System.Text.Json;

namespace DVLD.Services;

public class TestTypeService(ApplicationDbContext context,IDistributedCache cache): ITestTypeService
{
    private readonly ApplicationDbContext _context = context;
    private readonly IDistributedCache _cache = cache;

    public async Task<Result<TestTypeResponse>> GetAsync(int testTypeId,CancellationToken cancellationToken)
    {
        if(await _context.TestTypes.FirstOrDefaultAsync(x => x.Id == testTypeId&& x.IsActive, cancellationToken) is not { } testType)
            return Result.Failure<TestTypeResponse>(TestTypeErrors.NotFound);
        var response = testType.Adapt<TestTypeResponse>();
        return Result.Success(response);
    }
 
       public async Task<Result<IEnumerable<TestTypeResponse>>> GetAllAsync(
    CancellationToken cancellationToken)
    {
        var bytes = await _cache.GetAsync("TestsTypes", cancellationToken);

        if (bytes is not null)
        {
            var cachedResponse =
                JsonSerializer.Deserialize<IEnumerable<TestTypeResponse>>(bytes);

            return Result.Success(cachedResponse!);
        }

        var testTypes = await _context.TestTypes
            .AsNoTracking()
            .Where(x => x.IsActive)
            .ToListAsync(cancellationToken);

        var response = testTypes.Adapt<List<TestTypeResponse>>();

        var json = JsonSerializer.Serialize(response);
        bytes = Encoding.UTF8.GetBytes(json);

        await _cache.SetAsync(
            "TestsTypes",
            bytes,
            new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(1)
            },
            cancellationToken);

        return Result.Success<IEnumerable<TestTypeResponse>>(response);
    }
    
    public async Task<Result<TestTypeResponse>> CreateAsync(
    TestTypeRequest request,
    CancellationToken cancellationToken)
    {
        var isExist = await _context.TestTypes
            .AnyAsync(x => x.Title == request.Title, cancellationToken);

        if (isExist)
            return Result.Failure<TestTypeResponse>(TestTypeErrors.DuplicateName);

        var testType = request.Adapt<TestType>();

        await _context.TestTypes.AddAsync(testType, cancellationToken);

        await _context.SaveChangesAsync(cancellationToken);
        await _cache.RemoveAsync("TestsTypes", cancellationToken);
        return Result.Success(testType.Adapt<TestTypeResponse>());
    }
    public async Task<Result> UpdateAsync(int testTypeId, TestTypeRequest request, CancellationToken cancellationToken)
    {
        var testType = await _context.TestTypes.FirstOrDefaultAsync(x => x.Id == testTypeId&&x.IsActive, cancellationToken);
        if (testType is null) return Result.Failure<TestTypeResponse>(TestTypeErrors.NotFound);
        var isExist = await _context.TestTypes.AnyAsync(x => x.Title == request.Title && x.Id != testTypeId, cancellationToken);
        if (isExist) return Result.Failure(TestTypeErrors.DuplicateName);
        request.Adapt(testType);
        await _context.SaveChangesAsync(cancellationToken);
        await _cache.RemoveAsync("TestsTypes", cancellationToken);
        return Result.Success(testType);
    }
    public async Task<Result> DeleteAsync(int testTypeId, CancellationToken cancellationToken)
    {
        var testType = await _context.TestTypes.FirstOrDefaultAsync(x => x.Id == testTypeId, cancellationToken);
        if (testType is null) return Result.Failure(TestTypeErrors.NotFound);
        testType.IsActive = false;
        await _context.SaveChangesAsync(cancellationToken);
        await _cache.RemoveAsync("TestsTypes", cancellationToken);
        return Result.Success();
    }
}

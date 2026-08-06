using DVLD.Contracts.TestType;
using Mapster;

namespace DVLD.Services;

public class TestTypeService(ApplicationDbContext context): ITestTypeService
{
    private readonly ApplicationDbContext _context = context;


    public async Task<Result<TestTypeResponse>> GetAsync(int testTypeId,CancellationToken cancellationToken)
    {
        if(await _context.TestTypes.FirstOrDefaultAsync(x => x.Id == testTypeId&& x.IsActive, cancellationToken) is not { } testType)
            return Result.Failure<TestTypeResponse>(TestTypeErrors.NotFound);
        var response = testType.Adapt<TestTypeResponse>();
        return Result.Success(response);
    }
    public async Task<Result<IEnumerable<TestTypeResponse>>> GetAllAsync(CancellationToken cancellationToken)
    {
        var testTypes = await _context.TestTypes.Where(x=>x.IsActive).ToListAsync(cancellationToken);
        List<TestTypeResponse> response = testTypes.Adapt<List<TestTypeResponse>>();
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
        return Result.Success(testType);
    }
    public async Task<Result> DeleteAsync(int testTypeId, CancellationToken cancellationToken)
    {
        var testType = await _context.TestTypes.FirstOrDefaultAsync(x => x.Id == testTypeId, cancellationToken);
        if (testType is null) return Result.Failure(TestTypeErrors.NotFound);
        testType.IsActive = false;
        await _context.SaveChangesAsync(cancellationToken);
        return Result.Success();
    }
}

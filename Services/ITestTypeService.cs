using DVLD.Contracts.TestType;

namespace DVLD.Services;

public interface ITestTypeService
{
    Task<Result<TestTypeResponse>> GetAsync(int testTypeId, CancellationToken cancellationToken);
    Task<Result<IEnumerable<TestTypeResponse>>> GetAllAsync(CancellationToken cancellationToken);
    Task<Result<TestTypeResponse>> CreateAsync(TestTypeRequest request, CancellationToken cancellationToken);
    Task<Result> UpdateAsync(int testTypeId, TestTypeRequest request, CancellationToken cancellationToken);
    Task<Result> DeleteAsync(int testTypeId, CancellationToken cancellationToken);
}

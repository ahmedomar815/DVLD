using DVLD.Contracts.Test;

namespace DVLD.Services;

public interface ITestService
{
    Task<Result<TestResponse>> CreateAsync(string userId,TestRequest request, CancellationToken cancellationToken);
    
    }

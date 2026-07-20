using DVLD.Contracts.Application;

namespace DVLD.Services;

public interface IApplicationService
{
    Task<Result> Create( ApplicationRequest request, CancellationToken cancellationToken);
    Task<Result<ApplicationResponse>> Get(string applicationId, CancellationToken cancellationToken);
}


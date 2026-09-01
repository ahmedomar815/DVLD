using DVLD.Contracts.Application;

namespace DVLD.Services;

public interface IApplicationService
{
    Task<Result> CreateAsync(ApplicationRequest request, CancellationToken cancellationToken);
    Task<Result<ApplicationResponse>> GetAsync(string applicationId, CancellationToken cancellationToken);
    Task<Result> SetApprovedAsync(string applicationId, CancellationToken cancellationToken);
    Task<Result> SetRejectedAsync(string applicationId, CancellationToken cancellationToken);
    Task<Result> SetCancelledAsync(string applicationId, CancellationToken cancellationToken);
}


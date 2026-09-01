using DVLD.Contracts.ApplicationType;
using Microsoft.AspNetCore.Mvc;

namespace DVLD.Services;

public interface IApplicationTypeService
{
    Task<Result<ApplicationTypeResponse>> CreateAsync(ApplicationTypeRequest request, CancellationToken cancellationToken);
    Task<Result<ApplicationTypeResponse>> GetAsync(int applicationTypeId, CancellationToken cancellationToken);
    Task<Result> UpdateAsync(int applicationTypeId, ApplicationTypeRequest request, CancellationToken cancellationToken);
    Task<Result<List<ApplicationTypeResponse>>> GetAllAsync(CancellationToken cancellationToken);
    Task<Result> DeleteAsync(int applicationTypeId, CancellationToken cancellationToken);
    
}

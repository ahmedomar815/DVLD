using DVLD.Contracts.ApplicationType;
using Microsoft.AspNetCore.Mvc;

namespace DVLD.Services;

public interface IApplicationTypeService
{
    Task<Result<ApplicationTypeResponse>> CreateApplicationType(ApplicationTypeRequest request, CancellationToken cancellationToken);
    Task<Result<ApplicationTypeResponse>> Get([FromRoute] int applicationTypeId, CancellationToken cancellationToken);
    Task<Result> Update([FromRoute] int applicationTypeId, ApplicationTypeRequest request, CancellationToken cancellationToken);
    Task<Result<List<ApplicationTypeResponse>>> GetAll(CancellationToken cancellationToken);
     Task<Result> Delete(int applicationTypeId, CancellationToken cancellationToken);
    
}

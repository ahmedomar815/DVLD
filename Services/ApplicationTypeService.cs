using DVLD.Contracts.ApplicationType;
using DVLD.Persistence;
using Mapster;
using Microsoft.AspNetCore.Mvc;

namespace DVLD.Services;

public class ApplicationTypeService(ApplicationDbContext context):IApplicationTypeService
{
    private readonly ApplicationDbContext _context = context;

    public async Task<Result<ApplicationTypeResponse>> Get(
    [FromRoute] int applicationTypeId,
    CancellationToken cancellationToken)
    {
        var applicationType = await _context.ApplicationTypes
            .FindAsync([applicationTypeId], cancellationToken);

        return applicationType is null
            ? Result.Failure<ApplicationTypeResponse>(ApplicationTypeErrors.NotFound)
            : Result.Success(applicationType.Adapt<ApplicationTypeResponse>());
    }
    public async Task<Result<List<ApplicationTypeResponse>>> GetAll( CancellationToken cancellationToken)
    {
        var applicationTypes = await _context.ApplicationTypes.Where(x=>x.IsActive)
            .ProjectToType<ApplicationTypeResponse>()
            .ToListAsync(cancellationToken);

        return Result.Success(applicationTypes);
    }

    public async Task<Result>CreateApplicationType(ApplicationTypeRequest request,CancellationToken cancellationToken)
    {
     
        var exists = await _context.ApplicationTypes
            .AnyAsync(x => x.Name == request.Name, cancellationToken);
        if (exists) return Result.Failure(ApplicationTypeErrors.DulicatedName);
        var applicationType = request.Adapt<ApplicationType>();
        await _context.ApplicationTypes.AddAsync(applicationType, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return Result.Success();
    }

    public async  Task<Result> Update(  int applicationTypeId, ApplicationTypeRequest request, CancellationToken cancellationToken)

    {
        var applicationType = await _context.ApplicationTypes
            .FindAsync([applicationTypeId], cancellationToken);
        if (applicationType is null) return Result.Failure(ApplicationTypeErrors.NotFound);
        var exists = await _context.ApplicationTypes.AnyAsync(x => x.Name == request.Name&&x.Id != applicationTypeId, cancellationToken);
        if (exists) return Result.Failure(ApplicationTypeErrors.DulicatedName);
        applicationType =request.Adapt(applicationType);
        await _context.SaveChangesAsync(cancellationToken);
        return Result.Success();
    }
   
    public async Task<Result> Delete( int applicationTypeId, CancellationToken cancellationToken)
    {
        var applicationType = await _context.ApplicationTypes
      .FindAsync([applicationTypeId], cancellationToken);

        if (applicationType is null)
            return Result.Failure(ApplicationTypeErrors.NotFound);

        applicationType.IsActive = false;
        await _context.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }



}

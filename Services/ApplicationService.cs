using DVLD.Persistence;
using Hangfire;
using Mapster;
using static System.Net.Mime.MediaTypeNames;


namespace DVLD.Services;

public class ApplicationService(ApplicationDbContext context,INotificationService notificationService):IApplicationService
{
    private readonly ApplicationDbContext _context = context;
    private readonly INotificationService _notificationService = notificationService;

    public async Task<Result<ApplicationResponse>>Get(string applicationId,CancellationToken cancellationToken)
    {
        
        var application = _context.Applications
            .Include(a=>a.User)
            .Include(x=>x.ApplicationType)
            .FirstOrDefault(x => x.Id == applicationId);
        if (application is null) return Result.Failure<ApplicationResponse>(ApplicationErrors.NotFound);
        var strStatus=EnumHelper.GetName<ApplicationStatus>(application.Status);
        var userResponse = application.User.Adapt<UserResponse>();
        var response = new ApplicationResponse(strStatus,application.PaidFees,application.ApplicationType.Name, userResponse);
        return Result.Success<ApplicationResponse>(response);

    }

    public async Task<Result>Create(ApplicationRequest request, CancellationToken cancellationToken)
    {

        var applicationType = await _context.ApplicationTypes.AsNoTracking()
      .FirstOrDefaultAsync(x => x.Id == request.ApplicationTypeId, cancellationToken);
        if (applicationType is null) return Result.Failure(ApplicationTypeErrors.NotFound);

        var userExist = await _context.Users.AnyAsync(x => x.Id == request.UserId, cancellationToken);
        if (!userExist) return Result.Failure(UserErrors.UserNotFound);

        var application = request.Adapt<Application>();
        application.PaidFees = applicationType.Fees;

        await _context.Applications.AddAsync(application, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        BackgroundJob.Enqueue(() => _notificationService.SendNewApplication(application.Id));
        return Result.Success();
    }
    public async Task<Result> SetApprovedAsync(string applicationId,CancellationToken cancellationToken)
    {

        if( await _context.Applications.FindAsync(applicationId, cancellationToken) is not {  } application)
            return Result.Failure(ApplicationErrors.NotFound);
        if (application.Status != ApplicationStatus.Pending)
            return Result.Failure(ApplicationErrors.InvalidStatus);
        application.Status=ApplicationStatus.Approved;
        await _context.SaveChangesAsync(cancellationToken);
        BackgroundJob.Enqueue(() => _notificationService.SendApplicationApproved(application.Id));
        return Result.Success(applicationId);
    }
    public async Task<Result> SetRejectedAsync(string applicationId, CancellationToken cancellationToken)
    {

        if (await _context.Applications.FindAsync(applicationId, cancellationToken) is not { } application)
            return Result.Failure(ApplicationErrors.NotFound);
        if (application.Status != ApplicationStatus.Pending)
            return Result.Failure(ApplicationErrors.InvalidStatus with {Description= "Only pending applications can be rejected" });
        application.Status = ApplicationStatus.Rejected;
        await _context.SaveChangesAsync(cancellationToken);
        BackgroundJob.Enqueue(() => _notificationService.SendApplicationRejected(application.Id));

        return Result.Success();
    }
    public async Task<Result> SetCancelledAsync(string applicationId, CancellationToken cancellationToken)
    {
        
        if (await _context.Applications.FindAsync(applicationId, cancellationToken) is not { } application)
            return Result.Failure(ApplicationErrors.NotFound);
        application.Status = ApplicationStatus.Cancelled;
        await _context.SaveChangesAsync(cancellationToken);
        BackgroundJob.Enqueue(() => _notificationService.SendApplicationCancelled(application.Id));
        return Result.Success();
    }

}

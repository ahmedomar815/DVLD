using DVLD.Persistence;
using Hangfire;
using Mapster;


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
        
        var isExist = await _context.ApplicationTypes.AnyAsync(x=>x.Id==request.ApplicationTypeId, cancellationToken);
        if(!isExist) return Result.Failure(ApplicationTypeErrors.NotFound);
        var userExist = await _context.Users.AnyAsync(x=>x.Id==request.UserId, cancellationToken);
        if (!userExist) return Result.Failure(UserErrors.UserNotFound);
        var application = request.Adapt<Application>();
        application.PaidFees = _context.ApplicationTypes.AsNoTracking().Where(x => x.Id == request.ApplicationTypeId).Select(x => x.Fees).FirstOrDefault();
        await _context.Applications.AddAsync(application, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        BackgroundJob.Enqueue(()=> _notificationService.SendNewApplication(application));
        return Result.Success();
    }
}

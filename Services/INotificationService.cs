namespace DVLD.Services;

public interface INotificationService
{
    Task SendNewApplication(string applicationId);
    Task SendApplicationApproved(string applicationId);
    Task SendApplicationRejected(string applicationId);
    Task SendApplicationCancelled(string applicationId);
}

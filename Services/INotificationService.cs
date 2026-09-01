namespace DVLD.Services;

public interface INotificationService
{
    Task SendNewApplicationAsync(string applicationId);
    Task SendApplicationApprovedAsync(string applicationId);
    Task SendApplicationRejectedAsync(string applicationId);
    Task SendApplicationCancelledAsync(string applicationId);
    Task SendTestAppointmentAsync(string testAppointmentId);
}

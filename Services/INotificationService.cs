namespace DVLD.Services;

public interface INotificationService
{
    Task SendNewApplication(Application application);
}

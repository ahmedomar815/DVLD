using DVLD.Helpers;
using DVLD.Persistence;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;

namespace DVLD.Services;

public class NotificationService(ApplicationDbContext context, IEmailSender emailSender) : INotificationService
{
    private readonly ApplicationDbContext _context = context;
    private readonly IEmailSender _emailSender = emailSender;

    public Task SendNewApplication(string applicationId)
        => SendStatusEmailAsync(applicationId, "application_created_email", "DVLD Application Already Created");

    public Task SendApplicationApproved(string applicationId)
        => SendStatusEmailAsync(applicationId, "application_approved_email", "DVLD Application Approved");

    public Task SendApplicationRejected(string applicationId)
        => SendStatusEmailAsync(applicationId, "application_rejected_email", "DVLD Application Rejected");

    public Task SendApplicationCancelled(string applicationId)
        => SendStatusEmailAsync(applicationId, "application_cancelled_email", "DVLD Application Cancelled");

    private async Task SendStatusEmailAsync(
        string applicationId,
        string templateName,
        string subject)
    {
        var application = await _context.Applications
            .AsNoTracking()
            .Include(a => a.User)
                .ThenInclude(u => u.Country)
            .Include(a => a.ApplicationType)
            .FirstOrDefaultAsync(x => x.Id == applicationId);

        if (application is null)
            return;

        var user = application.User;
        if (user is null || string.IsNullOrWhiteSpace(user.Email))
            return;

        var country = user.Country;
        var applicationType = application.ApplicationType;

        var placeHolders = new Dictionary<string, string>
        {
            { "{{fullName}}", $"{user.FirstName} {user.SecondName} {user.ThirdName} {user.FourthName}" },
            { "{{userID}}", user.Id },
            { "{{applicationId}}", application.Id },
            { "{{countryName}}", country?.Name ?? string.Empty },
            { "{{applicationType}}", applicationType?.Name ?? string.Empty },
            { "{{paidFees}}", applicationType?.Fees.ToString() ?? string.Empty },
            { "{{status}}", application.Status.ToString() }
        };

        var body = EmailBodyBuilder.GenerateEmailBody(templateName, placeHolders);
        await _emailSender.SendEmailAsync(user.Email!, subject, body);
    }
}
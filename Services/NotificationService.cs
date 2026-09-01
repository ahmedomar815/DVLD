using DVLD.Helpers;
using DVLD.Persistence;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace DVLD.Services;

public class NotificationService(ApplicationDbContext context, IEmailSender emailSender) : INotificationService
{
    private readonly ApplicationDbContext _context = context;
    private readonly IEmailSender _emailSender = emailSender;

    public Task SendNewApplicationAsync(string applicationId)
        => SendStatusEmailAsync(applicationId, "application_created_email", "DVLD Application Already Created");

    public Task SendApplicationApprovedAsync(string applicationId)
        => SendStatusEmailAsync(applicationId, "application_approved_email", "DVLD Application Approved");

    public Task SendApplicationRejectedAsync(string applicationId)
        => SendStatusEmailAsync(applicationId, "application_rejected_email", "DVLD Application Rejected");

    public Task SendApplicationCancelledAsync(string applicationId)
        => SendStatusEmailAsync(applicationId, "application_cancelled_email", "DVLD Application Cancelled");

   
    public async Task SendTestAppointmentAsync(string testAppointmentId)
    {
        var data = await _context.TestAppointments
       .Where(x => x.Id == testAppointmentId)
         .Select(x => new
       {
         TestTypeTitle = x.TestType.Title,
         TestTypeDescription = x.TestType.Description,
         x.AppointmentDate,
         x.PaidFees,
         Email=x.AppointmentOwner.Email,
         OwnerFirstName = x.AppointmentOwner.FirstName,
         OwnerSecondName = x.AppointmentOwner.SecondName,
         OwnerThirdName = x.AppointmentOwner.ThirdName
        })
       .FirstOrDefaultAsync();

        var placeholderValues = new Dictionary<string, string>
        {
            { "TestType", data!.TestTypeTitle },
            {"Description", data.TestTypeDescription},
            { "AppointmentDate", data.AppointmentDate.ToString() },
            { "PaidFees",data.PaidFees.ToString()},
            {"Name", data.OwnerFirstName + " " + data.OwnerSecondName+data.OwnerThirdName }

        };
           var body=EmailBodyBuilder.GenerateEmailBody("TestAppointmentConfirmation", placeholderValues);
        await _emailSender.SendEmailAsync(data.Email!, "DVLD Test Appointment Confirmation", body);
    }
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

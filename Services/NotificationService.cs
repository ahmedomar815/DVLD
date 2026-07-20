using DVLD.Helpers;
using DVLD.Persistence;
using Microsoft.AspNetCore.Identity.UI.Services;

namespace DVLD.Services;

public class NotificationService(ApplicationDbContext context ,IEmailSender emailSender):INotificationService
{
    private readonly ApplicationDbContext _context = context;
    private readonly IEmailSender _emailSender = emailSender;

    public async Task SendNewApplication(Application application)
    {
        var user = await _context.Users.AsNoTracking().FirstOrDefaultAsync(x => x.Id == application.UserId);
        var country = await  _context.Countries.AsNoTracking().FirstOrDefaultAsync(a => a.Id == user!.CountryId);
        var applicationType = await _context.ApplicationTypes.AsNoTracking().FirstOrDefaultAsync(x => x.Id == application.ApplicationTypeId);
        var placeHolders = new Dictionary<string, string>()
        {

            {"{{fullName}}", $"{user!.FirstName} {user.SecondName}  {user.ThirdName} {user.FourthName}"},
            { "{{userID}}",user.Id },
            {"{{applicationId}}",application.Id },
            {"{{countryName}}",country!.Name },
            {"{{applicationType}}",applicationType!.Name },
            {"{{paidFees}} " ,applicationType.Fees.ToString() },
            {"{{status}}" ,application.Status.ToString() }
        };
        var body = EmailBodyBuilder.GenerateEmailBody("application_created_email", placeHolders);
        await _emailSender.SendEmailAsync(user.Email!, "DVLD Application already created", body);
    }
}

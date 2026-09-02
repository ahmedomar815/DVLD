
using MailKit.Security;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Options;

namespace DVLD.Health;

public sealed class MailProviderHealthCheck(IOptions<MailSettings> mailSettings) : IHealthCheck
{
    private readonly MailSettings _mailSettings = mailSettings.Value;

    public async Task<HealthCheckResult> CheckHealthAsync(
        HealthCheckContext context,
        CancellationToken cancellationToken = default)
    {
        try
        {
            using var smtp = new MailKit.Net.Smtp.SmtpClient();

            await smtp.ConnectAsync(
                _mailSettings.Host,
                _mailSettings.Port,
                SecureSocketOptions.StartTls,
                cancellationToken);
            await smtp.AuthenticateAsync(
                _mailSettings.Mail,
                _mailSettings.Password,
                cancellationToken);
            await smtp.DisconnectAsync(true, cancellationToken);

            return HealthCheckResult.Healthy("SMTP provider is reachable.");
        }
        catch (Exception exception)
        {
            return HealthCheckResult.Unhealthy(
                "SMTP provider is unavailable.",
                exception);
        }
    }
}

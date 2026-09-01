using DVLD.Properties.Abstractions;

namespace DVLD.Errors;

public record LicenseErrors
{
    public static Error DubplicatedLicenseNumber => new Error("License.DubplicatedLicenseNumber", "A license Number is Dublicated ", StatusCodes.Status409Conflict);
    public static Error ActiveLicenseAlreadyExists => new Error("License.ActiveLicenseAlreadyExists", "The driver already has an active, unexpired license of this type.", StatusCodes.Status409Conflict);
    public static Error RequiredTestsNotPassed => new Error("License.RequiredTestsNotPassed", "The applicant must pass every required test type before a license can be issued.", StatusCodes.Status400BadRequest);
    public static Error NotFound => new Error("License.NotFound", "the license is not found", StatusCodes.Status404NotFound);
}

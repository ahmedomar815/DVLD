namespace DVLD.Errors;

public record LicenseErrors
{
    public static Error DubplicatedLicenseNumber => new Error("License.DubplicatedLicenseNumber", "A license Number is Dublicated ", StatusCodes.Status409Conflict);
    public static Error NotFound => new Error("License.NotFound", "the license is not found", StatusCodes.Status404NotFound);
}

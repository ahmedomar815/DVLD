namespace DVLD.Errors;

public record DrivingLicenseApplicationErros
{
    public static Error NotFound => new Error("DrivingLicenseApplication.NotFound", "LicenseType  is not found", StatusCodes.Status404NotFound);

}

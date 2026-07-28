namespace DVLD.Errors;

public class LicenseTypeErrors
{
    public static Error NotFound => new Error("LicenseType.NotFound", "LicenseType  is not found", StatusCodes.Status404NotFound);
    public static Error DublicatedName => new Error("LicenseType.DublicatedName", "The Name is Dublicated", StatusCodes.Status409Conflict);

}

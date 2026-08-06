namespace DVLD.Errors;

public record LicenseTypeErrors
{
    public static Error NotFound => new Error("LicenseType.NotFound", "License type was not found.", StatusCodes.Status404NotFound);
    public static Error DuplicateName => new Error("LicenseType.DuplicateName", "A license type with this name already exists.", StatusCodes.Status409Conflict);

}

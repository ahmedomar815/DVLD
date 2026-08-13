namespace DVLD.Services;

public record DriverErrors
{
    public static Error NotFound => new Error("Driver.NotFound", "the driver  is not found", StatusCodes.Status404NotFound);

}

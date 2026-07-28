namespace DVLD.Errors;

public record ApplicationErrors
{
    public static Error NotFound => new Error(Code: "Application.NotFound", "the application  is not found", StatusCodes.Status404NotFound);
    public static Error InvalidStatus => new("Application.InvalidStatus", "Only pending applications can be approved.",StatusCodes.Status400BadRequest);
}

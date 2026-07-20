namespace DVLD.Errors;

public record ApplicationErrors
{
    public static Error NotFound => new Error("Application.NotFound", "the application  is not found", StatusCodes.Status400BadRequest);

}

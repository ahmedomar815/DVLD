namespace DVLD.Errors;

public record ApplicationTypeErrors
{
    public static Error DulicatedName => new Error("Applicationtype.DulicatedName", "the name is dublicated", StatusCodes.Status400BadRequest);
    public static Error NotFound => new Error("Applicationtype.NotFound", "the application type is not found", StatusCodes.Status404NotFound);
}


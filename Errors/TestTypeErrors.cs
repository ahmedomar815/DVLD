namespace DVLD.Errors;

public record TestTypeErrors
{
    public static Error DuplicateName => new Error("TestType.DuplicateName", "Title of TestType is already exist", StatusCodes.Status409Conflict);
    public static Error NotFound => new Error("TestType.NotFound", "the type test is not found", StatusCodes.Status404NotFound);
}

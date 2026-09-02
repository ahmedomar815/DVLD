using DVLD.Abstractions;

namespace DVLD.Errors;

public static class TestErrors
{
    public static Error NotFound => new(
        "Test.NotFound",
        "The test was not found.",
        StatusCodes.Status404NotFound);

    public static Error AppointmentAlreadyAssigned => new(
        "Test.AppointmentAlreadyAssigned",
        "The test appointment already has a test.",
        StatusCodes.Status409Conflict);
}

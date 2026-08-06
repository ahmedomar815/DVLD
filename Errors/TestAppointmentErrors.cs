namespace DVLD.Errors;

public record TestAppointmentErrors
{
    public static Error NotFound => new Error(Code: "TestAppointment.NotFound", "TestAppointment  is not found", StatusCodes.Status404NotFound);
    public static Error InvalidStatus => new("Application.InvalidStatus", "Only pending applications can be approved.",StatusCodes.Status400BadRequest);
}

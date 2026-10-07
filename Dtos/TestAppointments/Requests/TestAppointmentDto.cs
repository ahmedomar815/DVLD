namespace Api.Dtos.TestAppointments.Requests;

public sealed record TestAppointmentDto(
    DateTime AppointmentDate,
    [property: Range(typeof(decimal), "0", "79228162514264337593543950335")] decimal PaidFees,
    [property: Range(1, int.MaxValue)] int TestTypeId,
    [property: Required] string DrivingLicenseApplicationId);

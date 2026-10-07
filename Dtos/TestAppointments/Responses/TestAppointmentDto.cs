namespace Api.Dtos.TestAppointments.Responses;

public sealed record TestAppointmentDto(
    string Id,
    DateTime AppointmentDate,
    decimal PaidFees,
    int TestTypeId,
    string TestTypeTitle,
    string TestTypeDescription,
    decimal TestTypeFees,
    string DrivingLicenseApplicationId,
    string UserId);

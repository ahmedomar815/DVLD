namespace DVLD.Contracts.TestAppointment;

public record TestAppointmentRequest(DateTime AppointmentDate, decimal PaidFees, int TestTypeId, string DrivingLicenseApplicationId);


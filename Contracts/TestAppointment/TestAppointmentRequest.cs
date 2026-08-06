namespace DVLD.Contracts.TestAppointment;

public record TestAppointmentRequest(DateTime DateTime, decimal PaidFees, int TestTypeId, string DrivingLicenseApplicationId);


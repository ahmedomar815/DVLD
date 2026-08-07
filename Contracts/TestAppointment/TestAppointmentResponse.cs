using DVLD.Contracts.DrivingLicenseApplication;

namespace DVLD.Contracts.TestAppointment;

public record TestAppointmentResponse (string Id, DateTime AppointmentDate, decimal PaidFees,TestTypeResponse TestType);


using DVLD.Contracts.DrivingLicenseApplication;

namespace DVLD.Contracts.TestAppointment;

public record TestAppointmentResponse (string Id, DateTime DateTime, decimal PaidFees,TestTypeResponse TestType);


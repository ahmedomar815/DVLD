using DVLD.Contracts.TestAppointment;

namespace DVLD.Contracts.Test;

public record TestRequest
(string TestAppointmentId, TestResult TestResult, string Notes);

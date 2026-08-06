using DVLD.Contracts.TestAppointment;

namespace DVLD.Services;

public interface ITestAppointmentService
{
    Task<Result<TestAppointmentResponse>> GetAsync(string testAppointmentId, CancellationToken cancellationToken);
    Task<Result<TestAppointmentResponse>> CreateAsync(string userId, TestAppointmentRequest request, CancellationToken cancellationToken);
}
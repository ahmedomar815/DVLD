using DVLD.Contracts.TestAppointment;

namespace DVLD.Services;

public interface ITestAppointmentService
{
    Task<Result<TestAppointmentResponse>> GetAsync(string testAppointmentId, CancellationToken cancellationToken);
    Task<Result<TestAppointmentResponse>> CreateAsync(TestAppointmentRequest request, CancellationToken cancellationToken);
    Task<Result> UpdateAsync(string testAppointmentId, TestAppointmentRequest request, CancellationToken cancellationToken);
}
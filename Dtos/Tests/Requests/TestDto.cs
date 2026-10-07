namespace Api.Dtos.Tests.Requests;

public sealed record TestDto(
    [property: Required] string TestAppointmentId,
    TestResult TestResult,
    string? Notes);

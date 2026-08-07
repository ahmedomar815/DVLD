using DVLD.Contracts.TestAppointment;

namespace DVLD.Contracts.Test;

public record TestResponse(
    string Id,
    string Title,
    string Description
    ,TestAppointmentResponse TestAppointmentResponse
);
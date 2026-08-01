namespace DVLD.Entities;

public class Test
{
    public Test()
    {
        Id = Guid.CreateVersion7().ToString();
    }

    public string Id { get; init; }
    public string TestAppointmentId { get; set; } = string.Empty;
    public TestResult TestResult { get; set; }
    public string Notes { get; set; } = string.Empty;
    public string CreatedByUserId { get; set; } = default!;
    public TestAppointment TestAppointment { get; set; } = default!;
    public ApplicationUser ApplicationUser { get; set; } = default!;
}

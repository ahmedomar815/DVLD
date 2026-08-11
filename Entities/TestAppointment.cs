namespace DVLD.Entities;

public class TestAppointment
{
    public TestAppointment()
    {
        Id = Guid.CreateVersion7().ToString();
    }

    public string Id { get; init; }
    public DateTime AppointmentDate { get; set; }
    public decimal PaidFees { get; set; }
    public string CreatedByUserId { get; set; } = string.Empty;
    public int TestTypeId { get; set; }
    public string DrivingLicenseApplicationId { get; set; } = string.Empty;
    public string UserId { get; set; }= string.Empty;
    public DrivingLicenseApplication DrivingLicenseApplication { get; set; } = default!;
    public ApplicationUser ApplicationUser { get; set; } = default!;
    public ApplicationUser AppointmentOwner { get; set; } = default!;
    public TestType TestType { get; set; } = default!;
    public Test? Test { get; set; }
}

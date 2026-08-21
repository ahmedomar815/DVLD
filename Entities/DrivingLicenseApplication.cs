using System.ComponentModel;

namespace DVLD.Entities;

public class DrivingLicenseApplication
{
    public DrivingLicenseApplication()
    {
        Id=Guid.CreateVersion7().ToString();
    }
        
    public string Id { get; set; } = string.Empty;
    public string ApplicationId { get; set; } = string.Empty;
    public int LicenseTypeId { get; set; }

    public LicenseType LicenseType { get; set; } = default!;
    public Application Application { get; set; }=default!;
    public ICollection<TestAppointment> TestAppointments { get; set; } = [];
}

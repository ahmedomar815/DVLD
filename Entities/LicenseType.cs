namespace DVLD.Entities;

public class LicenseType
{
    public int Id { get; set; }
    public string Name { get; set; }=string.Empty;

    public string Description { get; set; } = string.Empty;

    public int MinimumAllowedAge { get; set; }

    public int DefaultValidityLength { get; set; }
    public decimal Fees { get; set; }
  public    DrivingLicenseApplication DrivingLicenseApplication { get; set; } = default!;
}

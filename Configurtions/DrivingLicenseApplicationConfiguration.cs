namespace DVLD.Configurtions;

public class DrivingLicenseApplicationConfiguration : IEntityTypeConfiguration<DrivingLicenseApplication>
{
    public void Configure(EntityTypeBuilder<DrivingLicenseApplication> builder)
    {
        builder.HasOne(x => x.Application).WithOne(x => x.DrivingLicenseApplication).HasForeignKey<DrivingLicenseApplication>(x => x.ApplicationId);
        builder.HasOne(x => x.LicenseType).WithOne(x =>x.DrivingLicenseApplication).HasForeignKey<DrivingLicenseApplication>(x => x.LicenseTypeId);

    }
}

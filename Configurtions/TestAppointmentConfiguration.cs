namespace DVLD.Configurtions;

public class TestAppointmentConfiguration : IEntityTypeConfiguration<TestAppointment>
{
    public void Configure(EntityTypeBuilder<TestAppointment> builder)
    {
        builder.Property(x => x.PaidFees).HasPrecision(18, 2).IsRequired();

        builder.HasOne(x => x.DrivingLicenseApplication)
            .WithMany(x => x.TestAppointments)
            .HasForeignKey(x => x.DrivingLicenseApplicationId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.ApplicationUser)
            .WithMany(x => x.TestAppointments)
            .HasForeignKey(x => x.CreatedByUserId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.TestType)
            .WithMany(x => x.TestAppointments)
            .HasForeignKey(x => x.TestTypeId)
            .OnDelete(DeleteBehavior.Restrict);

        
    }

}

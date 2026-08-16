namespace DVLD.Configurtions;

public class DriverConfiguration : IEntityTypeConfiguration<Driver>
{
    public void Configure(EntityTypeBuilder<Driver> builder)
    {
        builder.HasOne(d => d.ApplicationUser)
            .WithOne()
            .HasForeignKey<Driver>(d => d.ApplicationUserId);
            
    }
}

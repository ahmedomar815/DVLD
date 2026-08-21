namespace DVLD.Configurtions;

public class DriverConfiguration : IEntityTypeConfiguration<Driver>
{
    public void Configure(EntityTypeBuilder<Driver> builder)
    {
        builder.HasOne(d => d.ApplicationUser)
            .WithOne()
            .HasForeignKey<Driver>(d => d.ApplicationUserId);

        builder.HasOne(d => d.CreatedBy)
            .WithMany()
            .HasForeignKey(d => d.CreatedById)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(d => d.UpdatedBy)
            .WithMany()
            .HasForeignKey(d => d.UpdatedById)
            .OnDelete(DeleteBehavior.Restrict);
    }
}

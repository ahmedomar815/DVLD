namespace DVLD.Configurtions;

public class LicenseConfiguration : IEntityTypeConfiguration<License>
{
    public void Configure(EntityTypeBuilder<License> builder)
    {
        builder.HasKey(x => x.LicenseNumber);
        builder.Property(x => x.Notes).HasMaxLength(50);
        builder.Property(x => x.PaidFees).HasPrecision(18, 2);
        builder.HasOne(x => x.Application)
            .WithOne()
            .HasForeignKey<License>(x => x.ApplicaitonId);

        builder.HasOne(x => x.LicenseType)
           .WithOne()
           .HasForeignKey<License>(x => x.LicenseTypeId);


        builder.HasOne(x => x.Driver)
         .WithOne()
         .HasForeignKey<License>(x => x.DriverId);

        builder.HasOne(x => x.CreatedByUser)
         .WithMany()
         .HasForeignKey(x => x.CreatedByUserId);
         



    }
}



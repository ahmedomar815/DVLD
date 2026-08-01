namespace DVLD.Configurtions;

public class LicenseTypeConfiguration : IEntityTypeConfiguration<LicenseType>
{
    public void Configure(EntityTypeBuilder<LicenseType> builder)
    {
        builder.ToTable("LicenseType", table =>
        {
            table.HasCheckConstraint("CK_LicenseTypes_MinimumAllowedAge", "[MinimumAllowedAge] >= 18");
            table.HasCheckConstraint("CK_LicenseTypes_Fees", "[Fees] >= 0");
        });

     
        builder.HasIndex(x => x.Name).IsUnique();

        builder.Property(x => x.Name)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(x => x.Description)
            .HasMaxLength(500)
            .IsRequired();

        builder.Property(x => x.MinimumAllowedAge).IsRequired();
        builder.Property(x => x.DefaultValidityLength).IsRequired();
        builder.Property(x => x.Fees)
            .HasPrecision(18, 2)
            .IsRequired();
    }
}

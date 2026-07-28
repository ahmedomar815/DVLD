using Org.BouncyCastle.Asn1.Cms.Ecc;

namespace DVLD.Configurtions;

public class LicenseTypeConfiguration : IEntityTypeConfiguration<LicenseType>
{

    public void Configure(EntityTypeBuilder<LicenseType> builder)
    {
        builder.ToTable("ApplicationTypes");

      
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name)
            .HasMaxLength(100)
            .IsRequired();
        builder.HasIndex(x => x.Name).IsUnique();
        builder.Property(x => x.Description)
            .HasMaxLength(500)
            .IsRequired();

        builder.Property(x => x.MinimumAllowedAge)
            .IsRequired();

        builder.Property(x => x.DefaultValidityLength)
            .IsRequired();

        builder.Property(x => x.Fees)
            .HasColumnType("decimal(18,2)")
            .IsRequired();
        builder.ToTable("LicenseType", t =>
        {
            t.HasCheckConstraint(
                "CK_LicenseTypes_MinimumAllowedAge",
                "[MinimumAllowedAge] >= 18"
            );
        });
        builder.ToTable("LicenseType", t =>
        {
            t.HasCheckConstraint(
                "CK_LicenseTypes_Fees",
                "[Fees] >= 0"
            );
        });
    }
}

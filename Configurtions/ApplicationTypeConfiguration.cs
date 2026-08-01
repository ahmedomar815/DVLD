namespace DVLD.Configurtions;

public class ApplicationTypeConfiguration:IEntityTypeConfiguration<ApplicationType>
{
    public void Configure(EntityTypeBuilder<ApplicationType> builder)
    {
       
        builder.HasIndex(x => x.Name)
       .IsUnique();
        builder.ToTable(t =>
        {
            t.HasCheckConstraint(
                "CK_Application_PaidFees_Positive",
                "[Fees] > 0"
            );
        });

        builder.Property(x => x.Fees)
       .HasColumnType("decimal(18,2)");
    }

}

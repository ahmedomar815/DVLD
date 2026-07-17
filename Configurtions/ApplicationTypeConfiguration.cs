namespace DVLD.Configurtions;

public class ApplicationTypeConfiguration:IEntityTypeConfiguration<ApplicationType>
{
    public void Configure(EntityTypeBuilder<ApplicationType> builder)
    {
        builder.HasKey(x => x.Id);
        builder.HasIndex(x => x.Name)
       .IsUnique();
        builder.Property(x => x.Fees)
       .HasColumnType("decimal(18,2)");
    }

}

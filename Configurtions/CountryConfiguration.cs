namespace DVLD.Configurtions;

public class CountryConfiguration : IEntityTypeConfiguration<Country>
{
    public void Configure(EntityTypeBuilder<Country> builder)
    {
       builder.HasKey(c => c.Id);
        builder.HasIndex(x => x.Name)
       .IsUnique();
        builder.Property(c => c.Name).IsRequired().HasMaxLength(100);

    }
}

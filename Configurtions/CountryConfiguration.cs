namespace DVLD.Configurtions;

public class CountryConfiguration : IEntityTypeConfiguration<Country>
{
    public void Configure(EntityTypeBuilder<Country> builder)
    {
      
        builder.HasIndex(x => x.Name)
       .IsUnique();
        builder.Property(c => c.Name).IsRequired().HasMaxLength(100);
        builder.HasData(new Country
        {
            Id = 1,
            Name = "Egypt"
        });
    }
}

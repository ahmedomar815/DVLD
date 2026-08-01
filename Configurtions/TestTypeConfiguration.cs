namespace DVLD.Configurtions;

public class TestTypeConfiguration:IEntityTypeConfiguration<TestType>
{
    public void Configure(EntityTypeBuilder<TestType> builder)
    {
        builder.HasIndex(x => x.TestTypeTitle).IsUnique();
        builder.Property(x => x.TestTypeTitle).IsRequired().HasMaxLength(100);
        builder.Property(x => x.TestTypeDescription).IsRequired().HasMaxLength(500);
        builder.Property(x => x.TestTypeFees).IsRequired().HasPrecision(18, 2);
    }

}

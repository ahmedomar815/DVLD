

namespace DVLD.Configurtions;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.Property(x => x.FirstName).HasMaxLength(100);
        builder.Property(x => x.SecondName).HasMaxLength(100);
        builder.Property(x => x.ThirdName).HasMaxLength(100);
        builder.Property(x => x.FourthName).HasMaxLength(100);
        builder.HasIndex(x => x.Email).IsUnique();
        builder.HasOne(x => x.Country)
            .WithMany(c => c.Users)
            .HasForeignKey(x => x.CountryId)
            .OnDelete(DeleteBehavior.Restrict);
        
        builder.HasOne(x => x.UsreCreated)
            .WithMany(c => c.Users)
            .HasForeignKey(x => x.CreatedById)
            .OnDelete(DeleteBehavior.Restrict);
        builder.HasIndex(x => x.NationalId).IsUnique();
    }
}

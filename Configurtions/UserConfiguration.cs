

using DVLD.Abstractions.Consts;
using Microsoft.AspNetCore.Identity;

namespace DVLD.Configurtions;

public class UserConfiguration : IEntityTypeConfiguration<ApplicationUser>
{
    public void Configure(EntityTypeBuilder<ApplicationUser> builder)
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



     

        var applicationUser = new ApplicationUser
        {
            Id = DefaultUsers.AdminId,
            FirstName = "Admin",
            SecondName = "Admin",
            ThirdName = "Admin",
            FourthName = "Admin",
            Email = DefaultUsers.AdminEmail,
            NormalizedEmail = DefaultUsers.AdminEmail.ToUpper(),
            NationalId = DefaultUsers.NationalId,
            EmailConfirmed = true,
            SecurityStamp = DefaultUsers.AdminSecurityStamp,
            ConcurrencyStamp = DefaultUsers.AdminConcurrencyStamp,
            CountryId = 1,
            PasswordHash = "AQAAAAIAAYagAAAAEEHzKyIJPL0TIfoWgyTljHrOrXksVLJsfc7WAvE8VgVuSK/rIQEDjOdG7+VbEZNtZg=="
        };
       
        builder.HasData(applicationUser);
    }
           
}

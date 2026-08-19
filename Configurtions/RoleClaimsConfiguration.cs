using DVLD.Abstractions.Consts;
using Microsoft.AspNetCore.Identity;

namespace DVLD.Configurtions;

public class RoleClaimsConfiguration : IEntityTypeConfiguration<IdentityRoleClaim<string>>
{
    public void Configure(EntityTypeBuilder<IdentityRoleClaim<string>> builder)
    {
        var roleClaims = Permissions.GetAll()
            .Select((permission, index) => new IdentityRoleClaim<string>
            {
                Id = index + 1,
                RoleId = DefaultRoles.Admin.Id,
                ClaimType = Permissions.Type,
                ClaimValue = permission
            });

        builder.HasData(roleClaims);
    }
}

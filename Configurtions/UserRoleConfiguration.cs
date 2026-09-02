using DVLD.Abstractions.Consts;
using Microsoft.AspNetCore.Identity;

namespace DVLD.Configurtions;

public class UserRoleConfiguration : IEntityTypeConfiguration<IdentityUserRole<string>>
{
    public void Configure(EntityTypeBuilder<IdentityUserRole<string>> builder)
    {
        builder.HasData(new IdentityUserRole<string> { RoleId = DefaultRoles.Admin.Id, UserId = DefaultUsers.AdminId });
    }
}

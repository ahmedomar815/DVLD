using DVLD.Abstractions.Consts;

namespace DVLD.Configurtions;

public class ApplicationRoleConfiguration : IEntityTypeConfiguration<ApplicationRole>
{
    public void Configure(EntityTypeBuilder<ApplicationRole> builder)
    {

        builder.HasData(new ApplicationRole { Id = DefaultRoles.Admin.Id, ConcurrencyStamp = DefaultRoles.Admin.ConcurrencyStamp, Name = DefaultRoles.Admin.Nanme });
        builder.HasData(new ApplicationRole { Id = DefaultRoles.Member.Id, ConcurrencyStamp = DefaultRoles.Member.ConcurrencyStamp, Name = DefaultRoles.Member.Name });
    }
}

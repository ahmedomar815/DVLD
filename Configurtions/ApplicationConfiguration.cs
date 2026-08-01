

namespace DVLD.Configurtions;

public class ApplicationConfiguration : IEntityTypeConfiguration<Application>
{
    public void Configure(EntityTypeBuilder<Application> builder)
    {
        

        builder.HasOne(x => x.User)
            .WithMany(u => u.Applications)
            .HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        builder
    .HasOne(a => a.CreatedBy)
    .WithMany(x => x.CreatedApplications)
    .HasForeignKey(a => a.CreatedById)
    .OnDelete(DeleteBehavior.Restrict);
        builder.Property(x => x.PaidFees)
        .HasColumnType("decimal(10,2)")
        .IsRequired();

        builder.ToTable(t =>
        {
            t.HasCheckConstraint(
                "CK_Application_PaidFees_Positive",
                "[PaidFees] > 0"
            );
        });

        builder
            .HasOne(a => a.UpdatedBy)
            .WithMany(x => x.UpdatedApplications)
            .HasForeignKey(a => a.UpdatedById)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.ApplicationType)
     .WithMany(x=>x.Applications)
     .HasForeignKey(x => x.ApplicationTypeId)
     .OnDelete(DeleteBehavior.Restrict);

    }


}

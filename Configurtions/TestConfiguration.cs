namespace DVLD.Configurtions;

public class TestConfiguration : IEntityTypeConfiguration<Test>
{
    public void Configure(EntityTypeBuilder<Test> builder)
    {
        builder.Property(x => x.Notes).HasMaxLength(500);

        builder.HasOne(x => x.TestAppointment)
            .WithOne(x => x.Test)
            .HasForeignKey<Test>(x => x.TestAppointmentId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.ApplicationUser)
            .WithMany(x => x.Tests)
            .HasForeignKey(x => x.CreatedByUserId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}

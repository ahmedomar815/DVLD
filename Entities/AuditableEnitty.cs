namespace DVLD.Entities;

public class AuditableEnitty
{
    public ApplicationUser CreatedBy { get; set; } = default!;
    public string CreatedById { get; set; } = default!;

    public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
    public ApplicationUser? UpdatedBy { get; set; }
    public string? UpdatedById { get; set; }
    public DateTime? UpdatedOn { get; set; }


}

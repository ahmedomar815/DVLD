namespace DVLD.Entities;

public class AuditableEnitty
{
    public User CreatedBy { get; set; } = default!;
    public string CreatedById { get; set; } = default!;

    public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
    public User? UpdatedBy { get; set; }
    public string? UpdatedById { get; set; }
    public DateTime? UpdatedOn { get; set; }


}

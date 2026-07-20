

public class Application: AuditableEnitty
{
    public Application()
    {
        Id = Guid.CreateVersion7().ToString();
    }
    public string Id { get; set; } = default!;
    public string UserId { get; set; }= default!;
    public int ApplicationTypeId { get; set; } = default;
    public ApplicationStatus Status { get; set; } = ApplicationStatus.Pending;
    public ApplicationType ApplicationType { get; set; } = default!;
    public decimal PaidFees { get; set; } = default!;
    public User User { get; set; } = default!;
        
}

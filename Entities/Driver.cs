namespace DVLD.Entities;

public class Driver: AuditableEnitty
{
    public Driver()
    {
        Id = Guid.CreateVersion7().ToString();
    }
    public string Id { get; set; }
    public string ApplicationUserId { get; set; } = null!;
    public ICollection<License> Licenses { get; set; } = new List<License>();
    public ApplicationUser ApplicationUser { get; set; } = null!;
}

namespace DVLD.Entities;

public class Driver
{
    public Driver()
    {
        Id = Guid.CreateVersion7().ToString();
    }
    public string Id { get; set; }
    public string UserId { get; set; } = null!;
    public DateTime DateTimeCreated { get; set; } = DateTime.Now;
    public string ApplicationUserId { get; set; } = null!;
    public ApplicationUser User { get; set; } = null!;
}

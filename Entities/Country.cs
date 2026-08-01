namespace DVLD.Entities;

public class Country
{
    public string Name { get; set; } = default!;
    public  int Id { get; set; } = default;
    public ICollection<ApplicationUser> Users{ get; set; } = new List<ApplicationUser>();
}

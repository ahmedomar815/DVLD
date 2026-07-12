namespace DVLD.Entities;

public class Country
{
    public string Name { get; set; } = default!;
    public  int Id { get; set; } = default;
    public ICollection<User> Users{ get; set; } = new List<User>();
}

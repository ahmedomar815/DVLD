
using DVLD.Entities;

public class User/*:IdentityUser<string>*/
{
  public string Id { get; set; } = Guid.NewGuid().ToString();
    public string FirstName { get; set; } = default!;
    public string SecondName { get; set; } = default!;
    public string ThirdName { get; set; } = default!;
    public string FourthName { get; set; } = default!;
    public string Email { get; set; } = default!;
    public string NationalId { get; set; } = default!;
    public bool IsDisabled { get; set; } = default;
    public string Password { get; set; } = default!;
    public int CountryId { get; set; } = default;
    public string ?CreatedById { get; set; } = default;
    public User ?UsreCreated { get; set; } = default!;
    public Country Country { get; set; } = default!;
    public ICollection<Application> Applications{ get;set; } = new List<Application>();
    public ICollection<User> Users{ get;set;} = new List<User>();
    public ICollection<RefreshToken> RefreshTokens { get; set; } = new List<RefreshToken>();
    public ICollection<Application> CreatedApplications { get; set; }
        = new List<Application>();

    public ICollection<Application> UpdatedApplications { get; set; }
        = new List<Application>();
}


using Microsoft.AspNetCore.Identity;

public class ApplicationUser : IdentityUser<string>
{
     public ApplicationUser()
     {
        Id = Guid.CreateVersion7().ToString();
        
     }
    public string FirstName { get; set; } = default!;
    public string SecondName { get; set; } = default!;
    public string ThirdName { get; set; } = default!;
    public string FourthName { get; set; } = default!;
    
    public string NationalId { get; set; } = default!;
    public bool IsDisabled { get; set; } = default;
    public int CountryId { get; set; } = default;
    public string ?CreatedById { get; set; } = default;
    public ApplicationUser ?UsreCreated { get; set; } = default!;
    public Country Country { get; set; } = default!;
    public ICollection<Application> Applications{ get;set; } = new List<Application>();
    public ICollection<ApplicationUser> Users{ get;set;} = new List<ApplicationUser>();
    public ICollection<RefreshToken> RefreshTokens { get; set; } = new List<RefreshToken>();
    public ICollection<TestAppointment> TestAppointments { get; set; } = [];

    public ICollection<TestAppointment> OwnedAppointments { get; set; }
        = [];
    public ICollection<Test> Tests { get; set; } = [];
    public ICollection<Application> CreatedApplications { get; set; }
        = new List<Application>();

    public ICollection<Application> UpdatedApplications { get; set; }
        = new List<Application>();
}

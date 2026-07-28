
using System.Reflection;
using System.Security.Claims;

namespace DVLD.Persistence;

public class ApplicationDbContext(IHttpContextAccessor httpContextAccessor, DbContextOptions<ApplicationDbContext> options) :DbContext(options)
{
    private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;
    private readonly DbContextOptions<ApplicationDbContext> _options = options;

    public DbSet<User> Users { get; set; }
    public DbSet<Application> Applications { get; set; }

    public DbSet<Country>Countries { get; set; }

    public DbSet<ApplicationType>ApplicationTypes { get; set; }

    public DbSet<DrivingLicenseApplication> DrivingLicenseApplications { get; set; }
    public DbSet<LicenseType> LicenseTypes { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var entires = ChangeTracker.Entries<AuditableEnitty>();
        var userId=_httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
        foreach (var entry in entires)
        {
            if (entry.State == EntityState.Added)
            {
                entry.Entity.CreatedOn = DateTime.UtcNow;
                entry.Entity.CreatedById = userId!;
            }
            else if (entry.State == EntityState.Modified)
            {
                entry.Entity.UpdatedOn = DateTime.UtcNow;
                entry.Entity.UpdatedById = userId!;
            }
        }
        return base.SaveChangesAsync(cancellationToken);
    }


}

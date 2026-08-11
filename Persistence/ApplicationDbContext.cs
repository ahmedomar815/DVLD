
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System.Reflection;
using System.Security.Claims;

namespace DVLD.Persistence;

public class ApplicationDbContext(IHttpContextAccessor httpContextAccessor, DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<ApplicationUser, ApplicationRole,string>(options)
{
    private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;
    


    public DbSet<Application> Applications { get; set; }

    public DbSet<Country>Countries { get; set; }
    public DbSet<RefreshToken> RefreshTokens { get; set; }
    public DbSet<ApplicationType>ApplicationTypes { get; set; }

    public DbSet<DrivingLicenseApplication> DrivingLicenseApplications { get; set; }
    public DbSet<LicenseType> LicenseTypes { get; set; }
    public DbSet<TestType> TestTypes { get; set; }
    public DbSet<TestAppointment> TestAppointments { get; set; }
    public DbSet<Test> Tests { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
       
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        var cascadeFks = modelBuilder.Model.GetEntityTypes().SelectMany(t => t.GetForeignKeys())
              .Where(fk => fk.DeleteBehavior == DeleteBehavior.Cascade && !fk.IsOwnership);
        foreach (var fk in cascadeFks)
        {
            fk.DeleteBehavior = DeleteBehavior.Restrict;
        }
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

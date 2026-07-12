
using DVLD.Persistence;

public static class DependencyInjection
{
    public static IServiceCollection AddDependcies(this IServiceCollection services,IConfiguration configuration)
    {
        var connectionString =
        configuration.GetConnectionString("DefaultConnection")
         ?? throw new InvalidOperationException("Connection string"
         + "'DefaultConnection' not found.");




        services.AddDbContext<ApplicationDbContext>(options =>
        options.UseSqlServer(connectionString));
        services.AddControllers();
        //services.AddOpenApi();
        services.AddHttpContextAccessor();
        return services;
    }
   
}

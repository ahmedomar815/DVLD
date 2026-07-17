
using DVLD.Auth;
using DVLD.Persistence;
using DVLD.Services;
using Mapster;
using MapsterMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Reflection;
using System.Text;

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
        services.AddScoped<IAuthServices,AuthServices>();
        services.AddScoped<IJwtProvider, JwtProvider>();
        services.AddScoped<IApplicationTypeService, ApplicationTypeService>();
        services.AddOpenConfigApi();
        services.AddMapsterConfig();
        services.AddAuthCofig(configuration);
        return services;
    }
    private static IServiceCollection AddAuthCofig(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddOptions<JwtOptions>().BindConfiguration(JwtOptions.SectionName).ValidateDataAnnotations().ValidateOnStart();
        var jwtSettings = configuration.GetSection(JwtOptions.SectionName).Get<JwtOptions>();

        services.AddAuthentication(options =>
        {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
      .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme ,options =>
       {

           options.TokenValidationParameters = new TokenValidationParameters
           {
            ValidateIssuer = true,
            ValidateAudience = true,
               ValidateLifetime = true,
               ValidateIssuerSigningKey = true,
               IssuerSigningKey = new SymmetricSecurityKey (Encoding.UTF8.GetBytes(jwtSettings!.Key)),
               ValidIssuer = jwtSettings.Issuer,
               ValidAudience = jwtSettings.Audience,
           };

   
});
        return services;
    }
    private static IServiceCollection AddMapsterConfig(this IServiceCollection services)
    {
        var mappingconfig = TypeAdapterConfig.GlobalSettings;
        mappingconfig.Scan(Assembly.GetExecutingAssembly());
        services.AddSingleton<IMapper>(implementationInstance: new Mapper(mappingconfig));
        return services;
    }
    private static IServiceCollection AddOpenConfigApi(this IServiceCollection services)
    {
       // services.AddOpenApi(); 
        return services;
    }
}

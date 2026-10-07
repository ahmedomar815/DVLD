using DVLD.Auth;
using DVLD.Infrastructure;
using Hangfire;

using MapsterMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Reflection;
using System.Threading.RateLimiting;
public static class DependencyInjection
{
    public static IServiceCollection AddDependcies(this IServiceCollection services, IConfiguration configuration)
    {
        DVLD.ApplicationDependencyInjection.AddApplication(services);
        services.AddMediatR(configuration => configuration.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly));
        services.AddInfrastructure(configuration);
        services.AddControllers();
        services.AddHttpContextAccessor();
        
        services.AddOpenConfigApi();
        services.AddMapsterConfig();
        services.AddAuthCofig(configuration);
        services.AddExceptionHandler<GlobalExceptionHandler>();
        services.AddSingleton<IAuthorizationPolicyProvider, PermissionAuthorizationPolicyProvider>();
        services.AddScoped<IAuthorizationHandler, PermissionAuthorizationHandler>();
      
        services.AddProblemDetails();
        services.AddRateLimiter();
        services.AddDistributedMemoryCache();
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
      .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
       {
           options.SaveToken = true;
           options.TokenValidationParameters = new TokenValidationParameters
           {
               ValidateIssuer = true,
               ValidateAudience = true,
               ValidateLifetime = true,
               ValidateIssuerSigningKey = true,
               IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings!.Key)),
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
    private static IServiceCollection AddRateLimiter(this IServiceCollection services)
    {
        services.AddRateLimiter(options =>
        {
            options.RejectionStatusCode = StatusCodes.Status429TooManyRequests;
            options.AddPolicy("IpLimiter", httpContext =>
                RateLimitPartition.GetFixedWindowLimiter(
                    partitionKey: httpContext.Connection.RemoteIpAddress?.ToString() ?? "unknown",
                    factory: _ => new FixedWindowRateLimiterOptions
                    {
                        PermitLimit = 3,
                        Window = TimeSpan.FromMinutes(1)
                    }));
        });
        return services;
    }
    private static IServiceCollection AddOpenConfigApi(this IServiceCollection services)
    {
        services.AddOpenApi();
        services.AddAuthorization(o => o.AddPolicy("ApiTesterPolicy", b => b.RequireRole("tester")));
        return services;
    }
}

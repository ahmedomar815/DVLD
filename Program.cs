using Microsoft.IdentityModel.Logging;
using Scalar.AspNetCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDependcies(builder.Configuration);
builder.Host.UseSerilog((context, config) =>
{
    config.ReadFrom.Configuration(context.Configuration);
});

var app = builder.Build();
IdentityModelEventSource.ShowPII = app.Environment.IsDevelopment();

if (app.Environment.IsDevelopment())
{
     app.MapOpenApi();
     
}

app.MapOpenApi()
 .RequireAuthorization("ApiTesterPolicy");
app.MapHealthChecks("health");
app.UseHttpsRedirection();
app.UseRateLimiter();
app.UseExceptionHandler();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.MapOpenApi("/openapi/{documentName}");
app.Run();

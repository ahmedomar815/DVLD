using Microsoft.IdentityModel.Logging;
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
   // app.MapOpenApi();
}

/*app.UseHttpsRedirection();*/
app.UseExceptionHandler();
app.UseAuthentication();
app.MapControllers();
app.UseAuthorization();

app.Run();

using Profiles.Domain.Interfaces;
using Profiles.Persistence;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;


services.AddControllers();

services.AddRepositories();

var app = builder.Build();

app.UseRouting();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.Run();

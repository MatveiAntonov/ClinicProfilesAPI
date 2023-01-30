using Profiles.WebApi.Mappings;
using Profiles.Application.Services;
using Profiles.WebApi.Extensions;
using Profiles.Domain.Interfaces.Services;
using Profiles.Persistence;
using Profiles.Persistence.Contexts;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;

services.AddSingleton<ProfilesDbContext>();

services.AddRepositories();

services.AddScoped<IDoctorService, DoctorService>();
services.AddScoped<IPatientService, PatientService>();
services.AddScoped<IReceptionistService, ReceptionistService>();

services.AddAutoMapper(typeof(MappingProfile));

services.AddControllers();


var app = builder.Build();

app.UseRouting();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.ConfigureExceptionHandler();

app.Run();

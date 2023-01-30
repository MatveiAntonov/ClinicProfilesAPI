using Microsoft.Extensions.DependencyInjection;
using Profiles.Domain.Interfaces.Repositories;
using Profiles.Persistence.Repositories;

namespace Profiles.Persistence
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IDoctorRepository, DoctorRepository>();
            services.AddScoped<IPatientRepository, PatientRepository>();
            services.AddScoped<IReceptionistRepository, ReceptionistRepository>();
            return services;
        }
    }
}

using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace Profiles.Persistence.Contexts
{
    public class ProfilesDbContext
    {
        private readonly IConfiguration _configuration;

        public ProfilesDbContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IDbConnection CreateConnection()
            => new SqlConnection(_configuration.GetConnectionString("ProfilesConnection"));
    }
}

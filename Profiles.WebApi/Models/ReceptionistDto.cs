using Profiles.Domain.Entities.ForeignEntities;

namespace Profiles.WebApi.Models
{
    public class ReceptionistDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = String.Empty;
        public string LastName { get; set; } = String.Empty;
        public string MiddleName { get; set; } = String.Empty;
        public Office? Office { get; set; }
        public Account Account { get; set; } = new();
    }
}

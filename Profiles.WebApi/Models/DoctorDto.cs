using Profiles.Domain.Entities.ForeignEntities;

namespace Profiles.WebApi.Models
{
    public class DoctorDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = String.Empty;
        public string LastName { get; set; } = String.Empty;
        public string MiddleName { get; set; } = String.Empty;
        public DateTime DateOfBirth { get; set; }
        public DateTime? CareerStartYear { get; set; }
        public int Status { get; set; }
        public Office? Office { get; set; }
        public Specialization Specialization { get; set; } = new();
        public Account Account { get; set; } = new();
    }
}

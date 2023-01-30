using Profiles.Domain.Entities.ForeignEntities;

namespace Profiles.WebApi.Models.DTOs
{
    public class DoctorDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string MiddleName { get; set; } = string.Empty;
        public DateTime DateOfBirth { get; set; }
        public DateTime? CareerStartYear { get; set; }
        public string Status { get; set; }
        public int OfficeId { get; set; }
        public int SpecializationId { get; set; } = new();
        public int AccountId { get; set; } = new();
    }
}

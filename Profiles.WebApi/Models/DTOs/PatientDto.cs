using Profiles.Domain.Entities.ForeignEntities;

namespace Profiles.WebApi.Models.DTOs
{
    public class PatientDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string MiddleName { get; set; } = string.Empty;
        public DateTime DateOfBirth { get; set; }
        public int AccountId { get; set; } = new();
    }
}

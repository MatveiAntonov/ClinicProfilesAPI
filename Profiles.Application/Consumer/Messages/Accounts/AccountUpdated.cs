using Profiles.Domain.Entities.ForeignEntities;

namespace Events
{
    public class AccountUpdated
    {
        public string Id { get; set; }
        public string PhoneNumber { get; set; } = String.Empty;
        public string Url { get; set; } = String.Empty;
    }
}

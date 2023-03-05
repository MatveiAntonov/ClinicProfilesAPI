namespace Events
{
    public class DoctorDeleted
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string? MiddleName { get; set; } = string.Empty;
        public string SpecializationName { get; set; } = string.Empty;
        public string PhotoUrl { get; set; } = string.Empty;
        public string? OfficeName { get; set; } = string.Empty;
        public string OfficeCity { get; set; } = string.Empty;
        public string OfficeRegion { get; set; } = string.Empty;
        public string OfficeStreet { get; set; } = string.Empty;
        public string? OfficePostalCode { get; set; } = string.Empty;
        public int OfficeHouseNumber { get; set; }
    }
}

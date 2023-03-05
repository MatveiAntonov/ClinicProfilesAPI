using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Profiles.Domain.Entities.ForeignEntities
{
    public class Office
    {
        public int Id { get; set; }
        public string? OfficeName { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string Region { get; set; } = string.Empty;
        public string Street { get; set; } = string.Empty;
        public string PostalCode { get; set; } = string.Empty;
        public int HouseNumber { get; set; }
        public string PhotoUrl { get; set; } = String.Empty;
    }
}

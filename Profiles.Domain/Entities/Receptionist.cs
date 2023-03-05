using Profiles.Domain.Entities.ForeignEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Profiles.Domain.Entities
{
    public class Receptionist
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = String.Empty;
        public string LastName { get; set; } = String.Empty;
        public string? MiddleName { get; set; } = String.Empty;
        public int OfficeId { get; set; }
        public Office? Office { get; set; }
        public int AccountId { get; set; }
        public Account Account { get; set; } = new();
    }
}

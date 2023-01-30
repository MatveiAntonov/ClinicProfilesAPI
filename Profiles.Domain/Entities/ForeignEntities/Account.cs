using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Profiles.Domain.Entities.ForeignEntities
{
    public class Account
    {
        public int Id { get; set; }
        public string PhoneNumber { get; set; } = String.Empty;
        public int PhotoId { get; set; }
        public Photo? Photo { get; set; } = new();
    }
}

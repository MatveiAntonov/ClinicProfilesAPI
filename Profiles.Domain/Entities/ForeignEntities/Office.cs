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
        public string OfficeName { get; set; } = string.Empty;
    }
}

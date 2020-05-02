using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace smart_tour_api.Entities
{
    public class Indication:BaseEntity
    {
        public string Text { get; set; }
        public ICollection<LiveTourZone> LiveTourZones { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace smart_tour_api.Entities
{
    public class LiveTour : Tour
    {
        public ICollection<LiveTourZone> LiveTourZones { get; set; }
    }
}

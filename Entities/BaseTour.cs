using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace smart_tour_api.Entities
{
    public class BaseTour:Tour
    {
        public ICollection<BaseTourZone> BaseTourZones { get; set; }
    }
}

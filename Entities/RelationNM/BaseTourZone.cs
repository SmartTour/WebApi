using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace smart_tour_api.Entities
{
    public class BaseTourZone:BaseEntity
    {
        public int ContentID { get; set; }
        public Content Content { get; set; }
        public int BaseTourID { get; set; }
        public BaseTour BaseTour { get; set; }

        public int Order { get; set; }
    }
}

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace smart_tour_api.Entities
{
    public class BaseTourZone:EntityOfAgency
    {
        public int ContentID { get; set; }
        public Content Content { get; set; }
        public int BaseTourID { get; set; }

        public int Order { get; set; }
    }
}

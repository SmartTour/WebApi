using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace smart_tour_api.Entities
{
    public class ExternalMedia: EntityOfAgency
    {
        public string Name { get; set; }
        public string Url { get; set; }
        public string Type { get; set; }
    }
}

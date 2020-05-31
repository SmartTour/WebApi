using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace smart_tour_api.Entities
{
    public class Content:EntityOfAgency
    {
        public string Title { get; set; }
        public string Type { get; set; }
        public string UrlContent { get; set; }
        public string ContentHtml { get; set; }
        public string UrlImage { get; set; }
        public ICollection<BaseTourZone> BaseTourZones { get; set; }
        public ICollection<LiveTourZone> LiveTourZones { get; set; }

    }
}

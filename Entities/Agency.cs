

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace smart_tour_api.Entities
{
    public class Agency:BaseEntity
    {
        [Required]
        public string Name { get; set; }
        public string Code { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }

        public ICollection<User> Users { get; set; }
        public ICollection<BaseTour> BaseTours { get; set; }
        public ICollection<LiveTour> LiveTours { get; set; }
        public ICollection<Content> Contents { get; set; }
        public ICollection<DetectionElement> DetectionElements { get; set; }

    }
}

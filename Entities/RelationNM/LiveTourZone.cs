using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace smart_tour_api.Entities
{
    public class LiveTourZone: EntityOfAgency
    {
        public int LiveTourID { get; set; }
        public LiveTour LiveTour { get; set; }
        public int DetectionElementID { get; set; }
        public DetectionElement DetectionElement { get; set; }
        public int NextIndicationID { get; set; }
        public Indication NextIndication { get; set; }
        public int ContentID { get; set; }
        public Content Content { get; set; }

        public int Order { get; set; }

    }
}

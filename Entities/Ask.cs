using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace smart_tour_api.Entities
{
    public class Ask:EntityOfAgency
    {
        [Required]
        public string Question { get; set; }
        public string Answer { get; set; }

    }
}

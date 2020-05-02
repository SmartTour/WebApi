using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace smart_tour_api.Entities
{
    public class BaseEntity
    {
        public int Id { get; set; }

        public DateTime LastModifiedDate { get; set; }

        public DateTime CreationDate { get; set; }
    }
}

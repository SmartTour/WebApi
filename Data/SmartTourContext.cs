using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using smart_tour_api.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace smart_tour_api.Data
{
    public class SmartTourContext : DbContext
    {
        public SmartTourContext(DbContextOptions<SmartTourContext> options)
            : base(options)
        {
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Agency> Agencies { get; set; }
        
    }
}

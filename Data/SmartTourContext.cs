
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using smart_tour_api.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace smart_tour_api.Data
{
    public class SmartTourContext : DbContext
    {
        public SmartTourContext(DbContextOptions<SmartTourContext> options)
            : base(options)
        {
        }
        //Entities
        public DbSet<User> Users { get; set; }
        public DbSet<Agency> Agencies { get; set; }
        public DbSet<BaseTour> BaseTours { get; set; }
        public DbSet<LiveTour> LiveTours { get; set; }
        public DbSet<Phrase> Phrases { get; set; }
        public DbSet<Content> Contents { get; set; }
        public DbSet<ExternalMedia> ExternalMedias { get; set; }
        public DbSet<Indication> Indications { get; set; }
        public DbSet<DetectionElement> DetectionElements { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //BaseTourZone
            modelBuilder.Entity<BaseTourZone>()
                .HasOne(entity => entity.BaseTour)
                .WithMany(relationEntity => relationEntity.BaseTourZones)
                .HasForeignKey(entity => entity.BaseTourID)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<BaseTourZone>()
                .HasOne(entity => entity.Content)
                .WithMany(relationEntity => relationEntity.BaseTourZones)
                .HasForeignKey(entity => entity.ContentID)
                .OnDelete(DeleteBehavior.NoAction);

            //LiveTourZone
            modelBuilder.Entity<LiveTourZone>()
                .HasOne(entity => entity.LiveTour)
                .WithMany(relationEntity => relationEntity.LiveTourZones)
                .HasForeignKey(entity => entity.LiveTourID)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<LiveTourZone>()
                .HasOne(entity => entity.Content)
                .WithMany(relationEntity => relationEntity.LiveTourZones)
                .HasForeignKey(entity => entity.ContentID)
                .OnDelete(DeleteBehavior.NoAction);
            
            modelBuilder.Entity<LiveTourZone>()
                .HasOne(entity => entity.DetectionElement)
                .WithMany(relationEntity => relationEntity.LiveTourZones)
                .HasForeignKey(entity => entity.DetectionElementID)
                .OnDelete(DeleteBehavior.NoAction);
            
            modelBuilder.Entity<LiveTourZone>()
                .HasOne(entity => entity.NextIndication)
                .WithMany(relationEntity => relationEntity.LiveTourZones)
                .HasForeignKey(entity => entity.NextIndicationID)
                .OnDelete(DeleteBehavior.NoAction);
        }
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var entries = ChangeTracker
        .Entries()
        .Where(e => e.Entity is BaseEntity && (
                e.State == EntityState.Added
                || e.State == EntityState.Modified));

            foreach (var entityEntry in entries)
            {
                ((BaseEntity)entityEntry.Entity).LastModifiedDate = DateTime.Now;

                if (entityEntry.State == EntityState.Added)
                {
                    ((BaseEntity)entityEntry.Entity).CreationDate = DateTime.Now;
                }
            }
            return base.SaveChangesAsync(cancellationToken);
        }
    }
}

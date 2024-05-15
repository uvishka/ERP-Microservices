using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*namespace ERP.TimeTable.DataService.Data
{
    internal class AppDbContext
    {
    }
}
-*/
using Microsoft.EntityFrameworkCore;
using System;
using ERP.TimeTable.Core.Entities;

namespace ERP.TimeTable.DataService.Data
{
    public class AppDbContext : DbContext
    {
        // Define the DB Entities
        public virtual DbSet<Day> Days { get; set; }
        public virtual DbSet<Module> Modules { get; set; }
        public virtual DbSet<LectureHall> LectureHalls { get; set; }
        public virtual DbSet<TimeSlot> TimeSlots { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Define relationships between entities

            modelBuilder.Entity<TimeSlot>(entity =>
            {
                entity.HasOne(ts => ts.Day)
                      .WithMany(d => d.TimeSlots)
                      .HasForeignKey(ts => ts.DayId)
                      .OnDelete(DeleteBehavior.NoAction)
                      .HasConstraintName("FK_TimeSlots_Day");
            });

            modelBuilder.Entity<TimeSlot>(entity =>
            {
                entity.HasOne(ts => ts.Module)
                      .WithMany(m => m.TimeSlots)
                      .HasForeignKey(ts => ts.ModuleId)
                      .OnDelete(DeleteBehavior.NoAction)
                      .HasConstraintName("FK_TimeSlots_Module");
            });

            modelBuilder.Entity<TimeSlot>(entity =>
            {
                entity.HasOne(ts => ts.LectureHall)
                      .WithMany(lh => lh.TimeSlots)
                      .HasForeignKey(ts => ts.LectureHallId)
                      .OnDelete(DeleteBehavior.NoAction)
                      .HasConstraintName("FK_TimeSlots_LectureHall");
            });
        }
    }
}

using Microsoft.EntityFrameworkCore;
using ERP.UserManagement.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.UserManagement.DataServices
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //modelBuilder.Entity<Student>()
            //    .HasOne(s => s.Advisor)
            //    .WithMany(a => a.Advicees)
            //    .HasForeignKey(s => s.AdvisorId)
            //    .OnDelete(DeleteBehavior.NoAction);



        }
        public virtual DbSet<Student> Students { get; set; }
        public virtual DbSet<Lecturer> Lecturers { get; set; }
    }
}

using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ERP.ModuleManagement.Core.Entities;

namespace ERP.ModuleManagement.DataSevices.Data
{

    public class AppDbContext : DbContext
    {
        public virtual DbSet<Module> Modules { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        { }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);


        }

    }
}

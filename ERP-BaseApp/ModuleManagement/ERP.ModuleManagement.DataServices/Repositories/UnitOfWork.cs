using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ERP.ModuleManagement.DataSevices.Data;
using ERP.ModuleManagement.DataSevices.Repositories;
using ERP.ModuleManagement.DataSevices.Repositories.Interfaces;
using Microsoft.Extensions.Logging;
 



namespace ERP.ModuleManagement.DataServices.Repositories
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly AppDbContext _context;

        public IModuleRepositary Modules { get; }

        public UnitOfWork(AppDbContext context, ILoggerFactory loggerFactory)
        {
            _context = context;
            var logger = loggerFactory.CreateLogger("logs");

            Modules = new ModuleRepository(_context, logger);


        }
        public async Task<bool> CompleteAsync()
        {
            var result = await _context.SaveChangesAsync();
            return result > 0;
        }
        public void Dispose()
        {
            _context.Dispose();
        }

    }
}

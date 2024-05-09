using ERP.UserManagement.DataServices.Repositories.Interfaces;
using Microsoft.Extensions.Logging;

using ERP.UserManagement.DataServices.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ERP.UserManagement.DataService.Repositories;

namespace ERP.UserManagement.DataServices.Repositories
{
    public class UnitOfWorks : IUnitOfWorks, IDisposable
    {
        private readonly AppDbContext _context;

        public IStudentRepository Students { get; }

        public ILecturerRepository Lecturers { get; }

        public UnitOfWorks(AppDbContext context, ILoggerFactory loggerFactory)
        {
            _context = context;
            var logger = loggerFactory.CreateLogger("logs");

            Students = new StudentRepository(_context, logger);
            Lecturers = new LecturerRepository(_context, logger);


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

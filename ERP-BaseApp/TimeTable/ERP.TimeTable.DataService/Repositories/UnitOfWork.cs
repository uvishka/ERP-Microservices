using ERP.TimeTable.DataService.Repositories.Interfaces;
using ERP.TimeTable.DataService.Data;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.TimeTable.DataService.Repositories
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        public readonly AppDbContext _context;

        public IModuleRepository Module {  get;  }
        public ITimeSlotRepository TimeSlot { get; }
        public ILectureHallRepository LectureHall { get; }
        public IDayRepository Day {  get; }

        public UnitOfWork (AppDbContext context, ILoggerFactory loggerFactory)
        {
            _context = context;
            var logger = loggerFactory.CreateLogger("logs");

            Module = new ModuleRepository(_context, logger);
            Day = new DayRepository(_context, logger);
            TimeSlot = new TimeSlotsRepository(_context, logger);
            LectureHall = new LectureHallRepository(_context, logger);
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

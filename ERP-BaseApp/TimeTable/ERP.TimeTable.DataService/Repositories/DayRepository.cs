using ERP.TimeTable.Core.Entities;
using ERP.TimeTable.DataService.Repositories.Interfaces;
using ERP.TimeTable.DataService.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERP.TimeTable.DataService.Repositories
{
    public class DayRepository : GenericRepository<Day>, IDayRepository
    {

        public DayRepository(AppDbContext context, ILogger logger) : base(context, logger)
        {
        }

        public async Task<Day> GetDayAsync(Guid id)
        {
            try
            {
                var result = await _dbSet
                    .Include(d => d.TimeSlots)
                    .FirstOrDefaultAsync(d => d.Id == id);

                return result;
            }
            catch (Exception e)
            {
                _logger.LogError(e, "{repo} GetDayAsync function error", typeof(DayRepository));
                throw;
            }
        }

        public override async Task<IEnumerable<Day>> All()
        {
            try
            {
                return await _dbSet
                    .Include(d => d.TimeSlots)
                    .Where(d => d.Status == 1)
                    .AsNoTracking()
                    .AsSplitQuery()
                    .OrderBy(d => d.CreatedDate)
                    .ToListAsync();
            }
            catch (Exception e)
            {
                _logger.LogError(e, "{repo} All function error", typeof(DayRepository));
                throw;
            }
        }

        public override async Task<bool> Delete(Guid id)
        {
            try
            {
                var result = await _dbSet.FirstOrDefaultAsync(d => d.Id == id);

                if (result == null)
                    return false;

                result.Status = 0;
                result.UpdatedDate = DateTime.UtcNow;

                _dbSet.Update(result);
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception e)
            {
                _logger.LogError(e, "{repo} Delete function error", typeof(DayRepository));
                throw;
            }
        }

        public override async Task<bool> Update(Day day)
        {
            try
            {
                var result = await _dbSet.FirstOrDefaultAsync(d => d.Id == day.Id);

                if (result == null)
                    return false;

                result.Name = day.Name;
                result.UpdatedDate = DateTime.UtcNow;

                _dbSet.Update(result);
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception e)
            {
                _logger.LogError(e, "{repo} Update function error", typeof(DayRepository));
                throw;
            }
        }
    }
}

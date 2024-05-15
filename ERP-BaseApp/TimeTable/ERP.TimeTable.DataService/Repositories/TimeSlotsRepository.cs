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
    public class TimeSlotsRepository : GenericRepository<TimeSlot>, ITimeSlotRepository
    {

        public TimeSlotsRepository(AppDbContext context, ILogger logger) : base(context, logger)
        {
        }

        public async Task<TimeSlot> GetTimeSlotAsync(Guid id)
        {
            try
            {
                var result = await _dbSet
                    .Include(ts => ts.Day)
                    .Include(ts => ts.Module)
                    .Include(ts => ts.LectureHall)
                    .FirstOrDefaultAsync(ts => ts.Id == id);

                return result;
            }
            catch (Exception e)
            {
                _logger.LogError(e, "{repo} GetTimeSlotAsync function error", typeof(TimeSlotsRepository));
                throw;
            }
        }

        public override async Task<IEnumerable<TimeSlot>> All()
        {
            try
            {
                return await _dbSet
                    .Include(ts => ts.Day)
                    .Include(ts => ts.Module)
                    .Include(ts => ts.LectureHall)
                    .Where(ts => ts.Status == 1)
                    .AsNoTracking()
                    .AsSplitQuery()
                    .OrderBy(ts => ts.CreatedDate)
                    .ToListAsync();
            }
            catch (Exception e)
            {
                _logger.LogError(e, "{repo} All function error", typeof(TimeSlotsRepository));
                throw;
            }
        }

        public override async Task<bool> Delete(Guid id)
        {
            try
            {
                var result = await _dbSet.FirstOrDefaultAsync(ts => ts.Id == id);

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
                _logger.LogError(e, "{repo} Delete function error", typeof(TimeSlotsRepository));
                throw;
            }
        }

        public override async Task<bool> Update(TimeSlot timeSlot)
        {
            try
            {
                var result = await _dbSet.FirstOrDefaultAsync(ts => ts.Id == timeSlot.Id);

                if (result == null)
                    return false;

                result.StartTime = timeSlot.StartTime;
                result.EndTime = timeSlot.EndTime;
                result.DayId = timeSlot.DayId;
                result.ModuleId = timeSlot.ModuleId;
                result.LectureHallId = timeSlot.LectureHallId;
                result.UpdatedDate = DateTime.UtcNow;

                _dbSet.Update(result);
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception e)
            {
                _logger.LogError(e, "{repo} Update function error", typeof(TimeSlotsRepository));
                throw;
            }
        }
    }
}

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
    public class LectureHallRepository : GenericRepository<LectureHall>, ILectureHallRepository
    {
      /*  public LectureHallRepository(AppDbContext context, ILogger<LectureHallRepository> logger) : base(context, logger)
        {
        }*/

        public LectureHallRepository(AppDbContext context, ILogger logger) : base(context, logger)
        {
        }

        public async Task<LectureHall> GetLectureHallAsync(Guid id)
        {
            try
            {
                var result = await _dbSet
                    .Include(lh => lh.TimeSlots)
                    .FirstOrDefaultAsync(lh => lh.Id == id);

                return result;
            }
            catch (Exception e)
            {
                _logger.LogError(e, "{repo} GetLectureHallAsync function error", typeof(LectureHallRepository));
                throw;
            }
        }

        public override async Task<IEnumerable<LectureHall>> All()
        {
            try
            {
                return await _dbSet
                    .Include(lh => lh.TimeSlots)
                    .Where(lh => lh.Status == 1)
                    .AsNoTracking()
                    .AsSplitQuery()
                    .OrderBy(lh => lh.CreatedDate)
                    .ToListAsync();
            }
            catch (Exception e)
            {
                _logger.LogError(e, "{repo} All function error", typeof(LectureHallRepository));
                throw;
            }
        }

        public override async Task<bool> Delete(Guid id)
        {
            try
            {
                var result = await _dbSet.FirstOrDefaultAsync(lh => lh.Id == id);

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
                _logger.LogError(e, "{repo} Delete function error", typeof(LectureHallRepository));
                throw;
            }
        }

        public override async Task<bool> Update(LectureHall lectureHall)
        {
            try
            {
                var result = await _dbSet.FirstOrDefaultAsync(lh => lh.Id == lectureHall.Id);

                if (result == null)
                    return false;

                result.HallName = lectureHall.HallName;
                result.Capacity = lectureHall.Capacity;
                result.UpdatedDate = DateTime.UtcNow;

                _dbSet.Update(result);
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception e)
            {
                _logger.LogError(e, "{repo} Update function error", typeof(LectureHallRepository));
                throw;
            }
        }
    }
}


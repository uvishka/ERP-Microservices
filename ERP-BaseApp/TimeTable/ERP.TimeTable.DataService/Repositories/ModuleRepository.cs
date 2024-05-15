using ERP.TimeTable.Core.Entities;
using ERP.TimeTable.DataService.Repositories.Interfaces;
using ERP.TimeTable.DataService.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.TimeTable.DataService.Repositories
{
    public class ModuleRepository : GenericRepository<Module>, IModuleRepository
    {
        public ModuleRepository(AppDbContext context, ILogger logger) : base(context, logger)
        {
        }


        public override async Task<IEnumerable<Module>> All()
        {
            try
            {
                return await _dbSet.Where(x => x.Status == 1)
                    .AsNoTracking()
                    .AsSplitQuery()
                    .OrderBy(x => x.CreatedDate)
                    .ToListAsync();

            }
            catch (Exception e)
            {
                _logger.LogError(e, "{repo} All function error", typeof(ModuleRepository));
                throw;
            }

        }
        public override async Task<bool> Delete(Guid id)
        {
            try
            {
                //get my entity
                var result = await _dbSet.FirstOrDefaultAsync(x => x.Id == id);

                if (result == null)
                    return false;

                result.Status = 0;
                result.UpdatedDate = DateTime.UtcNow;

                return true;
            }
            catch (Exception e) {
                _logger.LogError(e, "{repo} Delete function error", typeof(ModuleRepository));
                throw;
            }
                    
        }

        public override async Task<bool> Update(Module module)
        {
            try
            {
                var result = await _dbSet.FirstOrDefaultAsync(x => x.Id == module.Id);

                if (result == null)
                    return false;

                result.UpdatedDate = DateTime.UtcNow;
                result.ModuleCode = module.ModuleCode;
                result.ModuleName = module.ModuleName;

                return true;
            }
            catch(Exception e)
            {
                _logger.LogError(e, "{repo} Update function error", typeof(ModuleRepository));
                throw;
            }
        }
    } 
}

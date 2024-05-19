using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ERP.ModuleManagement.DataSevices.Data;
using ERP.ModuleManagement.DataSevices.Repositories.Interfaces;
using ERP.ModuleManagement.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;



namespace ERP.ModuleManagement.DataSevices.Repositories
{
    public class ModuleRepository : GenericRepository<Module>,IModuleRepositary
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
                    .OrderBy(x => x.AddedDate)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} All function error", typeof(ModuleRepository));
                throw;
            }
        }

        public override async Task<bool> Delete(Guid id)
        {
            try
            {
                //get my enntity
                var result = await _dbSet.FirstOrDefaultAsync(x => x.Id == id);
                if (result == null)
                {
                    return false;
                }
                result.Status = 0;
                result.UpdateDate = DateTime.UtcNow;
                return true;

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} Delete function error", typeof(ModuleRepository));
                throw;
            }
        }

        public override async Task<bool> Update(Module module)
        {
            try
            {
                var result = await _dbSet.FirstOrDefaultAsync(x => x.Id == module.Id);
                if (result == null)
                {
                    return false;
                }
                result.UpdateDate = DateTime.UtcNow;
                result.ModuleName = module.ModuleName;
                result.ModuleCode = module.ModuleCode;
                result.ModuleCoordineter = module.ModuleCoordineter;
                result.Lectures = module.Lectures;
              ;


                return true;

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} Update function error", typeof(ModuleRepository));
                throw;
            }
        }
    }
}

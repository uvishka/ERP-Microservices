using ERP.UserManagement.Core.Entities;
using ERP.UserManagement.DataServices.Repositories.Interfaces;
using ERP.UserManagement.DataServices;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ERP.UserManagement.DataService.Repositories;

namespace ERP.UserManagement.DataServices.Repositories
{
    public class LecturerRepository : GenericRepository<Students>, ILecturerRepository
    {
        public LecturerRepository(AppDbContext context, ILogger logger) : base(context, logger)
        {

        }


        public override async Task<bool> UpdateAsync(Students entity)
        {
            try
            {
                var result = await _dbSet.FirstOrDefaultAsync(x => x.Id == entity.Id);

                if (result == null)
                    return false;

                result.UpdateDate = DateTime.UtcNow;
                result.Email = entity.Email;
                result.FirstName = entity.FirstName;
                result.LastName = entity.LastName;
                result.Department = entity.Department;
                result.Phone = entity.Phone;
                result.NationalId = entity.NationalId;
                result.DoB = entity.DoB;
               // result.Advicees = entity.Advicees;

                return true;

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} Update function error",
                    typeof(StudentRepository));
                throw;
            }
        }
    }
}


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

namespace ERP.UserManagement.DataService.Repositories
{
    public class StudentRepository : GenericRepository<Student>, IStudentRepository
    {
        public StudentRepository(AppDbContext context, ILogger logger) : base(context, logger)
        {

        }


        public override async Task<bool> UpdateAsync(Student entity)
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
                result.RegistrationNumber = entity.RegistrationNumber;
                result.Phone = entity.Phone;
                result.Address1 = entity.Address1;
                result.Address2 = entity.Address2;
                result.DateOfBirth = entity.DateOfBirth;
                result.District = entity.District;
                result.NationalId = entity.NationalId;
                result.Department = entity.Department;
               // result.Advisor = entity.Advisor;
                

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

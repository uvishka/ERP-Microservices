using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.UserManagement.DataServices.Repositories.Interfaces
{
    public interface IUnitOfWorks
    {
        public IStudentRepository Students { get; }
        public ILecturerRepository Lecturers { get; }

        Task<bool> CompleteAsync();
    }
}

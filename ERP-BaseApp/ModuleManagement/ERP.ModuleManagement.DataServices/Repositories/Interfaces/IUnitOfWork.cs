using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.ModuleManagement.DataSevices.Repositories.Interfaces
{
    public interface IUnitOfWork
    {
        IModuleRepositary Modules { get; }

        Task<bool> CompleteAsync();
    }
}

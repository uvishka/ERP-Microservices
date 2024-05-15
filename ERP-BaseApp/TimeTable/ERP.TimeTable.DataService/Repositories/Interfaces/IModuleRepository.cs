using ERP.TimeTable.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Module = ERP.TimeTable.Core.Entities.Module;

namespace ERP.TimeTable.DataService.Repositories.Interfaces
{
    public interface IModuleRepository : IGenericRepository<Module>
    {
    }
}

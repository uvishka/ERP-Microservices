using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.ModuleManagement.Core.Entities
{
    public class Module : BaseEntity
    {

        public string ModuleName { get; set; }

        public string ModuleCode { get; set; }

        public string Semester { get; set; }

        public string ModuleCoordineter { get; set; }

        public string Lectures { get; set; }
    }
}

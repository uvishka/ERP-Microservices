using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.ModuleManagement.Core.DTOs.Responses
{
   public class GetModuleByIdResponse
    {
        public Guid ModuleId { get; set; }
        public string ModuleName { get; set; } = string.Empty;

        public string ModuleCode { get; set; } = string.Empty;
        public string Semester { get; set; } = string.Empty;
        public string ModuleCoordineter { get; set; } = string.Empty;
        public string Lectures { get; set; } = string.Empty;

    }
}

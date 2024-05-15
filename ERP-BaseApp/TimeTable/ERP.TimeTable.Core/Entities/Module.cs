using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.TimeTable.Core.Entities
{
    public class Module : BaseEntity
    {
        public Module() { 
            TimeSlots = new HashSet<TimeSlot>();        
        }
       
        public string ModuleCode { get; set; }
        public string ModuleName { get; set; }
        public virtual ICollection<TimeSlot> TimeSlots { get; set; }
    }
}

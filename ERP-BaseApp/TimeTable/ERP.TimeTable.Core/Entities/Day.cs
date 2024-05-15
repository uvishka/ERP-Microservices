using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.TimeTable.Core.Entities
{
    public class Day : BaseEntity
    {
        public Day() {
            TimeSlots = new HashSet<TimeSlot>();
        }
        public string Name { get; set; }
        public virtual ICollection<TimeSlot> TimeSlots { get; set; }
    }

}

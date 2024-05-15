using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.TimeTable.Core.Entities
{
    public class LectureHall : BaseEntity
    {
        public LectureHall() { 
            TimeSlots = new HashSet<TimeSlot>();
        
        }
        public string HallName { get; set; }
        public int Capacity { get; set; }
        public virtual ICollection<TimeSlot> TimeSlots { get; set; }
    }

}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.TimeTable.Core.Entities
{
    public class TimeSlot : BaseEntity
    {
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }

        public int DayId { get; set; }
        public virtual Day Day { get; set; }

        public int ModuleId { get; set; }
        public virtual Module Module { get; set; }

        public int LectureHallId { get; set; }
        public virtual LectureHall LectureHall { get; set; }
    }
}


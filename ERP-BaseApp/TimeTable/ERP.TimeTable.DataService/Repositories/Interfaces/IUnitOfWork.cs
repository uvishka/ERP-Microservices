using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.TimeTable.DataService.Repositories.Interfaces
{
    public interface IUnitOfWork
    {
        IDayRepository Day {  get; }
        IModuleRepository Module { get; }
        ITimeSlotRepository TimeSlot { get; }
        ILectureHallRepository LectureHall { get; }

        Task<bool> CompleteAsync();

    }
}

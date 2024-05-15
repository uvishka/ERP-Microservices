using AutoMapper;
using ERP.TimeTable.DataService.Repositories.Interfaces;

namespace ERP.TimeTable.Api.Controllers
{
    public class TimeSlotsController : BaseController
    {
        public TimeSlotsController(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
        }
    }
}

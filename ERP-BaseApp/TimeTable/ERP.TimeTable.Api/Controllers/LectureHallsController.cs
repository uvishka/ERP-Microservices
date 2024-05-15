using AutoMapper;
using ERP.TimeTable.DataService.Repositories.Interfaces;

namespace ERP.TimeTable.Api.Controllers
{
    public class LectureHallsController : BaseController
    {
        public LectureHallsController(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
        }
    }
}

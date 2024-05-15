using AutoMapper;
using ERP.TimeTable.DataService.Repositories.Interfaces;

namespace ERP.TimeTable.Api.Controllers
{
    public class ModulesController : BaseController
    {
        public ModulesController(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
        }
    }
}

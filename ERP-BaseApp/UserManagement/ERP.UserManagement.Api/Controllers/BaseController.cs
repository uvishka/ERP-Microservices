using AutoMapper;
using ERP.UserManagement.DataServices.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;




namespace ERP.UserManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController : ControllerBase
    {
        protected readonly IUnitOfWorks _unitOfWorks;
        protected readonly IMapper _mapper;

        public BaseController(IUnitOfWorks unitOfWorks, IMapper mapper)
        {
            _unitOfWorks = unitOfWorks;
            _mapper = mapper;
        }

    }
}

using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ERP.ModuleManagement.Core.DTOs.Requests;
using ERP.ModuleManagement.Core.DTOs.Responses;
using ERP.ModuleManagement.Core.Entities;
using ERP.ModuleManagement.DataSevices.Repositories.Interfaces;



namespace ERP.ModuleManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Modulecontroller : BaseController
    {
        public Modulecontroller(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
        }

        [HttpGet]
        [Route("{moduleId:guid}")]
        public async Task<IActionResult> GetModule(Guid moduleId)
        {
            var module = await _unitOfWork.Modules.GetById(moduleId);

            if (module == null)
            {
                return NotFound();
            }

            var result = _mapper.Map<GetModuleByIdResponse>(module);

            return Ok(result);
        }

        [HttpPost("")]
        public async Task<IActionResult> AddGraduate([FromBody] CreateModuleRequest module)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var result = _mapper.Map<Module>(module);
            await _unitOfWork.Modules.Add(result);
            await _unitOfWork.CompleteAsync();

            return CreatedAtAction(nameof(GetModule), new { moduleId = result.Id }, result);

        }

        [HttpPut]
        [Route("{moduleId:guid}")]
        public async Task<IActionResult> UpdateModule(Guid moduleId, [FromBody] UpdateModuleRequest module)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            try
            {
                var existingModule = _mapper.Map<Module>(module);
                await _unitOfWork.Modules.Update(existingModule);
                await _unitOfWork.CompleteAsync();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while updating the Module.");
            }


            return NoContent();

        }

        [HttpGet("")]
        public async Task<IActionResult> GetAllModule()
        {
            var module = await _unitOfWork.Modules.All();

            if (module == null)
            {
                return NotFound();
            }

            var result = _mapper.Map<IEnumerable<GetModuleResponse>>(module);

            return Ok(result);
        }

        [HttpDelete]
        [Route("{moduleId:guid}")]
        public async Task<IActionResult> DeleteGraduate(Guid moduleId)
        {
            var module = await _unitOfWork.Modules.GetById(moduleId);

            if (module == null)
            {
                return NotFound();
            }

            await _unitOfWork.Modules.Delete(moduleId);
            await _unitOfWork.CompleteAsync();

            return NoContent();


        }


    }
}

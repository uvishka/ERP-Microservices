using AutoMapper;
using ERP.UserManagement.Core.DTOs.Request;
using ERP.UserManagement.Core.DTOs.Response;
using ERP.UserManagement.Core.Entities;
using ERP.UserManagement.DataServices.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Query;





namespace ERP.UserManagement.Api.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class LecturerManagementController : BaseController
    {
        public LecturerManagementController(IUnitOfWorks unitOfWorks, IMapper mapper) : base(unitOfWorks, mapper)
        {
        }


        [HttpGet]
        [Route("{lecturerId:guid}")]
        public async Task<IActionResult> GetLecturer(Guid lecturerId)
        {
            var lecturer = await _unitOfWorks.Lecturers.GetAsync(lecturerId);

            if (lecturer == null)
            {
                return NotFound();
            }

            var result = _mapper.Map<GetLecturerByIdResponse>(lecturer);

            return Ok(result);
        }

        [HttpPost("")]
        public async Task<IActionResult> AddLecturer([FromBody] CreateLecturerRequest lecturer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var result = _mapper.Map<Lecturer>(lecturer);
            await _unitOfWorks.Lecturers.AddAsync(result);
            await _unitOfWorks.CompleteAsync();

            return CreatedAtAction(nameof(GetLecturer), new { lecturerId = result.Id }, result);

        }

        [HttpPut]
        [Route("{lecturerId:guid}")]
        public async Task<IActionResult> UpdateLecturer(Guid lecturerId, [FromBody] UpdateLecturerRequest lecturer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            try
            {
                var existingLecturer = _mapper.Map<Lecturer>(lecturer);
                await _unitOfWorks.Lecturers.UpdateAsync(existingLecturer);
                await _unitOfWorks.CompleteAsync();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while updating the Lecturer.");
            }


            return NoContent();

        }

        [HttpGet("")]
        public async Task<IActionResult> GetAllLecturers()
        {
            var lecturer = await _unitOfWorks.Lecturers.GetAllAsync();

            if (lecturer == null)
            {
                return NotFound();
            }

            var result = _mapper.Map<IEnumerable<GetLecturersResponse>>(lecturer);

            return Ok(result);
        }


        [HttpDelete]
        [Route("{lecturerId:guid}")]
        public async Task<IActionResult> DeleteLecturer(Guid lecturerId)
        {
            var lecturer = await _unitOfWorks.Lecturers.GetAsync(lecturerId);

            if (lecturer == null)
            {
                return NotFound();
            }

            await _unitOfWorks.Lecturers.DeleteAsync(lecturerId);
            await _unitOfWorks.CompleteAsync();

            return NoContent();


        }


    }



}

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
    public class StudentManagementController : BaseController
    {
        public StudentManagementController(IUnitOfWorks unitOfWorks, IMapper mapper) : base(unitOfWorks, mapper)
        {
        }


        [HttpGet]
        [Route("{studentId:guid}")]
        public async Task<IActionResult> GetStudent(Guid studentId)
        {
            var student = await _unitOfWorks.Students.GetAsync(studentId);

            if (student == null)
            {
                return NotFound();
            }

            var result = _mapper.Map<GetStudentByIdResponse>(student);

            return Ok(result);
        }

        [HttpPost("")]
        public async Task<IActionResult> AddStudent([FromBody] CreateStudentRequest student)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }



            var result = _mapper.Map<Student>(student);
            await _unitOfWorks.Students.AddAsync(result);
            await _unitOfWorks.CompleteAsync();

            return CreatedAtAction(nameof(GetStudent), new { studentId = result.Id }, result);

        }

        [HttpPut]
        [Route("{studentId:guid}")]
        public async Task<IActionResult> UpdateStudent(Guid studentId, [FromBody] UpdateStudentRequest student)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            try
            {
                var existingStudent = _mapper.Map<Student>(student);
                await _unitOfWorks.Students.UpdateAsync(existingStudent);
                await _unitOfWorks.CompleteAsync();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while updating the Student.");
            }


            return NoContent();

        }

        [HttpGet("")]
        public async Task<IActionResult> GetAllStudents()
        {
            var student = await _unitOfWorks.Students.GetAllAsync();

            if (student == null)
            {
                return NotFound();
            }

            var result = _mapper.Map<IEnumerable<GetStudentsResponse>>(student);

            return Ok(result);
        }


        [HttpDelete]
        [Route("{studentId:guid}")]
        public async Task<IActionResult> DeleteStudent(Guid studentId)
        {
            var student = await _unitOfWorks.Students.GetAsync(studentId);

            if (student == null)
            {
                return NotFound();
            }

            await _unitOfWorks.Students.DeleteAsync(studentId);
            await _unitOfWorks.CompleteAsync();

            return NoContent();


        }


    }



}

using AutoFixture;
using AutoMapper;
using ERP.UserManagement.Api.Controllers;
using ERP.UserManagement.Core.DTOs.Request;
using ERP.UserManagement.Core.DTOs.Response;
using ERP.UserManagement.Core.Entities;
using ERP.UserManagement.DataServices.Repositories.Interfaces;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.UserManagement.Api.Tests.Controllers
{
    public class StudentManagementControllerTests
    {
        private readonly IFixture _fixture;
        private readonly Mock<IUnitOfWorks> _mock;
        private readonly Mock<IMapper> _mockMapper;
        private readonly StudentManagementController _controller;

        public StudentManagementControllerTests()
        {
            _fixture = new Fixture();
            _mock = _fixture.Freeze<Mock<IUnitOfWorks>>();
            _mockMapper = _fixture.Freeze<Mock<IMapper>>();
            _controller = new StudentManagementController(_mock.Object, _mockMapper.Object);
        }

        //Get All Student API test
        [Fact]
        public async Task GetAllLecturers_ShouldReturnOkResponse_WhenDataFound()
        {
            //Arange
            var studentMock = _fixture.Create<IEnumerable<Student>>();
            object value = _mock.Setup(x => x.Students.GetAllAsync()).ReturnsAsync(studentMock);

            var studentListMock = _fixture.Create<IEnumerable<GetStudentsResponse>>();
            object listValue = _mockMapper.Setup(x => x.Map<IEnumerable<GetStudentsResponse>>(studentMock)).Returns(studentListMock);

            //Act
            var result = await _controller.GetAllStudents().ConfigureAwait(false);

            //Assert
            result.Should().NotBeNull();
            //result.Should().BeAssignableTo<IEnumerable<GetStudentResponse>>();
            result.Should().BeAssignableTo<OkObjectResult>();
            result.As<OkObjectResult>().Value.Should().NotBeNull().And.BeOfType(studentListMock.GetType());
            _mock.Verify(x => x.Students.GetAllAsync(), Times.Once);
            _mockMapper.Verify(x => x.Map<IEnumerable<GetStudentsResponse>>(studentMock), Times.Once);

        }
        [Fact]
        public async Task GetAllStudents_ShouldReturnNotFound_WhenNoDataFound()
        {
            //Arange
            List<Student> response = null;
            object value = _mock.Setup(x => x.Students.GetAllAsync()).ReturnsAsync(response);

            //Act
            var result = await _controller.GetAllStudents().ConfigureAwait(false);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<NotFoundResult>();
            _mock.Verify(x => x.Students.GetAllAsync(), Times.Once);

        }
        [Fact]
        public async Task GetStudent_ShouldReturnOkResponse_WhenDataFound()
        {
            //Arange
            var studentMock = _fixture.Create<Student>();
            var studentId = _fixture.Create<Guid>();
            object value = _mock.Setup(x => x.Students.GetAsync(studentId)).ReturnsAsync(studentMock);

            var studentListMock = _fixture.Create<GetStudentByIdResponse>();
            object listValue = _mockMapper.Setup(x => x.Map<GetStudentByIdResponse>(studentMock)).Returns(studentListMock);

            //Act
            var result = await _controller.GetStudent(studentId).ConfigureAwait(false);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<OkObjectResult>();
            result.As<OkObjectResult>().Value.Should().NotBeNull().And.BeOfType(studentListMock.GetType());
            _mock.Verify(x => x.Students.GetAsync(studentId), Times.Once);
            _mockMapper.Verify(x => x.Map<GetStudentByIdResponse>(studentMock), Times.Once);

        }

        [Fact]
        public async Task GetStudent_ShouldReturnNotFound_WhenNoDataFound()
        {
            //Arange
            Student response = null;
            var studentId = _fixture.Create<Guid>();
            object value = _mock.Setup(x => x.Students.GetAsync(studentId)).ReturnsAsync(response);

            //Act
            var result = await _controller.GetStudent(studentId).ConfigureAwait(false);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<NotFoundResult>();
            _mock.Verify(x => x.Students.GetAsync(studentId), Times.Once);

        }
         [Fact]
        public async Task AddStudent_ShouldReturnOkResponse_WhenValidRequest()
        {
            //Arange
            var request = _fixture.Create<CreateStudentRequest>();
            var response = _fixture.Create<Student>();

            _mockMapper.Setup(x => x.Map<Student>(request)).Returns(response);
            _mock.Setup(x => x.Students.AddAsync(response)).ReturnsAsync(true);
            _mock.Setup(x => x.CompleteAsync()).ReturnsAsync(true);

            //Act
            var result = await _controller.AddStudent(request).ConfigureAwait(false);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<CreatedAtActionResult>();
            _mockMapper.Verify(x => x.Map<Student>(request), Times.Once);
            _mock.Verify(x => x.Students.AddAsync(response), Times.Once);
            _mock.Verify(x => x.CompleteAsync(), Times.Once);

        }
        [Fact]
        public async Task AddStudent_ShouldReturnBadResponse_WhenInvalidRequest()
        {
            //Arange
            var request = _fixture.Create<CreateStudentRequest>();
            _controller.ModelState.AddModelError("FirstName", "The FirstName field is required.");
            var response = _fixture.Create<Student>();

            _mockMapper.Setup(x => x.Map<Student>(request)).Returns(response);
            _mock.Setup(x => x.Students.AddAsync(response)).ReturnsAsync(true);
            _mock.Setup(x => x.CompleteAsync()).ReturnsAsync(true);

            //Act
            var result = await _controller.AddStudent(request).ConfigureAwait(false);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<BadRequestResult>();
            _mockMapper.Verify(x => x.Map<Student>(request), Times.Never);
            _mock.Verify(x => x.Students.AddAsync(response), Times.Never);
            _mock.Verify(x => x.CompleteAsync(), Times.Never);

        }

        //Delete Student API test

        [Fact]
        public async Task DeleteStudents_ShouldReturnNoContent_WhenGraduateDeleted()
        {
            //Arange

            var studentId = _fixture.Create<Guid>();
            var response = _fixture.Create<Student>();

            _mock.Setup(x => x.Students.GetAsync(studentId)).ReturnsAsync(response);
            _mock.Setup(x => x.Students.DeleteAsync(studentId)).ReturnsAsync(true);
            _mock.Setup(x => x.CompleteAsync()).ReturnsAsync(true);

            //Act
            var result = await _controller.DeleteStudent(studentId).ConfigureAwait(false);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<NoContentResult>();
            _mock.Verify(x => x.Students.GetAsync(studentId), Times.Once);
            _mock.Verify(x => x.Students.DeleteAsync(studentId), Times.Once);
            _mock.Verify(x => x.CompleteAsync(), Times.Once);

        }

        [Fact]
        public async Task DeleteStudent_ShouldReturnNotFound_WhenNoDataFound()
        {
            //Arange

            var studentId = _fixture.Create<Guid>();
            Student lecturerResponce = null;

            _mock.Setup(x => x.Students.GetAsync(studentId)).ReturnsAsync(lecturerResponce);

            //Act
            var result = await _controller.DeleteStudent(studentId).ConfigureAwait(false);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<NotFoundResult>();
            _mock.Verify(x => x.Students.GetAsync(studentId), Times.Once);
            _mock.Verify(x => x.Students.DeleteAsync(studentId), Times.Never);
            _mock.Verify(x => x.CompleteAsync(), Times.Never);

        }
        //Update Student API tests

        [Fact]
        public async Task UpdateStudent_ShouldReturnBadResponse_WhenInvalidRequest()
        {
            //Arange
            var request = _fixture.Create<UpdateStudentRequest>();
            var studentId = _fixture.Create<Guid>();
            var response = _fixture.Create<Student>();
            _controller.ModelState.AddModelError("FirstName", "The FirstName field is required.");


            //Act
            var result = await _controller.UpdateStudent(studentId, request).ConfigureAwait(false);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<BadRequestObjectResult>();
            _mockMapper.Verify(x => x.Map<Student>(request), Times.Never);
            _mock.Verify(x => x.Students.UpdateAsync(response), Times.Never);
            _mock.Verify(x => x.CompleteAsync(), Times.Never);

        }
        [Fact]
        public async Task UpdateStudent_ShouldReturnNoContentResponse_WhenValidRequest()
        {
            //Arange
            var request = _fixture.Create<UpdateStudentRequest>();
            var studentId = _fixture.Create<Guid>();
            var response = _fixture.Create<Student>();

            _mockMapper.Setup(x => x.Map<Student>(request)).Returns(response);
            _mock.Setup(x => x.Students.UpdateAsync(response)).ReturnsAsync(true);
            _mock.Setup(x => x.CompleteAsync()).ReturnsAsync(true);


            //Act
            var result = await _controller.UpdateStudent(studentId, request).ConfigureAwait(false);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<NoContentResult>();
            _mockMapper.Verify(x => x.Map<Student>(request), Times.Once);
            _mock.Verify(x => x.Students.UpdateAsync(response), Times.Once);
            _mock.Verify(x => x.CompleteAsync(), Times.Once);

        }


        [Fact]
        public async Task UpdateStudent_ShouldReturnStatusCode500_WhenErrorOccursSavingToDatabase()
        {
            //Arange
            var request = _fixture.Create<UpdateStudentRequest>();
            var studentId = _fixture.Create<Guid>();
            var response = _fixture.Create<Student>();

            _mockMapper.Setup(x => x.Map<Student>(request)).Returns(response);
            _mock.Setup(x => x.Students.UpdateAsync(response)).ReturnsAsync(true);
            _mock.Setup(x => x.CompleteAsync()).ThrowsAsync(new Exception("Database error occurred"));


            //Act
            var result = await _controller.UpdateStudent(studentId, request).ConfigureAwait(false);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<ObjectResult>();
            var statusCodeResult = (ObjectResult)result;
            statusCodeResult.StatusCode.Should().Be(500);
            statusCodeResult.Value.Should().Be("An error occurred while updating the Student.");
            _mockMapper.Verify(x => x.Map<Student>(request), Times.Once);
            _mock.Verify(x => x.Students.UpdateAsync(response), Times.Once);
            _mock.Verify(x => x.CompleteAsync(), Times.Once);

        }
    }
}

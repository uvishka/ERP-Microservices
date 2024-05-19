using AutoFixture;
using AutoMapper;
using ERP.UserManagement.Api.Controllers;
using Moq;
using ERP.UserManagement.DataServices.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using ERP.UserManagement.Core.Entities;
using ERP.UserManagement.Core.DTOs.Response;
using FluentAssertions;
using ERP.UserManagement.Core.DTOs.Request;

namespace ERP.UserManagement.Api.Tests.Controllers
{
    public class LecturerManagementControllerTests
    {
        private readonly IFixture _fixture;
        private readonly Mock<IUnitOfWorks> _mock;
        private readonly Mock<IMapper> _mockMapper;
        private readonly LecturerManagementController _controller;

        public LecturerManagementControllerTests()
        {
            _fixture = new Fixture();
            _mock = _fixture.Freeze<Mock<IUnitOfWorks>>();
            _mockMapper = _fixture.Freeze<Mock<IMapper>>();
            _controller = new LecturerManagementController(_mock.Object, _mockMapper.Object);
        }

        //Get All Lectures API test
        [Fact]
        public async Task GetAllLecturers_ShouldReturnOkResponse_WhenDataFound()
        {
            //Arange
            var lecturerMock = _fixture.Create<IEnumerable<Students>>();
            object value = _mock.Setup(x => x.Lecturers.GetAllAsync()).ReturnsAsync(lecturerMock);

            var lecturerListMock = _fixture.Create<IEnumerable<GetLecturersResponse>>();
            object listValue = _mockMapper.Setup(x => x.Map<IEnumerable<GetLecturersResponse>>(lecturerMock)).Returns(lecturerListMock);

            //Act
            var result = await _controller.GetAllLecturers().ConfigureAwait(false);

            //Assert
            result.Should().NotBeNull();
            //result.Should().BeAssignableTo<IEnumerable<GetStudentResponse>>();
            result.Should().BeAssignableTo<OkObjectResult>();
            result.As<OkObjectResult>().Value.Should().NotBeNull().And.BeOfType(lecturerListMock.GetType());
            _mock.Verify(x => x.Lecturers.GetAllAsync(), Times.Once);
            _mockMapper.Verify(x => x.Map<IEnumerable<GetLecturersResponse>>(lecturerMock), Times.Once);

        }

        [Fact]
        public async Task GetAllLecturers_ShouldReturnNotFound_WhenNoDataFound()
        {
            //Arange
            List<Students> response = null;
            object value = _mock.Setup(x => x.Lecturers.GetAllAsync()).ReturnsAsync(response);

            //Act
            var result = await _controller.GetAllLecturers().ConfigureAwait(false);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<NotFoundResult>();
            _mock.Verify(x => x.Lecturers.GetAllAsync(), Times.Once);

        }

        //Get Lecturer By Id API tests

        [Fact]
        public async Task GetLecturer_ShouldReturnOkResponse_WhenDataFound()
        {
            //Arange
            var lecturerMock = _fixture.Create<Students>();
            var lecturerId = _fixture.Create<Guid>();
            object value = _mock.Setup(x => x.Lecturers.GetAsync(lecturerId)).ReturnsAsync(lecturerMock);

            var lecturerListMock = _fixture.Create<GetLecturerByIdResponse>();
            object listValue = _mockMapper.Setup(x => x.Map<GetLecturerByIdResponse>(lecturerMock)).Returns(lecturerListMock);

            //Act
            var result = await _controller.GetLecturer(lecturerId).ConfigureAwait(false);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<OkObjectResult>();
            result.As<OkObjectResult>().Value.Should().NotBeNull().And.BeOfType(lecturerListMock.GetType());
            _mock.Verify(x => x.Lecturers.GetAsync(lecturerId), Times.Once);
            _mockMapper.Verify(x => x.Map<GetLecturerByIdResponse>(lecturerMock), Times.Once);

        }

        [Fact]
        public async Task GetLecturer_ShouldReturnNotFound_WhenNoDataFound()
        {
            //Arange
            Students response = null;
            var lecturerId = _fixture.Create<Guid>();
            object value = _mock.Setup(x => x.Lecturers.GetAsync(lecturerId)).ReturnsAsync(response);

            //Act
            var result = await _controller.GetLecturer(lecturerId).ConfigureAwait(false);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<NotFoundResult>();
            _mock.Verify(x => x.Lecturers.GetAsync(lecturerId), Times.Once);

        }

        //Add lecturer API tests
        [Fact]
        public async Task AddLecturer_ShouldReturnOkResponse_WhenValidRequest()
        {
            //Arange
            var request = _fixture.Create<CreateLecturerRequest>();
            var response = _fixture.Create<Students>();

            _mockMapper.Setup(x => x.Map<Students>(request)).Returns(response);
            _mock.Setup(x => x.Lecturers.AddAsync(response)).ReturnsAsync(true);
            _mock.Setup(x => x.CompleteAsync()).ReturnsAsync(true);

            //Act
            var result = await _controller.AddLecturer(request).ConfigureAwait(false);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<CreatedAtActionResult>();
            _mockMapper.Verify(x => x.Map<Students>(request), Times.Once);
            _mock.Verify(x => x.Lecturers.AddAsync(response), Times.Once);
            _mock.Verify(x => x.CompleteAsync(), Times.Once);

        }

        [Fact]
        public async Task AddLecturer_ShouldReturnBadResponse_WhenInvalidRequest()
        {
            //Arange
            var request = _fixture.Create<CreateLecturerRequest>();
            _controller.ModelState.AddModelError("FirstName", "The FirstName field is required.");
            var response = _fixture.Create<Students>();

            _mockMapper.Setup(x => x.Map<Students>(request)).Returns(response);
            _mock.Setup(x => x.Lecturers.AddAsync(response)).ReturnsAsync(true);
            _mock.Setup(x => x.CompleteAsync()).ReturnsAsync(true);

            //Act
            var result = await _controller.AddLecturer(request).ConfigureAwait(false);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<BadRequestResult>();
            _mockMapper.Verify(x => x.Map<Students>(request), Times.Never);
            _mock.Verify(x => x.Lecturers.AddAsync(response), Times.Never);
            _mock.Verify(x => x.CompleteAsync(), Times.Never);

        }

        //Delete Lecturer API test

        [Fact]
        public async Task DeleteLecturer_ShouldReturnNoContent_WhenGraduateDeleted()
        {
            //Arange

            var lecturerId = _fixture.Create<Guid>();
            var response = _fixture.Create<Students>();

            _mock.Setup(x => x.Lecturers.GetAsync(lecturerId)).ReturnsAsync(response);
            _mock.Setup(x => x.Lecturers.DeleteAsync(lecturerId)).ReturnsAsync(true);
            _mock.Setup(x => x.CompleteAsync()).ReturnsAsync(true);

            //Act
            var result = await _controller.DeleteLecturer(lecturerId).ConfigureAwait(false);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<NoContentResult>();
            _mock.Verify(x => x.Lecturers.GetAsync(lecturerId), Times.Once);
            _mock.Verify(x => x.Lecturers.DeleteAsync(lecturerId), Times.Once);
            _mock.Verify(x => x.CompleteAsync(), Times.Once);

        }

        [Fact]
        public async Task DeleteLecturer_ShouldReturnNotFound_WhenNoDataFound()
        {
            //Arange

            var lecturerId = _fixture.Create<Guid>();
            Students lecturerResponce = null;

            _mock.Setup(x => x.Lecturers.GetAsync(lecturerId)).ReturnsAsync(lecturerResponce);

            //Act
            var result = await _controller.DeleteLecturer(lecturerId).ConfigureAwait(false);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<NotFoundResult>();
            _mock.Verify(x => x.Lecturers.GetAsync(lecturerId), Times.Once);
            _mock.Verify(x => x.Lecturers.DeleteAsync(lecturerId), Times.Never);
            _mock.Verify(x => x.CompleteAsync(), Times.Never);

        }

        //Update Lecturer API tests

        [Fact]
        public async Task UpdateLecturer_ShouldReturnBadResponse_WhenInvalidRequest()
        {
            //Arange
            var request = _fixture.Create<UpdateLecturerRequest>();
            var lecturerId = _fixture.Create<Guid>();
            var response = _fixture.Create<Students>();
            _controller.ModelState.AddModelError("FirstName", "The FirstName field is required.");


            //Act
            var result = await _controller.UpdateLecturer(lecturerId, request).ConfigureAwait(false);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<BadRequestObjectResult>();
            _mockMapper.Verify(x => x.Map<Students>(request), Times.Never);
            _mock.Verify(x => x.Lecturers.UpdateAsync(response), Times.Never);
            _mock.Verify(x => x.CompleteAsync(), Times.Never);

        }


        [Fact]
        public async Task UpdateLecturer_ShouldReturnNoContentResponse_WhenValidRequest()
        {
            //Arange
            var request = _fixture.Create<UpdateLecturerRequest>();
            var lecturerId = _fixture.Create<Guid>();
            var response = _fixture.Create<Students>();

            _mockMapper.Setup(x => x.Map<Students>(request)).Returns(response);
            _mock.Setup(x => x.Lecturers.UpdateAsync(response)).ReturnsAsync(true);
            _mock.Setup(x => x.CompleteAsync()).ReturnsAsync(true);


            //Act
            var result = await _controller.UpdateLecturer(lecturerId, request).ConfigureAwait(false);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<NoContentResult>();
            _mockMapper.Verify(x => x.Map<Students>(request), Times.Once);
            _mock.Verify(x => x.Lecturers.UpdateAsync(response), Times.Once);
            _mock.Verify(x => x.CompleteAsync(), Times.Once);

        }

        [Fact]
        public async Task UpdateLecturer_ShouldReturnStatusCode500_WhenErrorOccursSavingToDatabase()
        {
            //Arange
            var request = _fixture.Create<UpdateLecturerRequest>();
            var lecturerId = _fixture.Create<Guid>();
            var response = _fixture.Create<Students>();

            _mockMapper.Setup(x => x.Map<Students>(request)).Returns(response);
            _mock.Setup(x => x.Lecturers.UpdateAsync(response)).ReturnsAsync(true);
            _mock.Setup(x => x.CompleteAsync()).ThrowsAsync(new Exception("Database error occurred"));


            //Act
            var result = await _controller.UpdateLecturer(lecturerId, request).ConfigureAwait(false);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<ObjectResult>();
            var statusCodeResult = (ObjectResult)result;
            statusCodeResult.StatusCode.Should().Be(500);
            statusCodeResult.Value.Should().Be("An error occurred while updating the Lecturer.");
            _mockMapper.Verify(x => x.Map<Students>(request), Times.Once);
            _mock.Verify(x => x.Lecturers.UpdateAsync(response), Times.Once);
            _mock.Verify(x => x.CompleteAsync(), Times.Once);

        }

    }
}
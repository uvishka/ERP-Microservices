using AutoMapper;
using ERP.UserManagement.Core.DTOs.Response;

using ERP.UserManagement.Core.Entities;

namespace ERP.UserManagement.Api.MappingProfiles;

public class DomainToResponse : Profile
{
    public DomainToResponse()
    {
        CreateMap<Student, GetStudentsResponse>()
           .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => $"{src.FirstName} {src.LastName}"))
           .ForMember(dest => dest.StudentID, opt => opt.MapFrom(src => src.Id));

        CreateMap<Student, GetStudentByIdResponse>()
            .ForMember(dest => dest.StudentID, opt => opt.MapFrom(src => src.Id));

        CreateMap<Lecturer, GetLecturersResponse>()
           .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => $"{src.FirstName} {src.LastName}"))
           .ForMember(dest => dest.LecturerID, opt => opt.MapFrom(src => src.Id));

        CreateMap<Lecturer, GetLecturerByIdResponse>()
            .ForMember(dest => dest.LecturerId, opt => opt.MapFrom(src => src.Id));
    }


}

using AutoMapper;
using ERP.UserManagement.Core.DTOs.Request;
using ERP.UserManagement.Core.Entities;

namespace ERP.StudentRegistration.Api.MappingProfiles
{
    public class RequestToDomain : Profile
    {

        public RequestToDomain()
        {
            CreateMap<CreateStudentRequest, Student>()
               .ForMember(dest => dest.Status, opt => opt.MapFrom(src => 1))
               .ForMember(dest => dest.AddedDate, opt => opt.MapFrom(src => DateTime.UtcNow))
               .ForMember(dest => dest.UpdateDate, opt => opt.MapFrom(src => DateTime.UtcNow));

                      
            CreateMap<UpdateStudentRequest, Student>()
               .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.StudentID))
               .ForMember(
               dest => dest.UpdateDate, opt => opt.MapFrom(src => DateTime.UtcNow))
               .ForMember(
                dest => dest.Status, opt => opt.MapFrom(src => 1));


            CreateMap<CreateLecturerRequest, Students>()
           .ForMember(dest => dest.Status, opt => opt.MapFrom(src => 1))
           .ForMember(dest => dest.AddedDate, opt => opt.MapFrom(src => DateTime.UtcNow))
           .ForMember(dest => dest.UpdateDate, opt => opt.MapFrom(src => DateTime.UtcNow));


            CreateMap<UpdateLecturerRequest, Students>()
               .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.LecturerID))
               .ForMember(
               dest => dest.UpdateDate, opt => opt.MapFrom(src => DateTime.UtcNow))
               .ForMember(
                dest => dest.Status, opt => opt.MapFrom(src => 1));
        }

    }
}

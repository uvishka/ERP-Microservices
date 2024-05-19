
using AutoMapper;
using ERP.ModuleManagement.Core.Entities;
using ERP.ModuleManagement.Core.DTOs.Requests;


namespace ERP.ModuleManagement.Api.Mapping_Profiles


{
    public class RequestToDomain :Profile
    {

        public RequestToDomain()
        {
            CreateMap<CreateModuleRequest, Module>()
                 .ForMember(
                dest => dest.Status, opt => opt.MapFrom(src => 1))
                 .ForMember(
                dest => dest.AddedDate, opt => opt.MapFrom(src => DateTime.UtcNow))
                .ForMember(
                dest => dest.UpdateDate, opt => opt.MapFrom(src => DateTime.UtcNow));

            CreateMap<UpdateModuleRequest, Module>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.ModuleId))
               .ForMember(
               dest => dest.UpdateDate, opt => opt.MapFrom(src => DateTime.UtcNow))
               .ForMember(
                dest => dest.Status, opt => opt.MapFrom(src => 1));
        }
    }
}

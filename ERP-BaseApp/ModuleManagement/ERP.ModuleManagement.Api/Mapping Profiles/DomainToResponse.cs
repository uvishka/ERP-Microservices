using AutoMapper;
using ERP.ModuleManagement.Core.Entities;
using ERP.ModuleManagement.Core.DTOs.Responses;

namespace ERP.ModuleManagement.Api.Mapping_Profiles
{
    public class DomainToResponse : Profile
    {

        public DomainToResponse()
        {

            CreateMap<Module, GetModuleResponse>()
                .ForMember(
                dest => dest.ModuleId, opt => opt.MapFrom(src => src.Id));
                

            CreateMap<Module, GetModuleByIdResponse>()
               .ForMember(
               dest => dest.ModuleId, opt => opt.MapFrom(src => src.Id));
        }
    }
}

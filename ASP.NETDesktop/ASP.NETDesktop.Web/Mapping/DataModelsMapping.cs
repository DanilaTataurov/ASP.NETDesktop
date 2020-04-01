using ASP.NETDesktop.Common.Enums;
using ASP.NETDesktop.Common.Extensions;
using ASP.NETDesktop.Domain.Entities;
using ASP.NETDesktop.Domain.Models.Dtos;
using AutoMapper;

namespace ASP.NETDesktop.Web.Mapping {
    public class DataModelsMapping : Profile {
        public DataModelsMapping() {
            CreateMap<Developer, DeveloperDto>()
                .ReverseMap()
                .ForMember(dest => dest.Projects, opt => opt.Ignore())
                .ForMember(dest => dest.Vacations, opt => opt.Ignore());

            CreateMap<Project, ProjectDto>()
                .ReverseMap()
                .ForMember(dest => dest.Developers, opt => opt.Ignore());

            CreateMap<Vacation, VacationDto>();

            CreateMap<Vacation, VacationDto>()
                .ForMember(dest=>dest.Status, opt=>opt.MapFrom(src=>
                    EnumExtensions.GetDescription(src.Status)))
                .ReverseMap()
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => 
                    EnumExtensions.ParseDescriptionToEnum<VacationStatus>(src.Status)))
                .ForMember(dest => dest.Developer, opt => opt.Ignore());
        }
    }
}

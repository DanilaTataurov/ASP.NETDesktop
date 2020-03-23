using ASP.NETDesktop.Domain.Entities;
using ASP.NETDesktop.Domain.Models.Dtos;
using AutoMapper;

namespace ASP.NETDesktop.Web.Mapping {
    public class DataModelsMapping : Profile {
        public DataModelsMapping() {
            CreateMap<Developer, DeveloperDto>();

            CreateMap<DeveloperDto, Developer>()
                .ForMember(dest => dest.Projects, opt => opt.Ignore())
                .ForMember(dest => dest.Vacations, opt => opt.Ignore());

            CreateMap<Project, ProjectDto>();

            CreateMap<ProjectDto, Project>()
                .ForMember(dest => dest.Developers, opt => opt.Ignore());

            CreateMap<Vacation, VacationDto>();

            CreateMap<VacationDto, Vacation>()
                .ForMember(dest => dest.Developer, opt => opt.Ignore())
                .ReverseMap();
        }
    }
}

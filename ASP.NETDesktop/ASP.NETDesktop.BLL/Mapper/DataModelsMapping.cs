using ASP.NETDesktop.Common.Dtos;
using ASP.NETDesktop.Domain.Entities;
using AutoMapper;

namespace ASP.NETDesktop.BLL.Mapper {
    public class DataModelsMapping : Profile {
        public DataModelsMapping() {
            CreateMap<Developer, DeveloperDto>()
                .ForMember(dest=>dest.Projects, opt=>opt.Ignore())
                .ForMember(dest=>dest.Vacations, opt=>opt.Ignore())
                .ReverseMap();

            CreateMap<Project, ProjectDto>()
                .ForMember(dest=>dest.Developers, opt=>opt.Ignore())
                .ReverseMap();

            CreateMap<Vacation, VacationDto>()
                .ForMember(dest=>dest.Developer, opt=>opt.Ignore())
                .ReverseMap();
        }
    }
}

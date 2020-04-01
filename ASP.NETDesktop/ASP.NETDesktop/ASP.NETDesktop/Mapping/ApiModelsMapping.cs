using ASP.NETDesktop.Common.ApiModels;
using ASP.NETDesktop.Models;
using AutoMapper;

namespace ASP.NETDesktop.Mapping {
    internal class ApiModelsMapping : Profile {
        public ApiModelsMapping() {
            CreateMap<DeveloperModel, DeveloperApiModel>()
                .ReverseMap()
                .ForMember(dest => dest.FullName, opt => opt.Ignore());

            CreateMap<ProjectModel, ProjectApiModel>()
                .ReverseMap();

            CreateMap<VacationModel, VacationApiModel>()
                .ReverseMap();
        }
    }
}

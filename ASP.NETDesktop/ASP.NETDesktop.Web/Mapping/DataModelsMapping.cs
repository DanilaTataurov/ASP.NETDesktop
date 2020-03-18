using ASP.NETDesktop.Common.Dtos;
using ASP.NETDesktop.Domain.Entities;
using AutoMapper;

namespace ASP.NETDesktop.Web.Mapping {
    public class DataModelsMapping : Profile {
        public DataModelsMapping() {
            CreateMap<Developer, DeveloperDto>()
                .ReverseMap();
        }
    }
}

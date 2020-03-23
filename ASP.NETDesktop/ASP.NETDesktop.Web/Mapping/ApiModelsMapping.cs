using System;
using System.Globalization;
using ASP.NETDesktop.Common.ApiModels;
using ASP.NETDesktop.Domain.Entities;
using ASP.NETDesktop.Domain.Models.Dtos;
using AutoMapper;

namespace ASP.NETDesktop.Web.Mapping {
    public class ApiModelsMapping : Profile {
        public ApiModelsMapping() {
            CreateMap<Developer, DeveloperApiModel>()
                .ReverseMap();

            CreateMap<DeveloperDto, DeveloperApiModel>()
                .ReverseMap();

            CreateMap<ProjectDto, ProjectApiModel>()
                .ReverseMap()
                .ForMember(dest => dest.StartDate, opts => opts.MapFrom(src =>
                    DateTime.Parse(src.StartDate, CultureInfo.CreateSpecificCulture("fr-FR"))))
                .ForMember(dest => dest.EndDate, opts => opts.MapFrom(src =>
                    DateTime.Parse(src.EndDate, CultureInfo.CreateSpecificCulture("fr-FR"))));

            CreateMap<VacationDto, VacationApiModel>()
                .ReverseMap()
                .ForMember(dest => dest.StartDate, opts => opts.MapFrom(src =>
                    DateTime.Parse(src.StartDate, CultureInfo.CreateSpecificCulture("fr-FR"))))
                .ForMember(dest => dest.EndDate, opts => opts.MapFrom(src =>
                    DateTime.Parse(src.EndDate, CultureInfo.CreateSpecificCulture("fr-FR"))));
        }
    }
}

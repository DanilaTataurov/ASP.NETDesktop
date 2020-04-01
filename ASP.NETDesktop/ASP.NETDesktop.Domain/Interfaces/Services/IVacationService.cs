using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ASP.NETDesktop.Domain.Entities;
using ASP.NETDesktop.Domain.Interfaces.Services.Base;
using ASP.NETDesktop.Domain.Interfaces.Services.Responses;
using ASP.NETDesktop.Domain.Models.Dtos;

namespace ASP.NETDesktop.Domain.Interfaces.Services {
    public interface IVacationService : IService<Vacation> {
        IEnumerable<VacationDto> List();
        IEnumerable<VacationDto> ListByDeveloperId(Guid id);
        Task<VacationDto> GetByIdAsync(Guid id);
        ServiceResult Create(VacationDto dto);
        ServiceResult Update(VacationDto dto);
        Task<bool> DeleteAsync(Guid id);
    }
}

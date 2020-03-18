using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ASP.NETDesktop.Common.Dtos;
using ASP.NETDesktop.Domain.Entities;
using ASP.NETDesktop.Domain.Interfaces.Services.Base;

namespace ASP.NETDesktop.Domain.Interfaces.Services {
    public interface IVacationService : IService<Vacation> {
        IEnumerable<Vacation> List();
        IEnumerable<Vacation> ListByDeveloperId(Guid id);
        Task<Vacation> GetByIdAsync(Guid id);
        string Create(VacationDto dto);
        Task<string> UpdateAsync(VacationDto dto);
        Task<bool> DeleteAsync(Guid id);
    }
}

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ASP.NETDesktop.Domain.Entities;
using ASP.NETDesktop.Domain.Interfaces.Services.Base;
using ASP.NETDesktop.Domain.Models.Dtos;

namespace ASP.NETDesktop.Domain.Interfaces.Services {
    public interface IDeveloperService : IService<Developer> {
        IEnumerable<DeveloperDto> List();
        Task<DeveloperDto> GetByIdAsync(Guid id);
        void Create(DeveloperDto dto);
        Task UpdateAsync(DeveloperDto dto);
        Task<bool> DeleteAsync(Guid id);

        Task AddProjectAsync(Guid developerId, Guid projectId);
        Task DeleteProjectAsync(Guid developerId, Guid projectId);
    }
}

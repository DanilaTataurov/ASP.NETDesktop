using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ASP.NETDesktop.Domain.Entities;
using ASP.NETDesktop.Domain.Interfaces.Services.Base;
using ASP.NETDesktop.Domain.Models.Dtos;

namespace ASP.NETDesktop.Domain.Interfaces.Services {
    public interface IProjectService : IService<Project> {
        IEnumerable<ProjectDto> List();
        Task<ProjectDto> GetByIdAsync(Guid id);
        void Create(ProjectDto dto);
        Task UpdateAsync(ProjectDto dto);
        Task<bool> DeleteAsync(Guid id);
    }
}

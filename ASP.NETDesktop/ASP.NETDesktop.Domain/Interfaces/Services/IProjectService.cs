using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ASP.NETDesktop.Common.Dtos;
using ASP.NETDesktop.Domain.Entities;
using ASP.NETDesktop.Domain.Interfaces.Services.Base;

namespace ASP.NETDesktop.Domain.Interfaces.Services {
    public interface IProjectService : IService<Project> {
        IEnumerable<Project> List();
        Task<Project> GetByIdAsync(Guid id);
        void Create(ProjectDto dto);
        Task UpdateAsync(ProjectDto dto);
        Task<bool> DeleteAsync(Guid id);
    }
}

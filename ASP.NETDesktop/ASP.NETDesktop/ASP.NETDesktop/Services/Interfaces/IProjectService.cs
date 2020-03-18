using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ASP.NETDesktop.Common.ApiModels;
using ASP.NETDesktop.Services.Models;

namespace ASP.NETDesktop.Services.Interfaces {
    public interface IProjectService {
        Task<ServiceResult<List<ProjectApiModel>>> ListAsync();
        Task<ServiceResult<ProjectApiModel>> GetByIdAsync(Guid id);
        Task<ServiceResult> CreateAsync(ProjectApiModel model);
        Task<ServiceResult> UpdateAsync(ProjectApiModel model);
        Task<ServiceResult> DeleteAsync(Guid id);
    }
}

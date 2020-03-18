using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ASP.NETDesktop.Common.ApiModels;
using ASP.NETDesktop.Services.Models;

namespace ASP.NETDesktop.Services.Interfaces {
    public interface IDeveloperService {
        Task<ServiceResult<List<DeveloperApiModel>>> ListAsync();
        Task<ServiceResult<DeveloperApiModel>> GetByIdAsync(Guid id);
        Task<ServiceResult> CreateAsync(DeveloperApiModel model);
        Task<ServiceResult> UpdateAsync(DeveloperApiModel model);
        Task<ServiceResult> DeleteAsync(Guid id);

        Task<ServiceResult> AddProjectAsync(Guid developerId, Guid projectId);
        Task<ServiceResult> DeleteProjectAsync(Guid developerId, Guid projectId);
    }
}

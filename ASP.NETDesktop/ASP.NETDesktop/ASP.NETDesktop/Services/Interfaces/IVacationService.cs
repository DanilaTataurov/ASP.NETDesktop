using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ASP.NETDesktop.Common.ApiModels;
using ASP.NETDesktop.Models.Responses;
using ASP.NETDesktop.Services.Models;

namespace ASP.NETDesktop.Services.Interfaces {
    public interface IVacationService {
        Task<ServiceResult<List<VacationApiModel>>> ListAsync();
        Task<ServiceResult<List<VacationApiModel>>> ListByDeveloperIdAsync(Guid id);
        Task<ServiceResult<VacationApiModel>> GetByIdAsync(Guid id);
        Task<ServiceResult<ApiResult>> CreateAsync(VacationApiModel model);
        Task<ServiceResult<ApiResult>> UpdateAsync(VacationApiModel model);
        Task<ServiceResult> DeleteAsync(Guid id);
    }
}

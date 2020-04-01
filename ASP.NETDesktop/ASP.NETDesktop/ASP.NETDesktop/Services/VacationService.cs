using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ASP.NETDesktop.Common.ApiModels;
using ASP.NETDesktop.Helpers;
using ASP.NETDesktop.Models.Responses;
using ASP.NETDesktop.Services.Interfaces;
using ASP.NETDesktop.Services.Models;

namespace ASP.NETDesktop.Services {
    public class VacationService : IVacationService {
        private readonly IApiService _apiService;

        public VacationService(IApiService apiService) {
            _apiService = apiService;
        }

        public async Task<ServiceResult<List<VacationApiModel>>> ListAsync() {
            var response = await _apiService.DoRequestAsync("GET", UrlHelper.VacationList, new { });
            return ServiceResult<List<VacationApiModel>>.State(response);
        }

        public async Task<ServiceResult<List<VacationApiModel>>> ListByDeveloperIdAsync(Guid id) {
            var response = await _apiService.DoRequestAsync("GET", UrlHelper.DeveloperVacations, new { id = id });
            return ServiceResult<List<VacationApiModel>>.State(response);
        }

        public async Task<ServiceResult<VacationApiModel>> GetByIdAsync(Guid id) {
            var response = await _apiService.DoRequestAsync("GET", UrlHelper.GetVacation, new { id = id });
            return ServiceResult<VacationApiModel>.State(response);
        }

        public async Task<ServiceResult<ApiResult>> CreateAsync(VacationApiModel model) {
            var response = await _apiService.DoRequestAsync("POST", UrlHelper.CreateVacation, model);
            return ServiceResult<ApiResult>.State(response);
        }

        public async Task<ServiceResult<ApiResult>> UpdateAsync(VacationApiModel model) {
            var response = await _apiService.DoRequestAsync("POST", UrlHelper.UpdateVacation, model);
            return ServiceResult<ApiResult>.State(response);
        }

        public async Task<ServiceResult> DeleteAsync(Guid id) {
            var response = await _apiService.DoRequestAsync("POST", UrlHelper.DeleteVacation, new { id = id });
            return ServiceResult.State(response);
        }
    }
}

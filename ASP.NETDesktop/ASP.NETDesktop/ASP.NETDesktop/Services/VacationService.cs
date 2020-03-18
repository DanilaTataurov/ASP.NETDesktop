using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ASP.NETDesktop.Common.ApiModels;
using ASP.NETDesktop.Helpers;
using ASP.NETDesktop.Services.Interfaces;
using ASP.NETDesktop.Services.Models;
using Newtonsoft.Json;

namespace ASP.NETDesktop.Services {
    public class VacationService : IVacationService {
        private readonly IApiService _apiService;

        public VacationService(IApiService apiService) {
            _apiService = apiService;
        }

        public async Task<ServiceResult<List<VacationApiModel>>> ListAsync() {
            var response = await _apiService.DoRequestAsync("GET", UrlHelper.baseUrl + UrlHelper.VacationList, new { });

            if (response.IsSuccess) {
                var jsonResult = JsonConvert.DeserializeObject(response.Message).ToString();
                IEnumerable<VacationApiModel> vacations = JsonConvert.DeserializeObject<IEnumerable<VacationApiModel>>(jsonResult);
                List<VacationApiModel> list = vacations.ToList();
                return ServiceResult<List<VacationApiModel>>.Ok(list);
            } else {
                return ServiceResult<List<VacationApiModel>>.Fail(response.Error);
            }
        }

        public async Task<ServiceResult<List<VacationApiModel>>> ListByDeveloperIdAsync(Guid id) {
            var response = await _apiService.DoRequestAsync("GET", UrlHelper.baseUrl + UrlHelper.DeveloperVacations, new { id = id });

            if (response.IsSuccess) {
                var jsonResult = JsonConvert.DeserializeObject(response.Message).ToString();
                IEnumerable<VacationApiModel> vacations = JsonConvert.DeserializeObject<IEnumerable<VacationApiModel>>(jsonResult);
                List<VacationApiModel> list = vacations.ToList();
                return ServiceResult<List<VacationApiModel>>.Ok(list);
            } else {
                return ServiceResult<List<VacationApiModel>>.Fail(response.Error);
            }
        }

        public async Task<ServiceResult<VacationApiModel>> GetByIdAsync(Guid id) {
            var response = await _apiService.DoRequestAsync("GET", UrlHelper.baseUrl + UrlHelper.GetVacation, new { id = id });
            if (response.IsSuccess) {
                var jsonResult = JsonConvert.DeserializeObject(response.Message).ToString();
                VacationApiModel vacation = JsonConvert.DeserializeObject<VacationApiModel>(jsonResult);
                return ServiceResult<VacationApiModel>.Ok(vacation);
            } else {
                return ServiceResult<VacationApiModel>.Fail(response.Error);
            }
        }

        public async Task<ServiceResult<string>> CreateAsync(VacationApiModel model) {
            var response = await _apiService.DoRequestAsync("POST", UrlHelper.baseUrl + UrlHelper.CreateVacation, model);
            if (response.IsSuccess) {
                var jsonResult = JsonConvert.DeserializeObject(response.Message).ToString();
                return ServiceResult<string>.Ok(jsonResult);
            } else {
                return ServiceResult<string>.Fail(response.Error);
            }
        }

        public async Task<ServiceResult<string>> UpdateAsync(VacationApiModel model) {
            var response = await _apiService.DoRequestAsync("POST", UrlHelper.baseUrl + UrlHelper.UpdateVacation, model);
            if (response.IsSuccess) {
                var jsonResult = JsonConvert.DeserializeObject(response.Message).ToString();
                return ServiceResult<string>.Ok(jsonResult);
            } else {
                return ServiceResult<string>.Fail(response.Error);
            }
        }

        public async Task<ServiceResult> DeleteAsync(Guid id) {
            var response = await _apiService.DoRequestAsync("POST", UrlHelper.baseUrl + UrlHelper.DeleteVacation, new { id = id });
            if (response.IsSuccess) {
                return ServiceResult.Ok();
            } else {
                return ServiceResult.Fail(response.Error);
            }
        }
    }
}

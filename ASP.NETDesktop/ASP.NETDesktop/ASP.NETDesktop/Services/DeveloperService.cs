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
    public class DeveloperService : IDeveloperService {
        private readonly IApiService _apiService;

        public DeveloperService(IApiService apiService) {
            _apiService = apiService;
        }

        public async Task<ServiceResult<List<DeveloperApiModel>>> ListAsync() {
            var response = await _apiService.DoRequestAsync("GET", UrlHelper.DeveloperList, new {});

            if (response.IsSuccess) {
                var jsonResult = JsonConvert.DeserializeObject(response.Message).ToString();
                IEnumerable<DeveloperApiModel> developers = JsonConvert.DeserializeObject<IEnumerable<DeveloperApiModel>>(jsonResult);
                List<DeveloperApiModel> list = developers.ToList();
                return ServiceResult<List<DeveloperApiModel>>.Ok(list);
            } else {
                return ServiceResult<List<DeveloperApiModel>>.Fail(response.Error);
            }
        }

        public async Task<ServiceResult<DeveloperApiModel>> GetByIdAsync(Guid id) {
            var response = await _apiService.DoRequestAsync("GET", UrlHelper.GetDeveloper, new { id = id });

            if (response.IsSuccess) {
                var jsonResult = JsonConvert.DeserializeObject(response.Message).ToString();
                DeveloperApiModel developer = JsonConvert.DeserializeObject<DeveloperApiModel>(jsonResult); 
                return ServiceResult<DeveloperApiModel>.Ok(developer);
            } else {
                return ServiceResult<DeveloperApiModel>.Fail(response.Error);
            }
        }

        public async Task<ServiceResult> CreateAsync(DeveloperApiModel model) {
            var response = await _apiService.DoRequestAsync("POST", UrlHelper.CreateDeveloper, model);
            if (response.IsSuccess) {
                return ServiceResult.Ok();
            } else {
                return ServiceResult.Fail(response.Error);
            }
        }

        public async Task<ServiceResult> UpdateAsync(DeveloperApiModel model) {
            var response = await _apiService.DoRequestAsync("POST", UrlHelper.UpdateDeveloper, model);
            if (response.IsSuccess) {
                return ServiceResult.Ok();
            } else {
                return ServiceResult.Fail(response.Error);
            }
        }

        public async Task<ServiceResult> DeleteAsync(Guid id) {
            var response = await _apiService.DoRequestAsync("POST", UrlHelper.DeleteDeveloper, new { id = id });
            if (response.IsSuccess) {
                return ServiceResult.Ok();
            } else {
                return ServiceResult.Fail(response.Error);
            }
        }

        public async Task<ServiceResult> AddProjectAsync(Guid developerId, Guid projectId) {
            var response = await _apiService.DoRequestAsync("POST", UrlHelper.AddDeveloperProject, new {
                developerId = developerId, projectId = projectId
            });
            if (response.IsSuccess) {
                return ServiceResult.Ok();
            } else {
                return ServiceResult.Fail(response.Message);
            }
        }

        public async Task<ServiceResult> DeleteProjectAsync(Guid developerId, Guid projectId) {
            var response = await _apiService.DoRequestAsync("POST", UrlHelper.DeleteDeveloperProject, new {
                developerId = developerId, projectId = projectId
            });
            if (response.IsSuccess) {
                return ServiceResult.Ok();
            } else {
                return ServiceResult.Fail(response.Message);
            }
        }
    }
}

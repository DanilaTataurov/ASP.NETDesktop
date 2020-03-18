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
    public class ProjectService : IProjectService {
        private readonly IApiService _apiService;

        public ProjectService(IApiService apiService) {
            _apiService = apiService;
        }

        public async Task<ServiceResult<List<ProjectApiModel>>> ListAsync() {
            var response = await _apiService.DoRequestAsync("GET", UrlHelper.baseUrl + UrlHelper.ProjectList, new { });

            if (response.IsSuccess) {
                var jsonResult = JsonConvert.DeserializeObject(response.Message).ToString();
                IEnumerable<ProjectApiModel> projects = JsonConvert.DeserializeObject<IEnumerable<ProjectApiModel>>(jsonResult);
                List<ProjectApiModel> list = projects.ToList();
                return ServiceResult<List<ProjectApiModel>>.Ok(list);
            } else {
                return ServiceResult<List<ProjectApiModel>>.Fail(response.Error);
            }
        }

        public async Task<ServiceResult<ProjectApiModel>> GetByIdAsync(Guid id) {
            var response = await _apiService.DoRequestAsync("GET", UrlHelper.baseUrl + UrlHelper.GetProject, new { id = id });

            if (response.IsSuccess) {
                var jsonResult = JsonConvert.DeserializeObject(response.Message).ToString();
                ProjectApiModel project = JsonConvert.DeserializeObject<ProjectApiModel>(jsonResult);
                return ServiceResult<ProjectApiModel>.Ok(project);
            } else {
                return ServiceResult<ProjectApiModel>.Fail(response.Error);
            }
        }

        public async Task<ServiceResult> CreateAsync(ProjectApiModel model) {
            var response = await _apiService.DoRequestAsync("POST", UrlHelper.baseUrl + UrlHelper.CreateProject, model);
            if (response.IsSuccess) {
                return ServiceResult.Ok();
            } else {
                return ServiceResult.Fail(response.Error);
            }
        }

        public async Task<ServiceResult> UpdateAsync(ProjectApiModel model) {
            var response = await _apiService.DoRequestAsync("POST", UrlHelper.baseUrl + UrlHelper.UpdateProject, model);
            if (response.IsSuccess) {
                return ServiceResult.Ok();
            } else {
                return ServiceResult.Fail(response.Error);
            }
        }

        public async Task<ServiceResult> DeleteAsync(Guid id) {
            var response = await _apiService.DoRequestAsync("POST", UrlHelper.baseUrl + UrlHelper.DeleteProject, new { id = id });
            if (response.IsSuccess) {
                return ServiceResult.Ok();
            } else {
                return ServiceResult.Fail(response.Error);
            }
        }
    }
}

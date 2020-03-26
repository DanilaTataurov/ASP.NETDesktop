using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ASP.NETDesktop.Common.ApiModels;
using ASP.NETDesktop.Helpers;
using ASP.NETDesktop.Services.Interfaces;
using ASP.NETDesktop.Services.Models;

namespace ASP.NETDesktop.Services {
    public class ProjectService : IProjectService {
        private readonly IApiService _apiService;

        public ProjectService(IApiService apiService) {
            _apiService = apiService;
        }

        public async Task<ServiceResult<List<ProjectApiModel>>> ListAsync() {
            var response = await _apiService.DoRequestAsync("GET", UrlHelper.ProjectList, new { });
            return ServiceResult<List<ProjectApiModel>>.State(response);
        }

        public async Task<ServiceResult<ProjectApiModel>> GetByIdAsync(Guid id) {
            var response = await _apiService.DoRequestAsync("GET", UrlHelper.GetProject, new { id = id });
            return ServiceResult<ProjectApiModel>.State(response);
        }

        public async Task<ServiceResult> CreateAsync(ProjectApiModel model) {
            var response = await _apiService.DoRequestAsync("POST", UrlHelper.CreateProject, model);
            return ServiceResult.State(response);
        }

        public async Task<ServiceResult> UpdateAsync(ProjectApiModel model) {
            var response = await _apiService.DoRequestAsync("POST", UrlHelper.UpdateProject, model);
            return ServiceResult.State(response);
        }

        public async Task<ServiceResult> DeleteAsync(Guid id) {
            var response = await _apiService.DoRequestAsync("POST", UrlHelper.DeleteProject, new { id = id });
            return ServiceResult.State(response);
        }
    }
}

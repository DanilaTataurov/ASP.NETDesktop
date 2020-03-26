using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ASP.NETDesktop.Common.ApiModels;
using ASP.NETDesktop.Helpers;
using ASP.NETDesktop.Services.Interfaces;
using ASP.NETDesktop.Services.Models;

namespace ASP.NETDesktop.Services {
    public class DeveloperService : IDeveloperService {
        private readonly IApiService _apiService;

        public DeveloperService(IApiService apiService) {
            _apiService = apiService;
        }

        public async Task<ServiceResult<List<DeveloperApiModel>>> ListAsync() {
            var response = await _apiService.DoRequestAsync("GET", UrlHelper.DeveloperList, new {});
            return ServiceResult<List<DeveloperApiModel>>.State(response);
        }

        public async Task<ServiceResult<DeveloperApiModel>> GetByIdAsync(Guid id) {
            var response = await _apiService.DoRequestAsync("GET", UrlHelper.GetDeveloper, new { id = id });
            return ServiceResult<DeveloperApiModel>.State(response);
        }

        public async Task<ServiceResult> CreateAsync(DeveloperApiModel model) {
            var response = await _apiService.DoRequestAsync("POST", UrlHelper.CreateDeveloper, model);
            return ServiceResult.State(response);
        }

        public async Task<ServiceResult> UpdateAsync(DeveloperApiModel model) {
            var response = await _apiService.DoRequestAsync("POST", UrlHelper.UpdateDeveloper, model);
            return ServiceResult.State(response);
        }

        public async Task<ServiceResult> DeleteAsync(Guid id) {
            var response = await _apiService.DoRequestAsync("POST", UrlHelper.DeleteDeveloper, new { id = id });
            return ServiceResult.State(response);
        }

        public async Task<ServiceResult> AddProjectAsync(Guid developerId, Guid projectId) {
            var response = await _apiService.DoRequestAsync("POST", UrlHelper.AddDeveloperProject, new {
                developerId = developerId, projectId = projectId
            });
            return ServiceResult.State(response);
        }

        public async Task<ServiceResult> DeleteProjectAsync(Guid developerId, Guid projectId) {
            var response = await _apiService.DoRequestAsync("POST", UrlHelper.DeleteDeveloperProject, new {
                developerId = developerId, projectId = projectId
            });
            return ServiceResult.State(response);
        }
    }
}

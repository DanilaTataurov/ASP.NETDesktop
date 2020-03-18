using System.Threading.Tasks;
using ASP.NETDesktop.Helpers;
using ASP.NETDesktop.Services.Interfaces;
using ASP.NETDesktop.Services.Models;
using Xamarin.Forms;

namespace ASP.NETDesktop.Services {
    public class AccountService : IAccountService {
        private readonly IApiService _apiService;

        public AccountService(IApiService apiService) {
            _apiService = apiService;
        }

        public async Task<ServiceResult> LoginAsync(string username, string password) {
            var response = await _apiService.DoLoginAsync(username, password);
            if (response.IsSuccess) {
                Application.Current.Properties["token"] = response.Message;
                await Application.Current.SavePropertiesAsync();
                return ServiceResult.Ok();
            } else {
                return ServiceResult.Fail(response.Error);
            }
        }

        public async Task<ServiceResult> LogoutAsync() {
            Application.Current.Properties.Remove("token");
            await Application.Current.SavePropertiesAsync();

            var response = await _apiService.DoRequestAsync("POST", UrlHelper.baseUrl + UrlHelper.Logout, new { });
            if (response.IsSuccess) {
                return ServiceResult.Ok();
            }
            return ServiceResult.Fail(response.Error);
        }
    }
}

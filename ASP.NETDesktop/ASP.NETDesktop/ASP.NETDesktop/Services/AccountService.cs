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

            Application.Current.Properties["token"] = response.Message;
            await Application.Current.SavePropertiesAsync();
            return ServiceResult.State(response);
        }

        public async Task<ServiceResult> LogoutAsync() {
            Application.Current.Properties.Remove("token");
            await Application.Current.SavePropertiesAsync();

            var response = await _apiService.DoRequestAsync("POST", UrlHelper.Logout, new { });
            return ServiceResult.State(response);
        }
    }
}

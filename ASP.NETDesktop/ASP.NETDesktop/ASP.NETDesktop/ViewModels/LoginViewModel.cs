using System.Linq;
using System.Text.RegularExpressions;
using ASP.NETDesktop.Helpers;
using ASP.NETDesktop.Services.Interfaces;
using ASP.NETDesktop.ViewModels.Base;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using Xamarin.Forms;

namespace ASP.NETDesktop.ViewModels {
    public class LoginViewModel : BaseViewModel {
        private readonly IAccountService _accountService;
        private readonly INavigationService _navigationService;
        private readonly IPageDialogService _pageDialogService;

        public string Username { get; set; }
        public string Password { get; set; }
        public DelegateCommand LoginCommand { get; set; }

        public LoginViewModel(IAccountService accountService, INavigationService navigationService, IPageDialogService pageDialogService) {
            GetCredentials();
            _accountService = accountService;
            _navigationService = navigationService;
            _pageDialogService = pageDialogService;
            LoginCommand = new DelegateCommand(Login);
        }

        private void GetCredentials() {
            var properties = Application.Current.Properties;

            if (properties.ContainsKey("username") && properties.ContainsKey("password")) {
                Username = properties.FirstOrDefault(x => x.Key == "username").Value.ToString();
                Password = properties.FirstOrDefault(x => x.Key == "password").Value.ToString();
            }
        }

        private void SaveCredentials() {
            Application.Current.Properties["username"] = Username;
            Application.Current.Properties["password"] = Password;
        }

        private bool IsValid() {
            if (string.IsNullOrEmpty(Username) || string.IsNullOrEmpty(Password)) {
                _pageDialogService.DisplayAlertAsync("Please fill in all the form fields.", "", "OK");
                return false;
            }
            if (!Regex.IsMatch(Username, ValidationHelper.EmailPattern)) {
                _pageDialogService.DisplayAlertAsync("Email field is not correct.", "", "OK");
                return false;
            }
            return true;
        }

        private async void Login() {
            if (!IsValid()) return;
            var result = await _accountService.LoginAsync(Username, Password);
            if (!result.IsSuccess) {
                await _pageDialogService.DisplayAlertAsync(result.Error, "", "OK");
                return;
            }
            SaveCredentials();
            await _navigationService.NavigateAsync("/NavigationPage/MainView");
        }
    }
}

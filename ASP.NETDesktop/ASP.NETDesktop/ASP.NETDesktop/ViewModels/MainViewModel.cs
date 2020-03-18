using ASP.NETDesktop.Services.Interfaces;
using ASP.NETDesktop.ViewModels.Base;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;

namespace ASP.NETDesktop.ViewModels {
    public class MainViewModel : BaseViewModel {
        private readonly IAccountService _accountService;
        private readonly IPageDialogService _pageDialogService;
        private readonly INavigationService _navigationService;

        public DelegateCommand DevelopersCommand { get; set; }
        public DelegateCommand ProjectsCommand { get; set; }
        public DelegateCommand LogoutCommand { get; set; }

        public MainViewModel(IAccountService accountService, IPageDialogService pageDialogService, INavigationService navigationService) {
            _accountService = accountService;
            _pageDialogService = pageDialogService;
            _navigationService = navigationService;
            DevelopersCommand = new DelegateCommand(DeveloperList);
            ProjectsCommand = new DelegateCommand(ProjectList);
            LogoutCommand = new DelegateCommand(Logout);
        }

        private async void DeveloperList() {
            if (!IsConnected()) {
                await _pageDialogService.DisplayAlertAsync("", errorConnectionMessage, "Ok");
            } else {
                await _navigationService.NavigateAsync("/NavigationPage/DevelopersView");
            }
        }

        private async void ProjectList() {
            if (!IsConnected()) {
                await _pageDialogService.DisplayAlertAsync("", errorConnectionMessage, "Ok");
            } else {
                await _navigationService.NavigateAsync("/NavigationPage/ProjectsView");
            }
        }

        private async void Logout() {
            var result = await _accountService.LogoutAsync();
            if (result.IsSuccess) {
                await _navigationService.NavigateAsync("/NavigationPage/LoginView");
            } else {
                await _pageDialogService.DisplayAlertAsync("", result.Error, "OK");
            }
        }
    }
}

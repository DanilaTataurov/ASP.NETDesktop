using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ASP.NETDesktop.Common.ApiModels;
using ASP.NETDesktop.Services.Interfaces;
using ASP.NETDesktop.ViewModels.Base;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;

namespace ASP.NETDesktop.ViewModels {
    public class DevelopersViewModel : BaseViewModel {
        private readonly IDeveloperService _developerService;
        private readonly IPageDialogService _pageDialogService;
        private readonly INavigationService _navigationService;
        public DelegateCommand BackCommand { get; set; }
        public DelegateCommand AddCommand { get; set; }
        public List<DeveloperApiModel> Developers { get; set; }
        private DeveloperApiModel _selectedDeveloper { get; set; }

        public DeveloperApiModel SelectedDeveloper {
            get { return _selectedDeveloper; }
            set {
                if (_selectedDeveloper != value) {
                    _selectedDeveloper = value;
                    GetAsync();
                }
            }
        }

        public DevelopersViewModel(IDeveloperService developerService, IPageDialogService pageDialogService, INavigationService navigationService) {
            _developerService = developerService;
            _pageDialogService = pageDialogService;
            _navigationService = navigationService;
            BackCommand = new DelegateCommand(BackAsync);
            AddCommand = new DelegateCommand(AddAsync);

            var list = Task.Run(() => ListAsync());
            Developers = new List<DeveloperApiModel>(list.Result.ToList());
        }

        private async Task<List<DeveloperApiModel>> ListAsync() {
            var result = await _developerService.ListAsync();
            if (result.IsSuccess) {
                return result.Data;
            } else {
                await _navigationService.NavigateAsync("/NavigationPage/MainView");
                return null;
            }
        }

        private async void GetAsync() {
            if (!IsConnected()) {
                await _pageDialogService.DisplayAlertAsync("", errorConnectionMessage, "Ok");
            } else {
                await _navigationService.NavigateAsync("/NavigationPage/DeveloperView", new NavigationParameters { { "Id", SelectedDeveloper.Id } });
            }
        }

        private async void AddAsync() {
            if (!IsConnected()) {
                await _pageDialogService.DisplayAlertAsync("", errorConnectionMessage, "Ok");
            } else {
                await _navigationService.NavigateAsync("/NavigationPage/AddDeveloperView");
            }
        }

        private async void BackAsync() {
            if (!IsConnected()) {
                await _pageDialogService.DisplayAlertAsync("", errorConnectionMessage, "Ok");
            } else {
                await _navigationService.NavigateAsync("/NavigationPage/MainView");
            }
        }
    }
}

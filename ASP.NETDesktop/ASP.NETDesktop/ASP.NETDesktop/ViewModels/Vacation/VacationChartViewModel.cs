using System;
using System.Linq;
using ASP.NETDesktop.ViewModels.Base;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;

namespace ASP.NETDesktop.ViewModels.Vacation {
    public class VacationChartViewModel : BaseViewModel, INavigationAware {
        private readonly INavigationService _navigationService;
        private readonly IPageDialogService _pageDialogService;

        public Guid Id { get; set; }
        public bool IsCreate { get; set; }
        public DelegateCommand BackCommand { get; set; }

        public VacationChartViewModel(INavigationService navigationService, IPageDialogService pageDialogService) {
            _navigationService = navigationService;
            _pageDialogService = pageDialogService;
            BackCommand = new DelegateCommand(BackAsync);
        }

        private async void BackAsync() {
            if (!IsConnected()) {
                await _pageDialogService.DisplayAlertAsync("", errorConnectionMessage, "Ok");
            } else {
                if (IsCreate) {
                    await _navigationService.NavigateAsync("/NavigationPage/AddVacationView", new NavigationParameters { { "Id", Id } });
                } else {
                    await _navigationService.NavigateAsync("/NavigationPage/EditVacationView", new NavigationParameters { { "Id", Id } });
                }
            }
        }

        public void OnNavigatedTo(INavigationParameters parameters) {
            Id = Guid.Parse(parameters.FirstOrDefault(x => x.Key == "Id").Value.ToString());
            IsCreate = (bool)parameters.FirstOrDefault(x => x.Key == "IsCreate").Value;
        }

        public void OnNavigatedFrom(INavigationParameters parameters) { }
    }
}

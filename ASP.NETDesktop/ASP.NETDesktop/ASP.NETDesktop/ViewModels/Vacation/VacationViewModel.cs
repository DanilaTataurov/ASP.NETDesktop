using System;
using System.Linq;
using System.Threading.Tasks;
using ASP.NETDesktop.Models;
using ASP.NETDesktop.Services.Interfaces;
using ASP.NETDesktop.ViewModels.Base;
using AutoMapper;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;

namespace ASP.NETDesktop.ViewModels {
    public class VacationViewModel : BaseViewModel, INavigationAware {
        private readonly IMapper _mapper;
        private readonly IVacationService _vacationService;
        private readonly IPageDialogService _pageDialogService;
        private readonly INavigationService _navigationService;

        public Guid Id { get; set; }

        private VacationModel _vacation;
        public VacationModel Vacation { get => _vacation; set => SetProperty(ref _vacation, value); }
        private string _status;
        public string Status { get => _status; set => SetProperty(ref _status, value); }
        public DelegateCommand BackCommand { get; set; }
        public DelegateCommand UpdateCommand { get; set; }
        public DelegateCommand DeleteCommand { get; set; }

        public VacationViewModel(IMapper mapper, IVacationService vacationService, IPageDialogService pageDialogService, INavigationService navigationService) {
            _mapper = mapper;
            _vacationService = vacationService;
            _pageDialogService = pageDialogService;
            _navigationService = navigationService;
            BackCommand = new DelegateCommand(BackAsync);
            UpdateCommand = new DelegateCommand(UpdateAsync);
            DeleteCommand = new DelegateCommand(DeleteAsync);
        }

        private async Task<VacationModel> GetAsync(Guid id) {
            var result = await _vacationService.GetByIdAsync(id);
            VacationModel vacation = _mapper.Map<VacationModel>(result.Data);
            return vacation;
        }

        private async void UpdateAsync() {
            if (!IsConnected()) {
                await _pageDialogService.DisplayAlertAsync("", errorConnectionMessage, "Ok");
            } else {
                await _navigationService.NavigateAsync("/NavigationPage/EditVacationView", new NavigationParameters { { "Id", Id } });
            }
        }

        private async void DeleteAsync() {
            bool delete = await _pageDialogService.DisplayAlertAsync("Are you sure you want to delete this vacation?", "", "Ok", "Cancel");
            if (delete) {
                var result = await _vacationService.DeleteAsync(Id);
                if (result.IsSuccess) { 
                    await _navigationService.NavigateAsync("/NavigationPage/VacationsView", new NavigationParameters{ { "Id", Vacation.DeveloperId } });
                } else {
                    await _pageDialogService.DisplayAlertAsync("", result.Error, "OK");
                }
            }
        }

        private async void BackAsync() {
            if (!IsConnected()) {
                await _pageDialogService.DisplayAlertAsync("", errorConnectionMessage, "Ok");
            } else {
                await _navigationService.NavigateAsync("/NavigationPage/VacationsView", new NavigationParameters { { "Id", Vacation.DeveloperId } });
            }
        }

        public void OnNavigatedTo(INavigationParameters parameters) {
            Id = Guid.Parse(parameters.FirstOrDefault(x => x.Key == "Id").Value.ToString());
            var vacation = Task.Run(() => GetAsync(Id));
            Vacation = vacation.Result;
            Status = Vacation.Status;
        }

        public void OnNavigatedFrom(INavigationParameters parameters) { }
    }
}

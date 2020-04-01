using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ASP.NETDesktop.Common.ApiModels;
using ASP.NETDesktop.Models;
using ASP.NETDesktop.Services.Interfaces;
using ASP.NETDesktop.ViewModels.Base;
using AutoMapper;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;

namespace ASP.NETDesktop.ViewModels.Vacation {
    public class VacationsViewModel : BaseViewModel, INavigationAware {
        private readonly IMapper _mapper;
        private readonly IVacationService _vacationService;
        private readonly INavigationService _navigationService;
        private readonly IPageDialogService _pageDialogService;

        public Guid Id { get; set; }
        public DelegateCommand AddCommand { get; set; }
        public DelegateCommand BackCommand { get; set; }

        private List<VacationModel> _vacations;
        public List<VacationModel> Vacations { get => _vacations; set => SetProperty(ref _vacations, value); }

        private VacationModel _selectedVacation { get; set; }
        public VacationModel SelectedVacation {
            get { return _selectedVacation; }
            set {
                if (_selectedVacation != value) {
                    _selectedVacation = value;
                    GetAsync();
                }
            }
        }

        public VacationsViewModel(IMapper mapper, IVacationService vacationService, INavigationService navigationService, IPageDialogService pageDialogService) {
            _mapper = mapper;
            _vacationService = vacationService;
            _navigationService = navigationService;
            _pageDialogService = pageDialogService;
            BackCommand = new DelegateCommand(BackAsync);
            AddCommand = new DelegateCommand(AddAsync);
            Vacations = new List<VacationModel>();
        }

        private async Task<List<VacationApiModel>> ListAsync(Guid id) {
            var result = await _vacationService.ListByDeveloperIdAsync(id);
            return result.Data;
        }

        private async void GetAsync() {
            if (!IsConnected()) {
                await _pageDialogService.DisplayAlertAsync("", errorConnectionMessage, "Ok");
            } else {
                await _navigationService.NavigateAsync("/NavigationPage/VacationView", new NavigationParameters { { "Id", SelectedVacation.Id } });
            }
        }

        private async void AddAsync() {
            if (!IsConnected()) {
                await _pageDialogService.DisplayAlertAsync("", errorConnectionMessage, "Ok");
            } else {
                await _navigationService.NavigateAsync("/NavigationPage/AddVacationView", new NavigationParameters { { "Id", Id } });
            }
        }

        private async void BackAsync() {
            if (!IsConnected()) {
                await _pageDialogService.DisplayAlertAsync("", errorConnectionMessage, "Ok");
            } else {
                await _navigationService.NavigateAsync("/NavigationPage/DeveloperView", new NavigationParameters { {  "Id", Id } });
            }
        }

        public void OnNavigatedTo(INavigationParameters parameters) {
            Id = Guid.Parse(parameters.FirstOrDefault(x => x.Key == "Id").Value.ToString());
            var list = Task.Run(() => ListAsync(Id));
            Vacations = new List<VacationModel>(_mapper.Map<List<VacationModel>>(list.Result.ToList()));
        }

        public void OnNavigatedFrom(INavigationParameters parameters) { }
    }
}

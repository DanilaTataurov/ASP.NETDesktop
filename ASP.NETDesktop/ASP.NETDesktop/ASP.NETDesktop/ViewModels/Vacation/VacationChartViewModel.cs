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
    public class VacationChartViewModel : BaseViewModel, INavigationAware {
        private readonly IMapper _mapper;
        private readonly IVacationService _vacationService;
        private readonly INavigationService _navigationService;
        private readonly IPageDialogService _pageDialogService;

        public Guid Id { get; set; }
        public bool IsCreate { get; set; }
        public DelegateCommand BackCommand { get; set; }

        private List<VacationModel> _vacations;
        public List<VacationModel> Vacations { get => _vacations; set => SetProperty(ref _vacations, value); }

        public VacationChartViewModel(IMapper mapper, IVacationService vacationService, INavigationService navigationService, IPageDialogService pageDialogService) {
            _mapper = mapper;
            _vacationService = vacationService;
            _navigationService = navigationService;
            _pageDialogService = pageDialogService;
            BackCommand = new DelegateCommand(BackAsync);
            Vacations = new List<VacationModel>();
        }

        private async Task<List<VacationApiModel>> ListAsync() {
            var result = await _vacationService.ListAsync();
            return result.Data;
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

            var list = Task.Run(() => ListAsync());
            Vacations = new List<VacationModel>(_mapper.Map<List<VacationModel>>(list.Result.ToList()));
        }

        public void OnNavigatedFrom(INavigationParameters parameters) { }
    }
}

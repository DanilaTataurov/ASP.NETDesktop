using System;
using System.Collections.Generic;
using System.Linq;
using ASP.NETDesktop.Common.ApiModels;
using ASP.NETDesktop.Common.Enums;
using ASP.NETDesktop.Common.Extensions;
using ASP.NETDesktop.Models;
using ASP.NETDesktop.Services.Interfaces;
using ASP.NETDesktop.ViewModels.Base;
using AutoMapper;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;

namespace ASP.NETDesktop.ViewModels.Vacation {
    public class AddVacationViewModel : BaseViewModel, INavigatedAware {
        private readonly IMapper _mapper;
        private readonly IVacationService _vacationService;
        private readonly IPageDialogService _pageDialogService;
        private readonly INavigationService _navigationService;

        public List<string> _statusDescriptions;
        public List<string> StatusDescriptions { get => _statusDescriptions; set => SetProperty(ref _statusDescriptions, value); }

        public string _selectedStatus;
        public string SelectedStatus { get => _selectedStatus; set => SetProperty(ref _selectedStatus, value); }

        public Guid Id { get; set; }

        private VacationModel _vacation;
        public VacationModel Vacation { get => _vacation; set => SetProperty(ref _vacation, value); }
        public DelegateCommand BackCommand { get; set; }
        public DelegateCommand CreateCommand { get; set; }
        public DelegateCommand ChartCommand { get; set; }


        private DateTime _minimumDate = new DateTime(DateTime.Now.Year, 1, 1);
        public DateTime MinimumDate { get => _minimumDate; set => SetProperty(ref _minimumDate, value); }

        private DateTime _maximumDate = new DateTime(DateTime.Now.Year, 1, 1).AddYears(1).AddTicks(-1);
        public DateTime MaximumDate { get => _maximumDate; set => SetProperty(ref _maximumDate, value); }

        public AddVacationViewModel(IMapper mapper, IVacationService vacationService, IPageDialogService pageDialogService, INavigationService navigationService) {
            _mapper = mapper;
            _vacationService = vacationService;
            _pageDialogService = pageDialogService;
            _navigationService = navigationService;
            BackCommand = new DelegateCommand(Back);
            CreateCommand = new DelegateCommand(Create);
            ChartCommand = new DelegateCommand(GetChart);
            Vacation = new VacationModel();
            StatusDescriptions = EnumExtensions.GetDescriptions<VacationStatus>();
        }

        private async void Back() {
            if (!IsConnected()) {
                await _pageDialogService.DisplayAlertAsync("", errorConnectionMessage, "Ok");
            } else {
                await _navigationService.NavigateAsync("/NavigationPage/VacationsView", new NavigationParameters { { "Id", Id } });
            }
        }

        private async void Create() {
            if (Vacation.StartDate > Vacation.EndDate) {
                await _pageDialogService.DisplayAlertAsync("", "Start date cannot be more than end date", "OK");
                return;
            }

            VacationApiModel model = _mapper.Map<VacationApiModel>(Vacation);
            model.DeveloperId = Id;
            model.Status = EnumExtensions.ParseDescriptionToEnum<VacationStatus>(SelectedStatus);
            var result = await _vacationService.CreateAsync(model);

            if (result.IsSuccess) {
                await _pageDialogService.DisplayAlertAsync("", result.Data, "Ok");
                if (result.Data.Contains("success")) {
                    await _navigationService.NavigateAsync("/NavigationPage/VacationsView", new NavigationParameters { { "Id", Id } });
                }
            } else {
                await _pageDialogService.DisplayAlertAsync("", result.Error, "OK");
            }
        }

        public async void GetChart() {
            await _navigationService.NavigateAsync("/NavigationPage/VacationChartView", new NavigationParameters { { "Id", Id }, {"IsCreate", true} });
        }

        public void OnNavigatedTo(INavigationParameters parameters) {
            Id = Guid.Parse(parameters.FirstOrDefault(x => x.Key == "Id").Value.ToString());
            Vacation.StartDate = DateTime.UtcNow;
            Vacation.EndDate = DateTime.UtcNow;
            SelectedStatus = EnumExtensions.GetEnumDescription(VacationStatus.CanMove);
        }

        public void OnNavigatedFrom(INavigationParameters parameters) { }
    }
}

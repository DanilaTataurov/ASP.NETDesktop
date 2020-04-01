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
    public class DeveloperViewModel : BaseViewModel, INavigationAware {
        private readonly IMapper _mapper;
        private readonly IDeveloperService _developerService;
        private readonly IPageDialogService _pageDialogService;
        private readonly INavigationService _navigationService;

        public Guid Id { get; set; }

        private DeveloperModel _developer;
        public DeveloperModel Developer { get => _developer; set => SetProperty(ref _developer, value); }

        public DelegateCommand BackCommand { get; set; }
        public DelegateCommand UpdateCommand { get; set; }
        public DelegateCommand DeleteCommand { get; set; }
        public DelegateCommand GetVacationsCommand { get; set; }
        public DelegateCommand GetProjectsCommand { get; set; }

        public DeveloperViewModel(IMapper mapper, IDeveloperService developerService, IPageDialogService pageDialogService, INavigationService navigationService) {
            _mapper = mapper;
            _developerService = developerService;
            _pageDialogService = pageDialogService;
            _navigationService = navigationService;
            BackCommand = new DelegateCommand(BackAsync);
            UpdateCommand = new DelegateCommand(UpdateAsync);
            DeleteCommand = new DelegateCommand(DeleteAsync);
            GetVacationsCommand = new DelegateCommand(GetVacationsAsync);
            GetProjectsCommand = new DelegateCommand(GetProjectsAsync);
        }

        private async Task<DeveloperModel> GetAsync(Guid id) {
            var result = await _developerService.GetByIdAsync(id);
            DeveloperModel developer = _mapper.Map<DeveloperModel>(result.Data);
            return developer;
        }

        public async void GetVacationsAsync() {
            if (!IsConnected()) {
                await _pageDialogService.DisplayAlertAsync("", errorConnectionMessage, "Ok");
            } else {
                await _navigationService.NavigateAsync("/NavigationPage/VacationsView", new NavigationParameters { { "Id", Id } });
            }
        }

        public async void GetProjectsAsync() {
            if (!IsConnected()) {
                await _pageDialogService.DisplayAlertAsync("", errorConnectionMessage, "Ok");
            } else {
                await _navigationService.NavigateAsync("/NavigationPage/DeveloperProjectsView", new NavigationParameters { { "Id", Id } });
            }
        }

        private async void UpdateAsync() {
            if (!IsConnected()) {
                await _pageDialogService.DisplayAlertAsync("", errorConnectionMessage, "Ok");
            } else {
                await _navigationService.NavigateAsync("/NavigationPage/EditDeveloperView", new NavigationParameters { { "Id", Id } });
            }
        }

        private async void DeleteAsync() {
            bool delete = await _pageDialogService.DisplayAlertAsync("Are you sure you want to delete this developer?", "", "Ok", "Cancel");
            if (delete) {
                var result = await _developerService.DeleteAsync(Id);
                if (result.IsSuccess) {
                    await _navigationService.NavigateAsync("/NavigationPage/DevelopersView");
                } else {
                    await _pageDialogService.DisplayAlertAsync("", result.Error, "OK");
                }
            }
        }

        private async void BackAsync() {
            if (!IsConnected()) {
                await _pageDialogService.DisplayAlertAsync("", errorConnectionMessage, "Ok");
            } else {
                await _navigationService.NavigateAsync("/NavigationPage/DevelopersView");
            }
        }

        public void OnNavigatedTo(INavigationParameters parameters) {
            Id = Guid.Parse(parameters.FirstOrDefault(x => x.Key == "Id").Value.ToString());
            var developer = Task.Run(() => GetAsync(Id));
            Developer = developer.Result;
        }

        public void OnNavigatedFrom(INavigationParameters parameters) { }
    }
}

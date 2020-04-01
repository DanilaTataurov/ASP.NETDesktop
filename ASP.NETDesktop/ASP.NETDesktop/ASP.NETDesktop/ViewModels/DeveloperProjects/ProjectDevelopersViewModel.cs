using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ASP.NETDesktop.Common.ApiModels;
using ASP.NETDesktop.Services.Interfaces;
using ASP.NETDesktop.ViewModels.Base;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using Xamarin.Forms;

namespace ASP.NETDesktop.ViewModels.DeveloperProjects {
    public class ProjectDevelopersViewModel : BaseViewModel, INavigatedAware {
        private readonly IDeveloperService _developerService;
        private readonly IProjectService _projectService;
        private readonly IPageDialogService _pageDialogService;
        private readonly INavigationService _navigationService;

        private Guid _id;
        public Guid Id { get => _id; set => SetProperty(ref _id, value); }
        public DelegateCommand BackCommand { get; set; }
        public DelegateCommand AddCommand { get; set; }
        public Command<object> DeleteCommand { get; set; }

        private List<DeveloperApiModel> _developer;
        public List<DeveloperApiModel> Developers { get => _developer; set => SetProperty(ref _developer, value); }

        public ProjectDevelopersViewModel(IDeveloperService developerService, IProjectService projectService, IPageDialogService pageDialogService, INavigationService navigationService) {
            _developerService = developerService;
            _projectService = projectService;
            _pageDialogService = pageDialogService;
            _navigationService = navigationService;
            BackCommand = new DelegateCommand(BackAsync);
            AddCommand = new DelegateCommand(AddAsync);
            DeleteCommand = new Command<object>(DeleteAsync);
        }

        private async Task<List<DeveloperApiModel>> ListAsync(Guid id) {
            var result = await _projectService.GetByIdAsync(id);
            List<DeveloperApiModel> developers = result.Data.Developers.ToList();
            return developers;
        }

        private async void BackAsync() {
            if (!IsConnected()) {
                await _pageDialogService.DisplayAlertAsync("", errorConnectionMessage, "Ok");
            } else {
                await _navigationService.NavigateAsync("/NavigationPage/ProjectView", new NavigationParameters { { "Id", Id } });
            }
        }

        public async void AddAsync() {
            if (!IsConnected()) {
                await _pageDialogService.DisplayAlertAsync("", errorConnectionMessage, "Ok");
            } else {
                await _navigationService.NavigateAsync("/NavigationPage/AddProjectDevelopersView", new NavigationParameters { { "Id", Id } });
            }
        }

        public async void DeleteAsync(object obj) {
            Guid id = Guid.Parse(obj.ToString());
            if (!IsConnected()) {
                await _pageDialogService.DisplayAlertAsync("", errorConnectionMessage, "Ok");
            } else {
                bool delete = await _pageDialogService.DisplayAlertAsync("Are you sure you want to delete this developer?", "", "Ok", "Cancel");
                if (delete) {
                    await _developerService.DeleteProjectAsync(id, Id);
                    await _navigationService.NavigateAsync("/NavigationPage/ProjectDevelopersView", new NavigationParameters { { "Id", Id } });
                }
            }
        }

        public void OnNavigatedTo(INavigationParameters parameters) {
            Id = Guid.Parse(parameters.FirstOrDefault(x => x.Key == "Id").Value.ToString());
            var list = Task.Run(() => ListAsync(Id));
            Developers = new List<DeveloperApiModel>(list.Result.ToList());
        }

        public void OnNavigatedFrom(INavigationParameters parameters) { }
    }
}

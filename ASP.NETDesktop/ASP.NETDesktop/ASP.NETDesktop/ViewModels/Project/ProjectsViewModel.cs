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
    public class ProjectsViewModel : BaseViewModel {
        private readonly IProjectService _projectService;
        private readonly IPageDialogService _pageDialogService;
        private readonly INavigationService _navigationService;
        public DelegateCommand BackCommand { get; set; }
        public DelegateCommand AddCommand { get; set; }
        public List<ProjectApiModel> Projects { get; set; }
        private ProjectApiModel _selectedProject { get; set; }

        public ProjectApiModel SelectedProject {
            get { return _selectedProject; }
            set {
                if (_selectedProject != value) {
                    _selectedProject = value;
                    GetAsync();
                }
            }
        }

        public ProjectsViewModel(IProjectService projectService, IPageDialogService pageDialogService, INavigationService navigationService) {
            _projectService = projectService;
            _pageDialogService = pageDialogService;
            _navigationService = navigationService;
            BackCommand = new DelegateCommand(BackAsync);
            AddCommand = new DelegateCommand(AddAsync);

            var list = Task.Run(() => ListAsync());
            var result = list.Result.ToList();
            Projects = new List<ProjectApiModel>(result);
        }

        private async Task<List<ProjectApiModel>> ListAsync() {
            var result = await _projectService.ListAsync();
            List<ProjectApiModel> projects = result.Data;
            return projects;
        }

        private async void GetAsync() {
            if (!IsConnected()) {
                await _pageDialogService.DisplayAlertAsync("", errorConnectionMessage, "Ok");
            } else {
                await _navigationService.NavigateAsync("/NavigationPage/ProjectView", new NavigationParameters { { "Id", SelectedProject.Id } });
            }
        }

        private async void AddAsync() {
            if (!IsConnected()) {
                await _pageDialogService.DisplayAlertAsync("", errorConnectionMessage, "Ok");
            } else {
                await _navigationService.NavigateAsync("/NavigationPage/AddProjectView");
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

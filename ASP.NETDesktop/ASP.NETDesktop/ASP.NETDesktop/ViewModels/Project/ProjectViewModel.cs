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
    public class ProjectViewModel : BaseViewModel, INavigationAware {
        private readonly IMapper _mapper;
        private readonly IProjectService _projectService;
        private readonly IPageDialogService _pageDialogService;
        private readonly INavigationService _navigationService;

        public Guid Id { get; set; }

        private ProjectModel _project;
        public ProjectModel Project { get => _project; set => SetProperty(ref _project, value); }
        public DelegateCommand BackCommand { get; set; }
        public DelegateCommand UpdateCommand { get; set; }
        public DelegateCommand DeleteCommand { get; set; }
        public DelegateCommand GetDevelopersCommand { get; set; }

        public ProjectViewModel(IMapper mapper, IProjectService projectService, IPageDialogService pageDialogService, INavigationService navigationService) {
            _mapper = mapper;
            _projectService = projectService;
            _pageDialogService = pageDialogService;
            _navigationService = navigationService;
            BackCommand = new DelegateCommand(BackAsync);
            UpdateCommand = new DelegateCommand(UpdateAsync);
            DeleteCommand = new DelegateCommand(DeleteAsync);
            GetDevelopersCommand = new DelegateCommand(GetDevelopersAsync);
        }

        private async Task<ProjectModel> GetAsync(Guid id) {
            var result = await _projectService.GetByIdAsync(id);
            ProjectModel project = _mapper.Map<ProjectModel>(result.Data);
            return project;
        }

        private async void GetDevelopersAsync() {
            if (!IsConnected()) {
                await _pageDialogService.DisplayAlertAsync("", errorConnectionMessage, "Ok");
            } else {
                await _navigationService.NavigateAsync("/NavigationPage/ProjectDevelopersView", new NavigationParameters { { "Id", Id } });
            }
        }

        private async void UpdateAsync() {
            if (!IsConnected())
            {
                await _pageDialogService.DisplayAlertAsync("", errorConnectionMessage, "Ok");
            } else {
                await _navigationService.NavigateAsync("/NavigationPage/EditProjectView", new NavigationParameters { { "Id", Id } });
            }
        }

        private async void DeleteAsync() {
            bool delete = await _pageDialogService.DisplayAlertAsync("Are you sure you want to delete this project?", "", "Ok", "Cancel");
            if (delete) {
                var result = await _projectService.DeleteAsync(Id);
                if (result.IsSuccess) {
                    await _navigationService.NavigateAsync("/NavigationPage/ProjectsView");
                } else {
                    await _pageDialogService.DisplayAlertAsync("", result.Error, "OK");
                }
            }
        }

        private async void BackAsync() {
            if (!IsConnected()) {
                await _pageDialogService.DisplayAlertAsync("", errorConnectionMessage, "Ok");
            } else {
                await _navigationService.NavigateAsync("/NavigationPage/ProjectsView");
            }
        }

        public void OnNavigatedTo(INavigationParameters parameters) {
            Id = Guid.Parse(parameters.FirstOrDefault(x => x.Key == "Id").Value.ToString());

            var project = Task.Run(() => GetAsync(Id));
            Project = project.Result;
        }

        public void OnNavigatedFrom(INavigationParameters parameters) { }
    }
}

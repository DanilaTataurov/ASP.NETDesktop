using System;
using ASP.NETDesktop.Common.ApiModels;
using ASP.NETDesktop.Models;
using ASP.NETDesktop.Services.Interfaces;
using ASP.NETDesktop.ViewModels.Base;
using AutoMapper;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;

namespace ASP.NETDesktop.ViewModels {
    public class AddProjectViewModel : BaseViewModel {
        private readonly IMapper _mapper;
        private readonly IProjectService _projectService;
        private readonly IPageDialogService _pageDialogService;
        private readonly INavigationService _navigationService;

        private ProjectModel _project;
        public ProjectModel Project { get => _project; set => SetProperty(ref _project, value); }
        public DelegateCommand BackCommand { get; set; }
        public DelegateCommand CreateCommand { get; set; }

        public AddProjectViewModel(IMapper mapper, IProjectService projectService, IPageDialogService pageDialogService, INavigationService navigationService) {
            _mapper = mapper;
            _projectService = projectService;
            _pageDialogService = pageDialogService;
            _navigationService = navigationService;
            BackCommand = new DelegateCommand(Back);
            CreateCommand = new DelegateCommand(Create);
            Project = new ProjectModel();
            Project.StartDate = DateTime.UtcNow;
            Project.EndDate = DateTime.UtcNow;
        }

        private async void Back() {
            if (!IsConnected()) {
                await _pageDialogService.DisplayAlertAsync("", errorConnectionMessage, "Ok");
            } else {
                await _navigationService.NavigateAsync("/NavigationPage/ProjectsView");
            }
        }

        private async void Create() {
            ProjectApiModel model = _mapper.Map<ProjectApiModel>(Project);
            var result = await _projectService.CreateAsync(model);
            if (result.IsSuccess) {
                await _navigationService.NavigateAsync("/NavigationPage/ProjectsView");
            } else {
                await _pageDialogService.DisplayAlertAsync("", result.Error, "OK");
            }
        }
    }
}

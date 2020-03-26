using System;
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

namespace ASP.NETDesktop.ViewModels {
    public class EditProjectViewModel : BaseViewModel, INavigationAware {
        private readonly IMapper _mapper;
        private readonly IProjectService _projectService;
        private readonly IPageDialogService _pageDialogService;
        private readonly INavigationService _navigationService;

        public Guid Id { get; set; }

        private ProjectModel _project;
        public ProjectModel Project { get => _project; set => SetProperty(ref _project, value); }
        public DelegateCommand BackCommand { get; set; }
        public DelegateCommand EditCommand { get; set; }

        public EditProjectViewModel(IMapper mapper, IProjectService projectService, IPageDialogService pageDialogService, INavigationService navigationService) {
            _mapper = mapper;
            _projectService = projectService;
            _pageDialogService = pageDialogService;
            _navigationService = navigationService;
            BackCommand = new DelegateCommand(BackAsync);
            EditCommand = new DelegateCommand(EditAsync);
        }

        private async Task<ProjectModel> GetAsync(Guid id) {
            var result = await _projectService.GetByIdAsync(id);
            ProjectModel project = _mapper.Map<ProjectModel>(result.Data);
            return project;
        }

        private async void EditAsync() {
            ProjectApiModel model = _mapper.Map<ProjectApiModel>(Project);
            model.Id = Id;
            var result = await _projectService.UpdateAsync(model);
            if (result.IsSuccess) {
                await _navigationService.NavigateAsync("/NavigationPage/ProjectView", new NavigationParameters { { "Id", Id } });
            } else {
                await _pageDialogService.DisplayAlertAsync("", result.Error, "OK");
            }
        }

        private async void BackAsync() {
            if (!IsConnected()) {
                await _pageDialogService.DisplayAlertAsync("", errorConnectionMessage, "Ok");
            } else {
                await _navigationService.NavigateAsync("/NavigationPage/ProjectView", new NavigationParameters { { "Id", Id } });
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

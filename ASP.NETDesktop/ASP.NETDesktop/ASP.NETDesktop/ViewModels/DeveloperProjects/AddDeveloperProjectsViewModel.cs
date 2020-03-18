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
    public class AddDeveloperProjectsViewModel : BaseViewModel, INavigatedAware {
        private readonly IDeveloperService _developerService;
        private readonly IProjectService _projectService;
        private readonly IPageDialogService _pageDialogService;
        private readonly INavigationService _navigationService;

        public Guid Id { get; set;}
        public DelegateCommand BackCommand { get; set; }
        public Command<object> AssignCommand { get; set; }

        private List<ProjectApiModel> _projects;
        public List<ProjectApiModel> Projects { get => _projects; set => SetProperty(ref _projects, value); }

        public AddDeveloperProjectsViewModel(IDeveloperService developerService, IProjectService projectService, IPageDialogService pageDialogService, INavigationService navigationService) {
            _developerService = developerService;
            _projectService = projectService;
            _pageDialogService = pageDialogService;
            _navigationService = navigationService;
            BackCommand = new DelegateCommand(BackAsync);
            AssignCommand = new Command<object>(AddAsync);
        }

        private async Task<List<ProjectApiModel>> ListAsync(Guid id) {
            var result = await _projectService.ListAsync();
            List<ProjectApiModel> projects = result.Data.Where(d=>d.Developers.All(x=>x.Id!=Id)).ToList();
            return projects;
        }

        private async void AddAsync(object obj) {
            Guid id = Guid.Parse(obj.ToString());
            if (!IsConnected()) {
                await _pageDialogService.DisplayAlertAsync("", errorConnectionMessage, "Ok");
            }
            else {
                var result = await _developerService.AddProjectAsync(Id, id);
                await _navigationService.NavigateAsync("/NavigationPage/DeveloperProjectsView", new NavigationParameters { { "Id", Id } });
            }
        }

        private async void BackAsync() {
            if (!IsConnected()) {
                await _pageDialogService.DisplayAlertAsync("", errorConnectionMessage, "Ok");
            } else {
                await _navigationService.NavigateAsync("/NavigationPage/DeveloperProjectsView", new NavigationParameters { { "Id", Id } });
            }
        }

        public void OnNavigatedTo(INavigationParameters parameters) {
            string id = parameters.FirstOrDefault(x => x.Key == "Id").Value.ToString();
            Id = Guid.Parse(id);

            var list = Task.Run(() => ListAsync(Id));
            var result = list.Result.ToList();
            Projects = new List<ProjectApiModel>(result);
        }

        public void OnNavigatedFrom(INavigationParameters parameters) { }
    }
}

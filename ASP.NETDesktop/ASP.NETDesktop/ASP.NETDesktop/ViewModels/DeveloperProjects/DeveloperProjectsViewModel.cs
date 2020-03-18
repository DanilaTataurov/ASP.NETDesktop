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
    public class DeveloperProjectsViewModel : BaseViewModel, INavigationAware {
        private readonly IDeveloperService _developerService;
        private readonly IPageDialogService _pageDialogService;
        private readonly INavigationService _navigationService;

        private Guid _id;
        public Guid Id { get => _id; set => SetProperty(ref _id, value); }
        public DelegateCommand BackCommand { get; set; }
        public DelegateCommand AddCommand { get; set; }
        public Command<object> DeleteCommand { get; set; }

        private List<ProjectApiModel> _projects;
        public List<ProjectApiModel> Projects { get => _projects; set => SetProperty(ref _projects, value); }


        public DeveloperProjectsViewModel(IDeveloperService developerService, IPageDialogService pageDialogService, INavigationService navigationService) {
            _developerService = developerService;
            _pageDialogService = pageDialogService;
            _navigationService = navigationService;
            BackCommand = new DelegateCommand(BackAsync);
            AddCommand = new DelegateCommand(AddAsync);
            DeleteCommand = new Command<object>(DeleteAsync);
        }

        private async Task<List<ProjectApiModel>> ListAsync(Guid id) {
            var result = await _developerService.GetByIdAsync(id);
            List<ProjectApiModel> projects = result.Data.Projects.ToList();
            return projects;
        }

        private async void BackAsync() {
            if (!IsConnected()) {
                await _pageDialogService.DisplayAlertAsync("", errorConnectionMessage, "Ok");
            } else {
                await _navigationService.NavigateAsync("/NavigationPage/DeveloperView", new NavigationParameters { { "Id", Id } });
            }
        }

        public async void AddAsync() {
            if (!IsConnected()) {
                await _pageDialogService.DisplayAlertAsync("", errorConnectionMessage, "Ok");
            } else {
                await _navigationService.NavigateAsync("/NavigationPage/AddDeveloperProjectsView", new NavigationParameters { { "Id", Id } });
            }
        }

        public async void DeleteAsync(object obj) {
            Guid id = Guid.Parse(obj.ToString());
            if (!IsConnected()) {
                await _pageDialogService.DisplayAlertAsync("", errorConnectionMessage, "Ok");
            } else {
                bool delete = await _pageDialogService.DisplayAlertAsync("Are you sure you want to delete this project?", "", "Ok", "Cancel");
                if (delete) {
                    await _developerService.DeleteProjectAsync(Id, id);
                    await _navigationService.NavigateAsync("/NavigationPage/DeveloperProjectsView", new NavigationParameters { { "Id", Id } });
                }
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

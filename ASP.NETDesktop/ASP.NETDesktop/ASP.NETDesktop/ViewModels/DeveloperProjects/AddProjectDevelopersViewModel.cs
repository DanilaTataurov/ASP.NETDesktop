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
    public class AddProjectDevelopersViewModel : BaseViewModel, INavigatedAware {
        private readonly IDeveloperService _developerService;
        private readonly IPageDialogService _pageDialogService;
        private readonly INavigationService _navigationService;

        public Guid Id { get; set; }
        public DelegateCommand BackCommand { get; set; }
        public Command<object> AssignCommand { get; set; }

        private List<DeveloperApiModel> _developers;
        public List<DeveloperApiModel> Developers {
            get => _developers;
            set => SetProperty(ref _developers, value);
        }

        public AddProjectDevelopersViewModel(IDeveloperService developerService, IPageDialogService pageDialogService, INavigationService navigationService) {
            _developerService = developerService;
            _pageDialogService = pageDialogService;
            _navigationService = navigationService;
            BackCommand = new DelegateCommand(BackAsync);
            AssignCommand = new Command<object>(AddAsync);
        }

        private async Task<List<DeveloperApiModel>> ListAsync(Guid id) {
            var result = await _developerService.ListAsync();
            List<DeveloperApiModel> developers = result.Data.Where(d => d.Projects.All(x => x.Id != Id)).ToList();
            return developers;
        }

        private async void AddAsync(object obj) {
            Guid id = Guid.Parse(obj.ToString());
            if (!IsConnected()) {
                await _pageDialogService.DisplayAlertAsync("", errorConnectionMessage, "Ok");
            } else {
                var result = await _developerService.AddProjectAsync(id, Id);
                await _navigationService.NavigateAsync("/NavigationPage/ProjectDevelopersView", new NavigationParameters { { "Id", Id } });
            }
        }

        private async void BackAsync() {
            if (!IsConnected()) {
                await _pageDialogService.DisplayAlertAsync("", errorConnectionMessage, "Ok");
            } else {
                await _navigationService.NavigateAsync("/NavigationPage/ProjectDevelopersView", new NavigationParameters { { "Id", Id } });
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

using ASP.NETDesktop.Common.ApiModels;
using ASP.NETDesktop.Models;
using ASP.NETDesktop.Services.Interfaces;
using ASP.NETDesktop.ViewModels.Base;
using AutoMapper;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;

namespace ASP.NETDesktop.ViewModels {
    public class AddDeveloperViewModel : BaseViewModel {
        private readonly IMapper _mapper;
        private readonly IDeveloperService _developerService;
        private readonly IPageDialogService _pageDialogService;
        private readonly INavigationService _navigationService;

        private DeveloperModel _developer;
        public DeveloperModel Developer { get => _developer; set => SetProperty(ref _developer, value); }
        public DelegateCommand BackCommand { get; set; }
        public DelegateCommand CreateCommand { get; set; }

        public AddDeveloperViewModel(IMapper mapper, IDeveloperService developerService, IPageDialogService pageDialogService, INavigationService navigationService) {
            _mapper = mapper;
            _developerService = developerService;
            _pageDialogService = pageDialogService;
            _navigationService = navigationService;
            BackCommand = new DelegateCommand(Back);
            CreateCommand = new DelegateCommand(Create);
            Developer = new DeveloperModel();
        }

        private async void Back() {
            if (!IsConnected()) {
                await _pageDialogService.DisplayAlertAsync("", errorConnectionMessage, "Ok");
            } else {
                await _navigationService.NavigateAsync("/NavigationPage/DevelopersView");
            }
        }

        private async void Create() {
            DeveloperApiModel model = _mapper.Map<DeveloperApiModel>(Developer);
            var result = await _developerService.CreateAsync(model);
            if (result.IsSuccess) {
                await _navigationService.NavigateAsync("/NavigationPage/DevelopersView");
            } else {
                await _pageDialogService.DisplayAlertAsync("", result.Error, "OK");
            }
        }
    }
}

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
    public class EditDeveloperViewModel : BaseViewModel, INavigationAware {
        private readonly IMapper _mapper;
        private readonly IDeveloperService _developerService;
        private readonly IPageDialogService _pageDialogService;
        private readonly INavigationService _navigationService;

        public Guid Id { get; set; }

        private DeveloperModel _developer;
        public DeveloperModel Developer { get => _developer; set => SetProperty(ref _developer, value); }
        public DelegateCommand BackCommand { get; set; }
        public DelegateCommand EditCommand { get; set; }

        public EditDeveloperViewModel(IMapper mapper, IDeveloperService developerService, IPageDialogService pageDialogService, INavigationService navigationService) {
            _mapper = mapper;
            _developerService = developerService;
            _pageDialogService = pageDialogService;
            _navigationService = navigationService;
            BackCommand = new DelegateCommand(BackAsync);
            EditCommand = new DelegateCommand(EditAsync);
        }

        private async Task<DeveloperModel> GetAsync(Guid id) {
            var result = await _developerService.GetByIdAsync(id);
            DeveloperModel developer = _mapper.Map<DeveloperModel>(result.Data);
            return developer;
        }

        private async void EditAsync() {
            DeveloperApiModel model = _mapper.Map<DeveloperApiModel>(Developer);
            model.Id = Id;
            var result = await _developerService.UpdateAsync(model);
            if (result.IsSuccess) {
                await _navigationService.NavigateAsync("/NavigationPage/DeveloperView", new NavigationParameters { { "Id", Id } });
            } else {
                await _pageDialogService.DisplayAlertAsync("", result.Error, "OK");
            }
        }

        private async void BackAsync() {
            if (!IsConnected()) {
                await _pageDialogService.DisplayAlertAsync("", errorConnectionMessage, "Ok");
            } else {
                await _navigationService.NavigateAsync("/NavigationPage/DeveloperView", new NavigationParameters { { "Id", Id } });
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

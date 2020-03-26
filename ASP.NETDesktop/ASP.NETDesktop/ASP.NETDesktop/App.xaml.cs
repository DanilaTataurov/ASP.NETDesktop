using ASP.NETDesktop.Services;
using ASP.NETDesktop.Services.Interfaces;
using ASP.NETDesktop.ViewModels;
using ASP.NETDesktop.ViewModels.DeveloperProjects;
using ASP.NETDesktop.ViewModels.Vacation;
using ASP.NETDesktop.Views;
using ASP.NETDesktop.Views.DeveloperProjects;
using ASP.NETDesktop.Views.Vacation;
using AutoMapper;
using Prism;
using Prism.Ioc;
using Prism.Unity;
using Xamarin.Forms;

namespace ASP.NETDesktop {
    public partial class App : PrismApplication {
        public App(IPlatformInitializer platformInitializer = null) : base(platformInitializer) {
            InitializeComponent();
        }

        protected override async void OnInitialized() {
            Application.Current.Properties.TryGetValue("token", out object token);

            if (Application.Current.Properties.ContainsKey("token") && token != null) {
                await NavigationService.NavigateAsync("/NavigationPage/MainView");
            } else {
                await NavigationService.NavigateAsync("/NavigationPage/LoginView");
            }
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry) {
            containerRegistry.RegisterInstance<IMapper>(new Mapper(Mapping.Configuration.Create()));
            containerRegistry.Register<IApiService, ApiService>();
            containerRegistry.Register<IAccountService, AccountService>();
            containerRegistry.Register<IProjectService, ProjectService>();
            containerRegistry.Register<IDeveloperService, DeveloperService>();
            containerRegistry.Register<IVacationService, VacationService>();

            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<LoginView, LoginViewModel>();
            containerRegistry.RegisterForNavigation<MainView, MainViewModel>();

            containerRegistry.RegisterForNavigation<AddDeveloperView, AddDeveloperViewModel>();
            containerRegistry.RegisterForNavigation<EditDeveloperView, EditDeveloperViewModel>();
            containerRegistry.RegisterForNavigation<DevelopersView, DevelopersViewModel>();
            containerRegistry.RegisterForNavigation<DeveloperView, DeveloperViewModel>();

            containerRegistry.RegisterForNavigation<AddDeveloperProjectsView, AddDeveloperProjectsViewModel>();
            containerRegistry.RegisterForNavigation<AddProjectDevelopersView, AddProjectDevelopersViewModel>();
            containerRegistry.RegisterForNavigation<DeveloperProjectsView, DeveloperProjectsViewModel>();
            containerRegistry.RegisterForNavigation<ProjectDevelopersView, ProjectDevelopersViewModel>();

            containerRegistry.RegisterForNavigation<AddProjectView, AddProjectViewModel>();
            containerRegistry.RegisterForNavigation<EditProjectView, EditProjectViewModel>();
            containerRegistry.RegisterForNavigation<ProjectsView, ProjectsViewModel>();
            containerRegistry.RegisterForNavigation<ProjectView, ProjectViewModel>();

            containerRegistry.RegisterForNavigation<AddVacationView, AddVacationViewModel>();
            containerRegistry.RegisterForNavigation<EditVacationView, EditVacationViewModel>();
            containerRegistry.RegisterForNavigation<VacationChartView, VacationChartViewModel>();
            containerRegistry.RegisterForNavigation<VacationsView, VacationsViewModel>();
            containerRegistry.RegisterForNavigation<VacationView, VacationViewModel>();
        }

        protected override void OnStart() { }
        protected override void OnSleep() { }
        protected override void OnResume() { }
    }
}

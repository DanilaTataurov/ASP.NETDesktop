using ASP.NETDesktop.BLL.Services;
using ASP.NETDesktop.BLL.Services.Base;
using ASP.NETDesktop.DAL.Common;
using ASP.NETDesktop.Domain.Interfaces.Services;
using Autofac;
using AutoMapper;

namespace ASP.NETDesktop.BLL.Common {
    public class ServiceModule : Module {
        protected override void Load(ContainerBuilder builder) {
            builder.RegisterModule(new RepositoryModule());
            builder.RegisterGeneric(typeof(BaseService<>)).AsSelf();
            builder.RegisterInstance<IMapper>(new AutoMapper.Mapper(Mapper.Configuration.Create()));

            builder.RegisterType<DeveloperService>().As<IDeveloperService>().InstancePerLifetimeScope();
            builder.RegisterType<ProjectService>().As<IProjectService>().InstancePerLifetimeScope();
            builder.RegisterType<VacationService>().As<IVacationService>().InstancePerLifetimeScope();

            base.Load(builder);
        }
    }
}

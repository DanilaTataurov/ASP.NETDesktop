using System.Data.Entity;
using ASP.NETDesktop.DAL.Repositories;
using ASP.NETDesktop.Domain.Interfaces;
using Autofac;

namespace ASP.NETDesktop.DAL.Common {
    public class RepositoryModule : Module {
        protected override void Load(ContainerBuilder builder) {
            builder.RegisterGeneric(typeof(EntityRepository<>)).InstancePerLifetimeScope();
            builder.RegisterType<Context.NETContext>().AsSelf().As(typeof(DbContext)).InstancePerLifetimeScope();
            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().InstancePerLifetimeScope();
            base.Load(builder);
        }
    }
}

using System.Threading.Tasks;
using ASP.NETDesktop.Domain.Interfaces.Repositories.Base;

namespace ASP.NETDesktop.Domain.Interfaces {
    public interface IUnitOfWork {
        IRepository<T> GetRepository<T>() where T : class;
        Task<int> CommitAsync();
        int Commit();
        bool AutoDetectChanges { get; set; }
    }
}

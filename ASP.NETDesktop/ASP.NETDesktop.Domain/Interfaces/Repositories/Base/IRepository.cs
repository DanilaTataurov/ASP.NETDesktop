using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ASP.NETDesktop.Domain.Interfaces.Repositories.Base {
    public interface IRepository<T> where T : class {
        IQueryable<T> All();
        IQueryable<T> AllIncluding(params Expression<Func<T, object>>[] includeProperties);
        IQueryable<T> Where(params Expression<Func<T, bool>>[] predicates);

        T FindById(object id);
        Task<T> FindByIdAsync(object id);
        void Add(T entity);
        void Update(T entity);
        void Remove(T entity);
        void Remove(object id);
        void Remove(IEnumerable<T> entities);
    }
}

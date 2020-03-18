using System;
using System.Collections.Generic;

namespace ASP.NETDesktop.Domain.Interfaces.Services.Base {
    public interface IService<T> where T : class {
        void Create(T entity);
        T GetById(Guid id);
        ICollection<T> GetAll();
        void Delete(T entity);
        void Delete(Guid id);
        void Update(T entity);
    }
}

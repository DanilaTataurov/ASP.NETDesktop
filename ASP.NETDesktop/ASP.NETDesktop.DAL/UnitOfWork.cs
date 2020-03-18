using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ASP.NETDesktop.DAL.Context;
using ASP.NETDesktop.DAL.Repositories;
using ASP.NETDesktop.Domain.Interfaces;
using ASP.NETDesktop.Domain.Interfaces.Repositories.Base;

namespace ASP.NETDesktop.DAL {
    public class UnitOfWork : IUnitOfWork, IDisposable {
        private readonly NETContext _context;
        private readonly List<object> _repositories = new List<object>();

        public UnitOfWork(NETContext context) {
            _context = context;
        }

        public bool AutoDetectChanges {
            get { return _context.Configuration.AutoDetectChangesEnabled; }
            set { _context.Configuration.AutoDetectChangesEnabled = value; }
        }

        public int Commit() {
            return _context.SaveChanges();
        }

        public Task<int> CommitAsync() {
            return _context.SaveChangesAsync();
        }

        public IRepository<T> GetRepository<T>() where T : class {
            var repo = (IRepository<T>) _repositories.SingleOrDefault(r => r is IRepository<T>);
            if (repo == null) {
                _repositories.Add(repo = new EntityRepository<T>(_context));
            }
            return repo;
        }
        private bool _disposed;

        public void Dispose() {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool isDisposing) {
            if (!_disposed) {
                if (isDisposing) {
                    _context?.Dispose();
                }
            }
            _disposed = true;
        }
    }
}

using System;
using System;
using System.Collections;
using System.Threading.Tasks;
using Core.interfaces;

namespace Infrastructure.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly StoreContext _context;
        private Hashtable _repositories;
        //hashtabels of reposetories stored
        public UnitOfWork(StoreContext context)
        {
            _context = context;
        }

        public async Task<int> Complete()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public IGenericREpository<TEntity> Repository<TEntity>() where TEntity : BaseEntity
        {
            if (_repositories == null) _repositories = new Hashtable();

            var type = typeof(TEntity).Name;

            if (!_repositories.ContainsKey(type))
            // type of entity repository to hash table
            {
                var repositoryType = typeof(GenericREpository<>);
                var repositoryInstance = Activator.CreateInstance(repositoryType
                .MakeGenericType(typeof(TEntity)), _context);

                _repositories.Add(type, repositoryInstance);
            }

            return (IGenericREpository<TEntity>)_repositories[type];
        }
    }
}
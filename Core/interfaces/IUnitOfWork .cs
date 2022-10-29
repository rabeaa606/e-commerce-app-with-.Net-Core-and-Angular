using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericREpository<TEntity> Repository<TEntity>() where TEntity : BaseEntity;
        Task<int> Complete();
    }
}
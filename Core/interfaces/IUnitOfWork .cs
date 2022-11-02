

namespace Core.interfaces;
public interface IUnitOfWork : IDisposable
{
    IGenericREpository<TEntity> Repository<TEntity>() where TEntity : BaseEntity;
    Task<int> Complete();
}

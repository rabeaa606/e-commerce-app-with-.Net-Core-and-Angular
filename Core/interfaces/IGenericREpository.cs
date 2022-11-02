namespace Core.interfaces;
public interface IGenericREpository<T> where T : BaseEntity
{
    void Add(T entity);
    void Update(T entity);
    void Delete(T entity);
    Task<T> GetByIdAsync(int id);
    Task<T> GetEntityWithSpec(ISpecification<T> spec);

    Task<IReadOnlyList<T>> ListAllAsync();
    Task<IReadOnlyList<T>> ListAsync(ISpecification<T> spec);
    Task<int> CountAsync(ISpecification<T> spec);


}

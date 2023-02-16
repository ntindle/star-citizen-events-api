using System.Linq.Expressions;


namespace SCEAPI.Repository.IRepository
{
    public interface IRepository<T> where T : class
    {
        Task CreateAsync(T entity);

        Task SaveAsync();

        Task RemoveAsync(T entity);

        Task<List<T>> GetAllAsync(Expression<Func<T, bool>>? filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null);

        Task<T> GetAsync(Expression<Func<T, bool>>? filter = null, bool tracked=true,
            Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null);
    }
}
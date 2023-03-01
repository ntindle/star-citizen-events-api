using System.Linq.Expressions;
using SCEAPI.Models;

namespace SCEAPI.Repository.IRepository
{
    public interface IEventRepository : IRepository<Event>
    {
        Task<Event> UpdateAsync(Event entity);

        Task<List<Event>> GetAllByDisplayNameAsync(string displayName, bool tracked = true, Func<IQueryable<Event>, IOrderedQueryable<Event>>? orderBy = null);
        Task<Event> GetByDisplayNameAsync(string displayName, bool tracked = true, Func<IQueryable<Event>, IOrderedQueryable<Event>>? orderBy = null);
    }
}
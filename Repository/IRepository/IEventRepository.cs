using System.Linq.Expressions;
using SCEAPI.Models;

namespace SCEAPI.Repository.IRepository
{
    public interface IEventRepository : IRepository<Event>
    {
        Task<Event> UpdateAsync(Event entity);

    }
}
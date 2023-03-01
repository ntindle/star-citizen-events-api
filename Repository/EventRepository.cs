using System.Linq.Expressions;
using SCEAPI.Models;
using SCEAPI.Data;
using SCEAPI.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace SCEAPI.Repository
{
    public class EventRepository : Repository<Event>, IEventRepository
    {
        private readonly ApplicationDbContext _db;
        public EventRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public async Task<Event> UpdateAsync(Event entity)
        {
            _db.Events.Update(entity);
            await _db.SaveChangesAsync();
            return entity;
        }

        public async Task<List<Event>> GetAllByDisplayNameAsync(string displayName, bool tracked = true, Func<IQueryable<Event>, IOrderedQueryable<Event>>? orderBy = null)
        {

            IQueryable<Event> query = dbSet;
            query = query.Where(x => Event.GenerateDisplayName(x.Name, x.StartDateTime, x.EndDateTime).Equals(displayName));
            if (orderBy != null)
                query = orderBy(query);
            if (!tracked)
                query = query.AsNoTracking();
            return await query.ToListAsync();
        }

        public async Task<Event> GetByDisplayNameAsync(string displayName, bool tracked = true, Func<IQueryable<Event>, IOrderedQueryable<Event>>? orderBy = null)
        {
            IQueryable<Event> query = dbSet;
            query = query.Where(x => Event.GenerateDisplayName(x.Name, x.StartDateTime, x.EndDateTime).Equals(displayName));
            if (orderBy != null)
                query = orderBy(query);
            if (!tracked)
                query = query.AsNoTracking();
            return await query.FirstOrDefaultAsync();
        }

        Expression<Func<Event, bool>> FilterByDisplayName(string displayName)
        {
            return x => x.DisplayName.Equals(displayName);
        }

    }
}
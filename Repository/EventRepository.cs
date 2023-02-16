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
        
    }
}
using InternConnect.Context;
using InternConnect.Context.Models;
using InternConnect.Data.Interfaces;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace InternConnect.Data.Repositories
{
    public class EventRepository : BaseRepository<Event>, IEventRepository
    {
        public EventRepository(InternConnectContext context) : base(context)
        {
        }

        public IEnumerable<Event> GetAllEventsWithAdmin()
        {
            return Context.Set<Event>().Include(e => e.Admin).ToList();
        }
    }
}
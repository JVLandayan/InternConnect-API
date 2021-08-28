using InternConnect.Context;
using InternConnect.Context.Models;
using InternConnect.Data.Interfaces;

namespace InternConnect.Data.Repositories
{
    public class EventRepository : BaseRepository<Event>, IEventRepository
    {
        public EventRepository(InternConnectContext context) : base(context)
        {
        }
    }
}
using System.Collections.Generic;
using InternConnect.Context.Models;

namespace InternConnect.Data.Interfaces
{
    public interface IEventRepository : IBaseRepository<Event>
    {
        public IEnumerable<Event> GetAllEventsWithAdmin();
    }
}
using System;
using InternConnect.Context.Models;
using InternConnect.Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace InternConnect.Data.Repositories
{
    public class EventRepository: BaseRepository<Event>, IEventRepository
    {
        public EventRepository(DbContext context) : base(context)
        {

        }

    }
}

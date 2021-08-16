using InternConnect.Context.Models;
using InternConnect.Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace InternConnect.Data.Repositories
{
    public class SectionRepository: BaseRepository<Section>, ISectionRepository
    {
        public SectionRepository(DbContext context) : base(context)
        {

        }

    }
}

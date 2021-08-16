using InternConnect.Context.Models;
using InternConnect.Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace InternConnect.Data.Repositories
{
    public class WebStateRepository: BaseRepository<WebState>, IWebStateRepository
    {
        public WebStateRepository(DbContext context) : base(context)
        {

        }


    }
}

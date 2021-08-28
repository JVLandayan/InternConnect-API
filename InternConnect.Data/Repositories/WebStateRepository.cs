using InternConnect.Context;
using InternConnect.Context.Models;
using InternConnect.Data.Interfaces;

namespace InternConnect.Data.Repositories
{
    public class WebStateRepository : BaseRepository<WebState>, IWebStateRepository
    {
        public WebStateRepository(InternConnectContext context) : base(context)
        {
        }
    }
}
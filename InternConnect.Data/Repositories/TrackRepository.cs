using InternConnect.Context;
using InternConnect.Context.Models;
using InternConnect.Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace InternConnect.Data.Repositories
{
    public class TrackRepository: BaseRepository<Track>, ITrackRepository
    {
        public TrackRepository(InternConnectContext context) : base(context)
        {

        }



    }
}

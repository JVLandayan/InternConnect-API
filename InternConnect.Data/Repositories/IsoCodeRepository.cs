using InternConnect.Context;
using InternConnect.Context.Models;
using InternConnect.Data.Interfaces;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace InternConnect.Data.Repositories
{
    public class IsoCodeRepository : BaseRepository<IsoCode>, IIsoCodeRepository
    {
        public IsoCodeRepository(InternConnectContext context) : base(context)
        {

        }

        public IEnumerable<IsoCode> GetAllCodesWithRelatedData()
        {
           return Context.Set<IsoCode>().Include(i => i.Program).Include(i=>i.Admin.Account).Include(i=>i.Admin.Section).ToList();
        }
    }
}
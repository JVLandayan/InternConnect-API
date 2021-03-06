using InternConnect.Context;
using InternConnect.Context.Models;
using InternConnect.Data.Interfaces;

namespace InternConnect.Data.Repositories
{
    public class PdfStateRepository : BaseRepository<PdfState>, IPdfStateRepository
    {
        public PdfStateRepository(InternConnectContext context) : base(context)
        {
        }
    }
}
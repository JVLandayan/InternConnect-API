using InternConnect.Context.Models;
using InternConnect.Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace InternConnect.Data.Repositories
{
    public class PdfStateRepository: BaseRepository<PdfState>, IPdfStateRepository
    {
        public PdfStateRepository(DbContext context) : base(context)
        {

        }


    }


}

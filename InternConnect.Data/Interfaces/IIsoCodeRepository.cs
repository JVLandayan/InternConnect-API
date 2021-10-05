using System.Collections.Generic;
using InternConnect.Context.Models;

namespace InternConnect.Data.Interfaces
{
    public interface IIsoCodeRepository : IBaseRepository<IsoCode>
    {
        public IEnumerable<IsoCode> GetAllCodesWithRelatedData();
    }
}
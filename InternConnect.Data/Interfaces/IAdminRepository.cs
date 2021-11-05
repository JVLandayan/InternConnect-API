using System.Collections.Generic;
using InternConnect.Context.Models;

namespace InternConnect.Data.Interfaces
{
    public interface IAdminRepository : IBaseRepository<Admin>
    {
        public IEnumerable<Admin> GetAllAdminsWithRelatedData();
        public Admin GetAdminWithEmail(int id);
    }
}
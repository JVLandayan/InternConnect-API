using InternConnect.Context.Models;

namespace InternConnect.Data.Interfaces
{
    public interface IAdminResponseRepository : IBaseRepository<AdminResponse>
    {
        public AdminResponse GetAdminResponseWithSubmission(int id);
    }
}
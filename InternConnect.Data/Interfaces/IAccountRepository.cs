using System.Collections.Generic;
using InternConnect.Context.Models;

namespace InternConnect.Data.Interfaces
{
    public interface IAccountRepository : IBaseRepository<Account>
    {
        IEnumerable<Account> GetAllAccountData();
    }
}
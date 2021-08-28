using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using InternConnect.Context;
using InternConnect.Context.Models;
using InternConnect.Data.Interfaces;

namespace InternConnect.Data.Repositories
{
    public class AccountRepository : BaseRepository<Account>, IAccountRepository
    {
        public AccountRepository(InternConnectContext context, IMapper mapper) : base(context)
        {
        }

        public IEnumerable<Account> GetAllAccountData()
        {
            var accountData = GetAll().ToList();

            foreach (var account in accountData)
            {
                account.Admin = Context.Set<Admin>().FirstOrDefault(admin => admin.AccountId == account.Id);
                account.Student = Context.Set<Student>().FirstOrDefault(student => student.AccountId == account.Id);
            }

            return accountData;
        }
    }
}
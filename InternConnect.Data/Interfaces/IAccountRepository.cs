﻿using InternConnect.Context.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternConnect.Data.Interfaces
{
    public interface IAccountRepository : IBaseRepository<Account>
    {
        IEnumerable<Account> GetAllAccountData();


    }
}

using InternConnect.Context.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using InternConnect.Data.Interfaces;
using InternConnect.Dto.Account;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace InternConnect.Service.Main.Repositories
{
    public interface IAccountService
    {
        public void Add(AccountDto.AddAccount entity);
        public void AddRange(List<Account> entity);
        public void Delete(Account entity);
        public void DeleteRange(List<Account> entities);
        public List<Account> GetAll();
        public Account GetById(int id);
    }
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IMapper _mapper;

        public AccountService(IAccountRepository account, IMapper mapper)
        {
            _accountRepository = account;
            _mapper = mapper;
        }


        public void Add(AccountDto.AddAccount entity)
        {
            _accountRepository.Add(_mapper.Map<Account>(entity));
        }


        public void AddRange(List<Account> entity)
        {
            _accountRepository.AddRange(entity);
        }

        public void Delete(Account entity)
        {
            
            _accountRepository.Remove(entity);
        }

        public void DeleteRange(List<Account> entities)
        {
            _accountRepository.RemoveRange(entities);
        }

        public List<Account> GetAll()
        {
            return _accountRepository.GetAll().ToList();
        }

        public Account GetById(int id)
        {
            return _accountRepository.Get(id);
        }
    }
}

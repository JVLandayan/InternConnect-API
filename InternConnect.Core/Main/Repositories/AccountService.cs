using System;
using InternConnect.Context.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using InternConnect.Context;
using InternConnect.Data.Interfaces;
using InternConnect.Dto.Account;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace InternConnect.Service.Main.Repositories
{
    public interface IAccountService
    {
        public void AddCoordinator(AccountDto.AddAccountCoordinator entity);
        public void AddStudent(AccountDto.AddAccountStudent payload);
        public void AddChair(AccountDto.AddAccountChair payload);
        public void AddRange(List<Account> entity);
        public void Delete(Account entity, int id);
        public void DeleteRange(List<Account> entities);
        public List<AccountDto.ReadAccount> GetAll();
        public Account GetById(int id);
    }
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IMapper _mapper;
        private readonly IAdminRepository _adminRepository;
        private readonly InternConnectContext _context;

        public AccountService(IAccountRepository account, IMapper mapper, IAdminRepository admin, InternConnectContext context)
        {
            _accountRepository = account;
            _mapper = mapper;
            _adminRepository = admin;
            _context = context;
        }


        public void AddStudent(AccountDto.AddAccountStudent payload)
        {
            var accountData = new Account
            {
                Email = payload.Email,
                Password = Guid.NewGuid().ToString(),
                ResetKey = Guid.NewGuid().ToString()
            };

            var studentData = new Student
            {
                ProgramId = payload.ProgramId,
                SectionId = payload.ProgramId,
                DateAdded = DateTime.Now,
                AddedBy = payload.AdminEmail
            };
            accountData.Student = studentData;
            _accountRepository.Add(accountData);
            _context.SaveChanges();

        }

        public void AddCoordinator(AccountDto.AddAccountCoordinator entity)
        {
            var accountData = new Account
            {
                Email = entity.Email,
                Password = Guid.NewGuid().ToString(),
                ResetKey = Guid.NewGuid().ToString()
            };

            var adminData = new Admin
            {
                AuthId = 3,
                SectionId = entity.SectionId,
                ProgramId = entity.ProgramId
            };
            accountData.Admin = adminData;
            _accountRepository.Add(accountData);

            _context.SaveChanges();

        }

        public void AddChair(AccountDto.AddAccountChair entity)
        {
            var accountData = new Account
            {
                Email = entity.Email,
                Password = Guid.NewGuid().ToString(),
                ResetKey = Guid.NewGuid().ToString()
            };
            var adminData = new Admin
            {
                AuthId = 2,
                ProgramId = entity.ProgramId
            };
            accountData.Admin = adminData;
            _accountRepository.Add(accountData);

            _context.SaveChanges();
        }


        public void AddRange(List<Account> entities)
        {
            _accountRepository.AddRange(entities);
        }

        public void Delete(Account entity, int id)
        {
            var accountData = GetById(id);
            _accountRepository.Remove(accountData);
        }

        public void DeleteRange(List<Account> entities)
        {
            _accountRepository.RemoveRange(entities);
        }

        public List<AccountDto.ReadAccount> GetAll()
        {
            var accountData = _accountRepository.GetAllAccountData().ToList();
            List<AccountDto.ReadAccount> mappedData = new List<AccountDto.ReadAccount>();
            foreach (var account in accountData)
            {
                mappedData.Add(_mapper.Map<Account, AccountDto.ReadAccount>(account));
            }

            return mappedData;
        }
         
        public Account GetById(int id)
        {
            return _accountRepository.Get(id);
        }
    }
}

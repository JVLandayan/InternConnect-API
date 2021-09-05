using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using InternConnect.Context;
using InternConnect.Context.Models;
using InternConnect.Data.Interfaces;
using InternConnect.Dto.Account;
using Microsoft.AspNetCore.Mvc;

namespace InternConnect.Service.Main
{
    public interface IAccountService
    {
        public AccountDto.ReadAccount AddCoordinator(AccountDto.AddAccountCoordinator entity);
        public AccountDto.ReadAccount AddStudent(AccountDto.AddAccountStudent payload);
        public AccountDto.ReadAccount AddChair(AccountDto.AddAccountChair payload);
        //public void AddRange(List<Account> entity);
        public void Delete(int id);
        //public void DeleteRange(List<Account> entities);
        public List<AccountDto.ReadAccount> GetAll();
        public Account GetById(int id);
    }

    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IAdminRepository _adminRepository;
        private readonly InternConnectContext _context;
        private readonly IMapper _mapper;

        public AccountService(IAccountRepository account, IMapper mapper, IAdminRepository admin,
            InternConnectContext context)
        {
            _accountRepository = account;
            _mapper = mapper;
            _adminRepository = admin;
            _context = context;
        }


        public AccountDto.ReadAccount AddStudent(AccountDto.AddAccountStudent payload)
        {

            if (_accountRepository.GetAll().FirstOrDefault(a => a.Email == payload.Email) != null)
            {
                return new AccountDto.ReadAccount();
            }

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
            return _mapper.Map<AccountDto.ReadAccount>(accountData);
        }

        public AccountDto.ReadAccount AddCoordinator(AccountDto.AddAccountCoordinator entity)
        {

            if (_accountRepository.GetAll().FirstOrDefault(a => a.Email == entity.Email) != null)
            {
                return new AccountDto.ReadAccount();
            }
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
            return _mapper.Map<AccountDto.ReadAccount>(accountData);
        }

        public AccountDto.ReadAccount AddChair(AccountDto.AddAccountChair entity)
        {
            if (_accountRepository.GetAll().FirstOrDefault(a => a.Email == entity.Email) != null)
            {
                return new AccountDto.ReadAccount();
            }
            var accountData = new Account
            {
                Email = entity.Email.ToUpper(),
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
            return _mapper.Map<AccountDto.ReadAccount>(accountData);
        }


        public void Delete(int id)
        {
            var accountData = GetById(id);
            _accountRepository.Remove(accountData);
        }


        public List<AccountDto.ReadAccount> GetAll()
        {
            var accountData = _accountRepository.GetAllAccountData().ToList();
            var mappedData = new List<AccountDto.ReadAccount>();
            foreach (var account in accountData) mappedData.Add(_mapper.Map<Account, AccountDto.ReadAccount>(account));

            return mappedData;
        }

        public Account GetById(int id)
        {
            return _accountRepository.Get(id);
        }
    }
}
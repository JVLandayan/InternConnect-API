using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using AutoMapper;
using InternConnect.Context;
using InternConnect.Context.Models;
using InternConnect.Data.Interfaces;
using InternConnect.Dto;
using InternConnect.Dto.Account;
using InternConnect.Service.ThirdParty;
using Microsoft.AspNetCore.WebUtilities;

namespace InternConnect.Service.Main
{
    public interface IAccountService
    {
        public AccountDto.ReadAccount AddCoordinator(AccountDto.AddAccountCoordinator entity);
        public AccountDto.ReadAccount AddStudent(AccountDto.AddAccountStudent payload);
        public List<AccountDto.AddAccountStudent> AddStudents(List<AccountDto.AddAccountStudent> payload);
        public AccountDto.ReadAccount AddChair(AccountDto.AddAccountChair payload);
        public AccountDto.ReadAccount AddTechCoordinator(AccountDto.AddAccountTechCoordinator entity);
        public void Delete(int id);
        public List<AccountDto.ReadAccount> GetAll();
        public void DeleteAll(int startYear, int endYear);
        public Account GetById(int id);
        public Account ChangeDean(ChangeDeanModel payload);
    }

    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IAuthService _authService;
        private readonly IAcademicYearRepository _ayRepository;
        private readonly InternConnectContext _context;
        private readonly IEventRepository _eventRepository;
        private readonly IIsoCodeRepository _isoCodeRepository;
        private readonly ILogsRepository _logsRepository;
        private readonly IMailerService _mailerService;
        private readonly IMapper _mapper;
        private readonly ISectionRepository _sectionRepository;

        public AccountService(IAccountRepository account, IMapper mapper,
            InternConnectContext context, IAuthService authService, IAcademicYearRepository ayRepository,
            ISectionRepository sectionRepository, IMailerService mailerService, IIsoCodeRepository isoCodeRepository,
            ILogsRepository logsRepository, IEventRepository eventRepository)
        {
            _accountRepository = account;
            _mapper = mapper;
            _context = context;
            _authService = authService;
            _ayRepository = ayRepository;
            _sectionRepository = sectionRepository;
            _mailerService = mailerService;
            _isoCodeRepository = isoCodeRepository;
            _logsRepository = logsRepository;
            _eventRepository = eventRepository;
        }


        public AccountDto.ReadAccount AddStudent(AccountDto.AddAccountStudent payload)
        {
            if (_accountRepository.GetAll().FirstOrDefault(a => a.Email == payload.Email.ToUpper()) != null)
                return new AccountDto.ReadAccount();

            var accountData = new Account
            {
                Email = payload.Email.ToUpper(),
                Password = HashPassword(Guid.NewGuid().ToString()),
                ResetKey = TokenConfig(Guid.NewGuid().ToString())
            };

            var studentData = new Student
            {
                ProgramId = payload.ProgramId,
                SectionId = payload.SectionId,
                DateAdded = GetDate(),
                AddedBy = payload.AdminEmail,
                AuthId = 5
            };
            accountData.Student = studentData;
            _accountRepository.Add(accountData);
            _context.SaveChanges();
            _authService.Onboard(accountData.Email);
            return _mapper.Map<AccountDto.ReadAccount>(accountData);
        }

        public AccountDto.ReadAccount AddCoordinator(AccountDto.AddAccountCoordinator entity)
        {
            if (_accountRepository.GetAll().FirstOrDefault(a => a.Email == entity.Email.ToUpper()) != null)
                return new AccountDto.ReadAccount();
            var accountData = new Account
            {
                Email = entity.Email.ToUpper(),
                Password = HashPassword(Guid.NewGuid().ToString()),
                ResetKey = TokenConfig(Guid.NewGuid().ToString())
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
            _authService.Onboard(accountData.Email);
            return _mapper.Map<AccountDto.ReadAccount>(accountData);
        }

        public AccountDto.ReadAccount AddChair(AccountDto.AddAccountChair entity)
        {
            if (_accountRepository.GetAll().FirstOrDefault(a => a.Email == entity.Email.ToUpper()) != null)
                return new AccountDto.ReadAccount();
            var accountData = new Account
            {
                Email = entity.Email.ToUpper(),
                Password = HashPassword(Guid.NewGuid().ToString()),
                ResetKey = TokenConfig(Guid.NewGuid().ToString())
            };
            var adminData = new Admin
            {
                AuthId = 2,
                ProgramId = entity.ProgramId
            };
            accountData.Admin = adminData;
            _accountRepository.Add(accountData);
            _context.SaveChanges();
            _authService.Onboard(accountData.Email);
            return _mapper.Map<AccountDto.ReadAccount>(accountData);
        }

        public AccountDto.ReadAccount AddTechCoordinator(AccountDto.AddAccountTechCoordinator entity)
        {
            if (_accountRepository.GetAll().FirstOrDefault(a => a.Email == entity.Email.ToUpper()) != null)
                return new AccountDto.ReadAccount();
            var accountData = new Account
            {
                Email = entity.Email.ToUpper(),
                Password = HashPassword(Guid.NewGuid().ToString()),
                ResetKey = TokenConfig(Guid.NewGuid().ToString())
            };
            var adminData = new Admin
            {
                AuthId = 4
            };
            accountData.Admin = adminData;
            _accountRepository.Add(accountData);
            _context.SaveChanges();
            _authService.Onboard(accountData.Email);
            return _mapper.Map<AccountDto.ReadAccount>(accountData);
        }


        public void Delete(int id)
        {
            var accountData = _accountRepository.Get(id);
            _accountRepository.Remove(accountData);
            _context.SaveChanges();
        }


        public List<AccountDto.ReadAccount> GetAll()
        {
            var accountData = _accountRepository.GetAllAccountData().ToList();
            var mappedData = new List<AccountDto.ReadAccount>();
            foreach (var account in accountData) mappedData.Add(_mapper.Map<Account, AccountDto.ReadAccount>(account));

            return mappedData;
        }

        public void DeleteAll(int startYear, int endYear)
        {
            var ayList = _ayRepository.GetAll().First();
            var ayData = _ayRepository.Get(ayList.Id);
            ayData.StartYear = startYear;
            ayData.EndYear = endYear;
            ;
            _context.SaveChanges();

            #region Delete Accounts

            var adminList = _accountRepository.GetAllAccountData().Where(acc => acc.Admin != null);
            var studentList = _accountRepository.GetAllAccountData().Where(acc => acc.Student != null);
            var mappedList = adminList.Where(acc => acc.Admin.AuthId != 1);
            _accountRepository.RemoveRange(mappedList);
            _accountRepository.RemoveRange(studentList);

            #endregion

            #region Delete Sections

            _sectionRepository.RemoveRange(_sectionRepository.GetAll().ToList());

            #endregion

            #region Delete IsoCodes

            _isoCodeRepository.RemoveRange(_isoCodeRepository.GetAll().ToList());

            #endregion

            #region Delete Logs

            var logsData = _logsRepository.GetAll();
            _logsRepository.RemoveRange(logsData);

            #endregion

            #region Delete Events

            var eventsData = _eventRepository.GetAll();
            _eventRepository.RemoveRange(eventsData);

            #endregion


            _context.SaveChanges();
        }

        public Account GetById(int id)
        {
            return _accountRepository.Get(id);
        }

        public Account ChangeDean(ChangeDeanModel payload)
        {
            var accountData = _accountRepository.GetAll().First(a =>
                a.Email == payload.OldEmail.ToUpper() && a.Password == HashPassword(payload.Password));

            if (accountData != null)
            {
                try
                {
                    _mailerService.ChangeDean(payload.OldEmail, payload.NewEmail, accountData.ResetKey);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }

                return null;
            }

            return new Account();
        }

        public List<AccountDto.AddAccountStudent> AddStudents(List<AccountDto.AddAccountStudent> payload)
        {
            var accountList = new List<Account>();
            var rejectedAccounts = new List<AccountDto.AddAccountStudent>();

            foreach (var data in payload)
                if (_accountRepository.GetAll().FirstOrDefault(a => a.Email == data.Email.ToUpper()) != null)
                {
                    rejectedAccounts.Add(data);
                }

                else
                {
                    var accountData = new Account
                    {
                        Email = data.Email.ToUpper(),
                        Password = HashPassword(Guid.NewGuid().ToString()),
                        ResetKey = TokenConfig(Guid.NewGuid().ToString())
                    };

                    var studentData = new Student
                    {
                        ProgramId = data.ProgramId,
                        SectionId = data.SectionId,
                        DateAdded = GetDate(),
                        AddedBy = data.AdminEmail,
                        AuthId = 5
                    };
                    accountData.Student = studentData;
                    accountList.Add(accountData);
                }

            _accountRepository.AddRange(accountList);
            _context.SaveChanges();

            foreach (var accountData in accountList) _authService.Onboard(accountData.Email);
            return rejectedAccounts;
        }

        private string HashPassword(string password)
        {
            var passBytes = Encoding.ASCII.GetBytes(password);
            var sha = new SHA512Managed();
            var hash = sha.ComputeHash(passBytes);
            var encryptedPass = "";
            foreach (var b in hash) encryptedPass += b.ToString("x2");
            return encryptedPass;
        }

        private string TokenConfig(string token)
        {
            var encodedToken = Encoding.UTF8.GetBytes(token);
            var validToken = WebEncoders.Base64UrlEncode(encodedToken);
            return validToken;
        }

        private DateTime GetDate()
        {
            return TimeZoneInfo.ConvertTime(DateTime.Now,
                TimeZoneInfo.FindSystemTimeZoneById("Singapore Standard Time"));
        }
    }
}
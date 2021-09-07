using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using InternConnect.Context;
using InternConnect.Context.Models;
using InternConnect.Data.Interfaces;
using InternConnect.Dto.Account;
using Microsoft.AspNetCore.WebUtilities;

namespace InternConnect.Service.ThirdParty
{
    public interface IAuthService
    {
        public Account ForgotPassword(string email);
        public void Authenticate(string email, string password);
        public Account ResetPassword(AccountDto.UpdateAccount payload);
        public Account Onboard(string email);
    }

    public class AuthService : IAuthService
    {
        private readonly IAccountRepository _accountRepository;
        private readonly InternConnectContext _context;
        private readonly IMailerService _mailerService;

        public AuthService(InternConnectContext context, IAccountRepository accountRepository,
            IMailerService mailerService)
        {
            _context = context;
            _accountRepository = accountRepository;
            _mailerService = mailerService;
        }

        public void Authenticate(string email, string password)
        {
            var mappedEmail = email.ToUpper();
            var mappedPassword = HashPassword(password);
            var accountData = _accountRepository.Find(a => a.Email == mappedEmail && a.Password == mappedPassword)
                .FirstOrDefault();
            if (accountData == null) ;
        }

        public Account ForgotPassword(string email)
        {
            var accountData = _accountRepository.GetAll().FirstOrDefault(a => a.Email == email.ToUpper());
            if (accountData != null)
            {
                accountData.ResetKey = TokenConfig(Guid.NewGuid().ToString());
                _context.SaveChanges();
                _mailerService.ForgotPassword(accountData);
            }

            return accountData;
        }

        public Account Onboard(string email)
        {
            var accountData = _accountRepository.GetAll().FirstOrDefault(a => a.Email == email.ToUpper());
            if (accountData != null)
            {
                accountData.ResetKey = TokenConfig(Guid.NewGuid().ToString());
                _context.SaveChanges();
                _mailerService.Onboard(accountData);
            }

            return accountData;
        }

        public Account ResetPassword(AccountDto.UpdateAccount payload)
        {
            var accountData = _accountRepository.GetAll()
                .FirstOrDefault(a => a.Email == payload.Email.ToUpper() && a.ResetKey == payload.ResetKey);
            if (accountData != null)
            {
                accountData.Password = HashPassword(payload.Password);
                accountData.ResetKey = TokenConfig(Guid.NewGuid().ToString());
                _context.SaveChanges();
            }

            return accountData;
        }

        #region Password Hasher

        public static string HashPassword(string password)
        {
            var passBytes = Encoding.ASCII.GetBytes(password);
            var sha = new SHA512Managed();
            var hash = sha.ComputeHash(passBytes);
            var encryptedPass = "";
            foreach (var b in hash) encryptedPass += b.ToString("x2");
            return encryptedPass;
        }

        #endregion

        #region Reset Key Converter

        public string TokenConfig(string token)
        {
            var encodedToken = Encoding.UTF8.GetBytes(token);
            var validToken = WebEncoders.Base64UrlEncode(encodedToken);
            return validToken;

            #endregion
        }
    }
}
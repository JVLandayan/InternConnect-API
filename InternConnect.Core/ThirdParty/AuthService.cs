using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using AutoMapper;
using InternConnect.Context;
using InternConnect.Context.Models;
using InternConnect.Data;
using InternConnect.Data.Interfaces;
using InternConnect.Dto.Account;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace InternConnect.Service.ThirdParty
{
    public interface IAuthService
    {
        public Account ForgotPassword(string email);
        public AccountDto.ReadSession Authenticate(AuthenticationModel payload);
        public Account ResetPassword(AccountDto.UpdateAccount payload);
        public Account ChangeDean(AccountDto.UpdateAccount payload, string oldEmail);
        public Account Onboard(string email);
    }

    public class AuthService : IAuthService
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IOptions<AppSettings> _appSettings;
        private readonly IBaseRepository<Authorization> _authRepository;
        private readonly InternConnectContext _context;
        private readonly IMailerService _mailerService;
        private readonly IMapper _mapper;

        public AuthService(InternConnectContext context, IAccountRepository accountRepository,
            IMailerService mailerService, IOptions<AppSettings> appSettings,
            IBaseRepository<Authorization> authRepository, IMapper mapper)
        {
            _context = context;
            _accountRepository = accountRepository;
            _mailerService = mailerService;
            _appSettings = appSettings;
            _authRepository = authRepository;
            _mapper = mapper;
        }

        public AccountDto.ReadSession Authenticate(AuthenticationModel payload)
        {
            var mappedEmail = payload.Email.ToUpper();
            var mappedPassword = HashPassword(payload.Password);
            var accountData = _accountRepository.GetAllAccountData()
                .SingleOrDefault(a => a.Email == mappedEmail && a.Password == mappedPassword);
            if (accountData == null)
                return null;

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Value.Secret);

            var tokenDescriptor = new SecurityTokenDescriptor();
            tokenDescriptor.Subject = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Name, accountData.Id.ToString()),
                new Claim(ClaimTypes.Role,
                    accountData.Student == null
                        ? _authRepository.Get(accountData.Admin.AuthId).Name
                        : _authRepository.Get((int) accountData.Student.AuthId).Name)
            });
            tokenDescriptor.Expires = DateTime.UtcNow.AddDays(7);
            tokenDescriptor.SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256Signature);


            // authentication successful so generate jwt token

            var token = tokenHandler.CreateToken(tokenDescriptor);
            accountData.Token = tokenHandler.WriteToken(token);

            return _mapper.Map<AccountDto.ReadSession>(accountData);
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

        public Account ChangeDean(AccountDto.UpdateAccount payload, string oldEmail)
        {
            var accountData = _accountRepository.GetAll()
                .FirstOrDefault(a => a.Email == oldEmail.ToUpper() && a.ResetKey == payload.ResetKey);
            if (accountData != null)
            {
                accountData.Email = payload.Email.ToUpper();
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
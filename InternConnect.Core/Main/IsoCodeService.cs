using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using InternConnect.Context;
using InternConnect.Context.Models;
using InternConnect.Data.Interfaces;
using InternConnect.Dto.Account;
using InternConnect.Dto.AdminResponse;
using InternConnect.Dto.IsoCode;
using InternConnect.Service.ThirdParty;

namespace InternConnect.Service.Main
{
    public interface IIsoCodeService
    {
        public IList<IsoCodeDto.ReadIsoCode> GetAllByAdminId(int adminId);
        public IList<IsoCodeDto.ReadIsoCode> GetAllByProgramId(int programId);
        public IsoCodeDto.AddIsoCode AddIsocodeToCoordinator(IList<IsoCodeDto.AddIsoCode> payload, int adminId);
        public void TransferIsocodeToChair(IList<IsoCodeDto.TransferIsoCode> payload, int programId);
        public void DeleteIsoCode(int id);
        public void TransferIsocodeToCoordinator(IsoCodeDto.TransferIsoCode payload, int adminId);
    }

    public class IsoCodeService : IIsoCodeService
    {
        private readonly IIsoCodeRepository _isoCodeRepository;
        private readonly IMapper _mapper;
        private readonly InternConnectContext _context;
        private readonly IAdminRepository _adminRepository;
        private readonly IAccountRepository _accountRepository;

        public IsoCodeService(IIsoCodeRepository isoCodeRepository, IMapper mapper, InternConnectContext context, IAdminRepository adminRepository, IAccountRepository accountRepository)
        {
            _isoCodeRepository = isoCodeRepository;
            _mapper = mapper;
            _context = context;
            _adminRepository = adminRepository;
            _accountRepository = accountRepository;
        }



        public void DeleteIsoCode(int id)
        {
            _isoCodeRepository.Remove(_isoCodeRepository.Get(id));
            _context.SaveChanges();
        }

        public IList<IsoCodeDto.ReadIsoCode> GetAllByAdminId(int adminId)
        {
            var isoCodeList = _isoCodeRepository.GetAllCodesWithRelatedData().Where(i => i.AdminId == adminId);
            var accountList = _accountRepository.GetAll().ToList();
            IList<IsoCodeDto.ReadIsoCode> mappedList = new List<IsoCodeDto.ReadIsoCode>();
            foreach (var isoCode in isoCodeList)
            {
                mappedList.Add(_mapper.Map<IsoCodeDto.ReadIsoCode>(isoCode));
            }

            foreach (var item in mappedList)
            {
                item.AdminEmail = accountList.First(a => a.Id == item.Admin.AccountId).Email;
            }

            return mappedList.OrderBy(l=>l.Code).ToList();
        }

        public IList<IsoCodeDto.ReadIsoCode> GetAllByProgramId(int programId)
        {
            var isoCodeList = _isoCodeRepository.GetAllCodesWithRelatedData().Where(i => i.ProgramId == programId && i.Admin.AuthId == 3);
            var accountList = _accountRepository.GetAll().ToList();
            IList<IsoCodeDto.ReadIsoCode> mappedList = new List<IsoCodeDto.ReadIsoCode>();
            foreach (var isoCode in isoCodeList)
            {
                mappedList.Add(_mapper.Map<IsoCodeDto.ReadIsoCode>(isoCode));
            }
            foreach (var item in mappedList)
            {
                item.AdminEmail = accountList.First(a => a.Id == item.Admin.AccountId).Email;
            }

            return mappedList.OrderBy(l => l.Code).ToList();
        }

        public void TransferIsocodeToChair(IList<IsoCodeDto.TransferIsoCode> payload, int programId)
        {
            List<IsoCode> isoCodeList = new List<IsoCode>();
            var chairData = _adminRepository.GetAll().Where(a => a.AuthId == 2)
                .First(a => a.ProgramId == programId);
            var isoCodeListFromDb = _isoCodeRepository.GetAll().ToList();

            foreach (var item in payload)
            {
                var isoCodeData = isoCodeListFromDb.Find(i => i.Id == item.Id);
                isoCodeData.AdminId = chairData.Id;
                isoCodeList.Add(isoCodeData);
            }
            _context.UpdateRange(isoCodeList);
            _context.SaveChanges();
        }
        public void TransferIsocodeToCoordinator(IsoCodeDto.TransferIsoCode payload, int adminId)
        {
            IsoCode isoCodeData = _isoCodeRepository.Get(payload.Id);
            isoCodeData.AdminId = adminId;
            _context.SaveChanges();
        }



        public IsoCodeDto.AddIsoCode AddIsocodeToCoordinator(IList<IsoCodeDto.AddIsoCode> payload, int adminId)
        {
            List<IsoCode> isoCodeList = new List<IsoCode>();
            List<IsoCode> isoCodeListFromDb = _isoCodeRepository.GetAll().Where(i => i.ProgramId == payload.First().ProgramId).ToList();
            var adminData = _adminRepository.Get(adminId);
            foreach (var item in payload)
            {
                if (!isoCodeListFromDb.Exists(a => a.Code == item.Code))
                {
                    isoCodeList.Add(new IsoCode()
                    {
                        Code = item.Code,
                        AdminId = adminId,
                        ProgramId = (int)adminData.ProgramId,
                        SubmissionId = null,
                        Used = false
                    });
                }
                else
                {
                    return item;
                }

            }
            _context.AddRange(isoCodeList);
            _context.SaveChanges();
            return null;
        }

        //public IsoCodeDto.AddIsoCode BulkAdd(IList<IsoCodeDto.AddIsoCode> payload)
        //{
        //    List<IsoCode> isoCodeList = new List<IsoCode>();
        //    List<IsoCode> isoCodeListFromDb = _isoCodeRepository.GetAll().Where(i => i.ProgramId == payload.First().ProgramId).ToList();

        //    foreach (var item in payload)
        //    {
        //        if (!isoCodeListFromDb.Exists(a => a.Code == item.Code))
        //        {
        //            isoCodeList.Add(new IsoCode() { AdminId = item.AdminId, SubmissionId = null, ProgramId = item.ProgramId, Code = item.Code });
        //        }
        //        else
        //        {
        //            return item;
        //        }
        //    }
        //    _isoCodeRepository.AddRange(isoCodeList);
        //    _context.SaveChanges();
        //    return null;
        //}
    }
}
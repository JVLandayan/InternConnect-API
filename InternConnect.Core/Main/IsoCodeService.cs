using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using InternConnect.Context;
using InternConnect.Context.Models;
using InternConnect.Data.Interfaces;
using InternConnect.Dto.AdminResponse;
using InternConnect.Dto.IsoCode;
using InternConnect.Service.ThirdParty;

namespace InternConnect.Service.Main
{
    public interface IIsoCodeService
    {
        public IsoCodeDto.AddIsoCode BulkAdd(IList<IsoCodeDto.AddIsoCode> payload);
        public IList<IsoCodeDto.ReadIsoCode> GetAllByAdminId(int adminId);
        public IList<IsoCodeDto.ReadIsoCode> GetAllByProgramId(int programId);
        public void TransferIsocodeToCoordinator(IList<IsoCodeDto.TransferIsoCode> payload, int adminId);
        public void TransferIsocodeToChair(IList<IsoCodeDto.TransferIsoCode> payload, int programId);
        public void DeleteIsoCode(IList<IsoCodeDto.ReadIsoCode> payload);
    }

    public class IsoCodeService : IIsoCodeService
    {
        private readonly IIsoCodeRepository _isoCodeRepository;
        private readonly IMapper _mapper;
        private readonly InternConnectContext _context;
        private readonly IAdminRepository _adminRepository;

        public IsoCodeService(IIsoCodeRepository isoCodeRepository, IMapper mapper, InternConnectContext context, IAdminRepository adminRepository)
        {
            _isoCodeRepository = isoCodeRepository;
            _mapper = mapper;
            _context = context;
            _adminRepository = adminRepository;
        }

        public IsoCodeDto.AddIsoCode BulkAdd(IList<IsoCodeDto.AddIsoCode> payload)
        {
            List<IsoCode> isoCodeList = new List<IsoCode>();
            List<IsoCode> isoCodeListFromDb = _isoCodeRepository.GetAll().Where(i=>i.ProgramId == payload.First().ProgramId).ToList();

            foreach (var item in payload)
            {
                if (!isoCodeListFromDb.Exists(a=>a.Code == item.Code))
                {
                    isoCodeList.Add(new IsoCode() { AdminId = item.AdminId, SubmissionId = null, ProgramId = item.ProgramId, Code = item.Code });
                }
                else
                {
                    return item;
                }
            }
            _isoCodeRepository.AddRange(isoCodeList);
            _context.SaveChanges();
            return null;
        }

        public void DeleteIsoCode(IList<IsoCodeDto.ReadIsoCode> payload)
        {
            List<IsoCode> isoCodeList = new List<IsoCode>();

            foreach (var item in payload)
            {
                isoCodeList.Add(_isoCodeRepository.Get(item.Id));
            }
            _isoCodeRepository.RemoveRange(isoCodeList);
            _context.SaveChanges();


        }

        public IList<IsoCodeDto.ReadIsoCode> GetAllByAdminId(int adminId)
        {
            var isoCodeList = _isoCodeRepository.GetAllCodesWithRelatedData().Where(i => i.AdminId == adminId);
            IList<IsoCodeDto.ReadIsoCode> mappedList = new List<IsoCodeDto.ReadIsoCode>();
            foreach (var isoCode in isoCodeList)
            {
                mappedList.Add(_mapper.Map<IsoCodeDto.ReadIsoCode>(isoCode));
            }

            return mappedList;
        }

        public IList<IsoCodeDto.ReadIsoCode> GetAllByProgramId(int programId)
        {
            var isoCodeList = _isoCodeRepository.GetAllCodesWithRelatedData().Where(i => i.ProgramId == programId);
            IList<IsoCodeDto.ReadIsoCode> mappedList = new List<IsoCodeDto.ReadIsoCode>();
            foreach (var isoCode in isoCodeList)
            {
                mappedList.Add(_mapper.Map<IsoCodeDto.ReadIsoCode>(isoCode));
            }

            return mappedList;
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

        public void TransferIsocodeToCoordinator(IList<IsoCodeDto.TransferIsoCode> payload, int adminId)
        {
            List<IsoCode> isoCodeList = new List<IsoCode>();
            var isoCodeListFromDb = _isoCodeRepository.GetAll().ToList();
            foreach (var item in payload)
            {
                var isoCodeData = isoCodeListFromDb.Find(i => i.Id == item.Id);
                isoCodeData.AdminId = adminId;
                isoCodeList.Add(isoCodeData);
            }
            _context.UpdateRange(isoCodeList);
            _context.SaveChanges();
        }
    }
}
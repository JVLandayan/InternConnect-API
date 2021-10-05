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
        public void BulkAdd(IList<IsoCodeDto.AddIsoCode> payload);
        public IList<IsoCodeDto.ReadIsoCode> GetAllByAdminId(int adminId);
        public IList<IsoCodeDto.ReadIsoCode> GetAllByProgramId(int programId);
        public void TransferIsocode(IList<IsoCodeDto.ReadIsoCode> payload, int adminId);
        public void DeleteIsoCode(IList<IsoCodeDto.ReadIsoCode> payload);
    }

    public class IsoCodeService : IIsoCodeService
    {
        private readonly IIsoCodeRepository _isoCodeRepository;
        private readonly IMapper _mapper;
        private readonly InternConnectContext _context;

        public IsoCodeService(IIsoCodeRepository isoCodeRepository, IMapper mapper, InternConnectContext context)
        {
            _isoCodeRepository = isoCodeRepository;
            _mapper = mapper;
            _context = context;
        }

        public void BulkAdd(IList<IsoCodeDto.AddIsoCode> payload)
        {
            List<IsoCode> isoCodeList = new List<IsoCode>();
            foreach (var item in payload)
            {
                isoCodeList.Add(new IsoCode(){AdminId = item.AdminId, SubmissionId = null, ProgramId = item.ProgramId, Code = item.Code});
            }
            _isoCodeRepository.AddRange(isoCodeList);
            _context.SaveChanges();
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

        public void TransferIsocode(IList<IsoCodeDto.ReadIsoCode> payload, int adminId)
        {
            List<IsoCode> isoCodeList = new List<IsoCode>();
            foreach (var item in payload)
            {
                item.AdminId = adminId;
                isoCodeList.Add(_mapper.Map<IsoCode>(item));
            }
            _context.UpdateRange(isoCodeList);
            _context.SaveChanges();
        }
    }
}
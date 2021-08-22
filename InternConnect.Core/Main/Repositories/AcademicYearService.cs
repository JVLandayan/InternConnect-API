using System;
using InternConnect.Context.Models;
using InternConnect.Data.Interfaces;
using InternConnect.Dto.AcademicYear;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using AutoMapper;
using InternConnect.Context;

namespace InternConnect.Service.Main.Repositories
{
    public interface IAcademicYearService
    {
        public void UpdateAcademicYear(AcademicYearDto.UpdateAcademicYear payload);
        public AcademicYearDto.ReadAcademicYear GetAcademicYear(int id);
    }

    public class AcademicYearService : IAcademicYearService

    {
    private readonly IAcademicYearRepository _academicYearRepository;
    private readonly InternConnectContext _context;
    private readonly IMapper _mapper;

    public AcademicYearService(IAcademicYearRepository academicYear, IMapper mapper, InternConnectContext context)
    {
        _academicYearRepository = academicYear;
        _context = context;
        _mapper = mapper;
    }

        public AcademicYearDto.ReadAcademicYear GetAcademicYear(int id)
        {
            return _mapper.Map<AcademicYearDto.ReadAcademicYear>(_academicYearRepository.Get(id));
        }

        public void UpdateAcademicYear(AcademicYearDto.UpdateAcademicYear payload)
        {
            var academicYearData = _academicYearRepository.Get(payload.Id);
            _mapper.Map(payload, academicYearData);
            _context.SaveChanges();
        }
    }
}

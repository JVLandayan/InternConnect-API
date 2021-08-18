using System;
using InternConnect.Context.Models;
using InternConnect.Data.Interfaces;
using InternConnect.Dto.AcademicYear;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using AutoMapper;

namespace InternConnect.Service.Main.Repositories
{
    public class AcademicYearService
    {
        private readonly IAcademicYearRepository _ayrepository;
        private readonly IMapper _mapper;
        private readonly IPdfStateRepository _psRepository;

        public AcademicYearService(IAcademicYearRepository academicYear, IPdfStateRepository pdfState  ,IMapper mapper)
        {
            _ayrepository = academicYear;
            _psRepository = pdfState;
            _mapper = mapper;
        }

        //public void Add(AcademicYearDto.AddAcademicYear dto)
        //{

        //    _psRepository.Add(_mapper.Map<PdfState>(dto.DocState));

            
        //    _mapper.Map<AcademicYear>(dto);

        //}

        public void Update(AcademicYearDto.UpdateAcademicYear dto)
        {
            var model = _ayrepository.Get(dto.Id);
        }

        //public AcademicYear GetbyId(AcademicYearDto.ReadAcademicYear dto)
        //{

        //    return
        //}
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using InternConnect.Context.Models;
using InternConnect.Dto.AcademicYear;

namespace InternConnect.Profiles
{
    public class InternConnectMappings : Profile
    {
        public InternConnectMappings()
        {
            //Academic Year
            CreateMap<AcademicYear, AcademicYearDto.AddAcademicYear>().ReverseMap();
            CreateMap<AcademicYear, AcademicYearDto.ReadAcademicYear>().ReverseMap();
            CreateMap<AcademicYear, AcademicYearDto.UpdateAcademicYear>().ReverseMap();
        }
    }
}

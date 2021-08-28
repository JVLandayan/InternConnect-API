using System.Collections.Generic;
using AutoMapper;
using InternConnect.Context;
using InternConnect.Context.Models;
using InternConnect.Data.Interfaces;
using InternConnect.Dto.Program;

namespace InternConnect.Service.Main
{
    public interface IProgramService
    {
        public void AddProgram(ProgramDto.AddProgram payload);
        public void UpdateProgram(ProgramDto.UpdateProgram payload);
        public IEnumerable<ProgramDto.ReadProgram> GetAll();
        public ProgramDto.ReadProgram GetById(int id);
        public void UpdateIsoCode(ProgramDto.UpdateIsoCode payload);
    }

    public class ProgramService : IProgramService
    {
        private readonly InternConnectContext _context;
        private readonly IMapper _mapper;
        private readonly IProgramRepository _programRepository;

        public ProgramService(InternConnectContext context, IMapper mapper, IProgramRepository program)
        {
            _mapper = mapper;
            _context = context;
            _programRepository = program;
        }

        public void AddProgram(ProgramDto.AddProgram payload)
        {
            _programRepository.Add(_mapper.Map<Program>(payload));
            _context.SaveChanges();
        }

        public IEnumerable<ProgramDto.ReadProgram> GetAll()
        {
            var programData = _programRepository.GetAll();
            var mappedData = new List<ProgramDto.ReadProgram>();
            foreach (var program in programData) mappedData.Add(_mapper.Map<ProgramDto.ReadProgram>(program));

            return mappedData;
        }

        public ProgramDto.ReadProgram GetById(int id)
        {
            return _mapper.Map<ProgramDto.ReadProgram>(_programRepository.Get(id));
        }

        public void UpdateIsoCode(ProgramDto.UpdateIsoCode payload)
        {
            var programData = _programRepository.Get(payload.Id);
            _mapper.Map(payload, programData);
            _context.SaveChanges();
        }

        public void UpdateProgram(ProgramDto.UpdateProgram payload)
        {
            var programData = _programRepository.Get(payload.Id);
            _mapper.Map(payload, programData);
            _context.SaveChanges();
        }
    }
}
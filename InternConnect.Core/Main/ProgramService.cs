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
        public ProgramDto.ReadProgram AddProgram(ProgramDto.AddProgram payload);
        public void UpdateProgram(ProgramDto.UpdateProgram payload);
        public IEnumerable<ProgramDto.ReadProgram> GetAll();
        public ProgramDto.ReadProgram GetById(int id);
        public void UpdateIsoCode(ProgramDto.UpdateIsoCode payload);
        public void UpdateNumberOfHours(ProgramDto.UpdateNumberOfHours payload);
        public void DeleteProgram(int id);
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

        public ProgramDto.ReadProgram AddProgram(ProgramDto.AddProgram payload)
        {
            var payloadData = _mapper.Map<Program>(payload);
            _programRepository.Add(payloadData);
            _context.SaveChanges();
            return _mapper.Map<ProgramDto.ReadProgram>(payloadData);
        }

        public void DeleteProgram(int id)
        {
            _programRepository.Remove(_programRepository.Get(id));
            _context.SaveChanges();
        }

        public IEnumerable<ProgramDto.ReadProgram> GetAll()
        {
            var programData = _programRepository.GetAllProgramAndTracks();
            var mappedData = new List<ProgramDto.ReadProgram>();
            foreach (var program in programData) mappedData.Add(_mapper.Map<ProgramDto.ReadProgram>(program));

            return mappedData;
        }

        public ProgramDto.ReadProgram GetById(int id)
        {
            return _mapper.Map<ProgramDto.ReadProgram>(_programRepository.GetProgramAndTracks(id));
        }

        public void UpdateIsoCode(ProgramDto.UpdateIsoCode payload)
        {
            var programData = _programRepository.Get(payload.Id);
            _mapper.Map(payload, programData);
            _context.SaveChanges();
        }

        public void UpdateNumberOfHours(ProgramDto.UpdateNumberOfHours payload)
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
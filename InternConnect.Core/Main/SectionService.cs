using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using InternConnect.Context;
using InternConnect.Context.Models;
using InternConnect.Data.Interfaces;
using InternConnect.Dto.Section;

namespace InternConnect.Service.Main
{
    public interface ISectionService
    {
        public SectionDto.ReadSection AddSection(SectionDto.AddSection payload);
        public void UpdateSection(SectionDto.UpdateSection payload);
        public SectionDto.ReadSection GetById(int id);
        public IEnumerable<SectionDto.ReadSection> GetAll();
        public void DeleteSection(int id);
    }

    public class SectionService : ISectionService
    {
        private readonly InternConnectContext _context;
        private readonly IMapper _mapper;
        private readonly ISectionRepository _sectionRepository;

        public SectionService(IMapper mapper, InternConnectContext context, ISectionRepository section)
        {
            _mapper = mapper;
            _context = context;
            _sectionRepository = section;
        }

        public SectionDto.ReadSection AddSection(SectionDto.AddSection payload)
        {
            var payloadData = _mapper.Map<Section>(payload);
            _sectionRepository.Add(payloadData);
            _context.SaveChanges();
            return _mapper.Map<SectionDto.ReadSection>(payloadData);
        }

        public void DeleteSection(int id)
        {
            var sectionData = _sectionRepository.Get(id);
            _sectionRepository.Remove(sectionData);
            _context.SaveChanges();
        }

        public IEnumerable<SectionDto.ReadSection> GetAll()
        {
            var sectionData = _sectionRepository.GetAll();
            var mappedData = new List<SectionDto.ReadSection>();
            foreach (var section in sectionData) mappedData.Add(_mapper.Map<SectionDto.ReadSection>(section));

            return mappedData.OrderBy(s=>s.Name);
        }

        public SectionDto.ReadSection GetById(int id)
        {
            return _mapper.Map<SectionDto.ReadSection>(_sectionRepository.Get(id));
        }

        public void UpdateSection(SectionDto.UpdateSection payload)
        {
            var sectionData = _sectionRepository.Find(s => s.Id == payload.Id).First();
            _mapper.Map(payload, sectionData);
            _context.SaveChanges();
        }
    }
}
﻿using System.Collections.Generic;
using AutoMapper;
using InternConnect.Context;
using InternConnect.Context.Models;
using InternConnect.Data.Interfaces;
using InternConnect.Dto.Section;

namespace InternConnect.Service.Main
{
    public interface ISectionService
    {
        public void AddSection(SectionDto.AddSection payload);
        public void UpdateSection(SectionDto.UpdateSection payload);
        public SectionDto.ReadSection GetById(int id);
        public IEnumerable<SectionDto.ReadSection> GetAll();
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

        public void AddSection(SectionDto.AddSection payload)
        {
            _sectionRepository.Add(_mapper.Map<Section>(payload));
            _context.SaveChanges();
        }

        public IEnumerable<SectionDto.ReadSection> GetAll()
        {
            var sectionData = _sectionRepository.GetAll();
            var mappedData = new List<SectionDto.ReadSection>();
            foreach (var section in sectionData) mappedData.Add(_mapper.Map<SectionDto.ReadSection>(section));

            return mappedData;
        }

        public SectionDto.ReadSection GetById(int id)
        {
            return _mapper.Map<SectionDto.ReadSection>(_sectionRepository.Get(id));
        }

        public void UpdateSection(SectionDto.UpdateSection payload)
        {
            var sectionData = _sectionRepository.Find(s => s.Id == payload.Id);
            _mapper.Map(payload, sectionData);
            _context.SaveChanges();
        }
    }
}
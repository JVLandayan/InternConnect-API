﻿using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using InternConnect.Context;
using InternConnect.Context.Models;
using InternConnect.Data.Interfaces;
using InternConnect.Dto.Opportunity;

namespace InternConnect.Service.Main
{
    public interface IOpportunityService
    {
        public void AddOpportunity(OpportunityDto.AddOpportunity payload);
        public void UpdateOpportunity(OpportunityDto.UpdateOpportunity payload);
        public OpportunityDto.ReadOpportunity GetById(int id);

        public IEnumerable<OpportunityDto.ReadOpportunity> GetByCompanyId(int companyId);
        public IEnumerable<OpportunityDto.ReadOpportunity> GetAllOpportunities();
        public void DeleteOpportunity(int id);
    }

    public class OpportunityService : IOpportunityService
    {
        private readonly InternConnectContext _context;
        private readonly IMapper _mapper;
        private readonly IOpportunityRepository _opportunityRepository;

        public OpportunityService(InternConnectContext context, IMapper mapper, IOpportunityRepository opportunity)
        {
            _mapper = mapper;
            _context = context;
            _opportunityRepository = opportunity;
        }

        public void AddOpportunity(OpportunityDto.AddOpportunity payload)
        {
            _opportunityRepository.Add(_mapper.Map<Opportunity>(payload));
            _context.SaveChanges();
        }

        public void DeleteOpportunity(int id)
        {
            _opportunityRepository.Remove(_opportunityRepository.Get(id));
            _context.SaveChanges();
        }

        public IEnumerable<OpportunityDto.ReadOpportunity> GetAllOpportunities()
        {
            var opportunityList = _opportunityRepository.GetAll();
            var mappedList = new List<OpportunityDto.ReadOpportunity>();
            foreach (var opportunity in opportunityList)
                mappedList.Add(_mapper.Map<OpportunityDto.ReadOpportunity>(opportunity));

            return mappedList;
        }

        public IEnumerable<OpportunityDto.ReadOpportunity> GetByCompanyId(int companyId)
        {
            var opportunityList = _opportunityRepository.GetAll().Where(o=>o.CompanyId == companyId);
            var mappedList = new List<OpportunityDto.ReadOpportunity>();
            foreach (var opportunity in opportunityList)
                mappedList.Add(_mapper.Map<OpportunityDto.ReadOpportunity>(opportunity));

            return mappedList;
        }

        public OpportunityDto.ReadOpportunity GetById(int id)
        {
            return _mapper.Map<OpportunityDto.ReadOpportunity>(_opportunityRepository.Get(id));
        }

        public void UpdateOpportunity(OpportunityDto.UpdateOpportunity payload)
        {
            var opportunityData = _opportunityRepository.Get(payload.Id);
            _mapper.Map(payload, opportunityData);
            _context.SaveChanges();
        }
    }
}
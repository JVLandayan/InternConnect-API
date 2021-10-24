using System;
using System.Collections.Generic;
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
        public OpportunityDto.ReadOpportunity AddOpportunity(OpportunityDto.AddOpportunity payload);
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

        public OpportunityDto.ReadOpportunity AddOpportunity(OpportunityDto.AddOpportunity payload)
        {
            var payloadData = _mapper.Map<Opportunity>(payload);
            _opportunityRepository.Add(payloadData);
            payloadData.DateAdded = GetDate();
            _context.SaveChanges();

            return _mapper.Map<OpportunityDto.ReadOpportunity>(payloadData);
        }

        public void DeleteOpportunity(int id)
        {
            _opportunityRepository.Remove(_opportunityRepository.Get(id));
            _context.SaveChanges();
        }

        public IEnumerable<OpportunityDto.ReadOpportunity> GetAllOpportunities()
        {
            var opportunityList = _opportunityRepository.GetAllOpportunitiesAndCompanies();
            var mappedList = new List<OpportunityDto.ReadOpportunity>();
            foreach (var opportunity in opportunityList)
                mappedList.Add(_mapper.Map<OpportunityDto.ReadOpportunity>(opportunity));

            return mappedList.Where(o => o.Company.Status != Status.CompanyStatusList.EXPIRED.ToString())
                .OrderBy(o => o.Company.Name);
        }

        public IEnumerable<OpportunityDto.ReadOpportunity> GetByCompanyId(int companyId)
        {
            var opportunityList = _opportunityRepository.GetAllOpportunitiesAndCompanies()
                .Where(o => o.CompanyId == companyId);
            var mappedList = new List<OpportunityDto.ReadOpportunity>();
            foreach (var opportunity in opportunityList)
                mappedList.Add(_mapper.Map<OpportunityDto.ReadOpportunity>(opportunity));

            return mappedList;
        }

        public OpportunityDto.ReadOpportunity GetById(int id)
        {
            return _mapper.Map<OpportunityDto.ReadOpportunity>(_opportunityRepository.GetAllOpportunitiesAndCompanies()
                .First(c => c.Id == id));
        }

        public void UpdateOpportunity(OpportunityDto.UpdateOpportunity payload)
        {
            var opportunityData = _opportunityRepository.Get(payload.Id);
            _mapper.Map(payload, opportunityData);
            _context.SaveChanges();
        }

        private DateTime GetDate()
        {
            return TimeZoneInfo.ConvertTime(DateTime.Now,
                TimeZoneInfo.FindSystemTimeZoneById("Singapore Standard Time"));
        }
    }
}
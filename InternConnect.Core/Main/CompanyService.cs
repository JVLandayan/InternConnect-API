using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using InternConnect.Context;
using InternConnect.Context.Models;
using InternConnect.Data.Interfaces;
using InternConnect.Dto.Company;

namespace InternConnect.Service.Main
{
    public interface ICompanyService
    {
        public CompanyDto.ReadCompany AddCompany(CompanyDto.AddCompany payload);
        public void UpdateCompany(CompanyDto.UpdateCompany payload);
        public CompanyDto.ReadCompany GetById(int id);
        public IEnumerable<CompanyDto.ReadCompany> GetAllCompanies();
        public void UpdateCompanyStatus(CompanyDto.UpdateCompanyStatus payload);
        public void DeleteCompany(int id);
    }

    public class CompanyService : ICompanyService
    {
        private readonly ICompanyRepository _companyRepository;
        private readonly InternConnectContext _context;
        private readonly IMapper _mapper;
        private readonly IOpportunityRepository _opportunityRepository;

        public CompanyService(IMapper mapper, ICompanyRepository company, InternConnectContext context,
            IOpportunityRepository opportunity)
        {
            _mapper = mapper;
            _companyRepository = company;
            _context = context;
            _opportunityRepository = opportunity;
        }

        public CompanyDto.ReadCompany AddCompany(CompanyDto.AddCompany payload)
        {
            var payloadData = _mapper.Map<Company>(payload);
            payloadData.Status = Status.CompanyStatusList.NEW.ToString();
            payloadData.IsActive = true;
            payloadData.DateAdded = GetDate();
            _companyRepository.Add(payloadData);
            _context.SaveChanges();
            return _mapper.Map<CompanyDto.ReadCompany>(payloadData);
        }

        public void DeleteCompany(int id)
        {
            var companyData = _companyRepository.Get(id);
            companyData.IsActive = false;
            _context.SaveChanges();
        }

        public IEnumerable<CompanyDto.ReadCompany> GetAllCompanies()
        {
            var companyList = _companyRepository.GetAll();
            var mappedList = new List<CompanyDto.ReadCompany>();
            foreach (var company in companyList) mappedList.Add(_mapper.Map<CompanyDto.ReadCompany>(company));

            return mappedList.Where(c=>c.IsActive).OrderBy(c => c.Name).ToList();
        }

        public CompanyDto.ReadCompany GetById(int id)
        {
            var companyData = _companyRepository.Get(id);
            if (companyData.Status == Status.CompanyStatusList.EXPIRED.ToString() || companyData.IsActive == false) return null;
            return _mapper.Map<CompanyDto.ReadCompany>(companyData);
        }

        public void UpdateCompany(CompanyDto.UpdateCompany payload)
        {
            var companyData = _companyRepository.Get(payload.Id);


            if (DateTime.Compare(GetDate(), payload.Expiration) == 1)
            {
                companyData.Status = Status.CompanyStatusList.EXPIRED.ToString();
            }

            if (payload.AddressTwo == "") payload.AddressTwo = null;
            if (payload.AddressThree == "") payload.AddressThree = null;
            _mapper.Map(payload, companyData);
            companyData.IsActive = true;
            _context.SaveChanges();
        }

        public void UpdateCompanyStatus(CompanyDto.UpdateCompanyStatus payload)
        {
            var companyData = _companyRepository.Get(payload.Id);
            _mapper.Map(payload, companyData);
            _context.SaveChanges();
        }

        private DateTime GetDate()
        {
            return TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.FindSystemTimeZoneById("Singapore Standard Time"));
        }
    }
}
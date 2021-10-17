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
        //public void DeleteCompany(int id);
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
            payloadData.IsActive = true;
            _companyRepository.Add(payloadData);
            _context.SaveChanges();

            return _mapper.Map<CompanyDto.ReadCompany>(payloadData);
        }


        public IEnumerable<CompanyDto.ReadCompany> GetAllCompanies()
        {
            var companyList = _companyRepository.GetAll();
            var mappedList = new List<CompanyDto.ReadCompany>();
            foreach (var company in companyList) mappedList.Add(_mapper.Map<CompanyDto.ReadCompany>(company));

            return mappedList;
            ;
        }

        public CompanyDto.ReadCompany GetById(int id)
        {
            return _mapper.Map<CompanyDto.ReadCompany>(_companyRepository.Get(id));
        }

        public void UpdateCompany(CompanyDto.UpdateCompany payload)
        {
            var companyData = _companyRepository.Get(payload.Id);
            if (payload.AddressTwo == "") payload.AddressTwo = null;
            if (payload.AddressThree == "") payload.AddressThree = null;
            _mapper.Map(payload, companyData);
            _context.SaveChanges();
        }
    }
}
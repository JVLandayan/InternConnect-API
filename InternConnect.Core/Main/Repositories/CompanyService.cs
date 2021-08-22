using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using InternConnect.Context;
using InternConnect.Context.Models;
using InternConnect.Data.Interfaces;
using InternConnect.Dto.Company;
using Microsoft.EntityFrameworkCore;

namespace InternConnect.Service.Main.Repositories
{
    public interface ICompanyService
    {
        public void AddCompany(CompanyDto.AddCompany payload);
        public void UpdateCompany(CompanyDto.UpdateCompany payload);
        public CompanyDto.ReadCompany GetById(int id);
        public IEnumerable<CompanyDto.ReadCompany> GetAllCompanies();
        public void DeleteCompany(int id);
    }
    public class CompanyService : ICompanyService
    {
        private readonly IMapper _mapper;
        private readonly ICompanyRepository _companyRepository;
        private readonly InternConnectContext _context;
        private readonly IOpportunityRepository _opportunityRepository;

        public CompanyService(IMapper mapper, ICompanyRepository company, InternConnectContext context, IOpportunityRepository opportunity)
        {
            _mapper = mapper;
            _companyRepository = company;
            _context = context;
            _opportunityRepository = opportunity;

        }

        public void AddCompany(CompanyDto.AddCompany payload)
        {
            _companyRepository.Add(_mapper.Map<Company>(payload));
            _context.SaveChanges();
        }

        public void DeleteCompany(int id)
        {
            _opportunityRepository.RemoveRange(_opportunityRepository.GetAll().Where(o => o.CompanyId == id));
            _companyRepository.Remove(_companyRepository.Get(id));
            _context.SaveChanges();
        }

        public IEnumerable<CompanyDto.ReadCompany> GetAllCompanies()
        {
            var companyList = _companyRepository.GetAll();
            var mappedList = new List<CompanyDto.ReadCompany>();
            foreach (var company in companyList)
            {
                mappedList.Add(_mapper.Map<CompanyDto.ReadCompany>(company));
            }

            return mappedList;
;        }

        public CompanyDto.ReadCompany GetById(int id)
        {
            return _mapper.Map<CompanyDto.ReadCompany>(_companyRepository.Get(id));
        }

        public void UpdateCompany(CompanyDto.UpdateCompany payload)
        {
            var companyData = _companyRepository.Get(payload.Id);
            _mapper.Map(payload, companyData);
            _context.SaveChanges();
        }
    }
}

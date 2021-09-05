using System.Collections.Generic;
using AutoMapper;
using InternConnect.Context;
using InternConnect.Data.Interfaces;
using InternConnect.Dto.Admin;

namespace InternConnect.Service.Main
{
    public interface IAdminService
    {
        public void UpdateAdmin(AdminDto.UpdateAdmin entity, int id);
        public AdminDto.ReadAdmin GetById(int id);
        public IEnumerable<AdminDto.ReadAdmin> GetAll();
    }

    public class AdminService : IAdminService
    {
        private readonly IAdminRepository _adminRepository;
        private readonly InternConnectContext _context;
        private readonly IMapper _mapper;

        public AdminService(IMapper mapper, IAdminRepository repository, InternConnectContext context)
        {
            _mapper = mapper;
            _adminRepository = repository;
            _context = context;
        }

        public void UpdateAdmin(AdminDto.UpdateAdmin entity, int id)
        {
            var adminData = _adminRepository.Get(id);
            _mapper.Map(entity, adminData);
            _context.SaveChanges();
        }

        public AdminDto.ReadAdmin GetById(int id)
        {
            return _mapper.Map<AdminDto.ReadAdmin>(_adminRepository.Get(id));
        }

        public IEnumerable<AdminDto.ReadAdmin> GetAll()
        {
            var adminList = _adminRepository.GetAll();
            var mappedData = new List<AdminDto.ReadAdmin>();
            foreach (var admin in adminList) mappedData.Add(_mapper.Map<AdminDto.ReadAdmin>(admin));
            return mappedData;
        }
    }
}
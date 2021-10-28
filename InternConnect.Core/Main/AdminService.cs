using System.Collections.Generic;
using System.Linq;
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
        public IEnumerable<AdminDto.ReadCoordinator> GetAllCoordinatorByProgram(int programId);
        public IEnumerable<AdminDto.ReadCoordinator> GetAllChairByProgram();
        public IEnumerable<AdminDto.ReadCoordinator> GetAllTechCoordinators();
        public void UpdateAdminSection(AdminDto.UpdateAdminSection payloadFrom, AdminDto.UpdateAdminSection payloadTo);
        public void UpdateAdminProgram(AdminDto.UpdateAdminProgram payloadFrom, AdminDto.UpdateAdminProgram payloadTo);
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
            return _mapper.Map<AdminDto.ReadAdmin>(
                _adminRepository.GetAllAdminsWithRelatedData().First(a => a.Id == id));
        }

        public IEnumerable<AdminDto.ReadAdmin> GetAll()
        {
            var adminList = _adminRepository.GetAllAdminsWithRelatedData();
            var mappedData = new List<AdminDto.ReadAdmin>();
            foreach (var admin in adminList) mappedData.Add(_mapper.Map<AdminDto.ReadAdmin>(admin));
            return mappedData;
        }

        public IEnumerable<AdminDto.ReadCoordinator> GetAllCoordinatorByProgram(int programId)
        {
            var coordinatorList = _adminRepository.GetAllAdminsWithRelatedData().Where(a => a.ProgramId != null);
            var mappedData = new List<AdminDto.ReadCoordinator>();
            foreach (var admin in coordinatorList.Where(a => a.AuthId == 3 && a.ProgramId == programId))
                mappedData.Add(_mapper.Map<AdminDto.ReadCoordinator>(admin));
            return mappedData;
        }

        public IEnumerable<AdminDto.ReadCoordinator> GetAllChairByProgram()
        {
            var adminList = _adminRepository.GetAllAdminsWithRelatedData().Where(a => a.AuthId == 2);
            var mappedData = new List<AdminDto.ReadCoordinator>();
            foreach (var admin in adminList) mappedData.Add(_mapper.Map<AdminDto.ReadCoordinator>(admin));
            return mappedData;
        }

        public IEnumerable<AdminDto.ReadCoordinator> GetAllTechCoordinators()
        {
            var adminList = _adminRepository.GetAllAdminsWithRelatedData().Where(a => a.AuthId == 4);
            var mappedData = new List<AdminDto.ReadCoordinator>();
            foreach (var admin in adminList) mappedData.Add(_mapper.Map<AdminDto.ReadCoordinator>(admin));
            return mappedData.OrderBy(a => a.Account.Email);
        }

        public void UpdateAdminSection(AdminDto.UpdateAdminSection payloadFrom, AdminDto.UpdateAdminSection payloadTo)
        {
            var adminDataFrom = _adminRepository.Get(payloadFrom.Id);
            var adminDataTo = _adminRepository.Get(payloadTo.Id);

            //coord section to coord section target
            adminDataFrom.SectionId = payloadTo.SectionId;

            //coord section target to coord section from
            adminDataTo.SectionId = payloadFrom.SectionId;
            _context.SaveChanges();


        }

        public void UpdateAdminProgram(AdminDto.UpdateAdminProgram payloadFrom, AdminDto.UpdateAdminProgram payloadTo)
        {
            var adminDataFrom = _adminRepository.Get(payloadFrom.Id);
            var adminDataTo = _adminRepository.Get(payloadTo.Id);

            //coord section to coord section target
            adminDataFrom.ProgramId = payloadTo.ProgramId;

            //coord section target to coord section from
            adminDataTo.ProgramId = payloadFrom.ProgramId;
            _context.SaveChanges();
        }
    }
}
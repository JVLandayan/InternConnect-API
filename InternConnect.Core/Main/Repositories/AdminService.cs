using System.Collections.Generic;
using AutoMapper;
using InternConnect.Context.Models;
using InternConnect.Data.Interfaces;
using InternConnect.Dto.Account;
using Microsoft.EntityFrameworkCore;

namespace InternConnect.Service.Main.Repositories
{
    public interface IAdminService
    {

    }
    public class AdminService : IAdminService
    {
        private readonly IMapper _mapper;
        private IAdminRepository _adminRepository;

        public AdminService(IMapper mapper, IAdminRepository repository)
        {
            _mapper = mapper;
            _adminRepository = repository;
        }
        public void AddAdmin(int accountId, int programId, int sectionId)
        {

        }

        public void Delete(Account entity, int id)
        {
            throw new System.NotImplementedException();
        }

        public Admin GetById(int id)
        {
            throw new System.NotImplementedException();
        }
    }
}

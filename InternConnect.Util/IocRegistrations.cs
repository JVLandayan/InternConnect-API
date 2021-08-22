﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InternConnect.Data;
using InternConnect.Data.Interfaces;
using InternConnect.Data.Repositories;
using InternConnect.Service.Main.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace InternConnect.Util
{
    public static class IocRegistrations
    {
        public static IServiceCollection AddAppSettingsConfig(this IServiceCollection services, IConfiguration configuration)
        {
            //Data Repositories
            services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
            services.AddScoped<IAccountRepository, AccountRepository>();
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IAcademicYearRepository, AcademicYearRepository>();
            services.AddScoped<IAdminRepository, AdminRepository>();
            services.AddScoped<IAdminResponseRepository, AdminResponseRepository>();
            services.AddScoped<ICompanyRepository, CompanyRepository>();
            services.AddScoped<IEventRepository, EventRepository>();
            services.AddScoped<ILogsRepository, LogsRepository>();
            services.AddScoped<IOpportunityRepository, OpportunityRepository>();
            services.AddScoped<IPdfStateRepository, PdfStateRepository>();
            services.AddScoped<IProgramRepository, ProgramRepository>();
            services.AddScoped<ISectionRepository, SectionRepository>();
            services.AddScoped<IStudentRepository, StudentRepository>();
            services.AddScoped<ISubmissionRepository, SubmissionRepository>();
            services.AddScoped<ITrackRepository, TrackRepository>();
            services.AddScoped<IWebStateRepository, WebStateRepository>();



            //Services-ThirdParties

            return services;
        }
    }
}
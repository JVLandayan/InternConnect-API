using InternConnect.Data;
using InternConnect.Data.Interfaces;
using InternConnect.Data.Repositories;
using InternConnect.Service.Main;
using InternConnect.Service.ThirdParty;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace InternConnect.Util
{
    public static class IocRegistrations
    {
        public static IServiceCollection AddAppSettingsConfig(this IServiceCollection services,
            IConfiguration configuration)
        {
            //Data Repositories
            services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));

            services.AddScoped<IAccountRepository, AccountRepository>();
            services.AddScoped<IAccountService, AccountService>();

            services.AddScoped<IAcademicYearRepository, AcademicYearRepository>();
            services.AddScoped<IAcademicYearService, AcademicYearService>();

            services.AddScoped<IAdminRepository, AdminRepository>();
            services.AddScoped<IAdminService, AdminService>();

            services.AddScoped<IAdminResponseRepository, AdminResponseRepository>();
            services.AddScoped<IAdminResponseService, AdminResponseService>();


            services.AddScoped<ICompanyRepository, CompanyRepository>();
            services.AddScoped<ICompanyService, CompanyService>();


            services.AddScoped<IEventRepository, EventRepository>();
            services.AddScoped<IEventService, EventService>();

            services.AddScoped<ILogsRepository, LogsRepository>();
            services.AddScoped<ILogsService, LogsService>();

            services.AddScoped<IOpportunityRepository, OpportunityRepository>();
            services.AddScoped<IOpportunityService, OpportunityService>();


            services.AddScoped<IPdfStateRepository, PdfStateRepository>();
            services.AddScoped<IPdfStateService, PdfStateService>();


            services.AddScoped<IProgramRepository, ProgramRepository>();
            services.AddScoped<IProgramService, ProgramService>();

            services.AddScoped<ISectionRepository, SectionRepository>();
            services.AddScoped<ISectionService, SectionService>();

            services.AddScoped<IStudentRepository, StudentRepository>();
            services.AddScoped<IStudentService, StudentService>();

            services.AddScoped<ISubmissionRepository, SubmissionRepository>();
            services.AddScoped<ISubmissionService, SubmissionService>();

            services.AddScoped<ITrackRepository, TrackRepository>();
            services.AddScoped<ITrackService, TrackService>();

            services.AddScoped<IWebStateRepository, WebStateRepository>();
            services.AddScoped<IWebStateService, WebStateService>();


            //Services-ThirdParties
            services.AddScoped<IMailerService, MailerService>();
            services.AddScoped<IReportService, ReportService>();

            return services;
        }
    }
}
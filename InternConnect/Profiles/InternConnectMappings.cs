using AutoMapper;
using InternConnect.Context.Models;
using InternConnect.Dto.AcademicYear;
using InternConnect.Dto.Account;
using InternConnect.Dto.Admin;
using InternConnect.Dto.AdminLogs;
using InternConnect.Dto.AdminResponse;
using InternConnect.Dto.Company;
using InternConnect.Dto.Event;
using InternConnect.Dto.IsoCode;
using InternConnect.Dto.Opportunity;
using InternConnect.Dto.PdfState;
using InternConnect.Dto.Program;
using InternConnect.Dto.Section;
using InternConnect.Dto.Student;
using InternConnect.Dto.Submission;
using InternConnect.Dto.Track;
using InternConnect.Dto.WebState;

namespace InternConnect.Profiles
{
    public class InternConnectMappings : Profile
    {
        public InternConnectMappings()
        {
            //Academic Year
            CreateMap<AcademicYear, AcademicYearDto.AddAcademicYear>().ReverseMap();
            CreateMap<AcademicYear, AcademicYearDto.ReadAcademicYear>().ReverseMap();
            CreateMap<AcademicYear, AcademicYearDto.UpdateAcademicYear>().ReverseMap();

            //Account
            CreateMap<Account, AccountDto.MapAdmin>().ReverseMap();
            CreateMap<Account, AccountDto.AddAccountStudent>().ReverseMap();
            CreateMap<Account, AccountDto.AddAccountCoordinator>().ReverseMap();
            CreateMap<Account, AccountDto.AddAccountChair>().ReverseMap();
            CreateMap<Account, AccountDto.ReadAccount>().ReverseMap();
            CreateMap<Account, AccountDto.UpdateAccount>().ReverseMap();
            CreateMap<Account, AccountDto.ReadSession>().ReverseMap();
            CreateMap<Account, AccountDto.ReadCoordinator>().ReverseMap();

            //Admin

            CreateMap<Admin, AdminDto.UpdateAdmin>().ReverseMap();
            CreateMap<Admin, AdminDto.ReadAdmin>().ReverseMap();
            CreateMap<Admin, AdminDto.ReadCoordinator>().ReverseMap();

            //AdminResponse
            CreateMap<AdminResponse, AdminResponseDto.ReadResponse>().ReverseMap();
            CreateMap<AdminResponse, AdminResponseDto.UpdateChairResponse>().ReverseMap();

            CreateMap<AdminResponse, AdminResponseDto.UpdateEmailSentResponse>().ReverseMap();
            CreateMap<AdminResponse, AdminResponseDto.UpdateAcceptanceOfCoordinatorResponse>().ReverseMap();
            CreateMap<AdminResponse, AdminResponseDto.UpdateCompanyAgreesResponse>().ReverseMap();
            CreateMap<AdminResponse, AdminResponseDto.UpdateDeanResponse>().ReverseMap();

            //Company
            CreateMap<Company, CompanyDto.AddCompany>().ReverseMap();
            CreateMap<Company, CompanyDto.ReadCompany>().ReverseMap();
            CreateMap<Company, CompanyDto.UpdateCompany>().ReverseMap();
            CreateMap<Company, CompanyDto.UpdateCompanyStatus>().ReverseMap();

            //Event
            CreateMap<Event, EventDto.AddEvent>().ReverseMap();
            CreateMap<Event, EventDto.ReadEvent>().ReverseMap();
            CreateMap<Event, EventDto.UpdateEvent>().ReverseMap();

            //IsoCodes
            CreateMap<IsoCode, IsoCodeDto.AddIsoCode>().ReverseMap();
            CreateMap<IsoCode, IsoCodeDto.DeleteIsoCode>().ReverseMap();
            CreateMap<IsoCode, IsoCodeDto.ReadIsoCode>().ReverseMap();
            CreateMap<IsoCode, IsoCodeDto.TransferIsoCode>().ReverseMap();
            CreateMap<IsoCode, IsoCodeDto.UpdateIsoCode>().ReverseMap();

            //Logs 
            CreateMap<Logs, LogsDto.ReadLogs>().ReverseMap();

            //Opportunity
            CreateMap<Opportunity, OpportunityDto.AddOpportunity>().ReverseMap();
            CreateMap<Opportunity, OpportunityDto.UpdateOpportunity>().ReverseMap();
            CreateMap<Opportunity, OpportunityDto.ReadOpportunity>().ReverseMap();

            //PdfState
            CreateMap<PdfState, PdfStateDto.ReadPdfState>().ReverseMap();
            CreateMap<PdfState, PdfStateDto.UpdatePdfState>().ReverseMap();
            CreateMap<PdfState, PdfStateDto.AddPdfState>().ReverseMap();

            //Program
            CreateMap<Context.Models.Program, ProgramDto.ReadProgram>().ReverseMap();
            CreateMap<Context.Models.Program, ProgramDto.AddProgram>().ReverseMap();
            CreateMap<Context.Models.Program, ProgramDto.UpdateProgram>().ReverseMap();

            //Section
            CreateMap<Section, SectionDto.AddSection>().ReverseMap();
            CreateMap<Section, SectionDto.UpdateSection>().ReverseMap();
            CreateMap<Section, SectionDto.ReadSection>().ReverseMap();

            //Student
            CreateMap<Student, StudentDto.ReadStudent>().ReverseMap();
            CreateMap<Student, StudentDto.UpdateStudent>().ReverseMap();

            //Submission
            CreateMap<Submission, SubmissionDto.AddSubmission>().ReverseMap();
            CreateMap<Submission, SubmissionDto.UpdateSubmission>().ReverseMap();
            CreateMap<Submission, SubmissionDto.ReadSubmission>().ReverseMap();
            CreateMap<Submission, SubmissionDto.ReadStudentData>().ReverseMap();

            //Track
            CreateMap<Track, TrackDto.AddTrack>().ReverseMap();
            CreateMap<Track, TrackDto.UpdateTrack>().ReverseMap();
            CreateMap<Track, TrackDto.ReadTrack>().ReverseMap();

            //WebState
            CreateMap<WebState, WebStateDto.AddWebState>().ReverseMap();
            CreateMap<WebState, WebStateDto.UpdateWebState>().ReverseMap();
            CreateMap<WebState, WebStateDto.ReadWebState>().ReverseMap();
        }
    }
}
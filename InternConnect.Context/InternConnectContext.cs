using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InternConnect.Context.Models;
using Microsoft.EntityFrameworkCore;

namespace InternConnect.Context
{
    public class InternConnectContext : DbContext
    {
        public InternConnectContext(DbContextOptions<InternConnectContext> opt) : base(opt)
        {

        }
        public DbSet<AcademicYear> AcademicYear { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Authorization> Authorizations { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<Content> Content { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<Logs> Logs { get; set; }
        public DbSet<Opportunity> Opportunities { get; set; }
        public DbSet<PdfState> PdfState { get; set; }
        public DbSet<Program> Programs { get; set; }
        public DbSet<Section> Sections { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Submission> Submissions { get; set; }
        public DbSet<Track> Tracks { get; set; }
        public DbSet<WebState> WebState { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Accounts
            modelBuilder.Entity<Account>().HasKey(a => a.Id);
            modelBuilder.Entity<Account>().Property(a => a.Email).IsRequired();
            modelBuilder.Entity<Account>().Property(a => a.Password).IsRequired();
            modelBuilder.Entity<Account>().Property(a => a.ResetKey).IsRequired();
            //Student
            modelBuilder.Entity<Student>().HasKey(s => s.Id);
            modelBuilder.Entity<Student>()
                .HasOne(s => s.Account).WithOne(s => s.Student).HasForeignKey<Student>("AccountId");
            modelBuilder.Entity<Student>()
                .HasOne(s => s.Section).WithOne(s => s.Student).HasForeignKey<Student>("SectionId");
            modelBuilder.Entity<Student>()
                .HasOne(s => s.Submission).WithOne(s => s.Student).HasForeignKey<Student>("SubmissionId");
            modelBuilder.Entity<Student>()
                .HasOne(p => p.Program).WithOne(s => s.Student).HasForeignKey<Student>("ProgramId");
            modelBuilder.Entity<Student>().Property(s => s.DateAdded).IsRequired();
            modelBuilder.Entity<Student>().Property(s => s.AddedBy).IsRequired();

            //Admins
            modelBuilder.Entity<Admin>().HasKey(a => a.Id);
            modelBuilder.Entity<Admin>()
                .HasOne(a =>a.Authorization).WithOne(a => a.Admin).HasForeignKey<Admin>("AuthId");
            modelBuilder.Entity<Admin>()
                .HasOne(a => a.Program).WithOne(p => p.Admin).HasForeignKey<Admin>("ProgramId");
            modelBuilder.Entity<Admin>()
                .HasOne(a => a.Section).WithOne(s => s.Admin).HasForeignKey<Admin>("SectionId");
            modelBuilder.Entity<Admin>()
                .HasMany(a => a.Logs).WithOne(l => l.Admin).HasForeignKey(l => l.AdminId);
            modelBuilder.Entity<Admin>()
                .HasMany(a => a.Events).WithOne(e => e.Admin).HasForeignKey(e => e.AdminId);
            modelBuilder.Entity<Admin>()
                .HasOne(a => a.Account).WithOne(a => a.Admin).HasForeignKey<Admin>("AccountId").IsRequired();

            //Events
            modelBuilder.Entity<Event>().HasKey(e => e.Id);
            modelBuilder.Entity<Event>().Property(e => e.Name).IsRequired();
            modelBuilder.Entity<Event>().Property(e => e.StartDate).IsRequired();
            modelBuilder.Entity<Event>().Property(e => e.EndDate).IsRequired();



            //AcademicYear
            modelBuilder.Entity<AcademicYear>().HasKey(ay => ay.Id);
            modelBuilder.Entity<AcademicYear>().Property(ay => ay.StartDate).IsRequired();
            modelBuilder.Entity<AcademicYear>().Property(ay => ay.IgaarpEmail).IsRequired();
            modelBuilder.Entity<AcademicYear>().Property(ay => ay.EndDate).IsRequired();
            modelBuilder.Entity<AcademicYear>()
                .HasOne(ay => ay.PdfState).WithOne(s => s.AcademicYear).HasForeignKey<AcademicYear>("PdfStateId");

            //PDF State
            modelBuilder.Entity<PdfState>().HasKey(s => s.Id);
            modelBuilder.Entity<PdfState>().Property(s => s.IgaarpName).IsRequired();
            modelBuilder.Entity<PdfState>().Property(s => s.UstLogoFileName).IsRequired();
            modelBuilder.Entity<PdfState>().Property(s => s.CollegeLogoFileName).IsRequired();

            //Submissions
            modelBuilder.Entity<Submission>().HasKey(s => s.Id);
            modelBuilder.Entity<Submission>().Property(s => s.SubmissionDate).IsRequired();
            modelBuilder.Entity<Submission>().Property(s => s.LastName).IsRequired();
            modelBuilder.Entity<Submission>().Property(s => s.FirstName).IsRequired();
            modelBuilder.Entity<Submission>().Property(s => s.MiddleInitial).IsRequired();
            modelBuilder.Entity<Submission>().Property(s => s.StudentNumber).IsRequired();
            modelBuilder.Entity<Submission>().Property(s => s.CompanyName).IsRequired();
            modelBuilder.Entity<Submission>().Property(s => s.CompanyAddress).IsRequired();
            modelBuilder.Entity<Submission>().Property(s => s.AcceptanceLetterFileName).IsRequired();
            modelBuilder.Entity<Submission>().Property(s => s.CompanyProfileFileName).IsRequired();

            modelBuilder.Entity<Submission>()
                .HasOne(s => s.AcademicYear).WithOne(ay => ay.Submission).HasForeignKey<Submission>("AcademicYearId");

            //Authorization
            modelBuilder.Entity<Authorization>().HasKey(auth => auth.Id);
            modelBuilder.Entity<Authorization>().Property(auth => auth.Name).IsRequired();

            //Sections
            modelBuilder.Entity<Section>().HasKey(s => s.Id);
            modelBuilder.Entity<Section>().Property(s => s.Name).IsRequired();

            //Programs
            modelBuilder.Entity<Program>().HasKey(p => p.Id);
            modelBuilder.Entity<Program>().Property(p => p.Name).IsRequired();

            //Track
            modelBuilder.Entity<Track>().HasKey(t => t.Id);
            modelBuilder.Entity<Track>().Property(t => t.Name).IsRequired();
            modelBuilder.Entity<Track>()
                .HasOne(t => t.Programs).WithMany(p => p.Tracks).HasForeignKey(p => p.ProgramId);
            //Logs
            modelBuilder.Entity<Logs>().HasKey(l => l.Id);
            modelBuilder.Entity<Logs>().Property(l => l.DateStamped).IsRequired();
            modelBuilder.Entity<Logs>().Property(l => l.SubmissionId).IsRequired();

            //Content
            modelBuilder.Entity<Content>().HasKey(c => c.Id);
            modelBuilder.Entity<Content>()
                .HasOne(c => c.WebState).WithOne(ws => ws.Content).HasForeignKey<Content>("StateId");
            modelBuilder.Entity<Content>()
                .HasMany(c => c.Company).WithOne(c => c.Content).HasForeignKey(c => c.ContentId);

            //WebpageState
            modelBuilder.Entity<WebState>().HasKey(ws => ws.Id);
            modelBuilder.Entity<WebState>().Property(ws => ws.LogoFileName).IsRequired();
            modelBuilder.Entity<WebState>().Property(ws => ws.CoverPhotoFileName).IsRequired();

            //Companies
            modelBuilder.Entity<Company>().HasKey(c => c.Id);
            modelBuilder.Entity<Company>().Property(c=>c.Name).IsRequired();
            modelBuilder.Entity<Company>().Property(c => c.Address).IsRequired();
            modelBuilder.Entity<Company>().Property(c => c.City).IsRequired();
            modelBuilder.Entity<Company>()
                .HasMany(c=>c.Opportunities).WithOne(o=>o.Company).HasForeignKey(o=>o.CompanyId);



            //Opportunities
            modelBuilder.Entity<Opportunity>().HasKey(o => o.Id);
            modelBuilder.Entity<Opportunity>().Property(o => o.Title).IsRequired();
            modelBuilder.Entity<Opportunity>().Property(o => o.Position).IsRequired();












        }
    }

    
}

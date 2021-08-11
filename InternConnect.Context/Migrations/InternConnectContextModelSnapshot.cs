﻿// <auto-generated />
using System;
using InternConnect.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace InternConnect.Context.Migrations
{
    [DbContext(typeof(InternConnectContext))]
    partial class InternConnectContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.8")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("InternConnect.Context.Models.AcademicYear", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CollegeName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("IgaarpEmail")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PdfStateId")
                        .HasColumnType("int");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("PdfStateId")
                        .IsUnique();

                    b.ToTable("AcademicYear");
                });

            modelBuilder.Entity("InternConnect.Context.Models.Account", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ResetKey")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Accounts");
                });

            modelBuilder.Entity("InternConnect.Context.Models.Admin", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AccountId")
                        .HasColumnType("int");

                    b.Property<int>("AuthId")
                        .HasColumnType("int");

                    b.Property<int>("ProgramId")
                        .HasColumnType("int");

                    b.Property<int>("SectionId")
                        .HasColumnType("int");

                    b.Property<string>("StampFileName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("AccountId")
                        .IsUnique();

                    b.HasIndex("AuthId")
                        .IsUnique();

                    b.HasIndex("ProgramId")
                        .IsUnique();

                    b.HasIndex("SectionId")
                        .IsUnique();

                    b.ToTable("Admins");
                });

            modelBuilder.Entity("InternConnect.Context.Models.AdminResponse", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("AcceptedByChair")
                        .HasColumnType("bit");

                    b.Property<bool>("AcceptedByCoordinator")
                        .HasColumnType("bit");

                    b.Property<bool>("AcceptedByDean")
                        .HasColumnType("bit");

                    b.Property<bool>("CompanyAgrees")
                        .HasColumnType("bit");

                    b.Property<bool>("EmailSentByCoordinator")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.ToTable("AdminResponses");
                });

            modelBuilder.Entity("InternConnect.Context.Models.Authorization", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Authorizations");
                });

            modelBuilder.Entity("InternConnect.Context.Models.Company", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("AddressOne")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("AddressThree")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("AddressTwo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ContentId")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("HeaderFileName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LogoFileName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ContentId");

                    b.ToTable("Companies");
                });

            modelBuilder.Entity("InternConnect.Context.Models.Content", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("StateId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("StateId")
                        .IsUnique();

                    b.ToTable("Content");
                });

            modelBuilder.Entity("InternConnect.Context.Models.Event", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AdminId")
                        .HasColumnType("int");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("AdminId");

                    b.ToTable("Events");
                });

            modelBuilder.Entity("InternConnect.Context.Models.Logs", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AdminId")
                        .HasColumnType("int");

                    b.Property<DateTime>("DateStamped")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SubmissionId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("AdminId");

                    b.ToTable("Logs");
                });

            modelBuilder.Entity("InternConnect.Context.Models.Opportunity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CompanyId")
                        .HasColumnType("int");

                    b.Property<string>("Introduction")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Position")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CompanyId");

                    b.ToTable("Opportunities");
                });

            modelBuilder.Entity("InternConnect.Context.Models.PdfState", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CollegeLogoFileName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DeanName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("IgaarpName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UstLogoFileName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("PdfState");
                });

            modelBuilder.Entity("InternConnect.Context.Models.Program", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("EventId")
                        .HasColumnType("int");

                    b.Property<int>("IsoCode")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("EventId");

                    b.ToTable("Programs");
                });

            modelBuilder.Entity("InternConnect.Context.Models.Section", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Sections");
                });

            modelBuilder.Entity("InternConnect.Context.Models.Student", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AccountId")
                        .HasColumnType("int");

                    b.Property<string>("AddedBy")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DateAdded")
                        .HasColumnType("datetime2");

                    b.Property<int>("ProgramId")
                        .HasColumnType("int");

                    b.Property<int>("SectionId")
                        .HasColumnType("int");

                    b.Property<int>("SubmissionId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("AccountId")
                        .IsUnique();

                    b.HasIndex("ProgramId")
                        .IsUnique();

                    b.HasIndex("SectionId")
                        .IsUnique();

                    b.HasIndex("SubmissionId")
                        .IsUnique();

                    b.ToTable("Students");
                });

            modelBuilder.Entity("InternConnect.Context.Models.Submission", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AcademicYearId")
                        .HasColumnType("int");

                    b.Property<string>("AcceptanceLetterFileName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("AdminResponseId")
                        .HasColumnType("int");

                    b.Property<int>("CompanyId")
                        .HasColumnType("int");

                    b.Property<string>("CompanyProfileFileName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ContactPerson")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ContactPersonEmail")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ContactPersonPosition")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("EndorsementFileName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("IsoCode")
                        .HasColumnType("int");

                    b.Property<string>("JobDescription")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MiddleInitial")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("StudentNumber")
                        .HasColumnType("int");

                    b.Property<DateTime>("SubmissionDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("TrackId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("AcademicYearId")
                        .IsUnique();

                    b.HasIndex("AdminResponseId")
                        .IsUnique();

                    b.HasIndex("CompanyId")
                        .IsUnique();

                    b.ToTable("Submissions");
                });

            modelBuilder.Entity("InternConnect.Context.Models.Track", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ProgramId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ProgramId");

                    b.ToTable("Tracks");
                });

            modelBuilder.Entity("InternConnect.Context.Models.WebState", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CoverPhotoFileName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LogoFileName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("WebState");
                });

            modelBuilder.Entity("InternConnect.Context.Models.AcademicYear", b =>
                {
                    b.HasOne("InternConnect.Context.Models.PdfState", "PdfState")
                        .WithOne("AcademicYear")
                        .HasForeignKey("InternConnect.Context.Models.AcademicYear", "PdfStateId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("PdfState");
                });

            modelBuilder.Entity("InternConnect.Context.Models.Admin", b =>
                {
                    b.HasOne("InternConnect.Context.Models.Account", "Account")
                        .WithOne("Admin")
                        .HasForeignKey("InternConnect.Context.Models.Admin", "AccountId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("InternConnect.Context.Models.Authorization", "Authorization")
                        .WithOne("Admin")
                        .HasForeignKey("InternConnect.Context.Models.Admin", "AuthId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("InternConnect.Context.Models.Program", "Program")
                        .WithOne("Admin")
                        .HasForeignKey("InternConnect.Context.Models.Admin", "ProgramId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("InternConnect.Context.Models.Section", "Section")
                        .WithOne("Admin")
                        .HasForeignKey("InternConnect.Context.Models.Admin", "SectionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Account");

                    b.Navigation("Authorization");

                    b.Navigation("Program");

                    b.Navigation("Section");
                });

            modelBuilder.Entity("InternConnect.Context.Models.Company", b =>
                {
                    b.HasOne("InternConnect.Context.Models.Content", "Content")
                        .WithMany("Company")
                        .HasForeignKey("ContentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Content");
                });

            modelBuilder.Entity("InternConnect.Context.Models.Content", b =>
                {
                    b.HasOne("InternConnect.Context.Models.WebState", "WebState")
                        .WithOne("Content")
                        .HasForeignKey("InternConnect.Context.Models.Content", "StateId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("WebState");
                });

            modelBuilder.Entity("InternConnect.Context.Models.Event", b =>
                {
                    b.HasOne("InternConnect.Context.Models.Admin", "Admin")
                        .WithMany("Events")
                        .HasForeignKey("AdminId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Admin");
                });

            modelBuilder.Entity("InternConnect.Context.Models.Logs", b =>
                {
                    b.HasOne("InternConnect.Context.Models.Admin", "Admin")
                        .WithMany("Logs")
                        .HasForeignKey("AdminId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Admin");
                });

            modelBuilder.Entity("InternConnect.Context.Models.Opportunity", b =>
                {
                    b.HasOne("InternConnect.Context.Models.Company", "Company")
                        .WithMany("Opportunities")
                        .HasForeignKey("CompanyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Company");
                });

            modelBuilder.Entity("InternConnect.Context.Models.Program", b =>
                {
                    b.HasOne("InternConnect.Context.Models.Event", "Event")
                        .WithMany()
                        .HasForeignKey("EventId");

                    b.Navigation("Event");
                });

            modelBuilder.Entity("InternConnect.Context.Models.Student", b =>
                {
                    b.HasOne("InternConnect.Context.Models.Account", "Account")
                        .WithOne("Student")
                        .HasForeignKey("InternConnect.Context.Models.Student", "AccountId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("InternConnect.Context.Models.Program", "Program")
                        .WithOne("Student")
                        .HasForeignKey("InternConnect.Context.Models.Student", "ProgramId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("InternConnect.Context.Models.Section", "Section")
                        .WithOne("Student")
                        .HasForeignKey("InternConnect.Context.Models.Student", "SectionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("InternConnect.Context.Models.Submission", "Submission")
                        .WithOne("Student")
                        .HasForeignKey("InternConnect.Context.Models.Student", "SubmissionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Account");

                    b.Navigation("Program");

                    b.Navigation("Section");

                    b.Navigation("Submission");
                });

            modelBuilder.Entity("InternConnect.Context.Models.Submission", b =>
                {
                    b.HasOne("InternConnect.Context.Models.AcademicYear", "AcademicYear")
                        .WithOne("Submission")
                        .HasForeignKey("InternConnect.Context.Models.Submission", "AcademicYearId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("InternConnect.Context.Models.AdminResponse", "AdminResponse")
                        .WithOne("Submission")
                        .HasForeignKey("InternConnect.Context.Models.Submission", "AdminResponseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("InternConnect.Context.Models.Company", "Company")
                        .WithOne("Submission")
                        .HasForeignKey("InternConnect.Context.Models.Submission", "CompanyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AcademicYear");

                    b.Navigation("AdminResponse");

                    b.Navigation("Company");
                });

            modelBuilder.Entity("InternConnect.Context.Models.Track", b =>
                {
                    b.HasOne("InternConnect.Context.Models.Program", "Programs")
                        .WithMany("Tracks")
                        .HasForeignKey("ProgramId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Programs");
                });

            modelBuilder.Entity("InternConnect.Context.Models.AcademicYear", b =>
                {
                    b.Navigation("Submission");
                });

            modelBuilder.Entity("InternConnect.Context.Models.Account", b =>
                {
                    b.Navigation("Admin");

                    b.Navigation("Student");
                });

            modelBuilder.Entity("InternConnect.Context.Models.Admin", b =>
                {
                    b.Navigation("Events");

                    b.Navigation("Logs");
                });

            modelBuilder.Entity("InternConnect.Context.Models.AdminResponse", b =>
                {
                    b.Navigation("Submission");
                });

            modelBuilder.Entity("InternConnect.Context.Models.Authorization", b =>
                {
                    b.Navigation("Admin");
                });

            modelBuilder.Entity("InternConnect.Context.Models.Company", b =>
                {
                    b.Navigation("Opportunities");

                    b.Navigation("Submission");
                });

            modelBuilder.Entity("InternConnect.Context.Models.Content", b =>
                {
                    b.Navigation("Company");
                });

            modelBuilder.Entity("InternConnect.Context.Models.PdfState", b =>
                {
                    b.Navigation("AcademicYear");
                });

            modelBuilder.Entity("InternConnect.Context.Models.Program", b =>
                {
                    b.Navigation("Admin");

                    b.Navigation("Student");

                    b.Navigation("Tracks");
                });

            modelBuilder.Entity("InternConnect.Context.Models.Section", b =>
                {
                    b.Navigation("Admin");

                    b.Navigation("Student");
                });

            modelBuilder.Entity("InternConnect.Context.Models.Submission", b =>
                {
                    b.Navigation("Student");
                });

            modelBuilder.Entity("InternConnect.Context.Models.WebState", b =>
                {
                    b.Navigation("Content");
                });
#pragma warning restore 612, 618
        }
    }
}

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InternConnect.Context.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InternConnect.Context.Entity_Configurations
{
    public class SubmissionConfig : IEntityTypeConfiguration<Submission>
    {
        public void Configure(EntityTypeBuilder<Submission> modelBuilder)
        {
            modelBuilder.HasKey(s => s.Id);
            modelBuilder.Property(s => s.SubmissionDate).IsRequired();
            modelBuilder.Property(s => s.LastName).IsRequired();
            modelBuilder.Property(s => s.FirstName).IsRequired();
            modelBuilder.Property(s => s.MiddleInitial).IsRequired();
            modelBuilder.Property(s => s.StudentNumber).IsRequired();
            modelBuilder.Property(s => s.AcceptanceLetterFileName).IsRequired();
            modelBuilder.Property(s => s.CompanyProfileFileName).IsRequired();
            modelBuilder.Property(s => s.ContactPerson).IsRequired();
            modelBuilder.Property(s => s.ContactPersonEmail).IsRequired();
            modelBuilder.Property(s => s.ContactPersonPosition).IsRequired();
            modelBuilder.Property(s => s.IsoCode).IsRequired();

            modelBuilder
                .HasOne(s => s.AcademicYear).WithOne(ay => ay.Submission).HasForeignKey<Submission>("AcademicYearId");

            modelBuilder
                .HasOne(s => s.Company).WithOne(c => c.Submission).HasForeignKey<Submission>("CompanyId");

            modelBuilder
                .HasOne(s => s.AdminResponse).WithOne(ar => ar.Submission).HasForeignKey<Submission>("AdminResponseId");


        }



    }
}

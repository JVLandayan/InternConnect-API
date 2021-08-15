using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InternConnect.Context.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InternConnect.Context.Entity_Configurations
{
    public class AcademicYearConfig: IEntityTypeConfiguration<AcademicYear>
    {

        public void Configure(EntityTypeBuilder<AcademicYear> modelBuilder)
        {
            modelBuilder.HasKey(ay => ay.Id);
            modelBuilder.Property(ay => ay.StartDate).IsRequired();
            modelBuilder.Property(ay => ay.IgaarpEmail).IsRequired();
            modelBuilder.Property(ay => ay.EndDate).IsRequired();
            modelBuilder
                .HasOne(ay => ay.PdfState)
                .WithOne(s => s.AcademicYear).HasForeignKey<AcademicYear>("PdfStateId");

        }



    }
}

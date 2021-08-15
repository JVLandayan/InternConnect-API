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
    public class PdfStateConfig : IEntityTypeConfiguration<PdfState>
    {
        public void Configure(EntityTypeBuilder<PdfState> modelBuilder)
        {
            modelBuilder.HasKey(s => s.Id);
            modelBuilder.Property(s => s.IgaarpName).IsRequired();
            modelBuilder.Property(s => s.UstLogoFileName).IsRequired();
            modelBuilder.Property(s => s.CollegeLogoFileName).IsRequired();
            modelBuilder.Property(s => s.DeanName).IsRequired();
        }
    }
}

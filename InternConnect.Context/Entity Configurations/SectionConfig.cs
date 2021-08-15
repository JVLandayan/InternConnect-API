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
    public class SectionConfig : IEntityTypeConfiguration<Section>
    {
        public void Configure(EntityTypeBuilder<Section> modelBuilder)
        {
            modelBuilder.HasKey(s => s.Id);
            modelBuilder.Property(s => s.Name).IsRequired();
        }
    }
}

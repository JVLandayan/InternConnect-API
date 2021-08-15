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
    public class ProgramConfig : IEntityTypeConfiguration<Program>
    {
        public void Configure(EntityTypeBuilder<Program> modelBuilder)
        {
            modelBuilder.HasKey(p => p.Id);
            modelBuilder.Property(p => p.Name).IsRequired();
        }
    }
}

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
    public class CompanyConfig : IEntityTypeConfiguration<Company>
    {
        public void Configure(EntityTypeBuilder<Company> modelBuilder)
        {

            modelBuilder.HasKey(c => c.Id);
            modelBuilder.Property(c => c.Name).IsRequired();
            modelBuilder.Property(c => c.AddressOne).IsRequired();
            modelBuilder.Property(c => c.City).IsRequired();
            modelBuilder
                .HasMany(c => c.Opportunities).WithOne(o => o.Company).HasForeignKey(o => o.CompanyId);
        }
    }
}

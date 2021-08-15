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
    public class AdminConfig : IEntityTypeConfiguration<Admin>
    {
        public void Configure(EntityTypeBuilder<Admin> modelBuilder)
        {

            modelBuilder.HasKey(a => a.Id);
            modelBuilder
                .HasOne(a => a.Authorization).WithOne(a => a.Admin).HasForeignKey<Admin>("AuthId");
            modelBuilder
                .HasOne(a => a.Program).WithOne(p => p.Admin).HasForeignKey<Admin>("ProgramId");
            modelBuilder
                .HasOne(a => a.Section).WithOne(s => s.Admin).HasForeignKey<Admin>("SectionId");
            modelBuilder
                .HasMany(a => a.Logs).WithOne(l => l.Admin).HasForeignKey(l => l.AdminId);
            modelBuilder
                .HasMany(a => a.Events).WithOne(e => e.Admin).HasForeignKey(e => e.AdminId);
            modelBuilder
                .HasOne(a => a.Account).WithOne(a => a.Admin).HasForeignKey<Admin>("AccountId").IsRequired();

        }

    }
}

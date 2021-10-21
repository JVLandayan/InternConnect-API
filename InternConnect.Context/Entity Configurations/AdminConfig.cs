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
                .HasOne(a => a.Authorization).WithMany(a => a.Admins).HasForeignKey(a => a.AuthId);
            modelBuilder
                .HasOne(a => a.Program).WithMany(p => p.Admins).HasForeignKey(a => a.ProgramId);
            modelBuilder
                .HasOne(a => a.Section).WithMany(s => s.Admins).HasForeignKey(a => a.SectionId);
            modelBuilder
                .HasMany(a => a.Events).WithOne(e => e.Admin).HasForeignKey(e => e.AdminId);
            modelBuilder
                .HasOne(a => a.Account).WithOne(a => a.Admin).HasForeignKey<Admin>("AccountId").IsRequired();
        }
    }
}
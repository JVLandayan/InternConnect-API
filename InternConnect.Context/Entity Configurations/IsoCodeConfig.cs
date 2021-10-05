using InternConnect.Context.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InternConnect.Context.Entity_Configurations
{
    public class IsoCodeConfig : IEntityTypeConfiguration<IsoCode>
    {
        public void Configure(EntityTypeBuilder<IsoCode> modelBuilder)
        {
            modelBuilder.HasKey(e => e.Id);
            modelBuilder.Property(s => s.Code).IsRequired();
            modelBuilder
                .HasOne(i => i.Program).WithMany(p => p.IsoCodes).HasForeignKey(i => i.ProgramId).IsRequired();
            modelBuilder.Property(i => i.Used).IsRequired();
            modelBuilder
                .HasOne(i => i.Admin).WithMany(a => a.IsoCodes).HasForeignKey(i => i.AdminId).IsRequired();
        }
    }
}
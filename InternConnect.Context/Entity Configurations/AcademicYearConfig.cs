using InternConnect.Context.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InternConnect.Context.Entity_Configurations
{
    public class AcademicYearConfig : IEntityTypeConfiguration<AcademicYear>
    {
        public void Configure(EntityTypeBuilder<AcademicYear> modelBuilder)
        {
            modelBuilder.HasKey(ay => ay.Id);
            modelBuilder.Property(ay => ay.StartYear).IsRequired();
            modelBuilder.Property(ay => ay.IgaarpEmail).IsRequired();
            modelBuilder.Property(ay => ay.EndYear).IsRequired();
        }
    }
}
using InternConnect.Context.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InternConnect.Context.Entity_Configurations
{
    public class StudentConfig : IEntityTypeConfiguration<Student>
    {
        public void Configure(EntityTypeBuilder<Student> modelBuilder)
        {
            modelBuilder.HasKey(s => s.Id);
            modelBuilder
                .HasOne(s => s.Account).WithOne(s => s.Student).HasForeignKey<Student>("AccountId");
            modelBuilder
                .HasOne(s => s.Section).WithMany(s => s.Students).HasForeignKey(s => s.SectionId);
            modelBuilder
                .HasMany(s => s.Submissions).WithOne(s => s.Student).HasForeignKey(s => s.StudentId);
            modelBuilder
                .HasOne(p => p.Program).WithMany(s => s.Students).HasForeignKey(s => s.ProgramId);
            modelBuilder.Property(s => s.DateAdded).IsRequired();
            modelBuilder.Property(s => s.AddedBy).IsRequired();
        }
    }
}
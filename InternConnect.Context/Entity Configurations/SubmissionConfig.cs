using InternConnect.Context.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InternConnect.Context.Entity_Configurations
{
    public class SubmissionConfig : IEntityTypeConfiguration<Submission>
    {
        public void Configure(EntityTypeBuilder<Submission> modelBuilder)
        {
            modelBuilder.HasKey(s => s.Id);
            modelBuilder.Property(s => s.SubmissionDate).IsRequired();
            modelBuilder.Property(s => s.LastName).IsRequired();
            modelBuilder.Property(s => s.FirstName).IsRequired();
            modelBuilder.Property(s => s.MiddleInitial).IsRequired();
            modelBuilder.Property(s => s.StudentNumber).IsRequired();
            modelBuilder.Property(s => s.AcceptanceLetterFileName).IsRequired();
            modelBuilder.Property(s => s.CompanyProfileFileName).IsRequired();
            modelBuilder.Property(s => s.ContactPerson).IsRequired();
            modelBuilder.Property(s => s.ContactPersonEmail).IsRequired();
            modelBuilder.Property(s => s.ContactPersonPosition).IsRequired();
            modelBuilder.Property(s => s.IsoCode).IsRequired();

            modelBuilder
                .HasOne(s => s.AdminResponse).WithOne(ar => ar.Submission)
                .HasForeignKey<AdminResponse>(s => s.SubmissionId);
        }
    }
}
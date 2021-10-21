using InternConnect.Context.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InternConnect.Context.Entity_Configurations
{
    public class LogsConfig : IEntityTypeConfiguration<Logs>
    {
        public void Configure(EntityTypeBuilder<Logs> modelBuilder)
        {
            modelBuilder.HasKey(l => l.Id);
            modelBuilder.Property(l => l.DateStamped).IsRequired();
            modelBuilder.Property(l => l.SubmissionId).IsRequired();
            modelBuilder
                .HasOne(l => l.Submission).WithMany(s => s.Logs).HasForeignKey(s => s.SubmissionId).IsRequired();
            modelBuilder.Property(l => l.Action).IsRequired();
        }
    }
}
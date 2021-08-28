using InternConnect.Context.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InternConnect.Context.Entity_Configurations
{
    public class OpportunityConfig : IEntityTypeConfiguration<Opportunity>
    {
        public void Configure(EntityTypeBuilder<Opportunity> modelBuilder)
        {
            modelBuilder.HasKey(o => o.Id);
            modelBuilder.Property(o => o.Title).IsRequired();
            modelBuilder.Property(o => o.Position).IsRequired();
        }
    }
}
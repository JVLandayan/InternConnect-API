using InternConnect.Context.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InternConnect.Context.Entity_Configurations
{
    public class EventConfig : IEntityTypeConfiguration<Event>
    {
        public void Configure(EntityTypeBuilder<Event> modelBuilder)
        {
            modelBuilder.HasKey(e => e.Id);
            modelBuilder.Property(e => e.Name).IsRequired();
            modelBuilder.Property(e => e.StartDate).IsRequired();
            modelBuilder.Property(e => e.EndDate).IsRequired();
            modelBuilder.Property(e => e.AdminId).IsRequired();
        }
    }
}
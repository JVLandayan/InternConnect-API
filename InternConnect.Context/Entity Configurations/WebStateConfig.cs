using InternConnect.Context.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InternConnect.Context.Entity_Configurations
{
    public class WebStateConfig : IEntityTypeConfiguration<WebState>
    {
        public void Configure(EntityTypeBuilder<WebState> modelBuilder)
        {
            modelBuilder.HasKey(ws => ws.Id);
            modelBuilder.Property(ws => ws.LogoFileName).IsRequired();
            modelBuilder.Property(ws => ws.CoverPhotoFileName).IsRequired();
        }
    }
}
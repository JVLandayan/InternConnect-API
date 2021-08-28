using InternConnect.Context.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InternConnect.Context.Entity_Configurations
{
    public class AdminResponseConfig : IEntityTypeConfiguration<AdminResponse>
    {
        public void Configure(EntityTypeBuilder<AdminResponse> modelBuilder)
        {
            modelBuilder.HasKey(ar => ar.Id);
            modelBuilder.Property(ar => ar.Comments).IsRequired(false);
        }
    }
}
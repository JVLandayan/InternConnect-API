using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InternConnect.Context.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InternConnect.Context.Entity_Configurations
{
    public class ContentConfig : IEntityTypeConfiguration<Content>
    {
        public void Configure(EntityTypeBuilder<Content> modelBuilder)
        {
            modelBuilder.HasKey(c => c.Id);
            modelBuilder
                .HasOne(c => c.WebState).WithOne(ws => ws.Content).HasForeignKey<Content>("StateId");
            modelBuilder
                .HasMany(c => c.Company).WithOne(c => c.Content).HasForeignKey(c => c.ContentId);
        }

    }
}

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
    public class TrackConfig : IEntityTypeConfiguration<Track>
    {
        public void Configure(EntityTypeBuilder<Track> modelBuilder)
        {
            modelBuilder.HasKey(t => t.Id);
            modelBuilder.Property(t => t.Name).IsRequired();
            modelBuilder
                .HasOne(t => t.Programs).WithMany(p => p.Tracks).HasForeignKey(p => p.ProgramId);

        }

    }
}

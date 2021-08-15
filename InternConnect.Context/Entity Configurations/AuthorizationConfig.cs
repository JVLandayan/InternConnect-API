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
    public class AuthorizationConfig : IEntityTypeConfiguration<Authorization>
    {
        public void Configure(EntityTypeBuilder<Authorization> modelBuilder)
        {
            modelBuilder.HasKey(auth => auth.Id);
            modelBuilder.Property(auth => auth.Name).IsRequired();

        }
    }
}

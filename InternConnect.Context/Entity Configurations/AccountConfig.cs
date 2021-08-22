using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using InternConnect.Context.Models;
using Microsoft.EntityFrameworkCore;

namespace InternConnect.Context.Entity_Configurations
{
    public class AccountConfig : IEntityTypeConfiguration<Account>
    {
        public void Configure(EntityTypeBuilder<Account> modelBuilder)
        {
            modelBuilder.HasKey(a => a.Id);
            modelBuilder.Property(a => a.Email).IsRequired();
            modelBuilder.Property(a => a.Password).IsRequired();
            modelBuilder.Property(a => a.ResetKey).IsRequired();


        }

    }
}

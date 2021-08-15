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
    public class AdminResponseConfig : IEntityTypeConfiguration<AdminResponse>
    {
        public void Configure(EntityTypeBuilder<AdminResponse> modelBuilder)
        {
            modelBuilder.HasKey(ar => ar.Id);

        }
    }
}

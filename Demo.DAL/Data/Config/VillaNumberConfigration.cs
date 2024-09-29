using Demo.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.DAL.Data.Config
{
    public class VillaNumberConfigration : IEntityTypeConfiguration<VillaNumber>
    {
        public void Configure(EntityTypeBuilder<VillaNumber> builder)
        {
            builder.HasOne(x => x.Villa).WithMany()
                .HasForeignKey(x => x.VillaId);
            builder.Property(x => x.VillaNo).IsRequired();
        }
    }
}

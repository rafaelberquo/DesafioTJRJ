using DesafioTJRJ.Business.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesafioTJRJ.Data.Mappings
{
    public class FormaCompraMap : IEntityTypeConfiguration<FormaCompra>
    {
        public void Configure(EntityTypeBuilder<FormaCompra> builder)
        {
            builder.ToTable("FormaCompra");

            builder.HasKey(f => f.CodFormaCompra);

            builder.Property(f => f.CodFormaCompra)
                .HasColumnName("CodFormaCompra");

            builder.Property(f => f.Descricao)
                .HasColumnName("Descricao")
                .HasMaxLength(40)
                .IsRequired();

            builder.HasMany(f => f.LivroPrecosFormaCompra)
                .WithOne(lp => lp.FormaCompra)
                .HasForeignKey(lp => lp.CodFormaCompra);
        }
    }
}

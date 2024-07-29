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
    public class LivroPrecoFormaCompraMap : IEntityTypeConfiguration<LivroPrecoFormaCompra>
    {
        public void Configure(EntityTypeBuilder<LivroPrecoFormaCompra> builder)
        {
            builder.ToTable("LivroPrecoFormaCompra");

            builder.HasKey(lp => new { lp.CodL, lp.CodFormaCompra });

            builder.Property(lp => lp.CodL)
                .HasColumnName("CodL");

            builder.Property(lp => lp.CodFormaCompra)
                .HasColumnName("CodFormaCompra");

            builder.Property(lp => lp.Preco)
                .HasColumnName("Preco")
                .HasColumnType("decimal(10, 2)")
                .IsRequired();

            builder.HasOne(lp => lp.Livro)
                .WithMany(l => l.LivroPrecosFormaCompra)
                .HasForeignKey(lp => lp.CodL);

            builder.HasOne(lp => lp.FormaCompra)
                .WithMany(f => f.LivroPrecosFormaCompra)
                .HasForeignKey(lp => lp.CodFormaCompra);
        }
    }
}
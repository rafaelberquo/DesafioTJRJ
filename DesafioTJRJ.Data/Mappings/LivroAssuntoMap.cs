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
    public class LivroAssuntoMap : IEntityTypeConfiguration<LivroAssunto>
    {
        public void Configure(EntityTypeBuilder<LivroAssunto> builder)
        {
            builder.ToTable("LivroAssunto");

            builder.HasKey(la => new { la.CodL, la.CodAs });

            builder.Property(la => la.CodL)
                .HasColumnName("CodL");

            builder.Property(la => la.CodAs)
                .HasColumnName("CodAs");

            builder.HasOne(la => la.Livro)
                .WithMany(l => l.LivroAssuntos)
                .HasForeignKey(la => la.CodL);

            builder.HasOne(la => la.Assunto)
                .WithMany(a => a.LivroAssuntos)
                .HasForeignKey(la => la.CodAs);
        }
    }

}
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
    public class LivroAutorMap : IEntityTypeConfiguration<LivroAutor>
    {
        public void Configure(EntityTypeBuilder<LivroAutor> builder)
        {
            builder.ToTable("LivroAutor");

            builder.HasKey(la => new { la.CodL, la.CodAu });

            builder.Property(la => la.CodL)
                .HasColumnName("CodL");

            builder.Property(la => la.CodAu)
                .HasColumnName("CodAu");

            builder.HasOne(la => la.Livro)
                .WithMany(l => l.LivroAutores)
                .HasForeignKey(la => la.CodL);

            builder.HasOne(la => la.Autor)
                .WithMany(a => a.LivroAutores)
                .HasForeignKey(la => la.CodAu);
        }
    }
}
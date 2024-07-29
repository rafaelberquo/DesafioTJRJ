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
    public class LivroMap : IEntityTypeConfiguration<Livro>
    {
        public void Configure(EntityTypeBuilder<Livro> builder)
        {
            builder.ToTable("Livro");

            builder.HasKey(l => l.CodL);

            builder.Property(l => l.CodL)
                .HasColumnName("CodL");

            builder.Property(l => l.Titulo)
                .HasColumnName("Titulo")
                .HasMaxLength(40)
                .IsRequired();

            builder.Property(l => l.Editora)
                .HasColumnName("Editora")
                .HasMaxLength(40)
                .IsRequired();

            builder.Property(l => l.Edicao)
                .HasColumnName("Edicao")
                .IsRequired();

            builder.Property(l => l.AnoPublicacao)
                .HasColumnName("AnoPublicacao")
                .HasMaxLength(4)
                .IsRequired();

            builder.HasMany(l => l.LivroAutores)
                .WithOne(la => la.Livro)
                .HasForeignKey(la => la.CodL);

            builder.HasMany(l => l.LivroAssuntos)
                .WithOne(la => la.Livro)
                .HasForeignKey(la => la.CodL);
        }
    }
}
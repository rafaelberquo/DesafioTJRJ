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
    public class AutorMap : IEntityTypeConfiguration<Autor>
    {
        public void Configure(EntityTypeBuilder<Autor> builder)
        {
            builder.ToTable("Autor");

            builder.HasKey(a => a.CodAu);

            builder.Property(a => a.CodAu)
                .HasColumnName("CodAu");

            builder.Property(a => a.Nome)
                .HasColumnName("Nome")
                .HasMaxLength(40)
                .IsRequired();

            builder.HasMany(a => a.LivroAutores)
                .WithOne(la => la.Autor)
                .HasForeignKey(la => la.CodAu);
        }
    }
}
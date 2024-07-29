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
    public class AssuntoMap : IEntityTypeConfiguration<Assunto>
    {
        public void Configure(EntityTypeBuilder<Assunto> builder)
        {
            builder.ToTable("Assunto");

            builder.HasKey(a => a.CodAs);

            builder.Property(a => a.CodAs)
                .HasColumnName("CodAs");

            builder.Property(a => a.Descricao)
                .HasColumnName("Descricao")
                .HasMaxLength(20)
                .IsRequired();

            builder.HasMany(a => a.LivroAssuntos)
                .WithOne(la => la.Assunto)
                .HasForeignKey(la => la.CodAs);
        }
    }
}
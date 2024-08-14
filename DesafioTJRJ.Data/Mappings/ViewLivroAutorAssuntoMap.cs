using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DesafioTJRJ.Business.Entities;

namespace DesafioTJRJ.Data.Mappings
{
    public class ViewLivroAutorAssuntoMap : IEntityTypeConfiguration<ViewLivroAutorAssunto>
    {
        public void Configure(EntityTypeBuilder<ViewLivroAutorAssunto> builder)
        {
            builder.HasNoKey();
            builder.ToView("vw_LivroAutorAssunto");
        }
    }
}
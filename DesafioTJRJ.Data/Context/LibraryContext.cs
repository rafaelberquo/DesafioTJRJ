using DesafioTJRJ.Business.Entities;
using DesafioTJRJ.Data.Mappings;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace DesafioTJRJ.Data.Context
{
    public class LibraryContext : DbContext
    {
        public LibraryContext(DbContextOptions<LibraryContext> options)
           : base(options)
        {
        }

        public DbSet<Assunto> Assuntos { get; set; }
        public DbSet<Autor> Autores { get; set; }
        public DbSet<FormaCompra> FormasCompra { get; set; }
        public DbSet<Livro> Livros { get; set; }
        public DbSet<LivroAutor> LivroAutores { get; set; }
        public DbSet<LivroAssunto> LivroAssuntos { get; set; }
        public DbSet<LivroPrecoFormaCompra> LivroPrecosFormaCompra { get; set; }
        public DbSet<ViewLivroAutorAssunto> ViewLivroAutorAssunto { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new AutorMap());
            modelBuilder.ApplyConfiguration(new LivroMap());
            modelBuilder.ApplyConfiguration(new AssuntoMap());
            modelBuilder.ApplyConfiguration(new LivroAutorMap());
            modelBuilder.ApplyConfiguration(new LivroAssuntoMap());
            modelBuilder.ApplyConfiguration(new FormaCompraMap());
            modelBuilder.ApplyConfiguration(new LivroPrecoFormaCompraMap());
            modelBuilder.ApplyConfiguration(new ViewLivroAutorAssuntoMap());
        }
    }
}
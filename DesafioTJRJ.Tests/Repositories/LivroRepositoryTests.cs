using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Xunit;
using DesafioTJRJ.Business.Entities;
using DesafioTJRJ.Data.Context;
using DesafioTJRJ.Data.Repository;

namespace DesafioTJRJ.Tests.Repositories
{
    public class LivroRepositoryTests : IDisposable
    {
        private readonly LivroRepository _livroRepository;
        private readonly LibraryContext _context;

        public LivroRepositoryTests()
        {
            var options = new DbContextOptionsBuilder<LibraryContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            _context = new LibraryContext(options);
            _livroRepository = new LivroRepository(_context);

            _context.Database.EnsureCreated();
        }

        public void Dispose()
        {
            _context.Database.EnsureDeleted();
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnAllLivros()
        {
            // Arrange
            _context.Livros.AddRange(
                new Livro { CodL = 1, Titulo = "Livro 1", Editora = "Editora 1", Edicao = 1, AnoPublicacao = "2023" },
                new Livro { CodL = 2, Titulo = "Livro 2", Editora = "Editora 2", Edicao = 2, AnoPublicacao = "2024" }
            );
            await _context.SaveChangesAsync();

            // Act
            var result = await _livroRepository.GetAllAsync();

            // Assert
            Assert.Equal(2, result.Count());
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnLivro_WhenLivroExists()
        {
            // Arrange
            var livro = new Livro { CodL = 1, Titulo = "Livro 1", Editora = "Editora 1", Edicao = 1, AnoPublicacao = "2023" };
            _context.Livros.Add(livro);
            await _context.SaveChangesAsync();

            // Act
            var result = await _livroRepository.GetByIdAsync(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, result.CodL);
            Assert.Equal("Livro 1", result.Titulo);
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnNull_WhenLivroDoesNotExist()
        {
            // Act
            var result = await _livroRepository.GetByIdAsync(1);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task AddAsync_ShouldAddLivro()
        {
            // Arrange
            var livro = new Livro { CodL = 1, Titulo = "Livro Teste", Editora = "Editora Teste", Edicao = 1, AnoPublicacao = "2023" };

            // Act
            await _livroRepository.AddAsync(livro);

            // Assert
            var result = await _context.Livros.FindAsync(1);
            Assert.NotNull(result);
            Assert.Equal("Livro Teste", result.Titulo);
        }

        [Fact]
        public async Task UpdateAsync_ShouldUpdateLivro()
        {
            // Arrange
            var livro = new Livro { CodL = 1, Titulo = "Livro Original", Editora = "Editora Original", Edicao = 1, AnoPublicacao = "2023" };
            _context.Livros.Add(livro);
            await _context.SaveChangesAsync();

            livro.Titulo = "Livro Atualizado";

            // Act
            await _livroRepository.UpdateAsync(livro);

            // Assert
            var result = await _context.Livros.FindAsync(1);
            Assert.NotNull(result);
            Assert.Equal("Livro Atualizado", result.Titulo);
        }

        [Fact]
        public async Task DeleteAsync_ShouldRemoveLivro()
        {
            // Arrange
            var livro = new Livro { CodL = 1, Titulo = "Livro Teste", Editora = "Editora Teste", Edicao = 1, AnoPublicacao = "2023" };
            _context.Livros.Add(livro);
            await _context.SaveChangesAsync();

            // Act
            await _livroRepository.DeleteAsync(1);

            // Assert
            var result = await _context.Livros.FindAsync(1);
            Assert.Null(result);
        }

        [Fact]
        public async Task GetLivroCompleto_ShouldReturnLivroWithRelatedEntities_WhenLivroExists()
        {
            // Arrange
            var livro = new Livro
            {
                CodL = 1,
                Titulo = "Livro Completo",
                Editora = "Editora Completa",
                Edicao = 1,
                AnoPublicacao = "2023",
                LivroAutores = new List<LivroAutor>
                {
                    new LivroAutor { Autor = new Autor { CodAu = 1, Nome = "Autor 1" } }
                },
                LivroAssuntos = new List<LivroAssunto>
                {
                    new LivroAssunto { Assunto = new Assunto { CodAs = 1, Descricao = "Assunto 1" } }
                },
                LivroPrecosFormaCompra = new List<LivroPrecoFormaCompra>
                {
                    new LivroPrecoFormaCompra { FormaCompra = new FormaCompra { CodFormaCompra = 1, Descricao = "Forma de Compra 1" } }
                }
            };
            _context.Livros.Add(livro);
            await _context.SaveChangesAsync();

            // Act
            var result = await _livroRepository.GetLivroCompleto(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, result.CodL);
            Assert.Equal("Livro Completo", result.Titulo);
            Assert.Contains(result.LivroAutores, la => la.Autor.Nome == "Autor 1");
            Assert.Contains(result.LivroAssuntos, la => la.Assunto.Descricao == "Assunto 1");
            Assert.Contains(result.LivroPrecosFormaCompra, lp => lp.FormaCompra.Descricao == "Forma de Compra 1");
        }
    }
}

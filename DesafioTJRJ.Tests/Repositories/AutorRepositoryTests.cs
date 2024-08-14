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
    public class AutorRepositoryTests : IDisposable
    {
        private readonly AutorRepository _autorRepository;
        private readonly LibraryContext _context;

        public AutorRepositoryTests()
        {
            var options = new DbContextOptionsBuilder<LibraryContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            _context = new LibraryContext(options);
            _autorRepository = new AutorRepository(_context);

            _context.Database.EnsureCreated();
        }

        public void Dispose()
        {
            _context.Database.EnsureDeleted();
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnAllAutores()
        {
            // Arrange
            _context.Autores.AddRange(
                new Autor { CodAu = 1, Nome = "Autor 1" },
                new Autor { CodAu = 2, Nome = "Autor 2" }
            );
            await _context.SaveChangesAsync();

            // Act
            var result = await _autorRepository.GetAllAsync();

            // Assert
            Assert.Equal(2, result.Count());
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnAutor_WhenAutorExists()
        {
            // Arrange
            var autor = new Autor { CodAu = 1, Nome = "Autor 1" };
            _context.Autores.Add(autor);
            await _context.SaveChangesAsync();

            // Act
            var result = await _autorRepository.GetByIdAsync(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, result.CodAu);
            Assert.Equal("Autor 1", result.Nome);
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnNull_WhenAutorDoesNotExist()
        {
            // Act
            var result = await _autorRepository.GetByIdAsync(1);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task AddAsync_ShouldAddAutor()
        {
            // Arrange
            var autor = new Autor { CodAu = 1, Nome = "Autor Teste" };

            // Act
            await _autorRepository.AddAsync(autor);

            // Assert
            var result = await _context.Autores.FindAsync(1);
            Assert.NotNull(result);
            Assert.Equal("Autor Teste", result.Nome);
        }

        [Fact]
        public async Task UpdateAsync_ShouldUpdateAutor()
        {
            // Arrange
            var autor = new Autor { CodAu = 1, Nome = "Autor Original" };
            _context.Autores.Add(autor);
            await _context.SaveChangesAsync();

            autor.Nome = "Autor Atualizado";

            // Act
            await _autorRepository.UpdateAsync(autor);

            // Assert
            var result = await _context.Autores.FindAsync(1);
            Assert.NotNull(result);
            Assert.Equal("Autor Atualizado", result.Nome);
        }

        [Fact]
        public async Task DeleteAsync_ShouldRemoveAutor()
        {
            // Arrange
            var autor = new Autor { CodAu = 1, Nome = "Autor Teste" };
            _context.Autores.Add(autor);
            await _context.SaveChangesAsync();

            // Act
            await _autorRepository.DeleteAsync(1);

            // Assert
            var result = await _context.Autores.FindAsync(1);
            Assert.Null(result);
        }
    }
}
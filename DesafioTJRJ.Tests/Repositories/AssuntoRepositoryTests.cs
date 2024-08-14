using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Xunit;
using DesafioTJRJ.Business.Entities;
using DesafioTJRJ.Data.Context;
using DesafioTJRJ.Data.Repository;
using DesafioTJRJ.Data.Mappings;

namespace DesafioTJRJ.Tests.Repositories
{
    public class AssuntoRepositoryTests : IDisposable
    {
        private readonly AssuntoRepository _assuntoRepository;
        private readonly LibraryContext _context;

        public AssuntoRepositoryTests()
        {
            var options = new DbContextOptionsBuilder<LibraryContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            _context = new LibraryContext(options);
            _assuntoRepository = new AssuntoRepository(_context);

            _context.Database.EnsureCreated();
        }

        public void Dispose()
        {
            _context.Database.EnsureDeleted();
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnAllAssuntos()
        {
            // Arrange
            _context.Assuntos.AddRange(
                new Assunto { CodAs = 1, Descricao = "Assunto 1" },
                new Assunto { CodAs = 2, Descricao = "Assunto 2" }
            );
            await _context.SaveChangesAsync();

            // Act
            var result = await _assuntoRepository.GetAllAsync();

            // Assert
            Assert.Equal(2, result.Count());
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnAssunto_WhenAssuntoExists()
        {
            // Arrange
            var assunto = new Assunto { CodAs = 1, Descricao = "Assunto 1" };
            _context.Assuntos.Add(assunto);
            await _context.SaveChangesAsync();

            // Act
            var result = await _assuntoRepository.GetByIdAsync(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, result.CodAs);
            Assert.Equal("Assunto 1", result.Descricao);
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnNull_WhenAssuntoDoesNotExist()
        {
            // Act
            var result = await _assuntoRepository.GetByIdAsync(1);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task AddAsync_ShouldAddAssunto()
        {
            // Arrange
            var assunto = new Assunto { CodAs = 1, Descricao = "Assunto Teste" };

            // Act
            await _assuntoRepository.AddAsync(assunto);

            // Assert
            var result = await _context.Assuntos.FindAsync(1);
            Assert.NotNull(result);
            Assert.Equal("Assunto Teste", result.Descricao);
        }

        [Fact]
        public async Task UpdateAsync_ShouldUpdateAssunto()
        {
            // Arrange
            var assunto = new Assunto { CodAs = 1, Descricao = "Assunto Original" };
            _context.Assuntos.Add(assunto);
            await _context.SaveChangesAsync();

            assunto.Descricao = "Assunto Atualizado";

            // Act
            await _assuntoRepository.UpdateAsync(assunto);

            // Assert
            var result = await _context.Assuntos.FindAsync(1);
            Assert.NotNull(result);
            Assert.Equal("Assunto Atualizado", result.Descricao);
        }

        [Fact]
        public async Task DeleteAsync_ShouldRemoveAssunto()
        {
            // Arrange
            var assunto = new Assunto { CodAs = 1, Descricao = "Assunto Teste" };
            _context.Assuntos.Add(assunto);
            await _context.SaveChangesAsync();

            // Act
            await _assuntoRepository.DeleteAsync(1);

            // Assert
            var result = await _context.Assuntos.FindAsync(1);
            Assert.Null(result);
        }
    }
}

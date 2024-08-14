using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Xunit;
using Moq;
using DesafioTJRJ.Business.Interfaces.Repository;
using DesafioTJRJ.Business.Services;
using DesafioTJRJ.Business.Entities;

namespace DesafioTJRJ.Tests.Services
{
    public class AutorServiceTests
    {
        private readonly Mock<IAutorRepository> _autorRepositoryMock;
        private readonly AutorService _autorService;

        public AutorServiceTests()
        {
            _autorRepositoryMock = new Mock<IAutorRepository>();
            _autorService = new AutorService(_autorRepositoryMock.Object);
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnAllAutores()
        {
            // Arrange
            var autores = new List<Autor>
            {
                new Autor { CodAu = 1, Nome = "Autor 1" },
                new Autor { CodAu = 2, Nome = "Autor 2" }
            };
            _autorRepositoryMock.Setup(repo => repo.GetAllAsync(It.IsAny<Expression<Func<Autor, object>>[]>()))
                                                   .ReturnsAsync(autores.AsQueryable());

            // Act
            var result = await _autorService.GetAllAsync();

            // Assert
            Assert.Equal(2, result.Count());
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnAutor_WhenAutorExists()
        {
            // Arrange
            var autor = new Autor { CodAu = 1, Nome = "Autor 1" };
            _autorRepositoryMock.Setup(repo => repo.GetByIdAsync(1, It.IsAny<Expression<Func<Autor, object>>[]>()))
                                                   .ReturnsAsync(autor);

            // Act
            var result = await _autorService.GetByIdAsync(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, result.CodAu);
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnNull_WhenAutorDoesNotExist()
        {
            // Arrange
            _autorRepositoryMock.Setup(repo => repo.GetByIdAsync(1, It.IsAny<Expression<Func<Autor, object>>[]>()))
                                .ReturnsAsync((Autor)null);

            // Act
            var result = await _autorService.GetByIdAsync(1);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task AddAsync_ShouldAddAutor()
        {
            // Arrange
            var autores = new List<Autor>();
            var autor = new Autor { CodAu = 1, Nome = "Autor Teste" };

            _autorRepositoryMock.Setup(repo => repo.AddAsync(It.IsAny<Autor>()))
                                .Callback<Autor>(a => autores.Add(a))
                                .Returns(Task.CompletedTask);

            // Act
            await _autorService.AddAsync(autor);

            // Assert
            Assert.Single(autores);
            Assert.Equal(autor, autores.First());
            Assert.Equal(1, autores.First().CodAu);
            Assert.Equal("Autor Teste", autores.First().Nome);
        }

        [Fact]
        public async Task UpdateAsync_ShouldUpdateAutor()
        {
            // Arrange
            var autores = new List<Autor>
            {
                new Autor { CodAu = 1, Nome = "Autor Original" }
            };
            var autorAtualizado = new Autor { CodAu = 1, Nome = "Autor Atualizado" };

            _autorRepositoryMock.Setup(repo => repo.UpdateAsync(It.IsAny<Autor>()))
                                .Callback<Autor>(a =>
                                {
                                    var autor = autores.FirstOrDefault(x => x.CodAu == a.CodAu);
                                    if (autor != null)
                                    {
                                        autor.Nome = a.Nome;
                                    }
                                })
                                .Returns(Task.CompletedTask);

            // Act
            await _autorService.UpdateAsync(autorAtualizado);

            // Assert
            Assert.Single(autores);
            Assert.Equal("Autor Atualizado", autores.First().Nome);
        }

        [Fact]
        public async Task DeleteAsync_ShouldRemoveAutor_WhenAutorExists()
        {
            // Arrange
            var autores = new List<Autor>
            {
                new Autor { CodAu = 1, Nome = "Autor Teste" }
            };
            var autorId = 1;

            _autorRepositoryMock.Setup(repo => repo.DeleteAsync(It.IsAny<int>()))
                                .Callback<int>(id =>
                                {
                                    var autor = autores.FirstOrDefault(a => a.CodAu == id);
                                    if (autor != null)
                                    {
                                        autores.Remove(autor);
                                    }
                                })
                                .Returns(Task.CompletedTask);

            // Act
            await _autorService.DeleteAsync(autorId);

            // Assert
            Assert.Empty(autores);
        }
    }
}
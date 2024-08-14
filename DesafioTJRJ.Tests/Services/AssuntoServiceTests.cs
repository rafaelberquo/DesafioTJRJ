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
    public class AssuntoServiceTests
    {
        private readonly Mock<IAssuntoRepository> _assuntoRepositoryMock;
        private readonly AssuntoService _assuntoService;

        public AssuntoServiceTests()
        {
            _assuntoRepositoryMock = new Mock<IAssuntoRepository>();
            _assuntoService = new AssuntoService(_assuntoRepositoryMock.Object);
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnAllAssuntos()
        {
            // Arrange
            var assuntos = new List<Assunto>
            {
                new Assunto { CodAs = 1, Descricao = "Assunto 1" },
                new Assunto { CodAs = 2, Descricao = "Assunto 2" }
            };
            _assuntoRepositoryMock.Setup(repo => repo.GetAllAsync(It.IsAny<Expression<Func<Assunto, object>>[]>()))
                                  .ReturnsAsync(assuntos.AsQueryable());

            // Act
            var result = await _assuntoService.GetAllAsync();

            // Assert
            Assert.Equal(2, result.Count());
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnAssunto_WhenAssuntoExists()
        {
            // Arrange
            var assunto = new Assunto { CodAs = 1, Descricao = "Assunto 1" };
            _assuntoRepositoryMock.Setup(repo => repo.GetByIdAsync(1, It.IsAny<Expression<Func<Assunto, object>>[]>()))
                                  .ReturnsAsync(assunto);

            // Act
            var result = await _assuntoService.GetByIdAsync(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, result.CodAs);
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnNull_WhenAssuntoDoesNotExist()
        {
            // Arrange
            _assuntoRepositoryMock.Setup(repo => repo.GetByIdAsync(1, It.IsAny<Expression<Func<Assunto, object>>[]>()))
                                  .ReturnsAsync((Assunto)null);

            // Act
            var result = await _assuntoService.GetByIdAsync(1);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task AddAsync_ShouldAddAssunto()
        {
            // Arrange
            var assuntos = new List<Assunto>();
            var assunto = new Assunto { CodAs = 1, Descricao = "Assunto Teste" };

            _assuntoRepositoryMock.Setup(repo => repo.AddAsync(It.IsAny<Assunto>()))
                                  .Callback<Assunto>(a => assuntos.Add(a))
                                  .Returns(Task.CompletedTask);

            // Act
            await _assuntoService.AddAsync(assunto);

            // Assert
            Assert.Single(assuntos);
            Assert.Equal(assunto, assuntos.First());
            Assert.Equal(1, assuntos.First().CodAs);
            Assert.Equal("Assunto Teste", assuntos.First().Descricao);
        }

        [Fact]
        public async Task UpdateAsync_ShouldUpdateAssunto()
        {
            // Arrange
            var assuntos = new List<Assunto>
            {
                new Assunto { CodAs = 1, Descricao = "Assunto Original" }
            };
            var assuntoAtualizado = new Assunto { CodAs = 1, Descricao = "Assunto Atualizado" };

            _assuntoRepositoryMock.Setup(repo => repo.UpdateAsync(It.IsAny<Assunto>()))
                                  .Callback<Assunto>(a =>
                                  {
                                      var assunto = assuntos.FirstOrDefault(x => x.CodAs == a.CodAs);
                                      if (assunto != null)
                                      {
                                          assunto.Descricao = a.Descricao;
                                      }
                                  })
                                  .Returns(Task.CompletedTask);

            // Act
            await _assuntoService.UpdateAsync(assuntoAtualizado);

            // Assert
            Assert.Single(assuntos);
            Assert.Equal("Assunto Atualizado", assuntos.First().Descricao);
        }

        [Fact]
        public async Task DeleteAsync_ShouldRemoveAssunto_WhenAssuntoExists()
        {
            // Arrange
            var assuntos = new List<Assunto>
            {
                new Assunto { CodAs = 1, Descricao = "Assunto Teste" }
            };
            var assuntoId = 1;

            _assuntoRepositoryMock.Setup(repo => repo.DeleteAsync(It.IsAny<int>()))
                                  .Callback<int>(id =>
                                  {
                                      var assunto = assuntos.FirstOrDefault(a => a.CodAs == id);
                                      if (assunto != null)
                                      {
                                          assuntos.Remove(assunto);
                                      }
                                  })
                                  .Returns(Task.CompletedTask);

            // Act
            await _assuntoService.DeleteAsync(assuntoId);

            // Assert
            Assert.Empty(assuntos);
        }
    }
}
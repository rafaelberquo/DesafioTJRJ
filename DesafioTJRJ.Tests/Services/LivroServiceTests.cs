using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Moq;
using Xunit;
using DesafioTJRJ.Business.Entities;
using DesafioTJRJ.Business.Interfaces.Repository;
using DesafioTJRJ.Business.Services;
using System.Linq.Expressions;
using DesafioTJRJ.Business.Interfaces.Services;

namespace DesafioTJRJ.Tests.Services
{
    public class LivroServiceTests
    {
        private readonly Mock<ILivroRepository> _livroRepositoryMock;
        private readonly LivroService _livroService;
        private readonly List<Livro> _livros;

        public LivroServiceTests()
        {
            _livroRepositoryMock = new Mock<ILivroRepository>();
            _livroService = new LivroService(_livroRepositoryMock.Object);
            _livros = new List<Livro>();
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnAllLivros()
        {
            // Arrange
            var livros = new List<Livro>
            {
                new Livro
                {
                    CodL = 1,
                    Titulo = "Livro 1",
                    Editora = "Editora 1",
                    Edicao = 1,
                    AnoPublicacao = "2023"
                },
                new Livro
                {
                    CodL = 2,
                    Titulo = "Livro 2",
                    Editora = "Editora 2",
                    Edicao = 2,
                    AnoPublicacao = "2024"
                }
            };

            _livroRepositoryMock.Setup(repo => repo.GetAllAsync(It.IsAny<Expression<Func<Livro, object>>[]>()))
                                .ReturnsAsync(livros.AsQueryable());

            // Act
            var result = await _livroService.GetAllAsync();

            // Assert
            Assert.Equal(2, result.Count());
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnLivro_WhenLivroExists()
        {
            // Arrange
            var livro = new Livro
            {
                CodL = 1,
                Titulo = "Livro 1",
                Editora = "Editora 1",
                Edicao = 1,
                AnoPublicacao = "2023",
                LivroAutores = new List<LivroAutor>
                {
                    new LivroAutor { CodL = 1, CodAu = 1, Autor = new Autor { CodAu = 1, Nome = "Autor 1" } },
                    new LivroAutor { CodL = 1, CodAu = 2, Autor = new Autor { CodAu = 2, Nome = "Autor 2" } }
                },
                LivroAssuntos = new List<LivroAssunto>
                {
                    new LivroAssunto { CodL = 1, CodAs = 1, Assunto = new Assunto { CodAs = 1, Descricao = "Assunto 1" } },
                    new LivroAssunto { CodL = 1, CodAs = 2, Assunto = new Assunto { CodAs = 2, Descricao = "Assunto 2" } }
                },
                LivroPrecosFormaCompra = new List<LivroPrecoFormaCompra>
                {
                    new LivroPrecoFormaCompra { CodL = 1, CodFormaCompra = 1, Preco = 29.99M, FormaCompra = new FormaCompra { CodFormaCompra = 1, Descricao = "Forma de Compra 1" } },
                    new LivroPrecoFormaCompra { CodL = 1, CodFormaCompra = 2, Preco = 39.99M, FormaCompra = new FormaCompra { CodFormaCompra = 2, Descricao = "Forma de Compra 2" } }
                }
            };

            _livroRepositoryMock.Setup(repo => repo.GetLivroCompleto(1))
                                .ReturnsAsync(livro);

            // Act
            var result = await _livroService.GetByIdAsync(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, result.CodL);
            Assert.Equal("Livro 1", result.Titulo);
            Assert.Equal("Editora 1", result.Editora);
            Assert.Equal(1, result.Edicao);
            Assert.Equal("2023", result.AnoPublicacao);

            Assert.NotNull(result.LivroAutores);
            Assert.Equal(2, result.LivroAutores.Count);
            Assert.Equal(1, result.LivroAutores.First().CodAu);
            Assert.Equal("Autor 1", result.LivroAutores.First().Autor.Nome);
            Assert.Equal(2, result.LivroAutores.Last().CodAu);
            Assert.Equal("Autor 2", result.LivroAutores.Last().Autor.Nome);

            Assert.NotNull(result.LivroAssuntos);
            Assert.Equal(2, result.LivroAssuntos.Count);
            Assert.Equal(1, result.LivroAssuntos.First().CodAs);
            Assert.Equal("Assunto 1", result.LivroAssuntos.First().Assunto.Descricao);
            Assert.Equal(2, result.LivroAssuntos.Last().CodAs);
            Assert.Equal("Assunto 2", result.LivroAssuntos.Last().Assunto.Descricao);

            Assert.NotNull(result.LivroPrecosFormaCompra);
            Assert.Equal(2, result.LivroPrecosFormaCompra.Count);
            Assert.Equal(1, result.LivroPrecosFormaCompra.First().CodFormaCompra);
            Assert.Equal(29.99M, result.LivroPrecosFormaCompra.First().Preco);
            Assert.Equal("Forma de Compra 1", result.LivroPrecosFormaCompra.First().FormaCompra.Descricao);
            Assert.Equal(2, result.LivroPrecosFormaCompra.Last().CodFormaCompra);
            Assert.Equal(39.99M, result.LivroPrecosFormaCompra.Last().Preco);
            Assert.Equal("Forma de Compra 2", result.LivroPrecosFormaCompra.Last().FormaCompra.Descricao);
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnNull_WhenLivroDoesNotExist()
        {
            // Arrange
            _livroRepositoryMock.Setup(repo => repo.GetLivroCompleto(1))
                                .ReturnsAsync((Livro)null);

            // Act
            var result = await _livroService.GetByIdAsync(1);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task AddAsync_ShouldAddLivro()
        {
            // Arrange
            var livro = new Livro
            {
                CodL = 1,
                Titulo = "Livro Teste",
                Editora = "Editora Teste",
                Edicao = 1,
                AnoPublicacao = "2023",
                LivroAutores = new List<LivroAutor>
                {
                    new LivroAutor { CodL = 1, CodAu = 1, Autor = new Autor { CodAu = 1, Nome = "Autor Teste 1" } }
                },
                LivroAssuntos = new List<LivroAssunto>
                {
                    new LivroAssunto { CodL = 1, CodAs = 1, Assunto = new Assunto { CodAs = 1, Descricao = "Assunto Teste 1" } }
                },
                LivroPrecosFormaCompra = new List<LivroPrecoFormaCompra>
                {
                    new LivroPrecoFormaCompra { CodL = 1, CodFormaCompra = 1, Preco = 29.99M, FormaCompra = new FormaCompra { CodFormaCompra = 1, Descricao = "Forma de Compra Teste 1" } }
                }
            };

            _livroRepositoryMock.Setup(repo => repo.AddAsync(It.IsAny<Livro>()))
                                .Callback<Livro>(livroAdded => _livros.Add(livroAdded))
                                .Returns(Task.CompletedTask);

            // Act
            await _livroService.AddAsync(livro);

            // Assert
            Assert.Single(_livros);
            var addedLivro = _livros.First();
            Assert.Equal(livro.CodL, addedLivro.CodL);
            Assert.Equal(livro.Titulo, addedLivro.Titulo);
            Assert.Equal(livro.Editora, addedLivro.Editora);
            Assert.Equal(livro.Edicao, addedLivro.Edicao);
            Assert.Equal(livro.AnoPublicacao, addedLivro.AnoPublicacao);

            Assert.NotNull(addedLivro.LivroAutores);
            Assert.Single(addedLivro.LivroAutores);
            Assert.Equal(1, addedLivro.LivroAutores.First().CodAu);
            Assert.Equal("Autor Teste 1", addedLivro.LivroAutores.First().Autor.Nome);

            Assert.NotNull(addedLivro.LivroAssuntos);
            Assert.Single(addedLivro.LivroAssuntos);
            Assert.Equal(1, addedLivro.LivroAssuntos.First().CodAs);
            Assert.Equal("Assunto Teste 1", addedLivro.LivroAssuntos.First().Assunto.Descricao);

            Assert.NotNull(addedLivro.LivroPrecosFormaCompra);
            Assert.Single(addedLivro.LivroPrecosFormaCompra);
            Assert.Equal(1, addedLivro.LivroPrecosFormaCompra.First().CodFormaCompra);
            Assert.Equal(29.99M, addedLivro.LivroPrecosFormaCompra.First().Preco);
            Assert.Equal("Forma de Compra Teste 1", addedLivro.LivroPrecosFormaCompra.First().FormaCompra.Descricao);
        }

        [Fact]
        public async Task UpdateAsync_ShouldUpdateLivro()
        {
            // Arrange
            _livros.Add(new Livro
            {
                CodL = 1,
                Titulo = "Livro Original",
                Editora = "Editora Original",
                Edicao = 1,
                AnoPublicacao = "2022",
                LivroAutores = new List<LivroAutor>
                {
                    new LivroAutor { CodL = 1, CodAu = 1, Autor = new Autor { CodAu = 1, Nome = "Autor Original" } }
                },
                LivroAssuntos = new List<LivroAssunto>
                {
                    new LivroAssunto { CodL = 1, CodAs = 1, Assunto = new Assunto { CodAs = 1, Descricao = "Assunto Original" } }
                },
                LivroPrecosFormaCompra = new List<LivroPrecoFormaCompra>
                {
                    new LivroPrecoFormaCompra { CodL = 1, CodFormaCompra = 1, Preco = 19.99M, FormaCompra = new FormaCompra { CodFormaCompra = 1, Descricao = "Forma de Compra Original" } }
                }
            });

            var livroAtualizado = new Livro
            {
                CodL = 1,
                Titulo = "Livro Atualizado",
                Editora = "Editora Atualizada",
                Edicao = 2,
                AnoPublicacao = "2023",
                LivroAutores = new List<LivroAutor>
                {
                    new LivroAutor { CodL = 1, CodAu = 2, Autor = new Autor { CodAu = 2, Nome = "Autor Atualizado" } }
                },
                LivroAssuntos = new List<LivroAssunto>
                {
                    new LivroAssunto { CodL = 1, CodAs = 2, Assunto = new Assunto { CodAs = 2, Descricao = "Assunto Atualizado" } }
                },
                LivroPrecosFormaCompra = new List<LivroPrecoFormaCompra>
                {
                    new LivroPrecoFormaCompra { CodL = 1, CodFormaCompra = 2, Preco = 39.99M, FormaCompra = new FormaCompra { CodFormaCompra = 2, Descricao = "Forma de Compra Atualizada" } }
                }
            };

            _livroRepositoryMock.Setup(repo => repo.UpdateAsync(It.IsAny<Livro>()))
                                .Callback<Livro>(livroUpdated =>
                                {
                                    var existingLivro = _livros.First(l => l.CodL == livroUpdated.CodL);
                                    _livros.Remove(existingLivro);
                                    _livros.Add(livroUpdated);
                                })
                                .Returns(Task.CompletedTask);

            // Act
            await _livroService.UpdateAsync(livroAtualizado);

            // Assert
            var updatedLivro = _livros.First();
            Assert.Equal(livroAtualizado.CodL, updatedLivro.CodL);
            Assert.Equal(livroAtualizado.Titulo, updatedLivro.Titulo);
            Assert.Equal(livroAtualizado.Editora, updatedLivro.Editora);
            Assert.Equal(livroAtualizado.Edicao, updatedLivro.Edicao);
            Assert.Equal(livroAtualizado.AnoPublicacao, updatedLivro.AnoPublicacao);

            Assert.NotNull(updatedLivro.LivroAutores);
            Assert.Single(updatedLivro.LivroAutores);
            Assert.Equal(2, updatedLivro.LivroAutores.First().CodAu);
            Assert.Equal("Autor Atualizado", updatedLivro.LivroAutores.First().Autor.Nome);

            Assert.NotNull(updatedLivro.LivroAssuntos);
            Assert.Single(updatedLivro.LivroAssuntos);
            Assert.Equal(2, updatedLivro.LivroAssuntos.First().CodAs);
            Assert.Equal("Assunto Atualizado", updatedLivro.LivroAssuntos.First().Assunto.Descricao);

            Assert.NotNull(updatedLivro.LivroPrecosFormaCompra);
            Assert.Single(updatedLivro.LivroPrecosFormaCompra);
            Assert.Equal(2, updatedLivro.LivroPrecosFormaCompra.First().CodFormaCompra);
            Assert.Equal(39.99M, updatedLivro.LivroPrecosFormaCompra.First().Preco);
            Assert.Equal("Forma de Compra Atualizada", updatedLivro.LivroPrecosFormaCompra.First().FormaCompra.Descricao);
        }

        [Fact]
        public async Task DeleteAsync_ShouldRemoveLivro_WhenLivroExists()
        {
            // Arrange
            _livros.Add(new Livro
            {
                CodL = 1,
                Titulo = "Livro Teste",
                Editora = "Editora Teste",
                Edicao = 1,
                AnoPublicacao = "2023"
            });
            var livroId = 1;

            _livroRepositoryMock.Setup(repo => repo.DeleteAsync(It.IsAny<int>()))
                                 .Callback<int>(id =>
                                 {
                                     var livro = _livros.FirstOrDefault(a => a.CodL == id);
                                     if (livro != null)
                                     {
                                         _livros.Remove(livro);
                                     }
                                 })
                                 .Returns(Task.CompletedTask);

            // Act
            await _livroService.DeleteAsync(livroId);

            // Assert
            Assert.Empty(_livros);
        }
    }
}
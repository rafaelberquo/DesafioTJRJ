using DesafioTJRJ.Business.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DesafioTJRJ.Business.Interfaces.Services
{
    public interface ILivroService : IBaseService<Livro>
    {
        Task<Livro> GetByIdAsync(int id);
    }
}
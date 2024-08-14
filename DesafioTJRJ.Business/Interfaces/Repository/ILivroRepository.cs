using DesafioTJRJ.Business.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DesafioTJRJ.Business.Interfaces.Repository
{
    public interface ILivroRepository : IBaseRepository<Livro>
    {
        Task<Livro> GetLivroCompleto(int id);
    }
}

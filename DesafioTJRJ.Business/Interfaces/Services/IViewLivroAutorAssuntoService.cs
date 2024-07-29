using DesafioTJRJ.Business.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesafioTJRJ.Business.Interfaces.Services
{
    public interface IViewLivroAutorAssuntoService : IBaseService<ViewLivroAutorAssunto>
    {
        Task<List<ViewLivroAutorAssunto>> GetAllPorAutor();

    }
}
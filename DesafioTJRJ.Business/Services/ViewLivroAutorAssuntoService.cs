using DesafioTJRJ.Business.Entities;
using DesafioTJRJ.Business.Interfaces.Repository;
using DesafioTJRJ.Business.Interfaces.Services;
using DesafioTJRJ.Business.Services.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesafioTJRJ.Business.Services
{
    public class ViewLivroAutorAssuntoService : BaseService<ViewLivroAutorAssunto>, IViewLivroAutorAssuntoService
    {
        private readonly IViewLivroAutorAssuntoRepository _viewRepository;

        public ViewLivroAutorAssuntoService(IViewLivroAutorAssuntoRepository repository) : base(repository)
        {
            _viewRepository = repository;
        }

        public async Task<List<ViewLivroAutorAssunto>> GetAllPorAutor()
        {
            var view = await GetAllAsync();
            return view.OrderBy(v => v.NomeAutor)
                       .ThenBy(v => v.TituloLivro)
                       .ToList();
        }
    }
}
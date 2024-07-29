using DesafioTJRJ.Business.Entities;
using DesafioTJRJ.Business.Interfaces.Repository;
using DesafioTJRJ.Data.BaseRepository.Base;
using DesafioTJRJ.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesafioTJRJ.Data.Repository
{
    public class ViewLivroAutorAssuntoRepository : BaseRepository<ViewLivroAutorAssunto>, IViewLivroAutorAssuntoRepository
    {
        public ViewLivroAutorAssuntoRepository(LibraryContext context) : base(context) { }
    }
}
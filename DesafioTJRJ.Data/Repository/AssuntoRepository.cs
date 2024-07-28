using DesafioTJRJ.Business.Entities;
using DesafioTJRJ.Data.BaseRepository.Base;
using DesafioTJRJ.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesafioTJRJ.Data.BaseRepository
{
    public class AssuntoRepository : BaseRepository<Assunto>
    {
        public AssuntoRepository(LibraryContext context) : base(context) { }
    }
}
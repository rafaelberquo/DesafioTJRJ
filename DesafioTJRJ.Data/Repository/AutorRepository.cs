using DesafioTJRJ.Business.Entities;
using DesafioTJRJ.Data.Context;
using DesafioTJRJ.Data.BaseRepository.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesafioTJRJ.Data.BaseRepository
{
    public class AutorRepository : BaseRepository<Autor>
    {
        public AutorRepository(LibraryContext context) : base(context) { }
    }
}
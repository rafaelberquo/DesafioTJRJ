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
    public class LivroRepository : BaseRepository<Livro>
    {
        public LivroRepository(LibraryContext context) : base(context) { }
    }
}
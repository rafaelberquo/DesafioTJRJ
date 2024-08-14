using DesafioTJRJ.Business.Entities;
using DesafioTJRJ.Data.Context;
using DesafioTJRJ.Data.BaseRepository.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DesafioTJRJ.Business.Interfaces.Repository;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace DesafioTJRJ.Data.Repository
{
    public class LivroRepository : BaseRepository<Livro>, ILivroRepository
    {
        public LivroRepository(LibraryContext context) : base(context) { }

        public async Task<Livro> GetLivroCompleto(int id)
        {
            return await GetAll().Include(l => l.LivroAutores).ThenInclude(la => la.Autor)  
                                 .Include(l => l.LivroAssuntos).ThenInclude(la => la.Assunto)
                                 .Include(l => l.LivroPrecosFormaCompra).ThenInclude(lp => lp.FormaCompra)
                                 .FirstOrDefaultAsync(l => l.CodL == id); 
        }
    }
}
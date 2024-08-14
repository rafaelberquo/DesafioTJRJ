using DesafioTJRJ.Business.Entities;
using DesafioTJRJ.Data.Context;
using DesafioTJRJ.Data.BaseRepository.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DesafioTJRJ.Business.Interfaces.Repository;

namespace DesafioTJRJ.Data.Repository
{
    public class FormaCompraRepository : BaseRepository<FormaCompra>, IFormaCompraRepository
    {
        public FormaCompraRepository(LibraryContext context) : base(context) { }
    }
}
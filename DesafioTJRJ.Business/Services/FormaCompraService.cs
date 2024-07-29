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
    public class FormaCompraService : BaseService<FormaCompra>, IFormaCompraService
    {
        private readonly IFormaCompraRepository _repository;

        public FormaCompraService(IFormaCompraRepository repository) : base(repository)
        {
            _repository = repository;
        }
    }
}
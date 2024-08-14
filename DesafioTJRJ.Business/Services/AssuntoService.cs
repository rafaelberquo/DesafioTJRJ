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
    public class AssuntoService : BaseService<Assunto>, IAssuntoService
    {
        private readonly IAssuntoRepository _repository;

        public AssuntoService(IAssuntoRepository repository) : base(repository)
        {
            _repository = repository;
        }

    }
}
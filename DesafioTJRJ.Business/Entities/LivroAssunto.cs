using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesafioTJRJ.Business.Entities
{
    public class LivroAssunto
    {
        public int CodL { get; set; }
        public int CodAs { get; set; }

        public virtual Livro Livro { get; set; }
        public virtual Assunto Assunto { get; set; }
    }
}
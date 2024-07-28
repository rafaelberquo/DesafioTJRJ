using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesafioTJRJ.Business.Entities
{
    public class LivroAutor
    {
        public int LivroId { get; set; }
        public int AutorId { get; set; }

        public virtual Livro Livro { get; set; }
        public virtual Autor Autor { get; set; }
    }
}
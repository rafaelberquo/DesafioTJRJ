using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesafioTJRJ.Business.Entities
{
    public class Autor
    {
        public int CodAu { get; set; }
        public string Nome { get; set; }

        public virtual ICollection<LivroAutor> LivroAutores { get; set; } = new List<LivroAutor>();
    }
}
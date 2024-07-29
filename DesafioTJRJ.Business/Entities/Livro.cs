using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesafioTJRJ.Business.Entities
{
    public class Livro
    {
        public int CodL { get; set; }
        public string Titulo { get; set; }
        public string Editora { get; set; }
        public int Edicao { get; set; }
        public string AnoPublicacao { get; set; }

        public virtual ICollection<LivroAutor> LivroAutores { get; set; } = new List<LivroAutor>();
        public virtual ICollection<LivroAssunto> LivroAssuntos { get; set; } = new List<LivroAssunto>();
        public virtual ICollection<LivroPrecoFormaCompra> LivroPrecosFormaCompra { get; set; } = new List<LivroPrecoFormaCompra>();
    }
}

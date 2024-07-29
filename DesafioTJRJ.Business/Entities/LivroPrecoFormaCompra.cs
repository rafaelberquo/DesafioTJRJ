using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesafioTJRJ.Business.Entities
{
    public class LivroPrecoFormaCompra
    {
        public int CodL { get; set; }
        public int CodFormaCompra { get; set; }
        public decimal Preco { get; set; }

        public virtual Livro Livro { get; set; }
        public virtual FormaCompra FormaCompra { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesafioTJRJ.Business.Entities
{
    public class FormaCompra
    {
        public int CodFormaCompra { get; set; }
        public string Descricao { get; set; }

        public virtual ICollection<LivroPrecoFormaCompra> LivroPrecosFormaCompra { get; set; } = new List<LivroPrecoFormaCompra>();
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesafioTJRJ.Business.Entities
{
    public class Assunto
    {
        public int CodAs { get; set; }
        public string Descricao { get; set; }

        public virtual ICollection<LivroAssunto> LivroAssuntos { get; set; } = new List<LivroAssunto>();
    }
}

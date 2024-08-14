using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesafioTJRJ.Business.Entities
{
    public class ViewLivroAutorAssunto
    {
        public int AutorId { get; set; }
        public string NomeAutor { get; set; }
        public int LivroId { get; set; }
        public string TituloLivro { get; set; }
        public string Editora { get; set; }
        public int Edicao { get; set; }
        public string AnoPublicacao { get; set; }
        public string DescricaoAssunto { get; set; }
    }
}

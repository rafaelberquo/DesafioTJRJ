using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DesafioTJRJ.UI.ViewModels
{
    public class LivroViewModel
    {
        public int CodL { get; set; }

        [DisplayName("Título")]
        [StringLength(40, ErrorMessage = "O título deve ter no máximo 40 caracteres.")]
        [Required(ErrorMessage = "O título é obrigatório.")]
        public string Titulo { get; set; }

        [DisplayName("Editora")]
        [StringLength(40, ErrorMessage = "A editora deve ter no máximo 40 caracteres.")]
        [Required(ErrorMessage = "A editora é obrigatória.")]
        public string Editora { get; set; }

        [DisplayName("Edição")]
        [Range(1, int.MaxValue, ErrorMessage = "A edição deve ser um valor positivo.")]
        [Required(ErrorMessage = "A edição é obrigatória.")]
        public int? Edicao { get; set; }

        [DisplayName("Ano de Publicação")]
        [RegularExpression(@"^\d{4}$", ErrorMessage = "O ano de publicação informado é inválido.")]
        [StringLength(4, ErrorMessage = "O ano de publicação deve ter 4 caracteres")]
        [Required(ErrorMessage = "O ano de publicação é obrigatório.")]
        public string AnoPublicacao { get; set; }

        public List<LivroAutorViewModel> Autores { get; set; } = new List<LivroAutorViewModel>();

        public List<LivroAssuntoViewModel> Assuntos { get; set; } = new List<LivroAssuntoViewModel>();

        public List<LivroPrecoFormaCompraViewModel> PrecosFormaCompra { get; set; } = new List<LivroPrecoFormaCompraViewModel>();
    }
}

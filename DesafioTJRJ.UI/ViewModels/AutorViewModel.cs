using System.ComponentModel.DataAnnotations;

namespace DesafioTJRJ.UI.ViewModels
{
    public class AutorViewModel
    {
        public int CodAu { get; set; }

        [Required(ErrorMessage = "O nome é obrigatório.")]
        [StringLength(40, ErrorMessage = "O nome deve ter no máximo 40 caracteres.")]
        public string Nome { get; set; }
    }
}

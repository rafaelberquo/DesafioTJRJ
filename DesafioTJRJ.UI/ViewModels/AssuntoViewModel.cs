using System.ComponentModel.DataAnnotations;

namespace DesafioTJRJ.UI.ViewModels
{
    public class AssuntoViewModel
    {
        public int CodAs { get; set; }

        [Required(ErrorMessage = "A descrição é obrigatória.")]
        [StringLength(20, ErrorMessage = "A descrição deve ter no máximo 20 caracteres.")]
        public string Descricao { get; set; }
    }
}

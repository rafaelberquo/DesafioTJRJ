using DesafioTJRJ.Business.Entities;

namespace DesafioTJRJ.UI.ViewModels
{
    public class LivroPrecoFormaCompraViewModel
    {
        public int CodL { get; set; }
        public int CodFormaCompra { get; set; }
        public decimal Preco { get; set; }
        public string FormaCompra { get; set; }

        public string PrecoFormatado => Preco.ToString("C");
    }
}

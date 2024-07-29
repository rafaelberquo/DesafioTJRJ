using DesafioTJRJ.Business.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DesafioTJRJ.UI.Controllers
{
    public class RelatoriosController : Controller
    {
        private readonly IViewLivroAutorAssuntoService _viewLivroAutorAssuntoService;

        public RelatoriosController(IViewLivroAutorAssuntoService viewLivroAutorAssuntoService)
        {
            _viewLivroAutorAssuntoService = viewLivroAutorAssuntoService;
        }

        public IActionResult Index()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> GerarRelatorio()
        {
            var livros = await _viewLivroAutorAssuntoService.GetAllPorAutor();

            using (var package = new OfficeOpenXml.ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("Relatório");

                // Cabeçalhos
                worksheet.Cells[1, 1].Value = "Autor";
                worksheet.Cells[1, 2].Value = "Título do Livro";
                worksheet.Cells[1, 3].Value = "Editora";
                worksheet.Cells[1, 4].Value = "Edição";
                worksheet.Cells[1, 5].Value = "Ano de Publicação";
                worksheet.Cells[1, 6].Value = "Assunto";

                var row = 2;

                foreach (var livro in livros)
                {
                    worksheet.Cells[row, 1].Value = livro.NomeAutor;
                    worksheet.Cells[row, 2].Value = livro.TituloLivro;
                    worksheet.Cells[row, 3].Value = livro.Editora;
                    worksheet.Cells[row, 4].Value = livro.Edicao;
                    worksheet.Cells[row, 5].Value = livro.AnoPublicacao;
                    worksheet.Cells[row, 6].Value = livro.DescricaoAssunto;
                    row++;
                }

                var stream = new MemoryStream();
                package.SaveAs(stream);
                stream.Position = 0;

                var fileName = "RelatorioLivros.xlsx";
                var contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

                return File(stream, contentType, fileName);
            }
        }
    }
}

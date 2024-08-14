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
            var autoresLivrosAssuntos = await _viewLivroAutorAssuntoService.GetAllPorAutor();

            using (var package = new OfficeOpenXml.ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("Relatório");
                worksheet.Cells[1, 1].Value = "Autor";
                worksheet.Cells[1, 2].Value = "Título do Livro";
                worksheet.Cells[1, 3].Value = "Editora";
                worksheet.Cells[1, 4].Value = "Edição";
                worksheet.Cells[1, 5].Value = "Ano de Publicação";
                worksheet.Cells[1, 6].Value = "Assunto";

                using (var headerCells = worksheet.Cells[1, 1, 1, 6])
                {
                    headerCells.Style.Font.Bold = true;
                    headerCells.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                }

                var linha = 2;
                var idAutorAtual = 0;
                var linhaInicialPorAutor = 0;
                foreach (var autLivAss in autoresLivrosAssuntos)
                {
                    if (autLivAss.AutorId != idAutorAtual)
                    {
                        if (idAutorAtual != 0) //evitar que a mesclagem ocorra na primeira iteração do loop
                        {
                            worksheet.Cells[linhaInicialPorAutor, 1, linha - 1, 1].Merge = true;
                            worksheet.Cells[linhaInicialPorAutor, 1].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                        }

                        linhaInicialPorAutor = linha;
                        idAutorAtual = autLivAss.AutorId;
                        worksheet.Cells[linha, 1].Value = autLivAss.NomeAutor;
                    }

                    worksheet.Cells[linha, 2].Value = autLivAss.TituloLivro;
                    worksheet.Cells[linha, 3].Value = autLivAss.Editora;
                    worksheet.Cells[linha, 4].Value = autLivAss.Edicao;
                    worksheet.Cells[linha, 5].Value = autLivAss.AnoPublicacao;
                    worksheet.Cells[linha, 6].Value = autLivAss.DescricaoAssunto;

                    linha++;
                }

                if (idAutorAtual != 0)
                {
                    worksheet.Cells[linhaInicialPorAutor, 1, linha - 1, 1].Merge = true;
                    worksheet.Cells[linhaInicialPorAutor, 1].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                }
                worksheet.Cells.AutoFitColumns();

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

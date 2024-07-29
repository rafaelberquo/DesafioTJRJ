using DesafioTJRJ.UI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Diagnostics;

namespace DesafioTJRJ.UI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            //string connectionString = "Server=localhost\\SQLEXPRESS;Database=DesafioTJRJ;User Id=desafio_app;Password=desafio_app01;TrustServerCertificate=True;";
            //using (SqlConnection connection = new SqlConnection(connectionString))
            //{
            //    try
            //    {
            //        connection.Open();
            //        Console.WriteLine("Conexão bem-sucedida!");
            //    }
            //    catch (Exception ex)
            //    {
            //        Console.WriteLine("Erro ao conectar: " + ex.Message);
            //    }
            //}

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Error(int? id)
        {
            var modelErro = new ErrorViewModel();

            if (!id.HasValue)
            {
                return StatusCode(500);
            }

            if (id.Value == 500)
            {
                modelErro.Mensagem = "Ocorreu um erro! Tente novamente mais tarde ou contate nosso suporte.";
                modelErro.Titulo = "Ocorreu um erro!";
                modelErro.ErroCode = id.Value;
            }
            else if (id.Value == 404)
            {
                modelErro.Mensagem = "A página que está procurando não existe! <br />Em caso de dúvidas entre em contato com nosso suporte";
                modelErro.Titulo = "Ops! Página não encontrada.";
                modelErro.ErroCode = id.Value;
            }
            else if (id.Value == 403)
            {
                modelErro.Mensagem = "Você não tem permissão para fazer isto.";
                modelErro.Titulo = "Acesso Negado";
                modelErro.ErroCode = id.Value;
            }
            else
            {
                return StatusCode(500);
            }

            return View("Error", modelErro);
        }
    }
}

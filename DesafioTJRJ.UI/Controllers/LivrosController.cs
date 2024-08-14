using AutoMapper;
using DesafioTJRJ.Business.Entities;
using DesafioTJRJ.Business.Interfaces.Services;
using DesafioTJRJ.Business.Services;
using DesafioTJRJ.UI.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;

namespace DesafioTJRJ.UI.Controllers
{
    public class LivrosController : Controller
    {
        private readonly ILivroService _livroService;
        private readonly IAutorService _autorService;
        private readonly IAssuntoService _assuntoService;
        private readonly IFormaCompraService _formaCompraService;
        private readonly IMapper _mapper;

        public LivrosController(ILivroService livroService, IAutorService autorService, IAssuntoService assuntoService, IFormaCompraService formaCompraService, IMapper mapper)
        {
            _livroService = livroService;
            _autorService = autorService;
            _assuntoService = assuntoService;
            _formaCompraService = formaCompraService;
            _mapper = mapper;
        }

        // GET: Livros
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var livros = await _livroService.GetAllAsync();
            var livroViewModels = _mapper.Map<IEnumerable<LivroViewModel>>(livros);
            return View(livroViewModels);
        }

        [HttpGet]
        public async Task<IActionResult> Cadastrar()
        {
            var viewModel = new LivroViewModel();
            await this.SetViewBag(viewModel.Assuntos, viewModel.Autores, viewModel.PrecosFormaCompra);

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Cadastrar(LivroViewModel livroViewModel)
        {
            this.ValidarAutoresAssuntosPrecosFormaCompra(livroViewModel);
            if (ModelState.IsValid)
            {
                var livro = _mapper.Map<Livro>(livroViewModel);
                await _livroService.AddAsync(livro);

                TempData["Success"] = "Cadastro realizado com sucesso!";
                return RedirectToAction(nameof(Index));
            }

            await this.SetViewBag(livroViewModel.Assuntos, livroViewModel.Autores, livroViewModel.PrecosFormaCompra);
            return View(livroViewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Editar(int id)
        {
            var livro = await _livroService.GetByIdAsync(id);
            if (livro == null)
            {
                TempData["Error"] = "Livro não encontrado!";
                return RedirectToAction(nameof(Index));
            }

            var viewModel = _mapper.Map<LivroViewModel>(livro);
            await this.SetViewBag(viewModel.Assuntos, viewModel.Autores, viewModel.PrecosFormaCompra);
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Editar(int id, LivroViewModel livroViewModel)
        {
            var livroBd = await _livroService.GetByIdAsync(id);
            if (livroBd == null || id != livroViewModel.CodL)
            {
                TempData["Error"] = "Livro não encontrado!";
                return RedirectToAction(nameof(Index));
            }

            this.ValidarAutoresAssuntosPrecosFormaCompra(livroViewModel);
            if (ModelState.IsValid)
            {
                var livro = _mapper.Map(livroViewModel, livroBd);
                await _livroService.UpdateAsync(livro);

                TempData["Success"] = "Edição realizada com sucesso!";
                return RedirectToAction(nameof(Index));
            }

            await this.SetViewBag(livroViewModel.Assuntos, livroViewModel.Autores, livroViewModel.PrecosFormaCompra);
            return View(livroViewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Visualizar(int id)
        {
            var livro = await _livroService.GetByIdAsync(id);
            if (livro == null)
            {
                TempData["Error"] = "Livro não encontrado!";
                return RedirectToAction(nameof(Index));
            }

            var viewModel = _mapper.Map<LivroViewModel>(livro);
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Remover(int id)
        {
            var livro = await _livroService.GetByIdAsync(id);
            var msgErro = livro == null ? "Livro não encontrado!" : null;
            if (msgErro != null)
            {
                TempData["Error"] = msgErro;
            }
            else
            {
                await _livroService.DeleteAsync(id);
                TempData["Success"] = "Exclusão realizada com sucesso!";
            }
            
            return RedirectToAction(nameof(Index));
        }

        private async Task SetViewBag(List<LivroAssuntoViewModel> assuntos, List<LivroAutorViewModel> autores, List<LivroPrecoFormaCompraViewModel> formasCompras)
        {
            var todosAutores = await _autorService.GetAllAsync();
            var todosAssuntos = await _assuntoService.GetAllAsync();
            var todasFormasCompra = await _formaCompraService.GetAllAsync();

            var autoresVinculados = autores.Select(a => a.CodAu).ToHashSet();
            var assuntosVinculados = assuntos.Select(a => a.CodAs).ToHashSet();
            var formasComprasVinculadas = formasCompras.Select(a => a.CodFormaCompra).ToHashSet();

            ViewBag.AutoresDisponiveis = todosAutores
                .Where(a => !autoresVinculados.Contains(a.CodAu))
                .Select(a => _mapper.Map<AutorViewModel>(a))
                .ToList();

            ViewBag.AssuntosDisponiveis = todosAssuntos
                .Where(a => !assuntosVinculados.Contains(a.CodAs))
                .Select(a => _mapper.Map<AssuntoViewModel>(a))
                .ToList();

            ViewBag.FormasCompraDisponiveis = todasFormasCompra
                .Where(a => !formasComprasVinculadas.Contains(a.CodFormaCompra))
                .Select(a => _mapper.Map<FormaCompraViewModel>(a))
                .ToList();
        }

        private void ValidarAutoresAssuntosPrecosFormaCompra(LivroViewModel viewModel)
        {
            if (viewModel.Autores == null || !viewModel.Autores.Any())
            {
                ModelState.AddModelError("Autores", "É necessário adicionar ao menos um autor.");
            }

            if (viewModel.Assuntos == null || !viewModel.Assuntos.Any())
            {
                ModelState.AddModelError("Assuntos", "É necessário adicionar ao menos um assunto.");
            }

            if (viewModel.PrecosFormaCompra == null || !viewModel.PrecosFormaCompra.Any())
            {
                ModelState.AddModelError("PrecosFormaCompra", "É necessário adicionar ao um preço por forma de compra.");
            }
        }
    }
}
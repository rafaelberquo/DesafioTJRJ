using AutoMapper;
using DesafioTJRJ.Business.Entities;
using DesafioTJRJ.Business.Interfaces.Services;
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
        private readonly IMapper _mapper;

        public LivrosController(ILivroService livroService, IAutorService autorService, IAssuntoService assuntoService, IMapper mapper)
        {
            _livroService = livroService;
            _autorService = autorService;
            _assuntoService = assuntoService;
            _mapper = mapper;
        }

        // GET: Livros
        public async Task<IActionResult> Index()
        {
            var livros = await _livroService.GetAllAsync();
            var livroViewModels = _mapper.Map<IEnumerable<LivroViewModel>>(livros);
            return View(livroViewModels);
        }

        public async Task<IActionResult> Cadastrar()
        {
            var viewModel = new LivroViewModel();
            await this.SetViewBag(viewModel.Assuntos, viewModel.Autores);

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Cadastrar(LivroViewModel livroViewModel)
        {
            if (ModelState.IsValid)
            {
                var livro = _mapper.Map<Livro>(livroViewModel);
                await _livroService.AddAsync(livro);

                TempData["Success"] = "Cadastro realizado com sucesso!";
                return RedirectToAction(nameof(Index));
            }

            await this.SetViewBag(livroViewModel.Assuntos, livroViewModel.Autores);
            return View(livroViewModel);
        }

        public async Task<IActionResult> Editar(int id)
        {
            var livro = await _livroService.GetByIdAsync(id);
            if (livro == null)
            {
                TempData["Error"] = "Livro não encontrado!";
                return RedirectToAction(nameof(Index));
            }

            var viewModel = _mapper.Map<LivroViewModel>(livro);
            await this.SetViewBag(viewModel.Assuntos, viewModel.Autores);
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

            if (ModelState.IsValid)
            {
                var livro = _mapper.Map(livroViewModel, livroBd);
                await _livroService.UpdateAsync(livro);

                TempData["Success"] = "Edição realizada com sucesso!";
                return RedirectToAction(nameof(Index));
            }

            await this.SetViewBag(livroViewModel.Assuntos, livroViewModel.Autores);
            return View(livroViewModel);
        }

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
            await _livroService.DeleteAsync(id);

            TempData["Success"] = "Remoção realizada com sucesso!";
            return RedirectToAction(nameof(Index));
        }

        private async Task SetViewBag(List<LivroAssuntoViewModel> assuntos, List<LivroAutorViewModel> autores)
        {
            var todosAutores = await _autorService.GetAllAsync();
            var todosAssuntos = await _assuntoService.GetAllAsync();
            var autoresVinculados = autores.Select(a => a.CodAu).ToHashSet();
            var assuntosVinculados = assuntos.Select(a => a.CodAs).ToHashSet();

            ViewBag.AutoresDisponiveis = todosAutores
                .Where(a => !autoresVinculados.Contains(a.CodAu))
                .Select(a => _mapper.Map<AutorViewModel>(a))
                .ToList();

            ViewBag.AssuntosDisponiveis = todosAssuntos
                .Where(a => !assuntosVinculados.Contains(a.CodAs))
                .Select(a => _mapper.Map<AssuntoViewModel>(a))
                .ToList();
        }
    }

}
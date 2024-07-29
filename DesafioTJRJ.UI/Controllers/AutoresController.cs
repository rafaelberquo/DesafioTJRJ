using AutoMapper;
using DesafioTJRJ.Business.Entities;
using DesafioTJRJ.Business.Interfaces.Services;
using DesafioTJRJ.UI.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace DesafioTJRJ.UI.Controllers
{
    public class AutoresController : Controller
    {
        private readonly IAutorService _autorService;
        private readonly IMapper _mapper;

        public AutoresController(IAutorService autorService, IMapper mapper)
        {
            _autorService = autorService;
            _mapper = mapper;
        }

        // GET: Autores
        public async Task<IActionResult> Index()
        {
            var autores = await _autorService.GetAllAsync();
            var autorViewModels = _mapper.Map<IEnumerable<AutorViewModel>>(autores);
            return View(autorViewModels);
        }

        // GET: Autores/Cadastrar
        public IActionResult Cadastrar()
        {
            return View();
        }

        // POST: Autores/Cadastrar
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Cadastrar(AutorViewModel autorViewModel)
        {
            if (ModelState.IsValid)
            {
                var autor = _mapper.Map<Autor>(autorViewModel);
                await _autorService.AddAsync(autor);

                TempData["Success"] = "Cadastro realizado com sucesso!";
                return RedirectToAction(nameof(Index));
            }
            return View(autorViewModel);
        }

        // GET: Autores/Editar/5
        public async Task<IActionResult> Editar(int id)
        {
            var autor = await _autorService.GetByIdAsync(id);
            if (autor == null)
            {
                TempData["Error"] = "Autor não encontrado!";
                return RedirectToAction(nameof(Index));
            }

            var autorViewModel = _mapper.Map<AutorViewModel>(autor);
            return View(autorViewModel);
        }

        // POST: Autores/Editar/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Editar(int id, AutorViewModel autorViewModel)
        {
            if (id != autorViewModel.CodAu)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var autor = _mapper.Map<Autor>(autorViewModel);
                await _autorService.UpdateAsync(autor);

                TempData["Success"] = "Edição realizada com sucesso!";
                return RedirectToAction(nameof(Index));
            }
            return View(autorViewModel);
        }

        // POST: Autores/Remover/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Remover(int id)
        {
            await _autorService.DeleteAsync(id);

            TempData["Success"] = "Remoção realizada com sucesso!";
            return RedirectToAction(nameof(Index));
        }

        // GET: Autores/Visualizar/5
        public async Task<IActionResult> Visualizar(int id)
        {
            var autor = await _autorService.GetByIdAsync(id);
            if (autor == null)
            {
                TempData["Error"] = "Autor não encontrado!";
                return RedirectToAction(nameof(Index));
            }

            var autorViewModel = _mapper.Map<AutorViewModel>(autor);
            return View(autorViewModel);
        }
    }

}

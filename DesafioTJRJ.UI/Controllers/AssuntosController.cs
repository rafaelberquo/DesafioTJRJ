using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DesafioTJRJ.UI.Controllers
{
    using AutoMapper;
    using Business.Services;
    using DesafioTJRJ.Business.Entities;
    using DesafioTJRJ.Business.Interfaces.Repository;
    using DesafioTJRJ.Business.Interfaces.Services;
    using DesafioTJRJ.UI.ViewModels;
    using global::AutoMapper;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using System.Threading.Tasks;

    namespace Web.Controllers
    {
        public class AssuntosController : Controller
        {
            private readonly IAssuntoService _assuntoService;
            private readonly IMapper _mapper;

            public AssuntosController(IAssuntoService assuntoService, IMapper mapper)
            {
                _assuntoService = assuntoService;
                _mapper = mapper;
            }

            public async Task<IActionResult> Index()
            {
                var assuntos = await _assuntoService.GetAllAsync();
                var assuntoViewModels = _mapper.Map<IEnumerable<AssuntoViewModel>>(assuntos);
                return View(assuntoViewModels);
            }

            public IActionResult Cadastrar()
            {
                return View();
            }

            [HttpPost]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> Cadastrar(AssuntoViewModel assuntoViewModel)
            {
                if (ModelState.IsValid)
                {
                    var assunto = _mapper.Map<Assunto>(assuntoViewModel);
                    await _assuntoService.AddAsync(assunto);

                    TempData["Success"] = "Cadastro realizado com sucesso!";
                    return RedirectToAction(nameof(Index));
                }
                return View(assuntoViewModel);
            }

            public async Task<IActionResult> Editar(int id)
            {
                var assunto = await _assuntoService.GetByIdAsync(id);
                if (assunto == null)
                {
                    TempData["Success"] = "Assunto não encontrado!";
                    return RedirectToAction(nameof(Index));
                }

                var assuntoViewModel = _mapper.Map<AssuntoViewModel>(assunto);
                return View(assuntoViewModel);
            }

            [HttpPost]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> Editar(int id, AssuntoViewModel assuntoViewModel)
            {
                if (id != assuntoViewModel.CodAs)
                {
                    return NotFound();
                }

                if (ModelState.IsValid)
                {
                    var assunto = _mapper.Map<Assunto>(assuntoViewModel);
                    await _assuntoService.UpdateAsync(assunto);

                    TempData["Success"] = "Edição realizada com sucesso!";
                    return RedirectToAction(nameof(Index));
                }
                return View(assuntoViewModel);
            }

            [HttpPost]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> Remover(int id)
            {
                await _assuntoService.DeleteAsync(id);

                TempData["Success"] = "Remoção realizada com sucesso!";
                return RedirectToAction(nameof(Index));
            }

            public async Task<IActionResult> Visualizar(int id)
            {
                var assunto = await _assuntoService.GetByIdAsync(id);
                if (assunto == null)
                {
                    TempData["Success"] = "Assunto não encontrado!";
                    return RedirectToAction(nameof(Index));
                }

                var assuntoViewModel = _mapper.Map<AssuntoViewModel>(assunto);
                return View(assuntoViewModel);
            }
        }
    }

}
